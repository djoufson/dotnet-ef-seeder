using Microsoft.EntityFrameworkCore;

using Sample.Console.Models;

namespace Sample.Console.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Todo> Todos { get; set; }
}
