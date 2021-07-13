using Ensek.Testing.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Ensek.Testing.Tests
{
    [TestClass]
    [Page(Url = "https://ensekautomationcandidatetest.azurewebsites.net/Energy/Buy")]
    public class AllFuelTests : SeleniumTest
    {
        [TestMethod]
        [Browser(Browser = "Chrome")]
        public void ValidGasPurchaseShouldSucceed()
        {
            Utilities.ResetValues(driver);
            (IWebElement row, IWebElement textBox, IWebElement button) = Utilities.SetupElements(driver, "1");
            textBox.SendKeys("5");
            button.Click();
            Utilities.WaitForPageLoad(driver);
            Assert.IsTrue(driver.FindElement(By.CssSelector("h2")).Text.Contains("Sale Confirmed!"));
        }

        [TestMethod]
        [Browser(Browser = "Chrome")]
        public void ValidElectricityPurchaseShouldSucceed()
        {
            Utilities.ResetValues(driver);
            (IWebElement row, IWebElement textBox, IWebElement button) = Utilities.SetupElements(driver, "3");
            textBox.SendKeys("5");
            button.Click();
            Utilities.WaitForPageLoad(driver);
            Assert.IsTrue(driver.FindElement(By.CssSelector("h2")).Text.Contains("Sale Confirmed!"));
        }

        [TestMethod]
        [Browser(Browser = "Chrome")]
        public void ValidOilPurchaseShouldSucceed()
        {
            Utilities.ResetValues(driver);
            (IWebElement row, IWebElement textBox, IWebElement button) = Utilities.SetupElements(driver, "4");
            textBox.SendKeys("5");
            button.Click();
            Utilities.WaitForPageLoad(driver);
            Assert.IsTrue(driver.FindElement(By.CssSelector("h2")).Text.Contains("Sale Confirmed!"));
        }
    }
}
