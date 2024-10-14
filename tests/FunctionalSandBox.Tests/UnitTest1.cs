using FluentAssertions;
using FunctionalSandBox.Common;


namespace FunctionalSandBox.Tests;

public class UnitTest1
{
    private static Maybe<Order> GetMostRecentOrder(string name)
    {
        var mostRecentOrder = SqlServerDatabase.GetByName(name)
        .Bind(x => SqlServerDatabase.GetOrderHistory(x.Id))
        .Bind(x => x.Last());

        return mostRecentOrder;
    }

    [Fact]
    public void Test1()
    {
        var order = GetMostRecentOrder("John Pion");

        var item = order switch
        {
            Something<Order> s => s.Value.Item,
            _ => "Error Message"
        };

        item.Should().Be("Order Item 45");
    }

    [Fact]
    public void Test2()
    {
        var order = GetMostRecentOrder("Hellen Pion");

        var item = order switch
        {
            Something<Order> s => s.Value.Item,
            _ => "Error Message"
        };

        order.Should().BeOfType<Nothing<Order>>();
    }
}