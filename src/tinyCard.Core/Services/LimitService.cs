using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyCard.Core.Data;
using tinyCard.Core.Model;
using tinyCard.Core.Services.Options;

namespace tinyCard.Core.Services
{
    public class LimitService : ILimitService
    {
        private CardDbContext _dbContext;

        public LimitService(CardDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Limit>> IssueNewCardLimitsAsync(IssueNewCardLimitsOptions options)
        {
            //    if (string.IsNullOrWhiteSpace(options?.CardNumber))
            //    {
            //        return null;
            //    }
            //    else
            //    {
            //        if (options?.CardNumber.Length != 16)
            //        {
            //            return null;
            //        }

            //        foreach (var c in options?.CardNumber)
            //        {
            //            if (!Char.IsDigit(c))
            //            {
            //                return null;
            //            }

            //        }
            //    }

            //    if (options?.CardPresentBalance == null)
            //    {
            //        return null;
            //    }

            //    if (options?.EcommerceBalance == null)
            //    {
            //        return null;
            //    }

            var limitList = new List<Limit>();
            var limit = new Limit()
            {
                TranType = TransactionType.CardPresent,
                LimitDate = DateTimeOffset.Now.Date,
                TranTypeLimit = 1500M,
                TranTypeBalance = 0M
            };

            _dbContext.Add(limit);
            limitList.Add(limit);

            limit = new Limit()
            {
                TranType = TransactionType.Ecommerce,
                LimitDate = DateTimeOffset.Now.Date,
                TranTypeLimit = 500M,
                TranTypeBalance = 0M
            };

            _dbContext.Add(limit);
            limitList.Add(limit);

            await _dbContext.SaveChangesAsync();

            return limitList;
        }
    }
}
