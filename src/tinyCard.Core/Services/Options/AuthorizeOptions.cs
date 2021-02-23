using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyCard.Core.Model;

namespace tinyCard.Core.Services.Options
{
    public class AuthorizeOptions
    {
        public string CardNumber { get; set; }
        public decimal TransactionAmount { get; set; }
        public int IntTranType { get; set; }
    }
}
