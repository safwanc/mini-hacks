//
//  Test.cs
//
//  Author:
//       Safwan Choudhury <safwan.choudhury@quanser.com>
//
//  Copyright (c) 2014 Quanser Inc.
//
//
using System;
using NUnit.Framework;
using System.Reflection;
using System.IO;
using Xamarin.UITest.iOS;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Linq;

namespace CreditCardValidation.UITests
{
    [TestFixture]
    public class CreditCardValidationTests
    {
        static readonly Func<AppQuery, AppQuery> EditTextView = c => c.Marked("CreditCardTextField");
        static readonly Func<AppQuery, AppQuery> ValidateButton = c => c.Marked("ValidateButton");
        static readonly Func<AppQuery, AppQuery> ShortErrorMessage = c => c.Marked("ErrorMessagesTextField").Text("Credit card number is to short.");
        static readonly Func<AppQuery, AppQuery> LongErrorMessage = c => c.Marked("ErrorMessagesTextField").Text("Credit card number is to long.");
        static readonly Func<AppQuery, AppQuery> SuccessScreenNavBar = c => c.Class("UINavigationBar").Id("Valid Credit Card");
        static readonly Func<AppQuery, AppQuery> SuccessMessageLabel = c => c.Class("UILabel").Text("The credit card number is valid!");

        iOSApp _app;

        public string PathToIPA { get; set; }

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            string currentFile = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            FileInfo fi = new FileInfo(currentFile);
            string dir = fi.Directory.Parent.Parent.Parent.FullName;
            PathToIPA = Path.Combine(dir, "CreditCardValidation.iOS", "bin", "iPhoneSimulator", "Debug", "CreditCardValidationiOS.app");


        }

        [SetUp]
        public void SetUp()
        {
            _app = ConfigureApp.iOS.AppBundle(PathToIPA).ApiKey("YOUR_API_KEY_HERE").StartApp();
        }

        [Test]
        public void CreditCardNumber_ToShort_DisplayErrorMessage()
        {
            // Arrange - Nothing to do because the queries have already been initialized.

            // Act
            _app.EnterText(EditTextView, new string('9', 15));
            _app.Tap(ValidateButton);

            // Assert
            AppResult[] result = _app.Query(ShortErrorMessage);
            Assert.IsTrue(result.Any(), "The error message is not being displayed.");
        }

        [Test]
        public void CreditCardNumber_TooLong_DisplayErrorMessage()
        {
            // Arrange - There is nothing to do as the queries have already been created.

            // Act
            _app.EnterText(EditTextView, new string('9', 17));
            _app.Tap(ValidateButton);

            // Assert
            AppResult[] result = _app.Query(LongErrorMessage);
            Assert.IsTrue(result.Any(), "The error message is not being displayed.");
        }

        [Test]
        public void CreditCardNumber_CorrectSize_DisplaySuccessScreen()
        {
            // Arrange - Nothing to do, the queries have been defined.

            // Act - Enter a valid credit card number, tap Valid, and wait for the next screen to appear
            _app.EnterText(EditTextView, new string('9', 16));
            _app.Tap(ValidateButton);

            _app.WaitForElement(SuccessScreenNavBar, "Valid Credit Card Screen did not appear", TimeSpan.FromSeconds(5));

            // Assert - Make sure that the message is on the screen
            AppResult[] results = _app.Query(SuccessMessageLabel);
            Assert.IsTrue(results.Any(), "The success message was not displayed on the screen");
        }
    }
}
