using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server
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

        public int UserId;
        [Required]
        public User User;

        public int QueueInfoId;
        [Required]
        public QueueInfo QueueInfo;

        [Required]
        [MaxLength(32)]
        public string Nickname { get; set; }

        [Required]
        public AccessType AccessTypeName { get; set; }

        public override string ToString()
        {
            return Nickname;
        }
    }
}
