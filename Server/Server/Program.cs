using System;
using System.Linq;

namespace eQueue
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new eQueueContext())
            {
                for (int i = 0; i < 5; i++)
                {
                    context.Users.Add(new User { IsTemporary = false, Login = i.ToString() });
                }
                context.SaveChanges();

                var users = context.Users.ToArray();
                Console.WriteLine($"We have {users.Length} users.");
                foreach (var user in users)
                {
                    Console.WriteLine(user);
                }

                for (int i = 0; i < 5; i++)
                {
                    context.Queues.Add(new QueueInfo { Name = i.ToString() });
                }

                var queues = context.Queues.ToArray();
                Console.WriteLine($"We have {users.Length} queues.");
                foreach (var user in queues)
                {
                    Console.WriteLine(user);
                }

                var accesses = context.UserAccesses.ToArray();
                Console.WriteLine($"We have {users.Length} accesses.");
                foreach (var user in accesses)
                {
                    Console.WriteLine(user);
                }


                var orders = context.QueueOrders.ToArray();
                Console.WriteLine($"We have {users.Length} orders.");
                foreach (var user in orders)
                {
                    Console.WriteLine(user);
                }
            }
        }
            
    }
}