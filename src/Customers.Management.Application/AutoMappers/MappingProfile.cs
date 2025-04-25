using AutoMapper;
using Customers.Management.Application.Commons;
using Customers.Management.Application.Requests;
using Customers.Management.Application.Responses;
using Customers.Management.Domain.Entities;

namespace Customers.Management.Application.AutoMappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerResponse>()
            .ForMember(dest => dest.SignupChannel, opt => opt.MapFrom(src => src.SignupChannel.GetDescription()));
        CreateMap<CustomerRequest, Customer>()
            .ForMember(dest => dest.DateOfBirth, opt =>
            {
                opt.PreCondition(src => src.DateOfBirth.HasValue);
                opt.MapFrom(src => src.DateOfBirth!.Value);
            })
            .ForMember(dest => dest.SignupChannel, opt =>
            {
                opt.PreCondition(src => src.SignupChannel.HasValue);
                opt.MapFrom(src => src.SignupChannel!.Value);
            })
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
