using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyLand.Application.Administrator.ViewServices.Dtos
{
    public record ResultDto(bool IsSuccess, string? Message);
    public record ResultDto<TModel>(bool IsSuccess, string? Message, TModel? Model) where TModel : class;

}
