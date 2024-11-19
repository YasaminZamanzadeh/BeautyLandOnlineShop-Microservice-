using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Application.Services.Site.Payments.Dtos
{
    public class VerificationPayResultDto
    {
        public int Status { get; set; }
        public long RefId { get; set; }
    }
}
