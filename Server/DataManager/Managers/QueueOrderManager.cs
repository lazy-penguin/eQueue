using System;
using System.Collections.Generic;
using System.Linq;

namespace DataManagers
{
    public static class QueueOrderManager
    {
        public static List<QueueInfo> GetQueues(int userId)
        {
            using (var context = new eQueueContext())
            {
                return context.QueueOrders.Where(qo => qo.UserId == userId).Select(qo => qo.QueueInfo).ToList();
            }
        }

        public static List<User> GetUsers(int queueId)
        {
            using (var context = new eQueueContext())
            {
                return context.QueueOrders.Where(qo => qo.QueueInfoId == queueId).Select(qo => qo.User).ToList();
            }
        }

        public static int GetLastNumberInQueue(QueueInfo queue, eQueueContext context)
        {
            return context.QueueOrders.Where(qo => qo.QueueInfoId == queue.Id).Max(qo => qo.Number);
        }

        public static bool SwapUsers(int userIdA, int userIdB, int queueId)
        {
            using (var context = new eQueueContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var orderA = context.QueueOrders
                                                .Where(a => a.QueueInfoId == queueId && a.UserId == userIdA)
                                                .FirstOrDefault();

                        var orderB = context.QueueOrders
                                                .Where(b => b.QueueInfoId == queueId && b.UserId == userIdB)
                                                .FirstOrDefault();

                        int tempNumber = orderA.Number;
                        orderA.Number = orderB.Number;
                        orderB.Number = tempNumber;

                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool Insert(int userId, int queueId)
        {
            using (var context = new eQueueContext())
            {
                QueueOrder order = new QueueOrder { };
                User user = context.Users.Find(userId);
                QueueInfo queue = context.Queues.Find(queueId);

                user.QueueOrders.Add(order);
                queue.QueueOrders.Add(order);
                order.User = user;
                order.QueueInfo = queue;

                order.Number = GetLastNumberInQueue(queue, context);

                context.QueueOrders.Add(order);
                context.SaveChanges();
            }
            return true;
        }

        public static bool UpdateNumber(int userId, int queueId, int newNumber)
        {
            using (var context = new eQueueContext())
            {
                var order = context.QueueOrders
                                    .Where(qo => qo.QueueInfoId == queueId && qo.UserId == userId)
                                    .FirstOrDefault();
                order.Number = newNumber;
                context.SaveChanges();
            }
            return true;
        }

        public static void GetNext(int queueId)
        {
            using (var context = new eQueueContext())
            {
                var order = context.QueueOrders
                                   .Where(a => a.QueueInfoId == queueId && a.Number == 1)
                                   .FirstOrDefault();
                if (order == null)
                    return;
                context.QueueOrders.Remove(order);

                context.QueueOrders.Where(qo => qo.QueueInfoId == queueId)
                                   .AsEnumerable()
                                   .Select(qo => {
                                       qo.Number--;
                                       return qo;
                                   });
                context.SaveChanges();
            }
        }

        public static bool Delete(int userId, int queueId)
        {
            using (var context = new eQueueContext())
            {
                var order = context.QueueOrders
                                    .Where(a => a.QueueInfoId == queueId && a.UserId == userId)
                                    .FirstOrDefault();
                if (order == null)
                    return false;

                context.QueueOrders.Remove(order);

                context.QueueOrders.Where(qo => qo.QueueInfoId == queueId)
                                    .AsEnumerable()
                                    .Select(qo => {
                                        qo.Number--;
                                        return qo;
                                    });
                context.SaveChanges();
            }
            return true;
        }
    }
}
