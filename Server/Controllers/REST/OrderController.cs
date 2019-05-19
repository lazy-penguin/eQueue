using DataManagers;
using Server.Data;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Server.Controllers
{
    public class OrderController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Users(int queueId)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0 || !Auth.CheckAccess(uid, queueId))
                return StatusCode(HttpStatusCode.Forbidden);

            var users = QueueOrderManager.GetUsers(queueId);
            return new ObjectResult(users, users.GetType(), Request);
        }

        [HttpGet]
        public IHttpActionResult Swap(int userB, int id)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0 || !Auth.CheckAccess(uid, id) || !Auth.CheckAccess(userB, id))
                return StatusCode(HttpStatusCode.Forbidden);

            bool ret = QueueOrderManager.SwapUsers(uid, userB, id);
            return Ok(ret);
        }

        [HttpGet]
        public IHttpActionResult GetIn(int queueId)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0 || !Auth.CheckAccess(uid, queueId))
                return StatusCode(HttpStatusCode.Forbidden);

            QueueOrderManager.Insert(uid, queueId);
            return Ok(true);
        }

        [HttpGet]
        public IHttpActionResult Next(int id)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0 || !Auth.CheckAccess(uid, id))
                return StatusCode(HttpStatusCode.Forbidden);

            bool ret = QueueOrderManager.GetNext(id);
            return Ok(ret);
        }

        /*delete position in the queue*/
        [HttpDelete]
        public IHttpActionResult Exit(int queueId)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0 || !Auth.CheckAccess(uid, queueId))
                return StatusCode(HttpStatusCode.Forbidden);

            bool ret = QueueOrderManager.Delete(uid, queueId);
            return Ok(ret);
        }
    }
}