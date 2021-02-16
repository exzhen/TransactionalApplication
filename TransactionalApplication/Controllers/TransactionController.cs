using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransactionalApplication.Models;

namespace TransactionalApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {

        private readonly ILogger<TransactionController> _logger;
        private static Transaction t = new Transaction();

        public TransactionController(ILogger<TransactionController> logger)
        {
            _logger = logger;
        }

        [HttpGet("createuser")]
        public void Get()
        {
            using (var ctx = new AccountContext())
            {
                var stud = new User() { Name = "Bill", TotalAmount = 500 };
                _logger.LogInformation("test");
                ctx.Users.Add(stud);
                ctx.SaveChanges();
            }
        }

        [HttpPost("Add")]
        public async Task AddAmount(int userId, int amount)
        {
            await t.AddAmount(userId, amount);
        }

        [HttpPost("Deduct")]
        public async Task DeductAmount(int userId, int amount)
        {
            await t.DeductAmount(userId, amount);
        }
    }
}
