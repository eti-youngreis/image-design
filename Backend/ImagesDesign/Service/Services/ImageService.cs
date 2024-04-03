using AutoMapper;
using Common.Dto;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Repository.Entity;
using Repository.Interfaces;
using Repository.Repository;
using Service.Interfaces;

namespace Service.Services
{
    public class ImageService : IService<ImageDto>
    {
        private readonly IRepository<Image> repository;
        private readonly IMapper mapper;

        public ImageService(IRepository<Image> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ImageDto?> AddAsync(ImageDto entity)
        {
            if (entity.Image == null || entity.Image.FileName == null || entity.Image.FileName.Length == 0)
            {
                return null;
            }
            var path = await UploadImageAsync(entity.Image);
            entity.ImagePath = path;
            return mapper.Map<ImageDto>(await repository.AddAsync(mapper.Map<Image>(entity)));
        }

        public async Task<ImageDto?> DeleteAsync(int id)
        {
            var image = await repository.GetByIdAsync(id);
            if (image == null)
                return null;
            var filePath = image.ImagePath;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return mapper.Map<ImageDto>(await repository.DeleteByIdAsync(id));
        }

        public async Task<List<ImageDto>> GetAllAsync()
        {
            return await mapper.Map<Task<List<ImageDto>>>(repository.GetAllAsync());
        }

        public async Task<ImageDto?> GetByIdAsync(int id)
        {
            return mapper.Map<ImageDto?>(await repository.GetByIdAsync(id));
        }

        public async Task<ImageDto?> UpdateAsync(int id, ImageDto entity)
        {
            await repository.UpdateAsync(id, mapper.Map<Image>(entity));
            return entity;
        }

        private static async Task<string> UploadImageAsync(IFormFile image)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Images/", image.FileName);
            using (FileStream stream = new(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
                stream.Close();
            }
            return path;
        }
    }
}
