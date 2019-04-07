using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server
{
    public class QueueOrder
    {
        [Key]
        public int Id { get; set; }

        public int UserId;
        [Required]
        public User User;

        public int QueueInfoId;
        [Required]
        public QueueInfo QueueInfo;

        public int Number { get; set; }   
      
    }
}