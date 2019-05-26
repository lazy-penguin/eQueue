using System;
using System.Linq;

namespace DataManagers
{
    public static class QueueManager
    {
        /*create new QueueInfo and UserAcces with info about queue's owner*/
        public static QueueInfo Insert(string queueName, string link, DateTime? timer, int ownerId)
        {
            using (var context = new eQueueContext())
            {
                var queue = new QueueInfo { Name = queueName, Link = link, Timer = timer };
                var owner = context.Users.Find(ownerId);
                var access = new UserAccess
                {
                    Nickname = owner.Login,
                    AccessTypeName = AccessType.Owner,
                    User = owner,
                    QueueInfo = queue
                };

                context.Queues.Add(queue);
                context.UserAccesses.Add(access);
                context.SaveChanges();

                return queue;
            }
        }

        public static QueueInfo GetQueue(int id)
        {
            using (var context = new eQueueContext())
            {
                return context.Queues.Find(id);
            }
        }

        public static QueueInfo GetQueue(string link)
        {
            using (var context = new eQueueContext())
            {
                return context.Queues.FirstOrDefault(q => q.Link == link);
            }
        }

        public static string GetName(int id)
        {
            using (var context = new eQueueContext())
            {
                var queue = context.Queues.Find(id);
                if (queue == null)
                    return null;
                return queue?.Name;
            }
        }

        public static void UpdateName(int id, string newName)
        {
            using (var context = new eQueueContext())
            {
                var queue = context.Queues.Find(id);
                queue.Name = newName;
                context.SaveChanges();
            }
        }

        public static DateTime? GetTimer(int id)
        {
            using (var context = new eQueueContext())
            {
                var queue = context.Queues.Find(id);
                return queue?.Timer;
            }
        }

        public static void UpdateTimer(int id, DateTime newTimer)
        {
            using (var context = new eQueueContext())
            {
                var queue = context.Queues.Find(id);
                queue.Timer = newTimer;
                context.SaveChanges();
            }
        }

        public static bool Delete(int id)
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
