﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V128.WebAuthn;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using static System.Collections.Specialized.BitVector32;

namespace SeleniumWebBrowser
{
    [TestFixture]
    public class BO_MACHINE_ADD_02
    { 
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36");
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void TestLoginWithIncorrectCredentials()
        {
            driver.Navigate().GoToUrl("https://cfb9-2001-fb1-b2-96f1-3190-5a6d-1b46-7245.ngrok-free.app/pn_next_automation_bo/page/verification/login.jsp");

            // Click visit button if visible
            var visitButton = driver.FindElement(By.XPath("//*[@id='root']/div/main/div/div/section[1]/div/footer/button"));
            if (visitButton.Displayed)
            {
                visitButton.Click();
            }

            // Wait for 3 seconds
            Thread.Sleep(3000);

            // Locate username and password fields and login button again
            var inputUser = driver.FindElement(By.XPath("/html/body/form/div[4]/div/div[2]/table/tbody/tr[2]/td[2]/input"));
            var inputPassword = driver.FindElement(By.Id("password"));
            var loginButton = driver.FindElement(By.Id("login"));

            // Input incorrect credentials
            inputUser.Clear();
            inputPassword.Clear();

            inputUser.SendKeys("nopmontolPN");
            inputPassword.SendKeys("vtm@Promptnow2024");

            loginButton.Click();

            Thread.Sleep(3000);
            var Machine_Management = driver.FindElement(By.XPath("//*[@id=\"myslidemenu\"]/ul/li[7]/a"));
            Machine_Management.Click();

            var Machine = driver.FindElement(By.XPath("//*[@id=\"ui-id-2\"]"));
            Machine.Click();

            Thread.Sleep(2000);
            var add = driver.FindElement(By.XPath("//*[@id=\"doAddBtn\"]"));
            add.Click();

            Thread.Sleep(3000);
            var description = driver.FindElement(By.XPath("//*[@id=\"description\"]"));
            description.SendKeys("Hello");

            Thread.Sleep(1000);
            var remark = driver.FindElement(By.XPath("//*[@id=\"remark\"]"));
            remark.SendKeys("12123");

            // เลื่อนหน้าเว็บลงมา 100 พิกเซล
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0, 100);");

            // รอเล็กน้อยหากต้องการให้การเลื่อนหน้าจอเสร็จสมบูรณ์ (optional)
            Thread.Sleep(500); // รอ 0.5 วินาที (ปรับตามต้องการ)
            var save = driver.FindElement(By.XPath("//*[@id=\"ValidateBtn\"]"));
            save.Click();


        }

        [TearDown]
        public void Teardown()
        {
            // Uncomment the line below to close the browser after the test
            // driver.Quit();
        }
    }
}
