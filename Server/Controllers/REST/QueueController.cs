using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using DataManagers;
using Server.Data;

namespace Server.Controllers
{
    public class QueueController : ApiController
    {
       [HttpGet]
       public IHttpActionResult Queue(int id)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0 || !Auth.CheckAccess(uid, id))
                return StatusCode(HttpStatusCode.Forbidden);

            var queue = QueueManager.GetQueue(id);
            var owner = UserAccessManager.GetOwner(id);
            if (queue == null || owner == null)
                return null;
            var data = new QueueData(owner.UserId, queue.Name, owner.Nickname, queue.Link, queue.Timer);
            return new ObjectResult(data, data.GetType(), Request);
        }

        [HttpGet]
        public IHttpActionResult Name(int id)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0 || !Auth.CheckAccess(uid, id))
                return StatusCode(HttpStatusCode.Forbidden);

            var name = QueueManager.GetName(id);
            return new ObjectResult(name, name.GetType(), Request);
        }

        [HttpPost]
        public IHttpActionResult Name(int id, string name)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0 || !Auth.CheckAccess(uid, id))
                return StatusCode(HttpStatusCode.Forbidden);

            QueueManager.UpdateName(id, name);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Timer(int id)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0 || !Auth.CheckAccess(uid, id))
                return StatusCode(HttpStatusCode.Forbidden);

            var timer = QueueManager.GetTimer(id).ToString();
            return new ObjectResult(timer, timer.GetType(), Request);
        }

        [HttpPost]
        public IHttpActionResult Timer(int id, DateTime timer)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0 || !Auth.CheckAccess(uid, id))
                return StatusCode(HttpStatusCode.Forbidden);

            QueueManager.UpdateTimer(id, timer);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Users(int id)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0 || !Auth.CheckAccess(uid, id))
                return StatusCode(HttpStatusCode.Forbidden);

            var users = UserAccessManager.GetUsers(id);
            return new ObjectResult(users, users.GetType(), Request);
        }

        [HttpPost]
        public IHttpActionResult Create([FromBody]QueueData queueData)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0)
                return StatusCode(HttpStatusCode.Forbidden);

            if (queueData == null)
                return BadRequest();

            string link = Guid.NewGuid().ToString();
            QueueManager.Insert(queueData.Name, link, queueData.Timer, queueData.UserNickname, queueData.UserId);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Join(int queueId, [FromBody]string nickname)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0)
                return StatusCode(HttpStatusCode.Forbidden);

            UserAccessManager.Insert(nickname, uid, queueId);
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0 || !Auth.CheckPrivilegedAccess(uid, id))
                return StatusCode(HttpStatusCode.Forbidden);

            bool ret = QueueManager.Delete(id);
            return Ok(ret);
        }
       
        [HttpPost]
        public IHttpActionResult Owner(int queueId, int userId)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0 || !Auth.CheckPrivilegedAccess(uid, queueId))
                return StatusCode(HttpStatusCode.Forbidden);

            UserAccessManager.ChangeOwner(userId, queueId);
            return Ok();
        }
    }
}