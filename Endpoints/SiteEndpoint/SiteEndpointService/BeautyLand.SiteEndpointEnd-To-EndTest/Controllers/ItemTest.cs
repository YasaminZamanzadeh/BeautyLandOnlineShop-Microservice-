using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BeautyLand.SiteEndpointEnd_To_EndTest.Controllers
{
    public class ItemTest : IDisposable
    {
        public readonly IWebDriver _webDriver;
        public ItemTest()
        {
            _webDriver = new ChromeDriver();
        }

        [Fact]
        public void Home_Title()
        {
            //Act
            _webDriver
                .Navigate()
                .GoToUrl("https://localhost:4001");

            //Assert
            Assert.Equal("localhost", _webDriver.Title);

        }

        [Fact]
        public void Home_Content()
        {
            //Act
            _webDriver
                .Navigate()
                .GoToUrl("https://localhost:4001/Home/Index");
            //Assert
            Assert.Contains(string.Empty, _webDriver.PageSource);
        }
        [Fact]
        public void Get_Items()
        {
            //Act
            _webDriver
                .Navigate()
                .GoToUrl("https://localhost:4001/Item");

            var webElement = _webDriver.FindElement(By.XPath("//div"));
            var webElements = new List<IWebElement>(webElement.FindElements(By.ClassName("container")));
            //Assert
            Assert.Equal(0, webElements.Count);
        }

        public void Dispose()
        {
            _webDriver.Close();
            _webDriver.Dispose();
        }
    }
}
