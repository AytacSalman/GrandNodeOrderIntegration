using AutoMapper;
using GrandNodeOrderIntegration.Models;
using GrandNodeOrderIntegration.Models.GrandNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandNodeOrderIntegration.Mappers
{
    public class InvoiceAddressProfile : Profile
    {
        public InvoiceAddressProfile()
        {
            CreateMap<ContentItem, InvoiceAddressModel>()
                .ForMember(dest =>
                    dest.Email,
                    opt => opt.MapFrom(src => src.CustomerEmail))
                .ForMember(dest =>
                    dest.FirstName,
                    opt => opt.MapFrom(src => src.InvoiceAddress.FirstName))
                .ForMember(dest =>
                    dest.LastName,
                    opt => opt.MapFrom(src => src.InvoiceAddress.LastName))
                .ForMember(dest =>
                    dest.Company,
                    opt => opt.MapFrom(src => src.InvoiceAddress.Company))
                .ForMember(dest =>
                    dest.CountryId,
                    opt => opt.MapFrom(src => "686704798b24f4aa1752a591"))
                .ForMember(dest =>
                    dest.StateProvinceId,
                    opt => opt.MapFrom(src => "686704798b24f4aa1752a594"))
                .ForMember(dest =>
                    dest.City,
                    opt => opt.MapFrom(src => src.InvoiceAddress.City))
                .ForMember(dest =>
                    dest.Address1,
                    opt => opt.MapFrom(src => src.InvoiceAddress.Address1))
                .ForMember(dest =>
                    dest.Address2,
                    opt => opt.MapFrom(src => src.InvoiceAddress.Address2))
                .ForMember(dest =>
                    dest.ZipPostalCode,
                    opt => opt.MapFrom(src => src.InvoiceAddress.PostalCode))
                .ForMember(dest =>
                    dest.PhoneNumber,
                    opt => opt.MapFrom(src => "5545072277"))
                .ForMember(dest =>
                    dest.Note,
                    opt => opt.MapFrom(src => "note"))
                .ForMember(dest =>
                    dest.AddressType,
                    opt => opt.MapFrom(src => 1))
                .ForMember(dest =>
                    dest.CreatedOnUtc,
                    opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
