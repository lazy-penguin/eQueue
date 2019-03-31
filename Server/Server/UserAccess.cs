using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eQueue
{
    public enum AccessType
    {
        Owner,
        Privilaged,
        Default
    }

    public class UserAccess
    {
        [Key]
        public int Id { get; set; }

        public ICollection<QueueInfo> Queues { get; set; }
        public ICollection<User> Users { get; set; }

        [Required]
        [MaxLength(32)]
        public string Nickname { get; set; }

        [Required]
        public AccessType AccessTypeName { get; set; }

        public UserAccess()
        {
            Queues = new List<QueueInfo>();
            Users = new List<User>();
        }

        public override string ToString()
        {
            return Nickname;
        }
    }
}
