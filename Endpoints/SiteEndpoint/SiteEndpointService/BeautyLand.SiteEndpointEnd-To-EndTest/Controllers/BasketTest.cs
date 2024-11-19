using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BeautyLand.SiteEndpointEnd_To_EndTest.Controllers
{
    public class BasketTest: IDisposable
    {
        private readonly IWebDriver _webDriver;
        public BasketTest()
        {
            _webDriver = new ChromeDriver();
        }

        [Fact]
        public void Add_Item_To_Basket()
        {
            //Act
            _webDriver
                .Navigate()
                .GoToUrl("https://localhost:7001/Basket");

            var AddToBasket =_webDriver.FindElement(By.Id("AddToBasket"));
            AddToBasket.Click();

            //Assert
            Assert.Equal("https://localhost:7001/Basket", _webDriver.Url.ToLower());
        }

        [Fact]
        public void Empty_Discount_Code()
        {
            //Act
            _webDriver
                .Navigate()
                .GoToUrl("https://localhost:6001/Basket");

            var txtDiscountCode =_webDriver.FindElement(By.Id("txtDiscountCode"));
            txtDiscountCode.Clear();

            var btnApplyDiscountCode = _webDriver.FindElement(By.Id("btnApplyDiscountCode"));
            btnApplyDiscountCode.Click();

            var webElement = _webDriver.FindElement(By.XPath("//div[@class='swal-title']")).Text;

            //Assert
            Assert.Equal("Enter the code please", webElement);
        }

        [Fact]
        public void Invalid_Discount_Code()
        {
            //Act
            _webDriver
                .Navigate()
                .GoToUrl("https://localhost:7001/Basket");

            var txtDiscountCode = _webDriver.FindElement(By.Id("txtDiscountCode"));
            txtDiscountCode.Clear();
            txtDiscountCode.SendKeys("Invalid");  

            var btnApplyDiscountCode = _webDriver.FindElement(By.Id("btnApplyDiscountCode"));
            btnApplyDiscountCode.Click();

            var webElement = _webDriver.FindElement(By.XPath("//div[@class='swal-title']")).Text;

            //Assert
            Assert.Equal("Success", webElement);
        }

        public void Dispose()
        {
            _webDriver.Quit();
            _webDriver.Dispose();
        }
    }
}
