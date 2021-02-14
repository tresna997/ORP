using Microsoft.EntityFrameworkCore;
using ORP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Context
{
    public class MyContext : DbContext
    {
        public MyContext() { }
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<OvertimeForm> OvertimeForms { get; set; }
        public DbSet<OvertimeFormEmployee> OvertimeFormEmployees { get; set; }
    }
    //protected override void OnModelCreating(ModelBuilder modelBuilder) 
    //{
    //    modelBuilder.Entity<Account>().HasOne(a => a.employee).WithOne(b => b.account).HasForeignKey<Account>(a => a.NIK);
    //}
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseLazyLoadingProxies();
    //}
}
