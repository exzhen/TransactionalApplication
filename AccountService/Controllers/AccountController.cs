using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AccountService.Models;

namespace AccountService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet("test")]
        public string GetTest()
        {
            _logger.LogInformation("test");
            return "Done";
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="totalAmount"></param>
        /// <returns></returns>
        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser(string name, string password, int totalAmount)
        {
            using (var db = new AccountServiceContext())
            {
                var user = new User() { Name = name, Password = password, TotalAmount = totalAmount };
                db.Users.Add(user);
                try
                {
                    await  db.SaveChangesAsync();
                    return Ok(user);
                }
                catch (DbUpdateException ex)
                {
                    return Problem(ex.ToString());
                }
            }
        }

        /// <summary>
        /// View user
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("ReadUser")]
        public async Task<ActionResult> ReadUser(string name, string password)
        {
            using (var db = new AccountServiceContext())
            {
                var user = await (from t in db.Users
                                  where t.Name == name && t.Password == password
                                  select t).FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound("User not found");
                }
                try
                {
                    return Ok(user);
                }
                catch (DbUpdateException ex)
                {
                    return Problem(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Add amount for an user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="amount"></param>
        /// <param name="tranName"></param>
        /// <returns></returns>
        [HttpPost("AddAmount")]
        public async Task<ActionResult> AddAmount(int userId, int amount, string tranName)
        {
            using (var db = new AccountServiceContext())
            {
                var user = await (from t in db.Users
                            where t.UserId == userId
                            select t).FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound("User not found");
                }
                user.TotalAmount += amount;

                TransactionHistory h = new TransactionHistory();
                h.TransactionName = tranName;
                h.TransactionAmount = amount;
                h.TransactionDateTime = DateTime.Now;
                h.UserId = userId;
                db.TransactionHistory.Add(h);

                try
                {
                    await db.SaveChangesAsync();
                    return Ok(user);
                }
                catch (DbUpdateException ex)
                {
                    return Problem(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Deduct amount for an user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="amount"></param>
        /// <param name="tranName"></param>
        /// <returns></returns>
        [HttpPost("DeductAmount")]
        public async Task<ActionResult> DeductAmount(int userId, int amount, string tranName)
        {
            using (var db = new AccountServiceContext())
            {
                var user = await (from t in db.Users
                            where t.UserId == userId
                            select t).FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound("User not found");
                }
                user.TotalAmount -= amount;

                TransactionHistory h = new TransactionHistory();
                h.TransactionName = tranName;
                h.TransactionAmount = -amount;
                h.TransactionDateTime = DateTime.Now;
                h.UserId = userId;
                db.TransactionHistory.Add(h);
                try
                {
                    await db.SaveChangesAsync();
                    return Ok(user);
                }
                catch (DbUpdateException ex)
                {
                    return Problem(ex.ToString());
                }
            }
        }
    }
}
