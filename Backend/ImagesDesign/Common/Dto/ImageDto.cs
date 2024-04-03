using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Common.Dto
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public IFormFile? Image { get; set; }
        public int UserId { get; set; }
    }
}
