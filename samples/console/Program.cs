using EntityFrameworkCore.Seeder;
using EntityFrameworkCore.Seeder.Base;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Sample.Console.Data;
using Sample.Console.Seeders;


var seederEngine = new SeederEngine(new NullLoggerFactory().CreateLogger<SeederEngine>());
using var dbContext = new AppDbContext();
ISeeder[] seeders = [
    new TodoSeeder(dbContext)
];

await seederEngine.RunAsync(seeders);
