using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataManagers
{
    public class UserToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }


    }
}
