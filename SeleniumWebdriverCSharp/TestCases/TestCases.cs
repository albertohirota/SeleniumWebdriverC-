using Google.Apis.Sheets.v4.Data;
using GoogleFramework;
using Login;

namespace SeleniumWebdriverCSharp
{
    public static class TestCases 
    {
        public static void TC001()
        {
            CommonFunctions.Delay(3000);
            Assert.IsTrue(Validation.IsElementVisible(GmailPage.ButtonSend), "Send button should be found");
        }

        public static void TC002()
        {
            Assert.IsTrue(Validation.IsTextElementValid(GmailPage.TitleEmail, "New Message"), "Email title should be valid");
        }

        public static void TC003()
        {
            CommonFunctions.Delay(1000);
            GmailPage.Click_ButtonDiscard();
            CommonFunctions.Delay(1000);
            Assert.IsFalse(Validation.IsElementNotVisible(GmailPage.TitleEmail), "Email should not exist");
            CommonFunctions.Delay(1000);
        }

        public static void TC004Setup()
        {
            CommonFunctions.Login(GoogleLogin.Sites.Gmail);
            CommonFunctions.WaitElementBePresent(GmailPage.ButtonCompose);
            GmailPage.Click_NewEmail();
            GmailPage.PopulateEmail("albertohirota@gmail.com", "Test Receiving", "Test body receiving email", "alberto.hirota@gmail.com", "eitihirota@gmail.com");
            GmailPage.Click_SendEmail();
            CommonFunctions.Delay(2000);
            GmailPage.WaitAndOpenReceivedEmail("Test Receiving");
        }

        public static void TC004()
        {
            Assert.IsTrue(Validation.DoesObjectExist("eitihirota@gmail.com", "email", "span"), "Bcc email should be visible");
        }

        public static void TC005()
        {
            Assert.IsTrue(Validation.IsElementVisible(GmailPage.ButtonReplyAll), "Button should be available");
        }

        public static void TC006Setup()
        {
            CommonFunctions.Login(GoogleLogin.Sites.Gmail);
            GmailPage.Click_NewEmail();
            GmailPage.PopulateEmail("albertohirota@gmail.com", "Test Receiving", "Test body receiving email", "alberto.hirota@gmail.com", "eitihirota@gmail.com");
            GmailPage.Click_SendEmail();
            GmailPage.WaitAndOpenReceivedEmail("Test Receiving");
        }

        public static void TC006()
        {
            GmailPage.Click_ButtonReply();
            CommonFunctions.Delay(3000);
            Assert.IsTrue(Validation.DoesObjectExist("albertohirota@gmail.com", "email", "span"), "Email should be visible");
            GmailPage.Click_ButtonDiscard();
        }

        public static void TC007()
        {
            GmailPage.Click_ButtonForward();
            GmailPage.SendKeyAndEnter(GmailPage.To, "eitihirota@gmail.com");
            GmailPage.Click_SendEmail();
            CommonFunctions.Delay(3000);
            GmailPage.GoToInbox();
            GmailPage.GoToInbox();
            Assert.IsTrue(Validation.IsTextElementValid(GmailPage.InboxListNewEmail, "Forwarded message"), "Email should be valid");
        }

        public static void TC101()
        {
            Assert.IsTrue(Validation.DoesFileInGDriveExists("TC101"), "File name should exist");
        }

        public static void TC102()
        {
            GDrivePage.Click_DriveMenuFolder("Shared with me");
            CommonFunctions.Delay(5000);
            Assert.IsTrue(Validation.DoesFileInGDriveExists("logo Lambton.PNG"), "File name should exist");
            GDrivePage.Click_DriveMenuFolder("My Drive");
        }

        public static void TC103()
        {
            GDrivePage.Click_NewFile();
            GDrivePage.Click_GoogleDocs();
            CommonFunctions.GoToTab(1);
            CommonFunctions.Delay(8000);
            GOfficePage.RenameDocumentName("TC103");
            CommonFunctions.Delay(5000);
            CommonFunctions.CloseTab(1);
            CommonFunctions.GoToTab(0);
            CommonFunctions.Delay(10000);
            Assert.IsTrue(Validation.DoesFileInGDriveExists("TC103"), "File name should exist");
            GDrivePage.DeleteFileInDrive("TC103");
        }

