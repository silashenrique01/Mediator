using AutoMapper;
using Intuitive.API.Dtos;
using Intuitive.Domain.Identity;

namespace Intuitive.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserApplication, UserApplicationDto>().ReverseMap();
            CreateMap<UserApplication, UserLoginDto>().ReverseMap();
        }
    }
}
