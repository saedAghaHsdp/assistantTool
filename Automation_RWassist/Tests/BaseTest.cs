using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Automation_RWassist.Configuration;
using Driver;
using Driver.UI.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium;

using Reporters.ReportFormat;
using Utilities;
using Utilities.Enums;

using static Driver.Enums;
using Assert = NUnit.Framework.Assert;

namespace Automation_RWassist.Tests
{
    [TestFixture]

    public abstract class BaseTest : Hooks
    {

        public Reports Reporters;
        protected IWebDriverUi Driver;
        public IWebDriver driver;

        public readonly string ClientURl = ConfigSetting.ClientURL;

        [SetUp]
        public void Setup()
        {
            try
            {
                //Reporters = ReportManager.GetInstance().StartTest(TestContext.CurrentContext.Test.Name, TestProjectType.Nunit);
                Reporters = ReporterUtilities.StartTest(TestContext.CurrentContext.Test.Name, TestProjectType.Nunit);
            }
            catch (Exception e)
            {
                Logger.InfoFailedWithException(e);
            }

        }

        [TearDown]
        public void AfterEachTest()
        {
            DriverUtilities.EndTestNunit(Reporters, TestContext.CurrentContext.Result.Outcome.Status);

            try
            {
                Logger.Info("End test");

                if (Driver != null)
                {
                    Driver.Close();
                }
                ProcessUtilities.KillChromeDriver();

            }
            catch (Exception ex)
            {
                Logger.InfoFailedWithException(ex);
            }
        }

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            Assert.IsTrue(ReporterUtilities.ReporterInit(), "Failed to load Extent Report");
            ReportManager instance = ReportManager.GetInstance();
            instance.SetOutputFolders();
            string workingDirectoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            
            Assert.IsTrue(instance.SetExtentReports(instance.HtmlFolderPath, workingDirectoryPath));
            TestRunMode = DriverUtilities.SelectRunMode();
        }

        [OneTimeTearDown]
        public static async Task OneTimeTearDown()
        {
            await ReporterUtilities.SaveDocs();
        }

        
        public void OpenBrowser(BrowserType browserType)
        {
            Logger.InfoStartMethod();
            try
            {
                Driver = DriverUtilities.OpenBrowser(browserType, ClientURl, TimeSpan.FromSeconds(5),TestRunMode);
            }
            catch (Exception ex)
            {
                Logger.InfoFailedWithException(ex);
            }

        }

        


        public void TakeScreenShot(string imageName)
        {
            DriverUtilities.AddScreenShotToStep(Driver, Reporters, imageName);
        }
    }
}

