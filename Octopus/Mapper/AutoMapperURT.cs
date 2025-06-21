using AutoMapper;
using Models.Dto.Banco;
using Models.Entities.Domain.DBOctopus.OctopusEntities;

namespace Octopus.Mapper
{
    public class AutoMapperURT : Profile
    {
        public AutoMapperURT()
        {
            CreateMap<Banco, BancoDto>()
            .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => src.Activo ?? false));
            CreateMap<TipoCuentaBancarium, DetalleTipoCuenta>();
            //CreateMap<Sesione, SesionDto>();
            //CreateMap<SesionDto, Sesione>();
            //CreateMap<Usuario, UsuarioDto>();
            //CreateMap<CreateUserDto, Usuario>();
            //CreateMap<CreateProfileUserDto, Usuario>();

        }
    }
}
