﻿using System;
using System.Linq;

namespace DataManagers
{
    public class TokenManager
    {
        /*user join the queue*/
        public static void Insert(int userId)
        {
            using (var context = new eQueueContext())
            {
                UserToken token = new UserToken { Token = Guid.NewGuid().ToString()};
                User user = context.Users.Find(userId);

                user.Tokens.Add(token);
                token.User = user;

                context.Tokens.Add(token);
                context.SaveChanges();
            }
        }

        public static void Insert(User user)
        {
                UserToken token = new UserToken { Token = Guid.NewGuid().ToString() };
                user.Tokens.Add(token);
                token.User = user;
        }

        public static string GetToken(int userId)
        {
            using (var context = new eQueueContext())
            {
                var token = context.Tokens
                                    .Where(t => t.UserId == userId)
                                    .FirstOrDefault();
                return token.Token;
            }
        }

        public static int GetUserId(string token)
        {
            using (var context = new eQueueContext())
            {
                var user = context.Tokens.FirstOrDefault(t => t.Token == token);
                return user != null ? user.Id : 0;
            }
        }

        public static void Delete(int userId)
        {
            using (var context = new eQueueContext())
            {
                var token = context.Tokens
                                    .Where(t => t.UserId == userId)
                                    .FirstOrDefault();
                context.Tokens.Remove(token);
                context.SaveChanges();
            }
        }
    }
}
