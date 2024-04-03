using System.ComponentModel.DataAnnotations.Schema;
namespace Repository.Entity
{
    public class Image
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string ImagePath { get; set; }
    }
}
