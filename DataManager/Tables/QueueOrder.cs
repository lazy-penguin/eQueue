using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManagers
{
    public class QueueOrder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int QueueInfoId { get; set; }
        public QueueInfo QueueInfo { get; set; }

        public int Number { get; set; }   
      
    }
}