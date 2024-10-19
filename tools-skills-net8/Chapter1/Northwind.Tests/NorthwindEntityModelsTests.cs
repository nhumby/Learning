using Northwind.DataContext;
using Northwind.EntityModels;

namespace Northwind.Tests;

public class NorthwindEntityModelsTests
{
    [Fact]
    public void CanConnectIsTrue()
    {
        // Arrange
        using (NorthwindContext db = new())
        {
            // Act
            bool canConnect = db.Database.CanConnect();

            // Assert
            Assert.True(canConnect);
        }
    }

    [Fact]
    public void ProviderIsSqlServer()
    {
        // Arrange
        using (NorthwindContext db = new())
        {
            // Act
            string? provider = db.Database.ProviderName;

            // Assert
            Assert.Equal("Microsoft.EntityFrameworkCore.SqlServer", provider);
        }
    }

    [Fact]
    public void ProductId1IsChai()
    {
        // Arrange
        using (NorthwindContext db = new())
        {
            // Act
            Product? product1 = db?.Products?.Single(p => p.ProductId == 1);

            // Assert
            Assert.Equal("Chai", product1?.ProductName);
        }
    }
}