using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Ensek.Testing.Framework;

namespace Ensek.Testing.Tests
{
    [TestClass]
    [Page(Url = "https://ensekautomationcandidatetest.azurewebsites.net/Energy/Buy")]
    public class GasTests : SeleniumTest
    {
        [TestMethod]
        [Browser(Browser = "Chrome")]
        public void ValidPurchaseShouldSucceed()
        {
            Utilities.ResetValues(driver);
            (IWebElement row, IWebElement textBox, IWebElement button) = Utilities.SetupElements(driver);
            textBox.SendKeys("5");
            button.Click();
            Utilities.WaitForPageLoad(driver);
            Assert.IsTrue(driver.FindElement(By.CssSelector("h2")).Text.Contains("Sale Confirmed!"));
        }

        [TestMethod]
        [Browser(Browser = "Chrome")]
        public void ValidPurchaseReducesAvailableStock()
        {
            Utilities.ResetValues(driver);
            (IWebElement row, IWebElement textBox, IWebElement button) = Utilities.SetupElements(driver);

            String InitialAvailableStock = row.FindElement(By.XPath("//td[3]")).Text;
            int stockToBuy = 10;

            textBox.SendKeys(stockToBuy.ToString());
            button.Click();
            Utilities.WaitForPageLoad(driver);
            driver.Navigate().GoToUrl("https://ensekautomationcandidatetest.azurewebsites.net/Energy/Buy");
            Utilities.WaitForPageLoad(driver);

            IWebElement updatedRow = driver.FindElement(By.XPath("//*[@id='energyType_EnergyTypeId' and @value='1']/.."));
            String currentAvailableStock = updatedRow.FindElement(By.XPath("//td[3]")).Text;
            Assert.AreEqual(Int32.Parse(currentAvailableStock), Int32.Parse(InitialAvailableStock)-stockToBuy);
        }

        [TestMethod]
        [Browser(Browser = "Chrome")]
        public void ZeroPurchaseShouldError()
        {
            Utilities.ResetValues(driver);
            (IWebElement row, IWebElement textBox, IWebElement button) = Utilities.SetupElements(driver);
            textBox.SendKeys("0");
            button.Click();
            Utilities.WaitForPageLoad(driver);
            Assert.IsTrue(driver.FindElement(By.CssSelector("h2")).Text.Contains("error"));
        }

        [TestMethod]
        [Browser(Browser = "Chrome")]
        public void AboveAvailablePurchaseShouldError()
        {
            Utilities.ResetValues(driver);
            (IWebElement row, IWebElement textBox, IWebElement button) = Utilities.SetupElements(driver);
            String maxAvailable = row.FindElement(By.XPath("//td[3]")).Text;
            textBox.SendKeys((Int32.Parse(maxAvailable)+1).ToString());
            button.Click();
            Utilities.WaitForPageLoad(driver);
            Assert.IsTrue(driver.FindElement(By.CssSelector("h2")).Text.Contains("error"));
        }

        [TestMethod]
        [Browser(Browser = "Chrome")]
        public void NegativePurchaseShouldError()
        {
            Utilities.ResetValues(driver);
            (IWebElement row, IWebElement textBox, IWebElement button) = Utilities.SetupElements(driver);
            textBox.SendKeys("-1");
            button.Click();
            Utilities.WaitForPageLoad(driver);
            Assert.IsTrue(driver.FindElement(By.CssSelector("h2")).Text.Contains("error"));
        }

        [TestMethod]
        [Browser(Browser = "Chrome")]
        public void PurchaseShouldErrorIfEntryIsInvalid()
        {
            Utilities.ResetValues(driver);
            (IWebElement row, IWebElement textBox, IWebElement button) = Utilities.SetupElements(driver);
            textBox.SendKeys("Invalid");
            button.Click();
            Utilities.WaitForPageLoad(driver);
            Assert.IsTrue(driver.FindElement(By.CssSelector("h2")).Text.Contains("error"));
        }
    }
}
