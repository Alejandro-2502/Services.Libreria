using AutoMapper;
using Libreria.Application.DTOs;
using Libreria.Application.Request;
using Libreria.Application.Responses;

namespace Libreria.Application.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<LibroDTO, LibroResponse>();
        CreateMap<LibroRequest, LibroDTO>();
        CreateMap<LibroIdRequest, LibroDTO>();
        CreateMap<UsuarioResponse, UserDTO>();
        CreateMap<UserDTO, UsuarioResponse>();
    }
}
