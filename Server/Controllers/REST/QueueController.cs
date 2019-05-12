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
        private int CheckToken()
        {
            string token = null;
            if (Request.Headers.Contains("Authorization"))
            {
                token = Request.Headers.GetValues("Authorization").First();
            }

            if (token == null)
                return 0;

            var userId = TokenManager.GetUserId(token);
            return userId;
        }
        private bool CheckAccess(int userId, int queueId)
        {
            return UserAccessManager.CheckAccess(userId, queueId);
        }
        private bool CheckPrivilegedAccess(int userId, int queueId)
        {
            return UserAccessManager.CheckPrivilegedAccess(userId, queueId);
        }

        [HttpGet]
        public IHttpActionResult Name(int id)
        {
            int uid = CheckToken();
            if (uid == 0 || !CheckAccess(uid, id))
                return StatusCode(HttpStatusCode.Forbidden);

            var name = QueueManager.GetName(id);
            return new ObjectResult(name, name.GetType(), Request);
        }

        [HttpPost]
        public IHttpActionResult Name(int id, string name)
        {
            int uid = CheckToken();
            if (uid == 0 || !CheckAccess(uid, id))
                return StatusCode(HttpStatusCode.Forbidden);

            QueueManager.UpdateName(id, name);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Timer(int id)
        {
            int uid = CheckToken();
            if (uid == 0 || !CheckAccess(uid, id))
                return StatusCode(HttpStatusCode.Forbidden);

            var timer = QueueManager.GetTimer(id).ToString();
            return new ObjectResult(timer, timer.GetType(), Request);
        }

        [HttpPost]
        public IHttpActionResult Timer(int id, DateTime timer)
        {
            int uid = CheckToken();
            if (uid == 0 || !CheckAccess(uid, id))
                return StatusCode(HttpStatusCode.Forbidden);

            QueueManager.UpdateTimer(id, timer).ToString();
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Users(int id)
        {
            int uid = CheckToken();
            if (uid == 0 || !CheckAccess(uid, id))
                return StatusCode(HttpStatusCode.Forbidden);

            var users = UserAccessManager.GetUsers(id);
            return new ObjectResult(users, users.GetType(), Request);
        }

        [HttpPost]
        public IHttpActionResult Create([FromBody]QueueData queueData)
        {
            int uid = CheckToken();
            if (uid == 0)
                return StatusCode(HttpStatusCode.Forbidden);

            if (queueData == null)
                return BadRequest();

            string link = Guid.NewGuid().ToString();
            QueueManager.Insert(queueData.Name, link, queueData.Timer, queueData.UserNickname, queueData.UserId);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Join(int userId, int queueId, [FromBody]string nickname)
        {
            int uid = CheckToken();
            if (uid == 0 || uid != userId)
                return StatusCode(HttpStatusCode.Forbidden);

            UserAccessManager.Insert(nickname, userId, queueId);
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            int uid = CheckToken();
            if (uid == 0 || !CheckPrivilegedAccess(uid, id))
                return StatusCode(HttpStatusCode.Forbidden);

            bool ret = QueueManager.Delete(id);
            return Ok(ret);
        }
       
        [HttpPost]
        public IHttpActionResult Owner(int queueId, int userId)
        {
            int uid = CheckToken();
            if (uid == 0 || !CheckPrivilegedAccess(uid, queueId))
                return StatusCode(HttpStatusCode.Forbidden);

            UserAccessManager.ChangeOwner(userId, queueId);
            return Ok();
        }
    }
}