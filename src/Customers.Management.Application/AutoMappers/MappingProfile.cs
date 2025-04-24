using AutoMapper;
using Customers.Management.Application.Requests;
using Customers.Management.Application.Responses;
using Customers.Management.Application.Shared;
using Customers.Management.Domain.Entities;

namespace Customers.Management.Application.AutoMappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDescription()));
        CreateMap<CustomerRequest, Customer>()
            .ForMember(dest => dest.DateOfBirth, opt =>
            {
                opt.PreCondition(src => src.DateOfBirth.HasValue);
                opt.MapFrom(src => src.DateOfBirth!.Value);
            })
            .ForMember(dest => dest.Status, opt =>
            {
                opt.PreCondition(src => src.Status.HasValue);
                opt.MapFrom(src => src.Status!.Value);
            })
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
