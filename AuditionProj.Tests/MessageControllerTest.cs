using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuditionProj;
using AuditionProj.Models;
using AuditionProj.Controllers;

namespace AuditionProj.Tests
{
    [TestClass]
    public class MessageControllerTest
    {
        [TestMethod]
        public void GetAllMessagesNotNull()
        {
            var controller = new MessageController();

            Assert.IsNotNull(controller.Get());
        }

        [TestMethod]
        public void GetMessageNotNull()
        {
            var controller = new MessageController();

            Assert.IsNotNull(controller.Get(100));
        }

        [TestMethod]
        public void PostMessage()
        {
            var controller = new MessageController();
            MessageModel message = new MessageModel
            {
                MessageContent = "test4"
            };

            controller.Post(message);
            var result = controller.Get();

            int maxID = (int)result.Max(msg => msg.ID);
            try
            {
                Assert.AreEqual(controller.Get(maxID).MessageContent, message.MessageContent);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                controller.Delete(maxID);
            }
        }
    }
}
