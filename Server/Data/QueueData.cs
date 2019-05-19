using System;

namespace Server.Data
{
    public class QueueData
    {
        public int Id;
        public int OwnerId;
        public string Name;
        public string OwnerNickname;
        public string Link;
        public DateTime? Timer;

        public QueueData(int id, int ownerId, string name, string ownerNickname, string link, DateTime? timer)
        {
            Id = id;
            OwnerId = ownerId;
            Name = name;
            OwnerNickname = ownerNickname;
            Link = link;
            Timer = timer;
        }
    }
}