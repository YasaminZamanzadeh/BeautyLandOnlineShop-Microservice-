using BeautyLand.Domain.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xunit;

namespace BeautyLand.DomainTest.Catalogs
{
    public class ItemTest
    {
        [Fact]
        public void Update_Price_Item()
        {
            //Arrange
            //What does data should be prepared?

            var item = new Filler<Item>().Create();
            int price = 3700;

            //Act
            //What does the operation should be done?

            item.UpdatePrice(price);

            //Assert
            //For passing test, we must implement it till reaching the desired purpose

            Assert.Equal(price, item.Price);
        }
        [Fact]
        public void Update_Price_Price_With_Zero_Value()
        {
            //Arrange
            var item = new Filler<Item>().Create();

            //Act
            var price = 0;

            //Assert
            Assert.Throws<Exception>(() => item.UpdatePrice(price));
        }
    }
}
