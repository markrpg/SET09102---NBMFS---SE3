using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessageFilteringWPF;
using System.Collections.Generic;

namespace NapierFilteringServiceUnitTests
{
    [TestClass]
    public class UnitTest2
    {

        [TestMethod]
        public void testAddingEmailSIR()
        {
            bool messageExists = false;
            NBMFilteringService nbm = new NBMFilteringService();
            nbm.NewMessage("E", 1, "admin@email.com", "SIR: 11/11/15", "Sort Code: 12-54-55 Nature of Incident: Theft ");
            List<Message> list = new List<Message>();
            list = nbm.GetMessageList();
            foreach (Message m in list)
            {
                if (m.GetType() == typeof(Email))
                {
                    if (m.MessageID == 1)
                    {
                        messageExists = true;
                    }
                }
            }

            Assert.AreEqual(true, messageExists);
        }

        [TestMethod]
        public void testAddingEmailStandard()
        {
            bool messageExists = false;
            NBMFilteringService nbm = new NBMFilteringService();
            nbm.NewMessage("E", 12, "bob@email.com", "Email Subject", "This is a Email");
            List<Message> list = new List<Message>();
            list = nbm.GetMessageList();
            foreach (Message m in list)
            {
                if (m.GetType() == typeof(Email))
                {
                    if (m.MessageID == 12)
                    {
                        messageExists = true;
                    }
                }
            }

            Assert.AreEqual(true, messageExists);

        }

        [TestMethod]
        public void testAddingTweet()
        {
            bool messageExists = false;
            NBMFilteringService nbm = new NBMFilteringService();     
            nbm.NewMessage("T", 123, "@bob", "", "This is a Tweet");
            List<Message> list = new List<Message>();
            list = nbm.GetMessageList();
            foreach(Message m in list)
            {
                if(m.GetType() == typeof(Tweet))
                {
                    if(m.MessageID == 123)
                    {
                        messageExists = true;
                    }
                }
            }

            Assert.AreEqual(true, messageExists);
        }

        [TestMethod]
        public void testAddingSMS()
        {
            bool messageExists = false;
            NBMFilteringService nbm = new NBMFilteringService();
            nbm.NewMessage("S", 1234, "447853324563", "", "This is a SMS");
            List<Message> list = new List<Message>();
            list = nbm.GetMessageList();
            foreach (Message m in list)
            {
                if (m.GetType() == typeof(SMS))
                {
                    if (m.MessageID == 1234)
                    {
                        messageExists = true;
                    }
                }
            }

            Assert.AreEqual(true, messageExists);
        }
        
    }
}
