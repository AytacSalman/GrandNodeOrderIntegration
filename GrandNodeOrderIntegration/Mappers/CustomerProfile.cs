using AutoMapper;
using GrandNodeOrderIntegration.Models;
using GrandNodeOrderIntegration.Models.GrandNode;

namespace GrandNodeOrderIntegration.Mappers
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            var customerGroups = new List<string>() { "686704798b24f4aa1752a6e1", "686704798b24f4aa1752a6e0" };
            CreateMap<ContentItem, CustomerModel>()
                .ForMember(dest =>
                    dest.Email,
                    opt => opt.MapFrom(src => src.CustomerEmail))
                .ForMember(dest =>
                    dest.Username,
                    opt => opt.MapFrom(src => src.CustomerEmail))
                .ForMember(dest =>
                    dest.IsTaxExempt,
                    opt => opt.MapFrom(src => false))
                .ForMember(dest =>
                    dest.FreeShipping,
                    opt => opt.MapFrom(src => false))
                .ForMember(dest =>
                    dest.Active,
                    opt => opt.MapFrom(src => true))
                .ForMember(dest =>
                    dest.Deleted,
                    opt => opt.MapFrom(src => false))
                .ForMember(dest =>
                    dest.FirstName,
                    opt => opt.MapFrom(src => src.CustomerFirstName))
                .ForMember(dest =>
                    dest.LastName,
                    opt => opt.MapFrom(src => src.CustomerLastName))
                .ForMember(dest =>
                    dest.StoreId,
                    opt => opt.MapFrom(src => "686704798b24f4aa1752a577"))
                .ForMember(dest =>
                    dest.Groups,
                    opt => opt.MapFrom(src => customerGroups));
        }

    }
}
