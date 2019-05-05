using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataManagers;

namespace Server.Controllers
{
    public class OrderController : Controller
    {

        [HttpGet]
        public bool Swap(int userA, int userB, int id)
        {
            return QueueOrderManager.SwapUsers(userA, userB, id);
        }

        [HttpGet]
        public bool GetIn(int queueId, int userId)
        {
            return QueueOrderManager.Insert(userId, queueId);
        }

        [HttpGet]
        public void Next(int queueId)
        {
            QueueOrderManager.GetNext(queueId);
        }

        /*delete position in the queue*/
        [HttpDelete]
        public bool Exit(int queueId, int userId)
        {
            return QueueOrderManager.Delete(userId, queueId);
        }
    }
}