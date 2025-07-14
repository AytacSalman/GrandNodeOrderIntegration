using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GrandNodeOrderIntegration.Models
{
    public class TrendyolOrderModel
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalPages { get; set; }
        public int TotalElements { get; set; }
        public List<ContentItem> Content { get; set; }
    }

    public class ContentItem
    {
        public ShipmentAddress ShipmentAddress { get; set; }
        public string OrderNumber { get; set; }
        public double GrossAmount { get; set; }
        public double TotalDiscount { get; set; }
        public double TotalTyDiscount { get; set; }
        public string TaxNumber { get; set; }
        public InvoiceAddress InvoiceAddress { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerEmail { get; set; }
        public long CustomerId { get; set; }
        public string CustomerLastName { get; set; }
        public long Id { get; set; }
        public string CargoTrackingNumber { get; set; }
        public string CargoTrackingLink { get; set; }
        public string CargoSenderNumber { get; set; }
        public string CargoProviderName { get; set; }
        public List<Line> Lines { get; set; }
        public string OrderDate { get; set; }
        public string TcIdentityNumber { get; set; }
        public string IdentityNumber { get; set; }
        public string CurrencyCode { get; set; }
        public List<PackageHistory> PackageHistories { get; set; }
        public string ShipmentPackageStatus { get; set; }
        public string Status { get; set; }
        public string DeliveryType { get; set; }
        public int TimeSlotId { get; set; }
        public string ScheduledDeliveryStoreId { get; set; }
        public string EstimatedDeliveryStartDate { get; set; }
        public string EstimatedDeliveryEndDate { get; set; }
        public double TotalPrice { get; set; }
        public string DeliveryAddressType { get; set; }
        public string AgreedDeliveryDate { get; set; }
        public bool AgreedDeliveryDateExtendible { get; set; }
        public string ExtendedAgreedDeliveryDate { get; set; }
        public string AgreedDeliveryExtensionStartDate { get; set; }
        public string AgreedDeliveryExtensionEndDate { get; set; }
        public string InvoiceLink { get; set; }
        public bool FastDelivery { get; set; }
        public string FastDeliveryType { get; set; }
        public string OriginShipmentDate { get; set; }
        public string LastModifiedDate { get; set; }
        public bool Commercial { get; set; }
        public bool DeliveredByService { get; set; }
        public bool Micro { get; set; }
        public bool GiftBoxRequested { get; set; }
        public string EtgbNo { get; set; }
        public string EtgbDate { get; set; }
        public bool PByTrendyol { get; set; }
        public bool ContainsDangerousProduct { get; set; }
    }

    public class Line
    {
        public int Quantity { get; set; }
        public int SalesCampaignId { get; set; }
        public string ProductSize { get; set; }
        public string MerchantSku { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductOrigin { get; set; }
        public int MerchantId { get; set; }
        public double Amount { get; set; }
        public double Discount { get; set; }
        public double TyDiscount { get; set; }
        public List<DiscountDetail> DiscountDetails { get; set; }
        public List<FastDeliveryOption> FastDeliveryOptions { get; set; }
        public string CurrencyCode { get; set; }
        public string ProductColor { get; set; }
        public long Id { get; set; }
        public string Sku { get; set; }
        public double VatBaseAmount { get; set; }
        public string Barcode { get; set; }
        public string OrderLineItemStatusName { get; set; }
        public double Price { get; set; }
        public int ProductCategoryId { get; set; }
        public double LaborCost { get; set; }
    }

    public class ShipmentAddress
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public AddressLines AddressLines { get; set; }
        public string City { get; set; }
        public int CityCode { get; set; }
        public string District { get; set; }
        public int DistrictId { get; set; }
        public int CountyId { get; set; }
        public string CountyName { get; set; }
        public string ShortAddress { get; set; }
        public string StateName { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public int NeighborhoodId { get; set; }
        public string Neighborhood { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public string FullAddress { get; set; }
    }

    public class InvoiceAddress : ShipmentAddress
    {
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }
    }

    public class AddressLines
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
    }

    public class DiscountDetail
    {
        public double LineItemPrice { get; set; }
        public double LineItemDiscount { get; set; }
        public double LineItemTyDiscount { get; set; }
    }

    public class FastDeliveryOption
    {
        public string Type { get; set; }
    }

    public class PackageHistory
    {
        public string CreatedDate { get; set; }
        public string Status { get; set; }
    }
}
