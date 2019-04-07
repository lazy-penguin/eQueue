using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server
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

        public ICollection<QueueOrder> QueueOrders { get; set; }
        public ICollection<UserAccess> UserAccesses { get; set; }

        public User()
        {
            QueueOrders = new List<QueueOrder>();
            UserAccesses = new List<UserAccess>();
        }

        public override string ToString()
        {
            return Login;
        }
    }
}
