using System;

namespace DataManagers
{
    public static class UserManager
    {
        public static User GetUser(int id)
        {
            using (var context = new eQueueContext())
            {
                return context.Users.Find(id);
            }
        }

        /*registrate new user*/
        public static User Insert()
        {
            using (var context = new eQueueContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        User user = new User { PasswordHash = null, LastActivity = DateTime.Now, IsTemporary = true };
                        user.Login = "user";
                        TokenManager.Insert(user);

                        context.Users.Add(user);
                        context.SaveChanges();

                        user.Login += user.Id;
                        context.SaveChanges();
                        transaction.Commit();
                        return user;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        return null;
                    }
                }
            }
        }

        public static bool MakeRegular(int id, string login, string passwordHash)
        {
            using (var context = new eQueueContext())
            {
                User user = context.Users.Find(id);
                if (!user.IsTemporary)
                    return false;

                user.Login = login;
                user.PasswordHash = passwordHash;
                user.IsTemporary = false;

                context.SaveChanges();
                return true;
            }
        }

        public static bool? GetStatus(int id)
        {
            using (var context = new eQueueContext())
            {
                User user = context.Users.Find(id);
                return user?.IsTemporary;
            }
        }

        public static bool UpdateStatus(int id)
        {
            using (var context = new eQueueContext())
            {
                User user = context.Users.Find(id);
                user.IsTemporary = false;
                context.SaveChanges();
            }
            return true;
        }

        public static string GetLogin(int id)
        {
            using (var context = new eQueueContext())
            {
                User user = context.Users.Find(id);
                return user?.Login;
            }
        }

        public static void UpdateLogin(int id, string newLogin)
        {
            using (var context = new eQueueContext())
            {
                User user = context.Users.Find(id);
                user.Login = newLogin;
                context.SaveChanges();
            }
        }

        public static void UpdatePassword(int id, string newPasswordHash)
        {
            using (var context = new eQueueContext())
            {
                User user = context.Users.Find(id);
                user.PasswordHash = newPasswordHash;
                context.SaveChanges();
            }
        }

        public static void UpdateActivity(int id)
        {
            using (var context = new eQueueContext())
            {
                User user = context.Users.Find(id);
                user.LastActivity = DateTime.Now;
                context.SaveChanges();
            }
        }

        public static bool Delete(int id)
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
