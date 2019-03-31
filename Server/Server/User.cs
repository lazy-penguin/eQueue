using System;
using System.ComponentModel.DataAnnotations;

namespace eQueue
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(32)]
        public string Login { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public DateTime LastActivity { get; set; }
        [Required]
        public bool IsTemporary { get; set; }

        public int? QueueOrderId;
        public QueueOrder QueueOrder;

        public int? UserAccessId;
        public UserAccess UserAccess;

        public override string ToString()
        {
            return Login;
        }
    }
}
