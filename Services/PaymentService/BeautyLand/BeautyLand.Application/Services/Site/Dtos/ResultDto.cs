using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Application.Services.Site.Dtos
{
    public class ResultDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class ResultDto<TModel>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public TModel Model { get; set; }
    }
}
