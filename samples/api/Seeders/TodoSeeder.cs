using EntityFrameworkCore.Seeder.Base;

using Sample.Api.Factories;
using Sample.Console.Data;

namespace Sample.Api.Seeders;
public class TodoSeeder(AppDbContext appDbContext) : ISeeder
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task SeedAsync()
    {
        var todos = new TodoFactory().Generate(10);
        await _appDbContext.AddRangeAsync(todos);
        await _appDbContext.SaveChangesAsync();
    }
}
