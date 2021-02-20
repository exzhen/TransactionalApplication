using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TransactionalApplication.Models;

namespace TransactionalApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {

        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ILogger<TransactionController> logger)
        {
            _logger = logger;
        }

        [HttpGet("test")]
        public string GetTest()
        {
            _logger.LogInformation("test");
            return "Done";
        }

        [HttpGet("createuser")]
        public ActionResult Get()
        {
            using (var ctx = new AccountContext())
            {
                var stud = new User() { Name = "Bill", TotalAmount = 500 };
                _logger.LogInformation("test");
                ctx.Users.Add(stud);
                ctx.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("Add")]
        public async Task<ActionResult> AddAmount(int userId, int amount)
        {
            using (var ctx = new AccountContext())
            {
                var user = await (from t in ctx.Users
                            where t.UserId == userId
                            select t).FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound("User not found");
                }
                user.TotalAmount += amount;

                TransactionHistory h = new TransactionHistory();
                h.TransactionAmount = amount;
                h.TransactionDateTime = DateTime.Now;
                h.UserId = userId;
                ctx.TransactionHistory.Add(h);

                try
                {
                    await ctx.SaveChangesAsync();
                    return Ok();
                }
                catch (DbUpdateException ex)
                {
                    return Problem(ex.ToString());
                }
            }
        }

        [HttpPost("Deduct")]
        public async Task<ActionResult> DeductAmount(int userId, int amount)
        {
            using (var ctx = new AccountContext())
            {
                var user = await (from t in ctx.Users
                            where t.UserId == userId
                            select t).FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound("User not found");
                }
                user.TotalAmount -= amount;

                TransactionHistory h = new TransactionHistory();
                h.TransactionAmount = -amount;
                h.TransactionDateTime = DateTime.Now;
                h.UserId = userId;
                ctx.TransactionHistory.Add(h);
                try
                {
                    await ctx.SaveChangesAsync();
                    return Ok();
                }
                catch (DbUpdateException ex)
                {
                    return Problem(ex.ToString());
                }
            }
        }
    }
}