        public static void TC104()
        {
            var fileList = GDrivePage.GetFileList();
            Assert.IsTrue(Validation.DoesTextContainsInList("TC301",fileList),"Text should exists in the list");
        }
        
        public static void TC201()
        {
            CalendarPage.CreateNewEvent();
            CalendarPage.Add_Title_SummaryPage("TC201");
            CalendarPage.Click_ButtonSaveSummaryPage();
            Assert.IsTrue(Validation.DoesCalendarEventExist("TC201"), "File event should exist");
            CalendarPage.DeleteEvent("TC201");
        }

        public static void TC202()
        {
            CalendarPage.CreateNewEvent();
            CalendarPage.Add_Title_SummaryPage("TC202");
            CalendarPage.Click_ButtonMoreOptionsSummaryPage();
            CalendarPage.Add_TextCalendarBody("This is TC202");
            CalendarPage.Click_ButtonSave();
            CommonFunctions.Delay(3000);
            CalendarPage.Click_ExistingEvent("TC202");
            Assert.IsTrue(Validation.DoesCalendarTextMessageBodyExist("This is TC202"), "Text body calendar event should exist");
            CalendarPage.Click_ButtonDeleteSummaryPage();
        }
        public static void TC203()
        {
            CalendarPage.CreateNewEvent();
            CalendarPage.Add_Title_SummaryPage("TC203");
            CalendarPage.Click_ButtonMoreOptionsSummaryPage();
            CommonFunctions.WaitElementBePresent(CalendarPage.AddGuest);
            CalendarPage.Add_Guest("alberto.hirota@gmail.com");
            CalendarPage.Add_TextCalendarBody("This is TC203");
            CommonFunctions.Delay(2000);
            CalendarPage.Click_ButtonSave();
            CalendarPage.Click_ButtonSend();
            CommonFunctions.Delay(2000);
            CalendarPage.Click_ExistingEvent("TC203");
            Assert.IsTrue(Validation.DoesGuestExist("alberto.hirota@gmail.com"), "Guest exist");
            CalendarPage.Click_ButtonDeleteSummaryPage();
        }

        public static void TC301()
        {
            Assert.IsTrue(Validation.DoesFileExistDocsSheetsSlides("TC301"), "File name should exist");
        }

        public static void TC302()
        {
            GOfficePage.Click_DocumentBlank();
            CommonFunctions.Delay(4000);
            GOfficePage.RenameDocumentName("TC302");
            CommonFunctions.Delay(3000);
            GOfficePage.Click_ButtonGoogle();
            Assert.IsTrue(Validation.DoesFileExistDocsSheetsSlides("TC302"), "File name should exist");
            GOfficePage.DeleteFile("TC302");
        }

        public static void TC303()
        {
            bool? exist = false;
            GOfficePage.Click_OpenFile("TC301");
            CommonFunctions.WaitElementBePresent(GOfficePage.DocumentStatus,15);
            var text = DocsPage.GetDocumentBody(false);

            for (int i = 0; i < text.Count; i++)
            {
                if (Validation.DoesTextContainsInString(text[i], "Test Case 301 and 303"))
                {
                    exist = true;
                    break;
                }
            }
            Assert.IsTrue(exist, "Text Body should exist");
            GOfficePage.Click_ButtonGoogle();
        }

        public static void TC304()
        {
            bool? exist = false;

            GOfficePage.Click_OpenFile("TC301");
            CommonFunctions.WaitElementBePresent(GOfficePage.DocumentStatus, 15);
            var texts = DocsPage.GetDocumentHeader(false);

            for (int i = 0; i < texts.Count; i++)
            {
                if (Validation.DoesTextContainsInString(texts[i], "Hello World"))
                {
                    exist = true;
                    break;
                }
            }   
            Assert.IsTrue(exist, "Text Header should exist");
            GOfficePage.Click_ButtonGoogle();
        }

        public static void TC305()
        {
            bool? exist = false;
            GOfficePage.Click_DocumentBlank();
            CommonFunctions.Delay(6000);
            GOfficePage.RenameDocumentName("TC305");
            CommonFunctions.Delay(1000);
            DocsPage.SendText_DocumentBody("Test Case 305");
            CommonFunctions.Delay(3000);
            
            CommonFunctions.Delay(1000);
            var text = DocsPage.GetDocumentBody(true);

            for (int i = 0; i < text.Count; i++)
            {
                if (Validation.DoesTextContainsInString(text[i], "Test Case 305"))
                {
                    exist = true;
                    break;
                }
            }
            Assert.IsTrue(exist, "Text Body should exist");

            GOfficePage.Click_ButtonGoogle();
            GOfficePage.DeleteFile("TC305");
        }

