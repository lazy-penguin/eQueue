using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class QueueController
    {
        /*create new QueueInfo and UserAcces with info about queue's owner*/
        bool Insert(String name, String link, DateTime timer, String nickname,int userId)
        {
            using (var context = new eQueueContext())
            {
                QueueInfo queue = new QueueInfo { Name = name, Link =  link, Timer = timer};
                UserAccess access = new UserAccess { Nickname = nickname, AccessTypeName = AccessType.Owner };
                User user = context.Users.Find(userId);


                access.User = user;
                access.QueueInfo = queue;

                user.UserAccesses.Add(access);
                queue.UserAccesses.Add(access);
                context.Queues.Add(queue);
                context.UserAccesses.Add(access);
                context.SaveChanges();
            }
            return true;
        }

        bool UpdateName(int id, String newName)
        {
            using (var context = new eQueueContext())
            {
                QueueInfo queue = context.Queues.Find(id);
                queue.Name = newName;
                context.SaveChanges();
            }
            return true;
        }

        bool UpdateTimer(int id, DateTime newTimer)
        {
            using (var context = new eQueueContext())
            {
                QueueInfo queue = context.Queues.Find(id);
                queue.Timer = newTimer;
                context.SaveChanges();
            }
            return true;
        }

        bool Delete(int id)
        {
            using (var context = new eQueueContext())
            {
                QueueInfo queue = context.Queues.Find(id);
                if (queue == null)
                    return false;
                context.Queues.Remove(queue);
                context.SaveChanges();
            }
            return true;
        }
    }
}
