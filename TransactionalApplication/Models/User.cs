using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionalApplication.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public int TotalAmount { get; set; }

        public List<TransactionHistory> TransactionHistory { get; set; }

    }
}
