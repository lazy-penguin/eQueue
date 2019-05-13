using System;

namespace Server.Data
{
    public class QueueData
    {
        public int UserId;
        public string Name;
        public string UserNickname;
        public string Link;
        public DateTime Timer;

        public QueueData(int userId, string name, string userNickname, string link, DateTime timer)
        {
            UserId = userId;
            Name = name;
            UserNickname = userNickname;
            Link = link;
            Timer = timer;
        }
    }
}