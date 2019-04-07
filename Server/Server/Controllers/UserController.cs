using System;

namespace Server
{
    class UserController
    {
        /*registrate new user*/
        bool Insert(String login, String passwordHash)
        {
            using (var context = new eQueueContext())
            {            
                User user = new User { IsTemporary = false, Login = login, PasswordHash = passwordHash, LastActivity = DateTime.Now };
                context.Users.Add(user);
                context.SaveChanges();
            }
            return true;
        }

        bool UpdateStatus(int id)
        {
            using (var context = new eQueueContext())
            {
                User user = context.Users.Find(id);
                user.IsTemporary = true;
                context.SaveChanges();
            }
            return true;
        }

        bool UpdateLogin(int id, String newLogin)
        {
            using (var context = new eQueueContext())
            {
                User user = context.Users.Find(id);
                user.Login = newLogin;
                context.SaveChanges();
            }
            return true;
        }

        bool UpdatePassword(int id, String newPasswordHash)
        {
            using (var context = new eQueueContext())
            {
                User user = context.Users.Find(id);
                user.PasswordHash = newPasswordHash;
                context.SaveChanges();
            }
            return true;
        }

        bool Delete(int id)
        {
            using (var context = new eQueueContext())
            {
                User user = context.Users.Find(id);
                if (user == null)
                    return false;
                context.Users.Remove(user);
                context.SaveChanges();
            }
            return true;
        }
    }
}
