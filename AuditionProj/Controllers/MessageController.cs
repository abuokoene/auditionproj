using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Cors;
using System.Web.Http;
using AuditionProj.Models;

namespace AuditionProj.Controllers
{
    [EnableCors(origins:"*", headers:"*", methods:"*")]
    public class MessageController : ApiController
    {
        MessagePersistence messagePersistence = new MessagePersistence();
        
        // GET: api/Message
        public IEnumerable<MessageModel> Get()
        {
            return messagePersistence.GetMessages();
        }

        // GET: api/Message/5
        public MessageModel Get(int id)
        {
            MessageModel message = messagePersistence.GetMessage(id);

            if (message.MessageContent == null)
                return new MessageModel();
            else
                return message;
        }

        // POST: api/Message
        public void Post([FromBody]MessageModel message)
        {
            messagePersistence.SaveMessage(message);
        }

        // PUT: api/Message/5
        public void Put(int id, [FromBody]MessageModel message)
        {
            messagePersistence.UpdateMessage(id, message);
        }

        // DELETE: api/Message/5
        public void Delete(int id)
        {
            messagePersistence.DeleteMessage(id);
        }
    }
}
