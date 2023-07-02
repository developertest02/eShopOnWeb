using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Tests;
using Xunit;
using FluentAssertions;

public class BasketRepositoryTests
{


    [Fact]
    public void CreateBasket_Should_Return_New_BasketId()
    {
        // Arrange
        var repository = new BasketRepository(Constants.CONNECTION_STRING);
        var buyerId = "buyer1";

        // Act
        var basketId = repository.CreateBasket(buyerId);

        // Assert
        basketId.Should().BeGreaterThan(0);
    }
}
