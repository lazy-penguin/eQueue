using System;

namespace DataManagers
{
    public static class QueueManager
    {
        /*create new QueueInfo and UserAcces with info about queue's owner*/
        public static QueueInfo Insert(string name, string link, DateTime timer, string nickname,int userId)
        {
            using (var context = new eQueueContext())
            {
                var queue = new QueueInfo { Name = name, Link =  link, Timer = timer};
                var access = new UserAccess { Nickname = nickname, AccessTypeName = AccessType.Owner };
                var user = context.Users.Find(userId);

                access.User = user;
                access.QueueInfo = queue;

                user.UserAccesses.Add(access);
                queue.UserAccesses.Add(access);
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
