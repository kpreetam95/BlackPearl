using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections;
using OpenQA.Selenium.Support.UI;

namespace BlackPearlCodility
{
    [TestClass]
    public class SearchTest
    {
        [TestMethod]
        public void test_1()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://codility-frontend-prod.s3.amazonaws.com/media/task_static/qa_csharp_search/862b0faa506b8487c25a3384cfde8af4/static/attachments/reference_page.html");

            //1. Check if query input and search button are on the main screen of the application:
            IWebElement SearchBox = driver.FindElement(By.Id("search-input"));
            IWebElement SearchButton = driver.FindElement(By.Id("search-button"));
            Assert.AreEqual(true, SearchBox.Displayed, "Search query input field exists");
            Assert.AreEqual(true, SearchButton.Displayed, "Search button exists");

            //2. Check if searching with empty query is forbidden:
            SearchBox.SendKeys("");
            SearchButton.Click();
            IWebElement EmptyQueryError = driver.FindElement(By.Id("error-empty-query"));
            Assert.AreEqual("Provide some query", EmptyQueryError.Text, "After trying to use an empty query, 'Provide some query' error message is displaying");

            //3. Check if at least one island is returned after querying for "isla"
            SearchBox.SendKeys("isla");
            SearchButton.Click();
            IWebElement SearchResults = driver.FindElement(By.Id("search-results"));
            String islaList = SearchResults.Text.ToString();
            Assert.AreEqual(true, islaList.Contains("Isla"), "There should be at least one result returned");

            //4. Check if user gets feedback if there are no results:
            SearchBox.Clear();
            SearchBox.SendKeys("castle");
            SearchButton.Click();
            IWebElement NoResultsForcastle = driver.FindElement(By.Id("error-no-results"));
            Assert.AreEqual("No results", NoResultsForcastle.Text, "As there are no castles around, so querying for castle returns 0 results");

            //5. Check if results match the query:
            SearchBox.Clear();
            SearchBox.SendKeys("port");
            SearchButton.Click();
            Assert.AreEqual("Port Royal", SearchResults.Text, "There is only one port around, Querying for port returns only one result Port Royal");

        }
    }
}
