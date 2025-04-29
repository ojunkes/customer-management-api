using AutoMapper;
using Customers.Management.Consumer.Responses;
using Customers.Management.Domain.Entities;

namespace Customers.Management.Consumer.AutoMappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<EnderecoResponse, Address>()
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Cep!.Replace("-", "")))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Logradouro))
            .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.Complemento))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unidade))
            .ForMember(dest => dest.Neighborhood, opt => opt.MapFrom(src => src.Bairro))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Localidade))
            .ForMember(dest => dest.StateInitials, opt => opt.MapFrom(src => src.Uf))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Estado))
            .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Regiao))
            .ForMember(dest => dest.IbgeCode, opt => opt.MapFrom(src => src.Ibge))
            .ForMember(dest => dest.Gia, opt => opt.MapFrom(src => src.Gia))
            .ForMember(dest => dest.AreaCode, opt => opt.MapFrom(src => src.Ddd))
            .ForMember(dest => dest.SiafiCode, opt => opt.MapFrom(src => src.Siafi));

    }
}
