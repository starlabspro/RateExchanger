namespace BuildingBlocks.EFCore;

public interface IDataSeeder
{
    Task SeedDataAsync();
}