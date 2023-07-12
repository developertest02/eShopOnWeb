
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore;
using Microsoft.eShopWeb.Web.Features.MyOrders;
using Microsoft.eShopWeb.Web.Features.OrderDetails;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Authorize] // Controllers that mainly require Authorization still use Controller/View; other pages use Pages
[Route("[controller]/[action]")]
public class OrderController : Controller
{
    private readonly IDataMaster _dataMaster;

    public OrderController(IDataMaster dataMaster)
    {
        _dataMaster = dataMaster;
    }

    [HttpGet]
    public async Task<IActionResult> MyOrders()
    {
        if (User == null)
        {
            return BadRequest("User is null");
        }
        if (User.Identity == null)
        {
            return BadRequest("User.Identity is null");
        }
        if (User.Identity.Name == null)
        {
            return BadRequest("User.Identity.Name is null");
        }

        var orders = await _dataMaster.FetchOrdersAsync(orderId: null, buyerId: User?.Identity?.Name);
        List<OrderViewModel> viewModels = new List<OrderViewModel>();
        foreach(var order in orders)
        {
            var vm = new OrderViewModel();
            
            vm.OrderNumber = order.Id;
            vm.OrderDate = order.OrderDate.ToOffset(TimeSpan.FromMinutes(0));
            vm.ShippingAddress = order.ShipToAddress;
            vm.Total = order.Total();
            viewModels.Add(vm);
          
        }
  // var viewModel = await _mediator.Send(new GetMyOrders(User.Identity.Name));

        return View(viewModels);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> Detail(int orderId)
    {
        if (User?.Identity?.Name == null)
        {
            return BadRequest("User.Identity.Name is null");
        }
        //Guard.Against.Null(User?.Identity?.Name, nameof(User.Identity.Name));
       // var viewModel = await _mediator.Send(new GetOrderDetails(User.Identity.Name, orderId));
        var orders = await _dataMaster.FetchOrdersAsync(orderId);
        var order = orders.SingleOrDefault();
        if(order == null)
        {
            return BadRequest("No such order found for this user.");
        }
        var viewModel = new OrderDetailViewModel();
        viewModel.OrderNumber = order.Id;
        viewModel.OrderDate = order.OrderDate;
        viewModel.ShippingAddress = order.ShipToAddress;
        viewModel.OrderItems = new List<OrderItemViewModel>();
        foreach(var item in order.OrderItems)
        {
            var vm = new OrderItemViewModel();
            vm.PictureUrl = item.ItemOrdered.PictureUri;
            vm.UnitPrice = item.UnitPrice;
            vm.Units = item.Units;
            vm.ProductId = item.ItemOrdered.CatalogItemId;
            vm.ProductName = item.ItemOrdered.ProductName;
            viewModel.OrderItems.Add(vm);
        }
        viewModel.Total = viewModel.OrderItems.Sum(n => n.Units * n.UnitPrice);

        return View(viewModel);
    }
}
