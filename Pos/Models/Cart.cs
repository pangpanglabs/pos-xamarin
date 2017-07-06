using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.Models
{
   public class Cart
    {
        public string Id { get; set; }
        public int TenantId { get; set; }
        public decimal ListPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal RemainAmount { get; set; }
        public Customer CustomerInfo { get; set; }
        public MileageUseInfo Mileage { get; set; }
        public CouponInfo CouponInfo { get; set; }
        public Info Info { get; set; }
        public IEnumerable<PaymentInfo> Payments { get; set; }
        public IEnumerable<CartItem> Items { get; set; }
        public IEnumerable<Suggest> Suggests { get; set; }
        public int UserId { get; set; }
        public int SpotId { get; set; }
        public int SalesmanId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class Suggest
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public decimal ListPrice { get; set; }
        public decimal SalePrice { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string ChannelName { get; set; }
        public IEnumerable<VirtualContent> VirtualContents { get; set; }
    }

    public class VirtualContent
    {
        public string Code { get; set; }
        public decimal ListPrice { get; set; }
        public decimal SalePrice { get; set; }
    }

    public class CartItem
    {
        public string Id { get; set; }
        public SkuInfo Sku { get; set; }
        public Offer Offer { get; set; }
        public int Quantity { get; set; }
        public decimal ListPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Discount { get; set; }
    }

    public class Offer
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public int ChannelId { get; set; }
        public string ChannelName { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public bool FitAllContents { get; set; }
        public Requirement Requirement { get; set; }
    }

    public class Requirement
    {
    }

    public class SkuInfo
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public IEnumerable<Option> Options { get; set; }
        public string Brandcode { get; set; }
        public string ContentCode { get; set; }
        public decimal ListPrice { get; set; }
        public IEnumerable<Offer> Offers { get; set; }

    }

    public class Option
    {
        public string K { get; set; }
        public string V { get; set; }
    }

    public class PaymentInfo
    {
        public string Method { get; set; }
        public int Amount { get; set; }
    }

    public class Info
    {
        public string Receipt { get; set; }
    }

    public class CouponInfo
    {
        public string No { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public DateTime StaartAt { get; set; }
        public DateTime EndAt { get; set; }
        public bool Enable { get; set; }
        public string CouponType { get; set; }
        public string EventId { get; set; }
        public string OfferName { get; set; }
    }

    public class MileageUseInfo
    {
        public int Current { get; set; }
        public int Available { get; set; }
        public int Use { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string No { get; set; }
        public string BrandCode { get; set; }
        public string Mobile { get; set; }
        public int Grade { get; set; }
        public string CardType { get; set; }
        public MileageInfo Mileage { get; set; }
        public Benefit Benefit { get; set; }
        public Profile Profile { get; set; }
    }

    public class Profile
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public string Email { get; set; }
        public bool Married { get; set; }
        public string Address { get; set; }
        public string AddressDetail { get; set; }
    }

    public class Benefit
    {
        public bool Can_redeem_mileage { get; set; }
        public int Mileage { get; set; }
        public int Max_Mileage_Percent { get; set; }
        public int Discount_Percent { get; set; }
        public int min_redeem_points { get; set; }
        public int Birthday_Discount_Percent { get; set; }
        public int Max_Birthday_Price { get; set; }

    }

    public class MileageInfo
    {
        public int CurrentPoints { get; set; }
        public int TotalEarnPoints { get; set; }
        public int TotalRedeemPoints { get; set; }
        public decimal TotalSaleAmount { get; set; }
    }
}
