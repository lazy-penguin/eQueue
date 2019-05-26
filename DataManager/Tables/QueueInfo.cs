using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataManagers
{
    public class QueueInfo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
        [Required]
        public string Link { get; set; }
        public DateTime? Timer { get; set; }
        public ICollection<QueueOrder> QueueOrders { get; set; }
        public ICollection<UserAccess> UserAccesses { get; set; }
        public QueueInfo()
        {
            QueueOrders = new List<QueueOrder>();
            UserAccesses = new List<UserAccess>();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
