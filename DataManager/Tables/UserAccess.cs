using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManagers
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

        [Required]

        public int UserId { get; set; }
        public User User { get; set; }

        [Required]

        public int QueueInfoId { get; set; }
        public QueueInfo QueueInfo { get; set; }

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
