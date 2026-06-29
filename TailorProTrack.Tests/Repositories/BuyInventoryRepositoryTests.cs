using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Tests.Repositories;

public class BuyInventoryRepositoryTests
{
    [Fact]
    public void MarkBuysUsed_SetsOnlyNullUsedRowsToTrue()
    {
        using var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<TailorProTrackContext>()
            .UseSqlite(connection)
            .Options;

        using (var setupContext = new TailorProTrackContext(options))
        {
            setupContext.Database.EnsureCreated();
            setupContext.BUY_INVENTORY.AddRange(
                CreateBuyInventory(null),
                CreateBuyInventory(false),
                CreateBuyInventory(true));
            setupContext.SaveChanges();
        }

        using (var actContext = new TailorProTrackContext(options))
        {
            var repository = new BuyInventoryRepository(actContext, null!, null!);

            repository.MarkBuysUsed();
        }

        using var assertContext = new TailorProTrackContext(options);
        var usedValues = assertContext.BUY_INVENTORY
            .OrderBy(buyInventory => buyInventory.ID)
            .Select(buyInventory => buyInventory.USED)
            .ToList();

        Assert.Equal(new bool?[] { true, false, true }, usedValues);
    }

    private static BuyInventory CreateBuyInventory(bool? used)
    {
        return new BuyInventory
        {
            COMPANY = "Test company",
            RNC = "000000000",
            DATE_MADE = DateTime.UtcNow,
            TOTAL_SALE = 1m,
            USED = used,
            USER_CREATED = 1,
            CREATED_AT = DateTime.UtcNow
        };
    }
}
