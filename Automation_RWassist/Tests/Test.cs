using System.Globalization;
using System.Net.Http;
using System.Threading;
using Driver;
using NUnit.Framework;
using Reporters.ReportFormat;

namespace Automation_RWassist.Tests

{
    [TestFixture]
    public class Test : BaseTest
    {
        #region Private variables
        private readonly string firstQuestionTextExpected = "Hi, please choose the field your problem related to:";
        private readonly string secondQuestionTextExpected = "I see you have WorkList failure, what exactly are you experienced?";
        private readonly string thirdQuestionTextExpected = "Verify in Application Pool that Advanced Workflow Service is running on DI nodes";
        private readonly string forthQuestionTextExpected = "Restart service in Application Pool";
        private readonly string fifthQuestionTextExpected = "Great!";    
        #endregion

        [Test]
        [TestInfo(testFeature: "test", testId: "", testType: TestType.Web, authorName: "G")]

        public void Test1()
        {
            #region Step 1 Open The RWS Troubleshooting tool
            Reporters.Step("Open The RWS Troubleshooting tool", "RWS Troubleshooting tool shall be opened");
            OpenBrowser(Enums.BrowserType.Chrome);
            var mainPage = initMainPage();
            Reporters.Assert.IsTrue(Driver.IsDisplayed(mainPage), "RWS Troubleshooting tool was not opened", "RWS Troubleshooting tool was opened");
            TakeScreenShot("Step 1");
            #endregion

            #region Step 2 Move to Troubleshooting Guide tab
            Reporters.Step("Move to Troubleshooting Guide tab", "Troubleshooting Guide tab shall be selected");
            Reporters.Assert.IsTrue(SelectTroubleShootingTab(), "Troubleshooting Guide tab was not selected", "Troubleshooting Guide tab was selected");
            TakeScreenShot("Step 2");
            #endregion

            #region Step 3 Verify question 1 text
            Reporters.Step("Verify that the text of question 1 is: 'Hi, please choose the field your problem related to:", "The text of question 1 shall be: 'Hi, please choose the field your problem related to:");
            bool firstTextAsExpected = CheckQuestionText(1, firstQuestionTextExpected);
            Reporters.Assert.IsTrue(firstTextAsExpected, "The text of question 1 was not : 'Hi, please choose the field your problem related to:", "The text of question 1 was: " + firstQuestionTextExpected);
            TakeScreenShot("Step 3");
            #endregion

            #region Step 4 In question 1, select the option  'Worklist is not presented correctly'"
            Reporters.Step("In question 1, select the option  'Worklist is not presented correctly'", "Question 2 shall be displayed, with the following text: 'I see you have WorkList failure, what exactly are you experienced?'");
            SelectOption("Worklist is not presented correctly");
            bool secondTextAsExpected = CheckQuestionText(2, secondQuestionTextExpected);
            Reporters.Assert.IsTrue(secondTextAsExpected, "Question 2 was not displayed with the following text: " + secondQuestionTextExpected, "Question 2 was displayed with the following text: " + secondQuestionTextExpected);
            TakeScreenShot("Step 4");
            #endregion

            #region Step 5 In question 2, select the option  'I open worklist, but the list is empty'"
            Reporters.Step("In question 2, select the option  'I open worklist, but the list is empty'", "Question 3 shall be displayed, with the following text: 'Verify in Application Pool that Advanced Workflow Service is running on DI nodes'");
            SelectOption("I open worklist, but the list is empty");
            bool thirdTextAsExpected = CheckQuestionText(3, thirdQuestionTextExpected);
            Reporters.Assert.IsTrue(thirdTextAsExpected, "Question 3 was not displayed with the following text: " + thirdQuestionTextExpected, "Question 3 was displayed with the following text: " + thirdQuestionTextExpected);
            TakeScreenShot("Step 5");
            #endregion

            #region Step 6 In question 3, select the option  'Advanced Workflow Service is NOT running on DI nodes'"
            Reporters.Step("In question 3, select the option  'Advanced Workflow Service is NOT running on DI nodes'", "Question 4 shall be displayed, with the following text: 'Restart service in Application Pool'");
            SelectOption("Advanced Workflow Service is NOT running on DI nodes");
            bool forthTextAsExpected = CheckQuestionText(4, forthQuestionTextExpected);
            Reporters.Assert.IsTrue(forthTextAsExpected, "Question 4 was not displayed with the following text: " + forthQuestionTextExpected, "Question 4 was displayed with the following text: " + forthQuestionTextExpected);
            TakeScreenShot("Step 6");
            #endregion

            #region Step 7 In question 4, select the option  'That solved my problem!'"
            Reporters.Step("In question 4, select the option  'That solved my problem!'", "Question 5 shall be displayed, with the following text: 'Great!'");
            SelectOption("That solved my problem!");
            bool fifthTextAsExpected = CheckQuestionText(5, fifthQuestionTextExpected);
            Reporters.Assert.IsTrue(fifthTextAsExpected, "Question 5 was not displayed with the following options: " + fifthQuestionTextExpected, "Question 5 was displayed with the following options: " + fifthQuestionTextExpected);
            TakeScreenShot("Step 6");
            #endregion

        }

        private object initMainPage()
        {
            return Driver.FindElement("//app-troubleshoot", Enums.SearchBy.Xpath);
        }
        private object initTroubleShootingTab()
        {
            return Driver.FindElement("#mat-tab-label-0-1");
        }
        private bool SelectTroubleShootingTab()
        {
            var tab = initTroubleShootingTab();
            Driver.Click(tab);
            Thread.Sleep(500);
            return Driver.GetAttribute(tab, "aria-selected").Equals("true");
        }
        private bool CheckQuestionText(int questionNumber, string textExpected)
        {
            var question = Driver.FindElement("//mat-step-header[@aria-posinset='" + questionNumber.ToString() + "']//div[contains(@class, 'mat-step-label')]", Enums.SearchBy.Xpath);
            var questionText = Driver.GetText(question);
            return questionText.Trim().Equals(textExpected);
        }
        private void SelectOption(string optionName)
        {
            var option = Driver.FindElement("//mat-radio-button//*[normalize-space(text())='" + optionName + "']", Enums.SearchBy.Xpath);
            Driver.Click(option);
        }
    }
}



