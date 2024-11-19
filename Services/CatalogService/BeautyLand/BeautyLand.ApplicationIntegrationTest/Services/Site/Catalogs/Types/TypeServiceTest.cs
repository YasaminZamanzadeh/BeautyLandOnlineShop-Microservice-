using BeautyLand.Application.Services.Site.Catalogs.Dtos.Types;
using BeautyLand.Application.Services.Site.Catalogs.Types;
using BeautyLand.SiteEndPointIntegrationTest.ClassFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xunit;

namespace BeautyLand.ApplicationIntegrationTest.Services.Site.Catalogs.Types
{
    public class TypeServiceTest : IClassFixture<TypeServices>
    {
        private readonly TypeServices _typeServices;
        public TypeServiceTest(TypeServices typeServices)
        {
            _typeServices = typeServices;
        }
        // [Fact]
        public void Create_Type()
        {
            //Arrange
            var type = new Filler<TypeDto>().Create();

            //Act
            var typeId = _typeServices.TypeService.CreateType(type);
            var typeById = _typeServices.TypeService.GetTypeById(typeId);

            //Assert
            Assert.NotNull(typeById);
            Assert.IsType<TypeDto>(typeById);
            Assert.Equal(type.Name, typeById.Name);
            Assert.Equal(type.Description, typeById.Description);

        }
    }

}
