using EntityFrameworkCore.Seeder.Extensions;
using Microsoft.EntityFrameworkCore;
using Sample.Console.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSeedersEngine();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=./database/app.db"));
builder.Services.AddSeedersFromAssembly(typeof(Program).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if(await app.MapSeedCommandsAsync(args))
{
    // Exit the application when the seed command is triggered and finishes execution
    return;
}

app.MapGet("/todos", async (AppDbContext ctx) =>
{
    return await ctx.Todos.ToArrayAsync();
});

app.Run();
