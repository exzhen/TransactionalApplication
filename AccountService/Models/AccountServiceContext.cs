using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Models
{
    public class AccountServiceContext : DbContext
    {
        public AccountServiceContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=AccountServiceDB;Trusted_Connection=True;");
        }



        public DbSet<User> Users { get; set; }
        public DbSet<TransactionHistory> TransactionHistory { get; set; }
    }
}
