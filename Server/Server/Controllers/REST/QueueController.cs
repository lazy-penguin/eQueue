using System;
using System.Collections.Generic;
using System.Web.Http;
using DataManagers;

namespace ServerSide.Controllers
{
    public class QueueController : ApiController
    {
        [HttpGet]
        public string Name(int id)
        {
            return QueueManager.GetName(id);
        }
        [HttpPost]
        public string Name(int id, string name)
        {
            return QueueManager.UpdateName(id, name);
        }

        [HttpGet]
        public string Timer(int id)
        {
            return QueueManager.GetTimer(id).ToString();
        }
        [HttpPost]
        public string Timer(int id, DateTime timer)
        {
            return QueueManager.UpdateTimer(id, timer).ToString();
        }

        [HttpGet]
        public List<User> AllUsers(int queueId)
        {
            return UserAccessManager.GetUsers(queueId);
        }

        public class QueueData
        {
            public int UserId;
            public string Name;
            public string UserNickname;
            public string Link;
            public DateTime Timer;
        }

        [HttpPost]
        public string Create([FromBody]QueueData queueData)
        {
            string link = Guid.NewGuid().ToString();
            QueueInfo queue = QueueManager.Insert(queueData.Name, link, queueData.Timer, queueData.UserNickname, queueData.UserId);
            return queueData.Name;
        }
        [HttpDelete]
        public void Delete(int id)
        {
            QueueManager.Delete(id);
        }
       
        [HttpPost]
        public string Owner(int queueId, int userId)
        {
            return UserAccessManager.ChangeOwner(userId, queueId);
        }
    }
}