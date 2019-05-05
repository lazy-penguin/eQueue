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

        public int UserId { get; set; }
        [Required]
        public User User { get; set; }

        public int QueueInfoId { get; set; }
        [Required]
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
