using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto;
public class BasketItem
{
    public int Id { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public int CatalogItemId { get; set; }
    public int BasketId { get; set; }
}

public class Basket
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
}

public class Catalog
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureUri { get; set; }
    public int CatalogTypeId { get; set; }
    public int CatalogBrandId { get; set; }
}

public class CatalogBrand
{
    public int Id { get; set; }
    public string Brand { get; set; }
}

public class CatalogType
{
    public int Id { get; set; }
    public string Type { get; set; }
}

public class OrderItem
{
    public int Id { get; set; }
    public int ItemOrdered_CatalogItemId { get; set; }
    public string ItemOrdered_ProductName { get; set; }
    public string ItemOrdered_PictureUri { get; set; }
    public decimal UnitPrice { get; set; }
    public int Units { get; set; }
    public int OrderId { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public string ShipToAddress_Street { get; set; }
    public string ShipToAddress_City { get; set; }
    public string ShipToAddress_State { get; set; }
    public string ShipToAddress_Country { get; set; }
    public string ShipToAddress_ZipCode { get; set; }
}

public class TestTable
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

