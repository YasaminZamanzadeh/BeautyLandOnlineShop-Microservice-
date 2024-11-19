using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Application.Services.Site.Dto
{
	public class ResultDto
	{
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class ResultDto<T>
    {
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
        public T Model { get; set; }
    }
}
