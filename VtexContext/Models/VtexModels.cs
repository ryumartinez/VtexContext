namespace VtexContext.Models;

public class Product
{
    public string productId { get; set; }
    public string productName { get; set; }
    public string brand { get; set; }
    public int brandId { get; set; }
    public string brandImageUrl { get; set; }
    public string linkText { get; set; }
    public string productReference { get; set; }
    public string productReferenceCode { get; set; }
    public string categoryId { get; set; }
    public string productTitle { get; set; }
    public string metaTagDescription { get; set; }
    public DateTime releaseDate { get; set; }
    public Dictionary<string, string> clusterHighlights { get; set; }
    public Dictionary<string, string> productClusters { get; set; }
    public Dictionary<string, string> searchableClusters { get; set; }
    public List<string> categories { get; set; }
    public List<string> categoriesIds { get; set; }
    public string link { get; set; }
    public List<string> Percentuals { get; set; }
    public List<string> Percentual { get; set; }
    public List<string> Total { get; set; }
    public List<string> TesteDeApi { get; set; }
    public List<string> Ale { get; set; }
    public List<string> TesteDaApi2 { get; set; }
    public List<string> Alcool { get; set; }
    public List<string> allSpecifications { get; set; }
    public List<string> allSpecificationsGroups { get; set; }
    public string description { get; set; }
    public List<Item> items { get; set; }
}

public class Item
{
    public string itemId { get; set; }
    public string name { get; set; }
    public string nameComplete { get; set; }
    public string complementName { get; set; }
    public string ean { get; set; }
    public List<ReferenceId> referenceId { get; set; }
    public string measurementUnit { get; set; }
    public decimal unitMultiplier { get; set; }
    public string modalType { get; set; }
    public bool isKit { get; set; }
    public List<KitItem> kitItems { get; set; }
    public List<Image> images { get; set; }
    public List<Seller> sellers { get; set; }
    public List<object> Videos { get; set; }
    public object estimatedDateArrival { get; set; }
}

public class ReferenceId
{
    public string Key { get; set; }
    public string Value { get; set; }
}

public class KitItem
{
    public string itemId { get; set; }
    public int amount { get; set; }
}

public class Image
{
    public string imageId { get; set; }
    public string imageLabel { get; set; }
    public string imageTag { get; set; }
    public string imageUrl { get; set; }
    public string imageText { get; set; }
    public DateTime imageLastModified { get; set; }
}

public class Seller
{
    public string sellerId { get; set; }
    public string sellerName { get; set; }
    public string addToCartLink { get; set; }
    public bool sellerDefault { get; set; }
    public CommertialOffer commertialOffer { get; set; }
}

public class CommertialOffer
{
    public Dictionary<string, DeliverySlaSample> DeliverySlaSamplesPerRegion { get; set; }
    public List<Installment> Installments { get; set; }
    public List<object> DiscountHighLight { get; set; }
    public List<object> GiftSkuIds { get; set; }
    public List<Teaser> Teasers { get; set; }
    public List<PromotionTeaser> PromotionTeasers { get; set; }
    public List<object> BuyTogether { get; set; }
    public List<object> ItemMetadataAttachment { get; set; }
    public decimal Price { get; set; }
    public decimal ListPrice { get; set; }
    public decimal PriceWithoutDiscount { get; set; }
    public decimal RewardValue { get; set; }
    public DateTime PriceValidUntil { get; set; }
    public int AvailableQuantity { get; set; }
    public bool IsAvailable { get; set; }
    public decimal Tax { get; set; }
    public int SaleChannel { get; set; }
    public List<DeliverySlaSample> DeliverySlaSamples { get; set; }
    public string GetInfoErrorMessage { get; set; }
    public string CacheVersionUsedToCallCheckout { get; set; }
    public PaymentOptions PaymentOptions { get; set; }
}

public class DeliverySlaSample
{
    public List<object> DeliverySlaPerTypes { get; set; }
    public object Region { get; set; }
}

public class Installment
{
    public decimal Value { get; set; }
    public decimal InterestRate { get; set; }
    public decimal TotalValuePlusInterestRate { get; set; }
    public int NumberOfInstallments { get; set; }
    public string PaymentSystemName { get; set; }
    public string PaymentSystemGroupName { get; set; }
    public string Name { get; set; }
}

public class Teaser
{
    public string Name { get; set; }
    public Dictionary<string, string> GeneralValues { get; set; }
    public TeaserConditions Conditions { get; set; }
    public TeaserEffects Effects { get; set; }
}

public class TeaserConditions
{
    public int MinimumQuantity { get; set; }
    public List<TeaserParameter> Parameters { get; set; }
}

public class TeaserEffects
{
    public List<TeaserParameter> Parameters { get; set; }
}

public class TeaserParameter
{
    public string Name { get; set; }
    public string Value { get; set; }
}

public class PromotionTeaser
{
    public string Name { get; set; }
    public Dictionary<string, string> GeneralValues { get; set; }
    public TeaserConditions Conditions { get; set; }
    public TeaserEffects Effects { get; set; }
}

public class PaymentOptions
{
    public List<InstallmentOption> installmentOptions { get; set; }
    public List<PaymentSystem> paymentSystems { get; set; }
    public List<object> payments { get; set; }
    public List<object> giftCards { get; set; }
    public List<object> giftCardMessages { get; set; }
    public List<object> availableAccounts { get; set; }
    public List<object> availableTokens { get; set; }
}

public class InstallmentOption
{
    public string paymentSystem { get; set; }
    public string bin { get; set; }
    public string paymentName { get; set; }
    public string paymentGroupName { get; set; }
    public decimal value { get; set; }
    public List<InstallmentDetail> installments { get; set; }
}

public class InstallmentDetail
{
    public int count { get; set; }
    public bool hasInterestRate { get; set; }
    public decimal interestRate { get; set; }
    public decimal value { get; set; }
    public decimal total { get; set; }
    public List<SellerMerchantInstallment> sellerMerchantInstallments { get; set; }
}

public class SellerMerchantInstallment
{
    public string id { get; set; }
    public int count { get; set; }
    public bool hasInterestRate { get; set; }
    public decimal interestRate { get; set; }
    public decimal value { get; set; }
    public decimal total { get; set; }
}

public class PaymentSystem
{
    public int id { get; set; }
    public string name { get; set; }
    public string groupName { get; set; }
    public string validator { get; set; }
    public string stringId { get; set; }
    public string template { get; set; }
    public bool requiresDocument { get; set; }
    public bool isCustom { get; set; }
    public string description { get; set; }
    public bool requiresAuthentication { get; set; }
    public DateTime dueDate { get; set; }
    public object availablePayments { get; set; }
}