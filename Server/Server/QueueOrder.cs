using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eQueue
{
    public class QueueOrder
    {
        [Key]
        public int Id { get; set; }

        public ICollection<QueueInfo> Queues { get; set; }
        public ICollection<User> Users { get; set; }

        public int Number { get; set; }   
        
        public QueueOrder()
        {
            Queues = new List<QueueInfo>();
            Users = new List<User>();
        }
    }
}