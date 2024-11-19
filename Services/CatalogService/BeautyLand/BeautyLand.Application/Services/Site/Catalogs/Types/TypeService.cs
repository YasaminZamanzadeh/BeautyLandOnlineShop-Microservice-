using Type = BeautyLand.Domain.Catalogs.Type;
using System.Collections.Generic;
using System.Linq;
using BeautyLand.Application.Services.Databases.Catalogs;
using BeautyLand.Application.Services.Site.Catalogs.Dtos.Types;
using System;

namespace BeautyLand.Application.Services.Site.Catalogs.Types
{
    public class TypeService : ITypeService
    {
        private readonly ICatalogDatabaseService _catalogContext;
        public TypeService(ICatalogDatabaseService catalogContext)
        {
            _catalogContext = catalogContext;
        }
        public Guid CreateType(TypeDto types)
        {
            Type type = new Type
            {
                Id = types.Id,  
                Name = types.Name,
                Description = types.Description,
            };

            _catalogContext.Types.Add(type);
            _catalogContext.SaveChanges();

            return type.Id;

        }

        public IEnumerable<TypeDto> GetType()
        {
            var types = _catalogContext.Types
                 .OrderBy(p => p.Name)
                 .Select(p => new TypeDto
                 {
                     Id = p.Id,
                     Name = p.Name,
                     Description = p.Description,
                 }).ToList();

            return types;
        }

        public TypeDto GetTypeById(Guid id)
        {
            var type = _catalogContext.Types
                  .SingleOrDefault(p => p.Id == id);

            return new TypeDto
            {
                Id = type.Id,
                Name = type.Name,   
                Description = type.Description,
            };
        }
    }
}
