using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// Create an order
        /// </summary>
        /// <param name="name"></param>
        /// <param name="userId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpPost("CreateOrder")]
        public async Task<ActionResult> CreateOrder(string name, int userId, int amount)
        {
            using (var db = new OrderServiceContext())
            {
                var order = new Order() { Name = name, UserId = userId, Amount = amount, State = "Submitted" };
                db.Order.Add(order);
                try
                {
                    await db.SaveChangesAsync();
                    return Ok(order);
                }
                catch (DbUpdateException ex)
                {
                    return Problem(ex.ToString());
                }
            }
        }

        /// <summary>
        /// View order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("ReadOrder")]
        public async Task<ActionResult> ReadOrder(int orderId)
        {
            using (var db = new OrderServiceContext())
            {
                var order = await (from t in db.Order
                                  where t.OrderId == orderId
                                  select t).FirstOrDefaultAsync();
                if (order == null)
                {
                    return NotFound("Order not found");
                }
                try
                {
                    return Ok(order);
                }
                catch (DbUpdateException ex)
                {
                    return Problem(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Update the state of order
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost("UpdateOrder")]
        public async Task<ActionResult> UpdateOrder(int orderId, string state)
        {
            using (var db = new OrderServiceContext())
            {
                var order = await (from t in db.Order
                                   where t.OrderId == orderId
                                   select t).FirstOrDefaultAsync();
                if (order == null)
                {
                    return NotFound("Order not found");
                }
                try
                {
                    order.State = state;
                    await db.SaveChangesAsync();
                    return Ok(order);
                }
                catch (DbUpdateException ex)
                {
                    return Problem(ex.ToString());
                }
            }
        }
    }
}
