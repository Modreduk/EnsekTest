using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Ensek.Testing.Tests
{
    [TestClass]
    public static class Utilities
    {
        public static (IWebElement, IWebElement, IWebElement) SetupElements(IWebDriver driver, String rowIndex = "1")
        {
            IWebElement row = driver.FindElement(By.XPath("//*[@id='energyType_EnergyTypeId' and @value='" + rowIndex + "']/.."));
            IWebElement textBox = row.FindElement(By.Id("energyType_AmountPurchased"));
            IWebElement button = row.FindElement(By.Name("Buy"));
            textBox.Clear();
            return (row, textBox, button);
        }

        public static void ResetValues(IWebDriver driver)
        {
            IWebElement resetButton = driver.FindElement(By.Name("Reset"));
            resetButton.Click();
            WaitForPageLoad(driver);
        }

        public static void WaitForPageLoad(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5.0));

            wait.Until(d => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}
