﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Application.Services.Site.Discounts.Dtos
{
    public class DiscountDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int Amount { get; set; }
        public bool IsUsed { get; set; }
    }
}