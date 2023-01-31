using AutoMapper;
using PlatformService.Models;
using PlatformService.Dtos;


namespace PlatformServices.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            // Source --> Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
       }
    }
}