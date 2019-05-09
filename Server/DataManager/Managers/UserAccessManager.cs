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
        public static bool Insert(string nickname, int userId, int queueId)
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
            return true;
        }

        public static string GetAccessType(int userId, int queueId)
        {
            using (var context = new eQueueContext())
            {
                var access = context.UserAccesses
                                    .Where(a => a.QueueInfoId == queueId && a.UserId == userId)
                                    .FirstOrDefault();
                return access.AccessTypeName.ToString();
            }
        }

        
        public static string UpdateAccessType(int userId, int queueId, AccessType type)
        {
            using (var context = new eQueueContext())
            {
                var access = context.UserAccesses
                                    .Where(a => a.QueueInfoId == queueId && a.UserId == userId)
                                    .FirstOrDefault();
                access.AccessTypeName = type;
                context.SaveChanges();
                return access.User.Login;
            }
        }

        public static string ChangeOwner(int userId, int queueId)
        {
            return UpdateAccessType(userId, queueId, AccessType.Owner);
        }

        public static string GetNickname(int userId, int queueId)
        {
            using (var context = new eQueueContext())
            {
                var access = context.UserAccesses
                                    .Where(a => a.QueueInfoId == queueId && a.UserId == userId)
                                    .FirstOrDefault();
                return access.Nickname;
            }
        }

        public static string UpdateNickname(int userId, int queueId, string newNickname)
        {
            using (var context = new eQueueContext())
            {
                var access = context.UserAccesses
                                    .Where(a => a.QueueInfoId == queueId && a.UserId == userId)
                                    .FirstOrDefault();

                access.Nickname = newNickname;
                context.SaveChanges();
                return access.Nickname;
            }
        }     
    }
}
