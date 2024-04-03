using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Dto
{
    public class DesignTemplateDto
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public virtual int UserId { get; set; }
    }
}
