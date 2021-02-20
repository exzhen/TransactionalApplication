using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }

        public int TotalAmount { get; set; }

        public List<TransactionHistory> TransactionHistory { get; set; }

    }
}
