using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tinyCard.Core.Model
{
    public class Card
    {
        public string CardNumber { get; set; }
        public decimal CurrentBalance { get; set; }
        public List<Limit> CardLimits { get; set; }
        public Card()
        {
            CardLimits = new List<Limit>();
        }
    }
}
