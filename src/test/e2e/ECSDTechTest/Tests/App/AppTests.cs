using System;
using System.Collections.Generic;
using ECSDTechTest.Solver;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace ECSDTechTest.Tests.App
{
    public class TechnicalTest
    {
        private IWebDriver _driver;

        [SetUp]
        public void Initialize()
        {
            _driver = new FirefoxDriver();
        }

        // Tests the provided app
        [Test]
        public void SolveChallenge()
        {
            _driver.Url = "http:/localhost:3000";

            // Click the RENDER THE CHALLENGE button
            _driver.FindElement(By.CssSelector("button[data-test-id=\"render-challenge\"]")).Click();

            // Wait for the challenge to exist
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _driver.FindElement(By.Id("home"));
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            // Solve each Row
            for (var row = 1; row < 4; row++)
            {
                // Build the array from the column
                var arr = new List<int>();
                for (var column = 0; column < 9; column++)
                {
                    arr.Add(Convert.ToInt32(_driver.FindElement(By.CssSelector($"td[data-test-id=\"array-item-{row}-{column}\"]")).Text));
                }
                // Solve the puzzle
                var answer = ChallengeSolver.SolveChallenge(arr);

                // Verify that we got a positive result
                answer.Should().NotBeNull("The center value should be found");

                // Fill in the answer
                _driver.FindElement(By.CssSelector($"input[data-test-id=\"submit-{row}\"]")).SendKeys(answer.ToString());
            }
            
            // Sign the test
            _driver.FindElement(By.CssSelector("input[data-test-id=\"submit-4\"]")).SendKeys("Clive Galway");

            // Submit
            // Use XPath to find a span with the text "Submit Answers", and click it
            _driver.FindElement(By.XPath("//span[contains(text(), 'Submit Answers')]")).Click();
            // This could probably be improved by selecting the first found button ancestor of this node
        }

        [TearDown]
        public void EndTest()
        {
            _driver.Close();
        }


    }
}
