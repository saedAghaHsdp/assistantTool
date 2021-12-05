using Driver.UI.Interfaces;
using Driver.UI.Selenium;
using NUnit.Framework;
using Reporters.ReportFormat;


[assembly: LevelOfParallelism(3),Parallelizable(ParallelScope.Self)]

namespace Automation_RWassist.Tests
{
    [SetUpFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class Hooks
    {
        public static DriverUtilities DriverUtilities = new DriverUtilities();
        public static ReporterUtilities ReporterUtilities = new ReporterUtilities();
        public static DriverUtilities.RunMode TestRunMode { get; set; }

        public IWebDriverUi WebDriverUi { get; set; } //For UI tests

    }
}


