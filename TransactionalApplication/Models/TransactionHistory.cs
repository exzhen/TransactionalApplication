using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionalApplication.Models
{
    public class TransactionHistory
    {
        public int TransactionHistoryId { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public int TransactionAmount { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
