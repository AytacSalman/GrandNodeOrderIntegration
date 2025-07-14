using AutoMapper;
using GrandNodeOrderIntegration.Models;
using GrandNodeOrderIntegration.Models.GrandNode;

namespace GrandNodeOrderIntegration.Mappers
{
    public class ShipmentAddressProfile : Profile
    {
        public ShipmentAddressProfile()
        {
            CreateMap<ContentItem, ShipmentAddressModel>()
                .ForMember(dest =>
                    dest.Email,
                    opt => opt.MapFrom(src => src.CustomerEmail))
                .ForMember(dest =>
                    dest.FirstName,
                    opt => opt.MapFrom(src => src.ShipmentAddress.FirstName))
                .ForMember(dest =>
                    dest.LastName,
                    opt => opt.MapFrom(src => src.ShipmentAddress.LastName))
                .ForMember(dest =>
                    dest.Company,
                    opt => opt.MapFrom(src => src.ShipmentAddress.Company))
                .ForMember(dest =>
                    dest.CountryId,
                    opt => opt.MapFrom(src => "686704798b24f4aa1752a591"))
                .ForMember(dest =>
                    dest.StateProvinceId,
                    opt => opt.MapFrom(src => "686704798b24f4aa1752a594"))
                .ForMember(dest =>
                    dest.City,
                    opt => opt.MapFrom(src => src.ShipmentAddress.City))
                .ForMember(dest =>
                    dest.Address1,
                    opt => opt.MapFrom(src => src.ShipmentAddress.Address1))
                .ForMember(dest =>
                    dest.Address2,
                    opt => opt.MapFrom(src => src.ShipmentAddress.Address2))
                .ForMember(dest =>
                    dest.ZipPostalCode,
                    opt => opt.MapFrom(src => src.ShipmentAddress.PostalCode))
                .ForMember(dest =>
                    dest.PhoneNumber,
                    opt => opt.MapFrom(src => src.ShipmentAddress.Phone))
                .ForMember(dest =>
                    dest.Note,
                    opt => opt.MapFrom(src => "note"))
                .ForMember(dest =>
                    dest.AddressType,
                    opt => opt.MapFrom(src => 0))
                .ForMember(dest =>
                    dest.CreatedOnUtc,
                    opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
