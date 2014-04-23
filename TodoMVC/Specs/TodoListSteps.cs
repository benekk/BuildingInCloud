using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow;
using TodoMVC;
using Xunit;

namespace Tests
{
    [Binding]
    public class TodoListSteps: TestsBase
    {
        private string siteUrl = "http://localhost/todoMVC";
        [Given(@"I am on TodoMVC site")]
        public void GivenIAmOnTodoMVCSite()
        {
            driver.Navigate().GoToUrl(siteUrl);
        }

        [When(@"I add ""(.*)"" task")]
        public void WhenIAddTask(string p0)
        {
            driver.FindElement(By.Id("new-todo")).SendKeys(p0+"\n");
        }

        [Then(@"I should have (.*) items on the list")]
        public void ThenIShouldHaveItemsOnTheList(int p0)
        {
            var nItems = driver.FindElement(By.Id("todo-list")).FindElements(By.TagName("Li")).Count();
            Assert.Equal(2, nItems);
        }

        [Given(@"I have already added three tasks")]
        public void GivenIHaveAlreadyAddedThreeTasks()
        {
            WhenIAddTask("Learn Scala#");
            WhenIAddTask("Learn VB 6");
            WhenIAddTask("Learn Clojure");
        }

        [When(@"I click delete button on task \# (.*)")]
        public void WhenIClickDeleteButtonOnTask(int p0)
        {
            var elementToDelete = driver.FindElement(By.Id("todo-list")).FindElements(By.TagName("Li")).Skip((p0-1)).First();
            var deleteButton = elementToDelete.FindElement(By.ClassName("destroy"));
            Actions action = new Actions(driver);
            action.MoveToElement(elementToDelete)
                .MoveToElement(deleteButton)
                .Click()
                .Build()
                .Perform();
        }


        [BeforeScenario]
        public void BeforeScenario()
        {
            driver = GetSauceConnectDriver("ALT.NET 2 - specflow");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if(driver!=null)
                driver.Quit();
        }
    }
}
