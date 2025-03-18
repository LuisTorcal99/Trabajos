
using AutoMapper;
using ExamenFinalApi.Models.DTOs.ObjetoDosDto;
using ExamenFinalApi.Models.DTOs.ObjetoDto;
using ExamenFinalApi.Models.DTOs.ObjetoTresDto;
using ExamenFinalApi.Models.DTOs.UserDto;
using ExamenFinalApi.Models.Entity;


namespace ExamenFinalApi.AutoMapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<ObjetoEntity, ObjetoDTO>().ReverseMap();
            CreateMap<CreateObjetoDTO, ObjetoEntity>().ReverseMap();
            CreateMap<ObjetoDosEntity, ObjetoDosDTO>().ReverseMap();
            CreateMap<CreateObjetoDosDTO, ObjetoDosEntity>().ReverseMap();
            CreateMap<ObjetoTresEntity, ObjetoTresDTO>().ReverseMap();
            CreateMap<CreateObjetoTresDTO, ObjetoTresEntity>().ReverseMap();
        }
    }
}
