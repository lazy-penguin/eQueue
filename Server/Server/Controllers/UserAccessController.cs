using System;
using System.Linq;
using System.Collections.Generic;

namespace Server
{
    class UserAccessController
    {
        List<QueueInfo> GetQueues(int userId, eQueueContext context)
        {
            return context.UserAccesses.Where(qo => qo.UserId == userId).Select(qo => qo.QueueInfo).ToList();
        }

        List<User> GetUsers(int queueId, eQueueContext context)
        {
            return context.UserAccesses.Where(qo => qo.QueueInfoId == queueId).Select(qo => qo.User).ToList();
        }

        /*user join the queue*/
        bool Insert(String nickname, int userId, int queueId)
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

        bool UpdateAccessType(int userId, int queueId, AccessType type)
        {
            using (var context = new eQueueContext())
            {
                var access = context.UserAccesses
                                    .Where(a => a.QueueInfoId == queueId && a.UserId == userId)
                                    .FirstOrDefault();
                access.AccessTypeName = type;
                context.SaveChanges();
            }
            return true;
        }

        bool UpdateNickname(int userId, int queueId, String newNickname)
        {
            using (var context = new eQueueContext())
            {
                var access = context.UserAccesses
                                    .Where(a => a.QueueInfoId == queueId && a.UserId == userId)
                                    .FirstOrDefault();

                access.Nickname = newNickname;
                context.SaveChanges();
            }
            return true;
        }     
    }
}
