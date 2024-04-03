using AutoMapper;
using Common.Dto;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MapProfile : Profile
    {

        public MapProfile()
        {
            CreateMap<DesignedImage, DesignedImageDto>().ReverseMap();
            CreateMap<DesignTemplate, DesignTemplateDto>().ReverseMap();
            //CreateMap<ImageDto, Image>().ForMember(dest => dest.ImagePath,
            //    act => act.MapFrom(src => src.ImagePath)).ReverseMap();
            CreateMap<Image, ImageDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<Task<DesignedImage>, Task<DesignedImageDto>>().ReverseMap();
            CreateMap<Task<DesignTemplate>, Task<DesignTemplateDto>>().ReverseMap();
            CreateMap<Task<Image>, Task<ImageDto>>().ReverseMap();
            CreateMap<Task<User>, Task<UserDto>>().ReverseMap();

            CreateMap<Task<List<DesignedImage>>, Task<List<DesignedImageDto>>>().ReverseMap();
            CreateMap<Task<List<DesignTemplate>>, Task<List<DesignTemplateDto>>>().ReverseMap();
            CreateMap<Task<List<Image>>, Task<List<ImageDto>>>().ReverseMap();
            CreateMap<Task<List<User>>, Task<List<UserDto>>>().ReverseMap();


        }
    }
}
