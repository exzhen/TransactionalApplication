using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public int UserId { get; set; }

        public int Amount { get; set; }

        [Required]
        [MaxLength(20)]
        public string State { get; set; }

    }
}
