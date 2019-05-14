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
        [HttpGet]
        public IHttpActionResult Login()
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0)
                return StatusCode(HttpStatusCode.Forbidden);

            var login = UserManager.GetLogin(uid);
            return new ObjectResult(login, login.GetType(), Request);
        }

        [HttpPost]
        public IHttpActionResult Login(string login)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0)
                return StatusCode(HttpStatusCode.Forbidden);

            UserManager.UpdateLogin(uid, login);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Nickname(int queueId)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0)
                return StatusCode(HttpStatusCode.Forbidden);

            var nickname = UserAccessManager.GetNickname(uid, queueId);
            return new ObjectResult(nickname, nickname.GetType(), Request);
        }

        [HttpPost]
        public IHttpActionResult Nickname(int queueId, [FromBody]string nickname)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0)
                return StatusCode(HttpStatusCode.Forbidden);

            UserAccessManager.UpdateNickname(uid, queueId, nickname);
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Status()
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0)
                return StatusCode(HttpStatusCode.Forbidden);

            UserManager.UpdateStatus(uid);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Access(int queueId)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0)
                return StatusCode(HttpStatusCode.Forbidden);

            var access = UserAccessManager.GetAccessType(uid, queueId);
            return new ObjectResult(access, access.GetType(), Request);
        }

        /*get user by token*/
        [HttpGet]
        public IHttpActionResult SignIn()
        {
            int uid = Auth.CheckToken(Request.Headers);
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
            if (user == null)
                return null;

            var data = new UserData(user.Login, user.Id, user.Tokens.Last().Token, user.IsTemporary);
            return new ObjectResult(data, data.GetType(), Request);
        }

        /*regular registration*/
        [HttpPost]
        public IHttpActionResult SignUp(string login, string password)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0)
                return StatusCode(HttpStatusCode.Forbidden);

            var passwordHash = Hasher.MD5Hash(password);
            bool ret = UserManager.MakeRegular(uid, login, passwordHash);
            return Ok(ret);
        }

        [HttpDelete]
        public IHttpActionResult Delete()
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0)
                return StatusCode(HttpStatusCode.Forbidden);

            bool ret = UserManager.Delete(uid);
            return Ok(ret);
        }

        [HttpGet]
        public IHttpActionResult Queues()
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0)
                return StatusCode(HttpStatusCode.Forbidden);

            var queues = UserAccessManager.GetQueues(uid);
            return new ObjectResult(queues, queues.GetType(), Request);
        }
    }
}