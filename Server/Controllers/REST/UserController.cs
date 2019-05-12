using System;
using System.Collections.Generic;
using System.Linq;
using DataManagers;
using System.Web.Http;
using System.Net;
using Server.Data;

namespace Server.Controllers
{
    public class UserController : ApiController
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

        [HttpGet]
        public IHttpActionResult Login(int id)
        {
            int uid = CheckToken();
            if (uid == 0 || uid != id)
                return StatusCode(HttpStatusCode.Forbidden);

            var login = UserManager.GetLogin(id);
            return new ObjectResult(login, login.GetType(), Request);
        }

        [HttpPost]
        public IHttpActionResult Login(int id, string login)
        {
            int uid = CheckToken();
            if (uid == 0 || uid != id)
                return StatusCode(HttpStatusCode.Forbidden);

            UserManager.UpdateLogin(id, login);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Nickname(int userId, int queueId)
        {
            int uid = CheckToken();
            if (uid == 0 || uid != userId)
                return StatusCode(HttpStatusCode.Forbidden);

            var nickname = UserAccessManager.GetNickname(userId, queueId);
            return new ObjectResult(nickname, nickname.GetType(), Request);
        }

        [HttpPost]
        public IHttpActionResult Nickname(int userId, int queueId, string nickname)
        {
            int uid = CheckToken();
            if (uid == 0 || uid != userId)
                return StatusCode(HttpStatusCode.Forbidden);

            UserAccessManager.UpdateNickname(userId, queueId, nickname);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Status(int id)
        {
            int uid = CheckToken();
            if (uid == 0 || uid != id)
                return StatusCode(HttpStatusCode.Forbidden);

            UserManager.UpdateStatus(id);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Access(int userId, int queueId)
        {
            int uid = CheckToken();
            if (uid == 0 || uid != userId)
                return StatusCode(HttpStatusCode.Forbidden);

            var access = UserAccessManager.GetAccessType(userId, queueId);
            return new ObjectResult(access, access.GetType(), Request);
        }

        /*get user by token*/
        [HttpGet]
        public IHttpActionResult SignIn()
        {
            int uid = CheckToken();
            if(uid == 0)
                return StatusCode(HttpStatusCode.Forbidden);

            var user = UserManager.GetUser(uid);
            UserData data = new UserData(user.Login, user.Id, null, user.IsTemporary);
            return new ObjectResult(data, data.GetType(), Request);
        }

        /*temporary registration*/
        [HttpGet]
        public IHttpActionResult SignUp()
        {
            var user = UserManager.Insert();
            var data = new UserData(user.Login, user.Id, user.Tokens.Last().Token, user.IsTemporary);
            return new ObjectResult(data, data.GetType(), Request);
        }

        /*regular registration*/
        [HttpPost]
        public IHttpActionResult SignUp([FromBody]UserData userData)
        {
            int uid = CheckToken();
            if (uid == 0)
                return StatusCode(HttpStatusCode.Forbidden);

            if (userData == null)
                return BadRequest();

            bool ret = UserManager.MakeRegular(userData.Id, userData.Name, null);  //where is password????
            return Ok(ret);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            int uid = CheckToken();
            if (uid == 0 || uid != id)
                return StatusCode(HttpStatusCode.Forbidden);

            bool ret = UserManager.Delete(id);
            return Ok(ret);
        }

        [HttpGet]
        public IHttpActionResult Queues(int id)
        {
            int uid = CheckToken();
            if (uid == 0 || uid != id)
                return StatusCode(HttpStatusCode.Forbidden);

            var queues = UserAccessManager.GetQueues(id);
            return new ObjectResult(queues, queues.GetType(), Request);
        }
    }
}