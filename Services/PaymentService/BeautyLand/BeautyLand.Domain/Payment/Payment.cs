using System;
using BeautyLand.Domain.Order;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyLand.Domain.Attributes;

namespace BeautyLand.Domain.Payment
{
    [AudiTable]
    public class Payment
    {
        public Guid Id { get; set; }
        public  int Amount { get; set; }
        public bool IsPaid { get; set; }
        public string Authority { get; set; }
        public long RefId { get; set; } = 0;
        public Order.Order Order { get; set; }
        public Guid OrderId { get; set; }
    }
}
