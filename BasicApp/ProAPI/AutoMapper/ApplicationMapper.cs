
using RestAPI.Models.DTOs.UserDto;
using AutoMapper;

using RestAPI.Models.Entity;
using RestAPI.Models.DTOs.VolantesDto;

namespace RestAPI.AutoMapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<VolantesEntity, VolantesDto>().ReverseMap();
            CreateMap<VolantesEntity, CreateVolantesDto>().ReverseMap();
        }
    }
}
