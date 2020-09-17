using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stark.Integration.Infobip.Models;
using Stark.Integration.Infobip.Models.Responses;

namespace Stark.Integration.Infobip.IntegrationTests
{
    [Ignore]
    [TestClass]
    public class IntegrationTest
    {
        private const string UserName = "";
        private const string Password = "";

        [TestMethod]
        public void SmsSendTest()
        {
            InfobipClient client = new InfobipClient(UserName, Password, TimeSpan.FromMinutes(5), new TurkeyPhoneNumberValidator(), new SimpleJsonSerializer(), new ConsoleAuditLogger(), "");
            HttpResponse<SmsResponse> response = client.Send(new List<Message>() { GetDummyMessage() });
        }

        [TestMethod]
        public void GetDeliveryReportsTest()
        {
            string packageId = "1457694661433301757";
            InfobipClient client = new InfobipClient(UserName, Password);
            HttpResponse<DeliveryReportResponse> response = client.GetDeliveryReports(packageId, String.Empty);
        }

        [TestMethod]
        public void GetBalanceTest()
        {
            InfobipClient client = new InfobipClient(UserName, Password);
            HttpResponse<BalanceDetailResponse> response = client.GetBalance();
        }

        private Message GetDummyMessage()
        {
            Message message = new Message("CAGRI SMS", new List<string>() { "+905542346742" }, "Whatsapp baby...");
            return message;
        }
    }
}
