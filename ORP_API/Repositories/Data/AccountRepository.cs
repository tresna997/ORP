using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ORP_API.Context;
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
                RoleId = registerViewModels.RoleId,
                CustomerId = registerViewModels.CustomerId,
                Password = registerViewModels.Password
            };
            var account = new Account()
            {
                NIK = registerViewModels.NIK,
                Password = registerViewModels.Password
            };

            var resultEmployee = employeeRepository.Create(employee);
            myContext.Add(account);
            var resultAccount = myContext.SaveChanges();

            if (resultEmployee > 0 && resultAccount > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public LoginViewModels Login(string email, string password)
        {
            LoginViewModels result = null;

            string connectStr = Configuration.GetConnectionString("MyConnection");

            using (IDbConnection db = new SqlConnection(connectStr))
            {
                string readSp = "sp_retrieve_login";
                var parameter = new { Email = email, Password = password };
                result = db.Query<LoginViewModels>(readSp, parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return result;
        }
    }
}
