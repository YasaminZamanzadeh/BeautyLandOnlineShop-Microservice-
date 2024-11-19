using BeautyLand.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Domain.Discounts
{
    [AudiTable]
    public class Discount
    {
        public Guid Id { get; set; }
        public  int Amount { get; set; }
        public string Code { get; set; }
        public bool IsUsed { get; set; }
    }
}
