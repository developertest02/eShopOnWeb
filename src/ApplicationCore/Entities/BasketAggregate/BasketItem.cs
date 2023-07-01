
namespace Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;

public class BasketItem : BaseEntity
{

    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public int CatalogItemId { get; private set; }
    public int BasketId { get; private set; }
    //System.Int32 Id, System.Decimal UnitPrice, System.Int32 Quantity, System.Int32 CatalogItemId, System.Int32 BasketId
    public BasketItem(int id, decimal unitPrice,  int quantity, int catalogItemId, int basketId)
    {
        Id = id;
        BasketId = basketId;
        CatalogItemId = catalogItemId;
        UnitPrice = unitPrice;
        SetQuantity(quantity);
    }
    public BasketItem(int catalogItemId, int quantity, decimal unitPrice)
    {
        CatalogItemId = catalogItemId;
        UnitPrice = unitPrice;
        SetQuantity(quantity);
    }

    public void AddQuantity(int quantity)
    {
        if (quantity < 0)
            throw new System.ArgumentOutOfRangeException();
        //Guard.Against.OutOfRange(quantity, nameof(quantity), 0, int.MaxValue);

        Quantity += quantity;
    }

    public void SetQuantity(int quantity)
    {
        if (quantity < 0)
            throw new System.ArgumentOutOfRangeException();
        //Guard.Against.OutOfRange(quantity, nameof(quantity), 0, int.MaxValue);

        Quantity = quantity;
    }
}
