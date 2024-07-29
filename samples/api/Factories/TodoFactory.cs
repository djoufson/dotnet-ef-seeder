using Bogus;
using EntityFrameworkCore.Seeder.Base;
using Sample.Console.Models;

namespace Sample.Console.Factories;
public class TodoFactory : Factory<Todo>
{
    protected override Faker<Todo> BuildRules()
    {
        return new Faker<Todo>()
            .RuleFor(t => t.Title, f => f.Lorem.Sentence())
            .RuleFor(t => t.IsCompleted, f => f.Random.Bool());
    }
}
