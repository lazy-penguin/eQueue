﻿using DataManagers;
using Server.Data;
using System;
using System.Net;
using System.Web.Http;

namespace Server.Controllers
{
    public class QueueController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Queue(string link)
        {
            var queue = QueueManager.GetQueue(link);
            if (queue == null)
                return NotFound();

            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0 || !Auth.CheckAccess(uid, queue.Id))
                return StatusCode(HttpStatusCode.Forbidden);

            var owner = UserAccessManager.GetOwner(queue.Id);
            if (owner == null)
                return InternalServerError();

            var data = new QueueData(queue.Id, owner.UserId, queue.Name, owner.Nickname, queue.Link, queue.Timer);
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
            var user = UserManager.GetUser(uid);

            if (queueData == null)
                return BadRequest();

            string link = Guid.NewGuid().ToString();
            var queueInfo = QueueManager.Insert(queueData.Name, link, queueData.Timer, user.Id);

            return new ObjectResult(new QueueData(queueInfo.Id, user.Id, queueInfo.Name, user.Login, queueInfo.Link, queueInfo.Timer), typeof(QueueData), Request);
        }

        [HttpGet]
        public IHttpActionResult Join(string link)
        {
            int uid = Auth.CheckToken(Request.Headers);
            if (uid == 0)
                return StatusCode(HttpStatusCode.Forbidden);

            var queue = QueueManager.GetQueue(link);
            if (queue == null)
                return NotFound();

            UserAccessManager.Insert(UserManager.GetUser(uid).Login, uid, queue.Id);
            return Ok(true);
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