using Microsoft.EntityFrameworkCore;
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

            if (options?.CurrentBalance == null)
            {
                return null;
            }

            var card = new Card()
            {
                CardNumber = options.CardNumber,
                CurrentBalance = options.CurrentBalance
            };

            _dbContext.Add(card);
            await _dbContext.SaveChangesAsync();

            return card;
        }

        public async Task<Card> RetrieveCardAsync(RetrieveCardOptions options)
        {
            var dbCard = await _dbContext.Set<Card>()
                .Where(c => c.CardNumber == options.CardNumber)
                .SingleOrDefaultAsync();

            return dbCard;
        }

        public async Task<Card> AuthorizeAsync(AuthorizeOptions options)
        {
            if (string.IsNullOrWhiteSpace(options?.CardNumber))
            {
                return null;
            }

            var intTransactionType = int.Parse(options?.IntTranType.ToString());
            var isDefined = Enum.IsDefined(typeof(TransactionType), intTransactionType);

            if (!isDefined)
            {
                return null;
            }

            if (options?.TransactionAmount == null)
            {
                return null;
            }

            if (options?.TransactionAmount == 0)
            {
                return null;
            }

            RetrieveCardOptions cardOptions = new RetrieveCardOptions()
            {
                CardNumber = options.CardNumber
            };

            var card = await RetrieveCardAsync(cardOptions);
            
            if (card != null)
            {
                if ((card.CardLimits) == null || ((card.CardLimits) != null && (card.CardLimits.Count < 2)))
                {
                    if ((options.TransactionAmount <= 1500M) && ((TransactionType)options.IntTranType == TransactionType.CardPresent))
                    {
                        var newLimit = new Limit();
                        newLimit.TranType = TransactionType.CardPresent;
                        newLimit.LimitDate = DateTimeOffset.Now.Date;
                        newLimit.TranTypeLimit = 1500M;
                        newLimit.TranTypeBalance = options.TransactionAmount;
                        card.CardLimits.Add(newLimit);
                        card.CurrentBalance += options.TransactionAmount;
                    }
                    else if ((options.TransactionAmount <= 500M) && ((TransactionType)options.IntTranType == TransactionType.Ecommerce))
                    {
                        var newLimit = new Limit();
                        newLimit.TranType = TransactionType.Ecommerce;
                        newLimit.LimitDate = DateTimeOffset.Now.Date;
                        newLimit.TranTypeLimit = 500M;
                        newLimit.TranTypeBalance = options.TransactionAmount;
                        card.CardLimits.Add(newLimit);
                        card.CurrentBalance += options.TransactionAmount;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    foreach (var item in card.CardLimits)
                    {
                        if (item.TranType == (TransactionType)intTransactionType)
                        {
                            if (item.LimitDate == DateTimeOffset.Now.Date)
                            {
                                if (item.TranTypeBalance + options.TransactionAmount <= item.TranTypeLimit)
                                {
                                    item.TranTypeBalance = item.TranTypeBalance + options.TransactionAmount;
                                    card.CurrentBalance += options.TransactionAmount;
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            else
                            {
                                if (options.TransactionAmount <= item.TranTypeLimit)
                                {
                                    item.LimitDate = DateTimeOffset.Now.Date;
                                    item.TranTypeBalance = options.TransactionAmount;
                                    card.CurrentBalance += options.TransactionAmount;
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        }
                    }
                }

                _dbContext.Update(card);
                _dbContext.SaveChanges();
                
                return (card);
            }
            else
            {
                return null;
            }
        }
    }
}
