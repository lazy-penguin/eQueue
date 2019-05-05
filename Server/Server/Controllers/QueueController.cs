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
            public int Id;
            public string Name;
            public string Link;
            public DateTime Timer;
            public QueueData(string name, int id, string link, DateTime timer)
            {
                this.Name = name;
                this.Id = id;
                this.Link = link;
                this.Timer = timer;
            }
        }

        [HttpPost]
        public QueueData Create(string name, DateTime timer, string nickname, int id)
        {
            string link = Guid.NewGuid().ToString();
            QueueInfo queue = QueueManager.Insert(name, link, timer, nickname, id);
            return new QueueData(name, queue.Id, link, timer);
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