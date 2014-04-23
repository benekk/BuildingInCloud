using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Xunit;

namespace TodoMVC
{
    public class SeleniumTests: TestsBase
    {
        private string siteUrl = "http://localhost/todoMVC";
        public SeleniumTests()
        {
            this.driver = GetSeleniumDriver(); //GetSauceConnectDriver("ALT.NET - selenium only");
        }

        [Fact]
        public void Should_have_a_placeholder()
        {
            driver.Navigate().GoToUrl(siteUrl);
            var placeholder = driver.FindElement(By.Id("new-todo")).GetAttribute("placeholder");
            Assert.Equal("What needs to be done?", placeholder);
        }

        [Fact]
        public void Should_add_new_items()
        {
            driver.Navigate().GoToUrl(siteUrl);
            driver.FindElement(By.Id("new-todo")).SendKeys("Learn F#\n");
            driver.FindElement(By.Id("new-todo")).SendKeys("Learn Akka\n");
            var nItems = driver.FindElement(By.Id("todo-list")).FindElements(By.TagName("Li")).Count();
            Assert.Equal(2, nItems);
        }

        [Fact]
        public void Should_remove_previously_added_item()
        {
            driver.Navigate().GoToUrl(siteUrl);
            driver.FindElement(By.Id("new-todo")).SendKeys("Learn F#\n");
            driver.FindElement(By.Id("new-todo")).SendKeys("Learn VB 6\n");
            driver.FindElement(By.Id("new-todo")).SendKeys("Learn Akka\n");
            Thread.Sleep(2000);
            var elementToDelete = driver.FindElement(By.Id("todo-list")).FindElements(By.TagName("Li")).Skip((1)).First();
            var deleteButton = elementToDelete.FindElement(By.ClassName("destroy"));
            Actions action = new Actions(driver);
            action.MoveToElement(elementToDelete)
                .MoveToElement(deleteButton)
                .Click()
                .Build()
                .Perform();
            Thread.Sleep(2000);
            var nItems = driver.FindElement(By.Id("todo-list")).FindElements(By.TagName("Li")).Count();
            Assert.Equal(2, nItems);
        }

        
    }
}
