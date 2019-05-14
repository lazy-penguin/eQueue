using System.Linq;
using System.Collections.Generic;

namespace DataManagers
{
    public static class UserAccessManager
    {
        public static List<QueueInfo> GetQueues(int userId)
        {
            using (var context = new eQueueContext())
            {
                return context.UserAccesses.Where(qo => qo.UserId == userId).Select(qo => qo.QueueInfo).ToList();
            }
        }

        public static List<User> GetUsers(int queueId)
        {
            using (var context = new eQueueContext())
            {
                return context.UserAccesses.Where(qo => qo.QueueInfoId == queueId).Select(qo => qo.User).ToList();
            }
        }

        /*user join the queue*/
        public static void Insert(string nickname, int userId, int queueId)
        {
            using (var context = new eQueueContext())
            {
                UserAccess access = new UserAccess { Nickname = nickname, AccessTypeName = AccessType.Default};
                User user = context.Users.Find(userId);
                QueueInfo queue = context.Queues.Find(queueId);

                user.UserAccesses.Add(access);
                queue.UserAccesses.Add(access);
                access.User = user;
                access.QueueInfo = queue;

                context.UserAccesses.Add(access);
                context.SaveChanges();
            }
        }

        public static bool CheckAccess(int userId, int queueId)
        {
            using (var context = new eQueueContext())
            {
                var access = context.UserAccesses
                                    .Where(a => a.QueueInfoId == queueId && a.UserId == userId)
                                    .FirstOrDefault();
                return access != null ? true : false;
            }
        }

        public static bool CheckPrivilegedAccess(int userId, int queueId)
        {
            using (var context = new eQueueContext())
            {
                var access = context.UserAccesses
                                    .Where(a => a.QueueInfoId == queueId && a.UserId == userId)
                                    .FirstOrDefault();
                return access?.AccessTypeName == AccessType.Owner ? true : false;
            }
        }

        public static string GetAccessType(int userId, int queueId)
        {
            using (var context = new eQueueContext())
            {
                var access = context.UserAccesses
                                    .Where(a => a.QueueInfoId == queueId && a.UserId == userId)
                                    .FirstOrDefault();
                return access?.AccessTypeName.ToString();
            }
        }
        
        public static void UpdateAccessType(int userId, int queueId, AccessType type)
        {
            using (var context = new eQueueContext())
            {
                var access = context.UserAccesses
                                    .Where(a => a.QueueInfoId == queueId && a.UserId == userId)
                                    .FirstOrDefault();
                access.AccessTypeName = type;
                context.SaveChanges();
            }
        }

        public static UserAccess GetOwner(int queueId)
        {
            using (var context = new eQueueContext())
            {
                return context.UserAccesses
                                    .Where(a => a.QueueInfoId == queueId && a.AccessTypeName == AccessType.Owner)
                                    .FirstOrDefault();
            }
        }

        public static void ChangeOwner(int userId, int queueId)
        {
            UpdateAccessType(userId, queueId, AccessType.Owner);
        }

        public static string GetNickname(int userId, int queueId)
        {
            using (var context = new eQueueContext())
            {
                var access = context.UserAccesses
                                    .Where(a => a.QueueInfoId == queueId && a.UserId == userId)
                                    .FirstOrDefault();
                return access?.Nickname;
            }
        }

        public static void UpdateNickname(int userId, int queueId, string newNickname)
        {
            using (var context = new eQueueContext())
            {
                var access = context.UserAccesses
                                    .Where(a => a.QueueInfoId == queueId && a.UserId == userId)
                                    .FirstOrDefault();

                access.Nickname = newNickname;
                context.SaveChanges();
            }
        }     
    }
}
