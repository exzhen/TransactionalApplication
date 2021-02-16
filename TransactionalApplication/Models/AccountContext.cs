using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionalApplication.Models
{
    public class AccountContext : DbContext
    {
        public AccountContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=TransactionalApplicationDB;Trusted_Connection=True;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TransactionHistory> TransactionHistory { get; set; }
    }
}
