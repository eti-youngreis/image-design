using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImagesDesign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IService<ImageDto> service;

        public ImageController(IService<ImageDto> service)
        {
            this.service = service;
        }

        //GET: api/<ImageController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var images = await service.GetAllAsync();
            var displayImages = new List<FileContentResult>();
            images.ForEach(i =>
            {
                var image = GetImage(i);
                if (image is not NotFoundResult )
                    displayImages.Add((FileContentResult)image);
            });
            return Ok(displayImages);
        }

        // GET api/<ImageController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult?> Get(int id)
        {
            var image = await service.GetByIdAsync(id);
            return GetImage(image);
        }

        private IActionResult GetImage(ImageDto? image)
        {
            if (image == null)
                return NotFound();
            var filePath = image.ImagePath;
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var fileExtension = Path.GetExtension(filePath).ToLowerInvariant();
            var contentType = "image/" + fileExtension[1..];
            return File(fileBytes, contentType);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ImageDto data)
        {
            if (data == null || data.Image == null || data.Image.Length == 0)
                return BadRequest("Invalid file");
            if (!data.Image.ContentType.StartsWith("image/"))
                return BadRequest("Uploaded file is not an image");
            var image = await service.AddAsync(data);
            return Ok(image);
        }

        // PUT api/<ImageController>/5
        //[HttpPut("{id}")]
        //public async Task<ImageDto?> Put(int id, ImageDto entity)
        //{
        //    return await service.UpdateAsync(id, entity);
        //}

        // DELETE api/<ImageController>/5
        [HttpDelete("{id}")]
        public async Task<ImageDto?> Delete(int id)
        {
            return await service.DeleteAsync(id);
        }

    }
}
