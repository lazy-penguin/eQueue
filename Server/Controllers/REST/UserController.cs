using System;
using System.Collections.Generic;
using System.Linq;
using DataManagers;
using System.Web.Http;

namespace ServerSide.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public string Login(int id)
        {
            return UserManager.GetLogin(id);
        }
        [HttpPost]
        public string Login(int id, string login)
        {
            return UserManager.UpdateLogin(id, login);
        }

        [HttpGet]
        public string Nickname(int userId, int queueId)
        {
            return UserAccessManager.GetNickname(userId, queueId);
        }
        [HttpPost]
        public string Nickname(int userId, int queueId, string nickname)
        {
            return UserAccessManager.UpdateNickname(userId, queueId, nickname);
        }

        [HttpGet]
        public void Status(int id)
        {
            UserManager.UpdateStatus(id);
        }
        [HttpGet]
        public string Access(int userId, int queueId)
        {
            return UserAccessManager.GetAccessType(userId, queueId);
        }
        public class UserData
        {
            public int Id;
            public string Name;
            public readonly string Token;
            public bool IsTemporary;
            public UserData(string name, int id, string token, bool isTemporary)
            {
                Name = name;
                Id = id;
                Token = token;
                IsTemporary = isTemporary;
            }
        }

        /*get user by token*/
        [HttpGet]
        public UserData SignIn(string token)
        {
            var userId = TokenManager.GetUserId(token);
            var user = UserManager.GetUser(userId);
            if (user == null)
                return null;
            var data = new UserData(user.Login, user.Id, token, user.IsTemporary);
            return data;
        }

        /*temporary registration*/
        [HttpGet]
        public UserData SignUp()
        {
            var user = UserManager.Insert();
            var data = new UserData(user.Login, user.Id, user.Tokens.Last().Token, user.IsTemporary);
            return data;
        }

        /*regular registration*/
        [HttpPost]
        public string SignUp([FromBody]UserData userData)
        {
            UserManager.MakeRegular(userData.Id, userData.Name, null);  //where is password&&&&&
            return userData.Name;
        }

        [HttpDelete]
        public void Delete(int id)
        {
            UserManager.Delete(id);
        }

        [HttpGet]
        public List<QueueInfo> Queues(int userId)
        {
            return UserAccessManager.GetQueues(userId);
        }
    }
}