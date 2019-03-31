using System.ComponentModel.DataAnnotations;

namespace eQueue
{
    public class QueueInfo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
        [Required]
        public string Link { get; set; }
        public int Timer { get; set; }

        public int? QueueOrderId;
        public QueueOrder QueueOrder;

        public int? UserAccessId;
        public UserAccess UserAccess;

        public override string ToString()
        {
            return Name;
        }
    }
}
