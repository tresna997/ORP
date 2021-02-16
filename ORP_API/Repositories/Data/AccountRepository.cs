using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ORP_API.Context;
using ORP_API.Handler;
using ORP_API.Models;
using ORP_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Repositories.Data
{
    public class AccountRepository : GeneralRepository<Account, MyContext, string>
    {
        private readonly MyContext myContext;
        private DbSet<Account> accounts;
        private readonly EmployeeRepository employeeRepository;
        private readonly SendEmail sendEmail = new SendEmail();
        public IConfiguration Configuration { get; }
        public AccountRepository(MyContext myContext, EmployeeRepository employeeRepository, IConfiguration configuration) : base(myContext)
        {
            myContext.Set<Account>();
            this.myContext = myContext;
            this.employeeRepository = employeeRepository;
            this.Configuration = configuration;
        }

        public int Register (RegisterViewModels registerViewModels)
        {
            var employee = new Employee()
            {
                NIK = registerViewModels.NIK,
                Name = registerViewModels.Name,
                BirthDate = registerViewModels.BirthDate,
                Gender = registerViewModels.Gender,
                Religion = registerViewModels.Religion,
                Email = registerViewModels.Email,
                PhoneNumber = registerViewModels.PhoneNumber,
                RoleId = 4,
                CustomerId = registerViewModels.CustomerId,
                Password = Hashing.HashPassword("B0o7c@mp")
            };
            var account = new Account()
            {
                NIK = registerViewModels.NIK,
                Password = Hashing.HashPassword("B0o7c@mp")
            };

            var resultEmployee = employeeRepository.Create(employee);
            myContext.Add(account);
            var resultAccount = myContext.SaveChanges();

            if (resultEmployee > 0 && resultAccount > 0)
            {
                sendEmail.SendPassword(employee.Email);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public LoginViewModels Login(LoginViewModels loginViewModels)
        {
            LoginViewModels result = null;

            string connectStr = Configuration.GetConnectionString("MyConnection");
            var v = myContext.Employee.Where(a => a.Email == loginViewModels.Email).FirstOrDefault();

            if (v != null)
            {
                if (Hashing.ValidatePassword(loginViewModels.Password, v.Password))
                {
                    using (IDbConnection db = new SqlConnection(connectStr))
                    {
                        string readSp = "sp_retrieve_login";
                        var parameter = new { Email = loginViewModels.Email, Password = loginViewModels.Password };
                        result = db.Query<LoginViewModels>(readSp, parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    }
                }
            }
            return result;
        }
    }
}
