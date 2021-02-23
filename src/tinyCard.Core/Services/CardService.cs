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
    public class CardService : ICardService
    {
        private CardDbContext _dbContext;

        public CardService(CardDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Card> IssueNewCardAsync(IssueNewCardOptions options)
        {
            if (string.IsNullOrWhiteSpace(options?.CardNumber))
            {
                return null;
            }
            else
            {
                if (options?.CardNumber.Length != 16)
                {
                    return null;
                }

                foreach (var c in options?.CardNumber)
                {
                    if (!Char.IsDigit(c))
                    {
                        return null;
                    }

                }
            }

            if (options?.CardPresentBalance == null)
            {
                return null;
            }

            if (options?.EcommerceBalance == null)
            {
                return null;
            }

            var card = new Card()
            {
                CardNumber = options.CardNumber,
                CardPresentBalance = options.CardPresentBalance,
                EcommerceBalance = options.EcommerceBalance,
            };

            _dbContext.Add(card);
            await _dbContext.SaveChangesAsync();

            return card;
        }
    }
}
