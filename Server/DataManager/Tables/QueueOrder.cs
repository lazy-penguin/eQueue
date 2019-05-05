using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManagers
{
    public class QueueOrder
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        [Required]
        public User User { get; set; }

        public int QueueInfoId { get; set; }
        [Required]
        public QueueInfo QueueInfo { get; set; }

        public int Number { get; set; }   
      
    }
}