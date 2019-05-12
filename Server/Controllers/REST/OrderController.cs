using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using DataManagers;
using Server.Data;

namespace Server.Controllers
{
    public class OrderController : ApiController
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

        [HttpGet]
        public IHttpActionResult Users(int id)
        {
            int uid = CheckToken();
            if (uid == 0 || !CheckAccess(uid, id))
                return StatusCode(HttpStatusCode.Forbidden);

            var users = QueueOrderManager.GetUsers(id);
            return new ObjectResult(users, users.GetType(), Request);
        }

        [HttpGet]
        public IHttpActionResult Swap(int userA, int userB, int id)
        {
            int uid = CheckToken();
            if (uid == 0 || uid != userA || !CheckAccess(userA, id) || !CheckAccess(userB, id))
                return StatusCode(HttpStatusCode.Forbidden);

            bool ret = QueueOrderManager.SwapUsers(userA, userB, id);
            return Ok(ret);
        }

        [HttpGet]
        public IHttpActionResult GetIn(int queueId, int userId)
        {
            int uid = CheckToken();
            if (uid == 0 || uid != userId || !CheckAccess(uid, queueId))
                return StatusCode(HttpStatusCode.Forbidden);

            bool ret = QueueOrderManager.Insert(userId, queueId);
            return Ok(ret);
        }

        [HttpGet]
        public IHttpActionResult Next(int id)
        {
            int uid = CheckToken();
            if (uid == 0 || !CheckAccess(uid, id))
                return StatusCode(HttpStatusCode.Forbidden);

            QueueOrderManager.GetNext(id);
            return Ok();
        }

        /*delete position in the queue*/
        [HttpDelete]
        public IHttpActionResult Exit(int queueId, int userId)
        {
            int uid = CheckToken();
            if (uid == 0 || uid != userId || !CheckAccess(uid, queueId))
                return StatusCode(HttpStatusCode.Forbidden);

            bool ret = QueueOrderManager.Delete(userId, queueId);
            return Ok(ret);
        }
    }
}