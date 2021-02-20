using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Models
{
    public class TransactionHistory
    {
        public int TransactionHistoryId { get; set; }

        [Required]
        [MaxLength(20)]
        public string TransactionName { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public int TransactionAmount { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
