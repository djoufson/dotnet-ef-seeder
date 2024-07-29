using Microsoft.EntityFrameworkCore;

using Sample.Console.Models;

namespace Sample.Console.Data;
public class AppDbContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=./database/app.db");
        base.OnConfiguring(optionsBuilder);
    }
}
