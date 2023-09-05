using AutoMapper;
using UploadImageApi.Models.Domain;
using UploadImageApi.Models.DTOs;

namespace UploadImageApi.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Image, ImageDto>().ReverseMap();
           


        }
    }
}
