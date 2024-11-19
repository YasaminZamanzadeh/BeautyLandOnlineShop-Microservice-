using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BeautyLand.Application.Services.Site.Catalogs.Dtos.Types;

namespace BeautyLand.Application.Services.Site.Catalogs.Types
{
    public interface ITypeService
    {
        IEnumerable<TypeDto> GetType();
        Guid CreateType(TypeDto type);
        TypeDto GetTypeById(Guid id);
    }
}
