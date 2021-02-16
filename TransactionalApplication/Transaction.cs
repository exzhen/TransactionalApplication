using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionalApplication.Models;

namespace TransactionalApplication
{
    public class Transaction
    {
        //public static Queue<int> requests = new Queue<int>();
        public async Task AddAmount(int userId, int amount)
        {
            using (var ctx = new AccountContext())
            {
                var user = (from t in ctx.Users
                            where t.UserId == userId
                            select t).FirstOrDefault();
                user.TotalAmount += amount;

                TransactionHistory h = new TransactionHistory();
                h.TransactionAmount = amount;
                h.TransactionDateTime = DateTime.Now;
                h.UserId = userId;
                ctx.TransactionHistory.Add(h);

                await ctx.SaveChangesAsync();


            }
        }

        public async Task DeductAmount(int userId, int amount)
        {
            using (var ctx = new AccountContext())
            {
                var user = (from t in ctx.Users
                            where t.UserId == userId
                            select t).FirstOrDefault();
                user.TotalAmount -= amount;

                TransactionHistory h = new TransactionHistory();
                h.TransactionAmount = -amount;
                h.TransactionDateTime = DateTime.Now;
                h.UserId = userId;
                ctx.TransactionHistory.Add(h);

                await ctx.SaveChangesAsync();


            }
        }
    }
}
