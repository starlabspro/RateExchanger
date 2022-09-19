using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateExchangerUserLimiter.Models
{
    public class UserMessage
    {
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public int Amount { get; set; }
        public string FromCurrencyCode { get; set; }
        public string ToCurrencyCode { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
