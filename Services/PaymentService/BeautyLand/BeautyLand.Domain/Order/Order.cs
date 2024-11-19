using BeautyLand.Domain.Attributes;
using BeautyLand.Domain.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Domain.Order
{
    [AudiTable]
    public class Order
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public ICollection<Payment.Payment> Payments { get; set; }
    }
}
