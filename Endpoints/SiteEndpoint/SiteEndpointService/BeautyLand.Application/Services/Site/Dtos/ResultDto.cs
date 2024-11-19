using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Application.Services.Site.Dtos
{
    public class ResultDto
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ResultDto<TModel>
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public TModel Model { get; set; }
    }

}
