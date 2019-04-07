using System;
using System.Collections.Generic;
using System.Linq;

namespace Server
{
    class QueueOrderController
    {
        List<QueueInfo> GetQueues(int userId, eQueueContext context)
        {
            return context.QueueOrders.Where(qo => qo.UserId == userId).Select(qo => qo.QueueInfo).ToList();
        }

        List<User> GetUsers(int queueId, eQueueContext context)
        {
            return context.QueueOrders.Where(qo => qo.QueueInfoId == queueId).Select(qo => qo.User).ToList();
        }

        int GetLastNumberInQueue(QueueInfo queue, eQueueContext context)
        {
            return context.QueueOrders.Where(qo => qo.QueueInfoId == queue.Id).Max(qo => qo.Number);
        }

        bool SwapUsers(int userIdA, int userIdB, int queueId, eQueueContext context)
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
            return true;
        }

        bool Insert(int userId, int queueId)
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

        bool UpdateNumber(int userId, int queueId, int newNumber)
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

        bool Delete(int userId, int queueId)
        {
            using (var context = new eQueueContext())
            {
                var order = context.QueueOrders
                                    .Where(a => a.QueueInfoId == queueId && a.UserId == userId)
                                    .FirstOrDefault();

                if (order.Number != 1)
                    return false;
                context.QueueOrders.Remove(order);

                IEnumerable<QueueOrder> orders = context.QueueOrders
                                                .Where(qo => qo.QueueInfoId == queueId && qo.UserId == userId)
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