        public static void TC401()
        {
            Assert.IsTrue(Validation.DoesFileExistDocsSheetsSlides("TC401"), "File name should exist");
        }

        public static void TC402()
        {
            GOfficePage.Click_DocumentBlank();
            CommonFunctions.Delay(7000);
            GOfficePage.RenameDocumentName("TC402");
            GOfficePage.Click_ButtonGoogle();
            CommonFunctions.Delay(2000);
            Assert.IsTrue(Validation.DoesFileExistDocsSheetsSlides("TC402"), "File name should exist");
            GOfficePage.DeleteFile("TC402");
        }

        public static void TC403()
        {
            bool? exist = false;
            GOfficePage.Click_OpenFile("TC401");
            GOfficePage.WaitDocumentBeingSaved();
            ValueRange values = SheetsPage.GetSheetsEntry("A2");
            for (int i = 0; i < values.Values.Count; i++)
            {
                if (values.Values[i].Contains("Test Case 401")) 
                    exist = true;
            }
            Assert.IsTrue(exist, "Text should exist in cell A2");
            GOfficePage.Click_ButtonGoogle();
        }

        public static void TC404()
        {
            bool? exist = false;
            GOfficePage.Click_DocumentBlank();
            CommonFunctions.Delay(4000);
            GOfficePage.RenameDocumentName("TC404");
            CommonFunctions.Delay(1000);
            SheetsPage.SendKeysOnSheet("B1","Test Case 404", true);
            CommonFunctions.Delay(1000);
            GOfficePage.RenameDocumentName("TC404");
            ValueRange values = SheetsPage.GetSheetsEntry("B1");
            IList<IList<Object>> items = values.Values; 
            foreach (var item in items)
            {
                foreach (var line in item)
                {
                    if (Validation.DoesTextContainsInString(line.ToString()!, "Test Case 404"))
                        exist = true;
                }
            }
            Assert.IsTrue(exist, "Text should exist in cell B1");
            GOfficePage.Click_ButtonGoogle();
            GOfficePage.DeleteFile("TC404");
        }

        public static void TC501()
        {
            Assert.IsTrue(Validation.DoesFileExistDocsSheetsSlides("TC501"), "File name should exist");
        }

        public static void TC502()
        {
            GOfficePage.Click_DocumentBlank();
            CommonFunctions.Delay(7000);
            GOfficePage.RenameDocumentName("TC502");
            GOfficePage.Click_ButtonGoogle();
            CommonFunctions.Delay(2000);
            Assert.IsTrue(Validation.DoesFileExistDocsSheetsSlides("TC502"), "File name should exist");
            GOfficePage.DeleteFile("TC502");
        }

        public static void TC503()
        {
            bool? exist = false;
            GOfficePage.Click_OpenFile("TC501");
            List<string> textList = SlidesPage.GetSlidesTexts(false);
            foreach (string text in textList)
            {
                if (Validation.DoesTextContainsInString(text, "Hello World"))
                    exist = true;
            }
            Assert.IsTrue(exist, "Text should exist in the presentation");
            GOfficePage.Click_ButtonGoogle();
        }

        public static void TC504()
        {
            bool? exist = false;
            GOfficePage.Click_DocumentBlank();
            CommonFunctions.Delay(4000);
            GOfficePage.RenameDocumentName("TC504");
            CommonFunctions.Delay(1000);
            SlidesPage.SendText_PresentationBody("TC504 - Hello");
            CommonFunctions.Delay(1000);
            GOfficePage.RenameDocumentName("TC504");
            CommonFunctions.Delay(1000);
            List<string> textList = SlidesPage.GetSlidesTexts(true);
            foreach (string text in textList)
            {
                if (Validation.DoesTextContainsInString(text, "TC504 - Hello"))
                    exist = true;
            }
            Assert.IsTrue(exist, "Text should exist in the presentation");
            GOfficePage.Click_ButtonGoogle();
            GOfficePage.DeleteFile("TC504");
        }
    }
}
