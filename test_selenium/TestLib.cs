using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace test_selenium
{
    class TestLib
    {

        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver("C:\\Users\\Kapitanka\\source\\repos\\test_selenium\\");
            driver.Url = "https://accounts.google.com";
            driver.Manage().Window.Maximize();
            System.Threading.Thread.Sleep(500);
        }

        [Test]
        public void test()
        {
            driver.FindElement(By.Id("identifierId")).SendKeys("nasstya.test01@gmail.com");
            driver.FindElement(By.Id("identifierNext")).Click();
            System.Threading.Thread.Sleep(1000);

            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys("testnasstya");
            driver.FindElement(By.Id("passwordNext")).Click();
            System.Threading.Thread.Sleep(1000);

            IWebElement accountInfo = driver.FindElement(By.XPath("//*[@id='gb']/div[2]/div[3]/div/div[2]/div/a"));
            accountInfo.Click();

            String ExpectAccountFIO = "test1 test2";
            String ResultAccountFIO = driver.FindElement(By.XPath("//*[@id='gb']/div[2]/div[6]/div[1]/div/div[1]")).Text;
            Assert.AreEqual(ExpectAccountFIO, ResultAccountFIO);

            String ExpectAccountEmail = "nasstya.test01@gmail.com";
            String ResultAccountEmail = driver.FindElement(By.XPath("//*[@id='gb']/div[2]/div[6]/div[1]/div/div[2]")).Text;
            Assert.AreEqual(ExpectAccountEmail, ResultAccountEmail);
            
        }

        [Test]
        public void test_incorrect()
        {
            IWebElement emailInput = driver.FindElement(By.Id("identifierId"));
            emailInput.SendKeys("dksfjskdfjkd");
            IWebElement nextEmailButton = driver.FindElement(By.Id("identifierNext"));
            nextEmailButton.Click();
            System.Threading.Thread.Sleep(2000);

            String ExpectEmailError = "Не удалось найти аккаунт Google";
            String ResultEmailError = driver.FindElement(By.XPath("//form/content/section/div/content/div[1]/div/div[2]/div[2]")).Text;
            Assert.AreEqual(ExpectEmailError, ResultEmailError);

            emailInput.Clear();
            emailInput.SendKeys("nasstya.test01@gmail.com");
            nextEmailButton.Click();
            System.Threading.Thread.Sleep(1000);

            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys("343546");
            driver.FindElement(By.Id("passwordNext")).Click();
            System.Threading.Thread.Sleep(1000);

            String ContainsPasswordError = "Неверный пароль.";
            String ResultPasswordError = driver.FindElement(By.XPath("//*[@id='password']/div[2]/div[2]")).Text;
            Assert.IsTrue(ResultPasswordError.Contains(ContainsPasswordError));

        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
}
