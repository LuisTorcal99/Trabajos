
using RestAPI.Models.DTOs.UserDto;
using AutoMapper;
using RestAPI.Models.Entity;
using RestAPI.Models.DTOs.VolantesDto;
using RestAPI.Models.DTOs.HouseUserDTO;
using RestAPI.Models.DTOs.HouseDTO;
using RestAPI.Models.DTOs.SubastasDTO;
using RestAPI.Models.DTOs.PujasDTO;


namespace RestAPI.AutoMapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<VolantesEntity, VolantesDto>().ReverseMap();
            CreateMap<VolantesEntity, CreateVolantesDto>().ReverseMap();
            CreateMap<HouseEntity, HouseDTO>().ReverseMap();
            CreateMap<HouseEntity, CreateHouseDto>().ReverseMap();
            CreateMap<HouseUserEntity, HouseUserDTO>().ReverseMap();
            CreateMap<HouseUserEntity, CreateHouseUserDto>().ReverseMap();
            CreateMap<SubastasEntity, SubastasDto>().ReverseMap();
            CreateMap<SubastasEntity, CrearSubastaDto>().ReverseMap();
            CreateMap<PujaEntity, PujaDto>().ReverseMap();
            CreateMap<PujaEntity, CrearPujaDto>().ReverseMap();
        }
    }
}
