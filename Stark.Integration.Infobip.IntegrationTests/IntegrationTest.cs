using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stark.Integration.Infobip.Models;

namespace Stark.Integration.Infobip.IntegrationTests
{
    [TestClass]
    public class IntegrationTest
    {
        private const string UserName = "";
        private const string Password = "";

        [TestMethod]
        public void SmsSendTest()
        {
            InfobipClient client = new InfobipClient(UserName, Password);
            client.Send(new List<Message>() { GetDummyMessage() });
        }

        [TestMethod]
        public void GetBalanceTest()
        {
            InfobipClient client = new InfobipClient(UserName, Password);
            decimal balance = client.GetBalance();
        }

        private Message GetDummyMessage()
        {
            Message message = new Message("CAGRI SMS", new List<string>() { "00905542346742", "00905415058766" }, "Whatsapp baby...");
            return message;
        }
    }
}
