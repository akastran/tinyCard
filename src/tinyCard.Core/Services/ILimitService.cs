using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyCard.Core.Model;
using tinyCard.Core.Services.Options;

namespace tinyCard.Core.Services
{
    public interface ILimitService
    {
        public Task<List<Limit>> IssueNewCardLimitsAsync(IssueNewCardLimitsOptions options);
    }
}
