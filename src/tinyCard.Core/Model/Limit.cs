using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tinyCard.Core.Model
{
    public class Limit
    {
        public Guid LimitId { get; set; }
        public TransactionType TranType { get; set; }
        public DateTimeOffset LimitDate { get; set; }
        public decimal TranTypeLimit { get; set; }
        public decimal TranTypeCurrentBalance { get; set; }
    }
}
