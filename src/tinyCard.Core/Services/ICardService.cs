using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyCard.Core.Model;
using tinyCard.Core.Services.Options;

namespace tinyCard.Core.Services
{
    public interface ICardService
    {
        public Task<Card> IssueNewCardAsync(IssueNewCardOptions options);
        public Task<Card> RetrieveCardAsync(RetrieveCardOptions options);
    }
}
