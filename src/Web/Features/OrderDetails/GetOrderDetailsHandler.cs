﻿using MediatR;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Features.OrderDetails;

public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetails, OrderDetailViewModel?>
{
    //private readonly IReadRepository<Order> _orderRepository;

    public GetOrderDetailsHandler()
    {
      
    }

    public async Task<OrderDetailViewModel?> Handle(GetOrderDetails request,
        CancellationToken cancellationToken)
    {
        return null;
        //var spec = new OrderWithItemsByIdSpec(request.OrderId);
        //var order = await _orderRepository.FirstOrDefaultAsync(spec, cancellationToken);

        //if (order == null)
        //{
        //    return null;
        //}

        //return new OrderDetailViewModel
        //{
        //    OrderDate = order.OrderDate,
        //    OrderItems = order.OrderItems.Select(oi => new OrderItemViewModel
        //    {
        //        PictureUrl = oi.ItemOrdered.PictureUri,
        //        ProductId = oi.ItemOrdered.CatalogItemId,
        //        ProductName = oi.ItemOrdered.ProductName,
        //        UnitPrice = oi.UnitPrice,
        //        Units = oi.Units
        //    }).ToList(),
        //    OrderNumber = order.Id,
        //    ShippingAddress = order.ShipToAddress,
        //    Total = order.Total()
        //};
    }
}
