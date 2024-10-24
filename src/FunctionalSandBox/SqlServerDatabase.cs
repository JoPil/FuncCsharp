using FunctionalSandBox.Common;

namespace FunctionalSandBox;
public class SqlServerDatabase
{
    public static Maybe<Person> GetByName(string name)
    {
        Maybe<Person> result = name switch
        {
            "John Pion" =>
            new Something<Person>(
                new Person
                {
                    FirstName = "John",
                    MiddleNames = ["Middle"],
                    LastName = "Pion"
                }),
            // "Chuck Norris" => throw new Exception("You cannot find Chunk nowhere"),
            _ => new Nothing<Person>()
        };

        return result;
    }
    public static IEnumerable<Order> GetOrderHistory(int id)
    {
        return [
            new Order{
                Id = id,
                Item = "Order Item 1",
                Quantity = 1,
                ItemId = 666
            },
            new Order{
                Id = id,
                Item = "Order Item 45",
                Quantity = 4,
                ItemId = 667
            }
        ];
    }
}