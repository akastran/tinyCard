using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyCard.Core.Model;

namespace tinyCard.Core.Services.Options
{
    public class IssueNewCardOptions
    {
        public string CardNumber { get; set; }
        public decimal CardPresentAvailableBalance { get; set; }
        public decimal EcommerceAvailableBalance { get; set; }
    }
}
