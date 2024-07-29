# Dotnet Ef Seeder

![Banner](./assets/banner.jpeg)

This package defines a pattern to register and execute seeders to populate a database with test data.
[Find the package here](https://www.nuget.org/packages/EntityFrameworkCore.Seeder/#readme-body-tab)

> Those coming from the Laravel world, the api is highly inspired by what is proposed in Laravel.

## Installation

The installation is pretty straight forward, enter the following command

``` sh
dotnet add package EntityFrameworkCore.Seeder
```

## Usage

The package is built to suit any .net application. It works by isolation the seeder of a specific entity. To generate fake data, we built a wrapper on to of `Bogus`. The rules are simple:

- Create a factory class for your entity
- Create a seeder for your entity
- Register the dependencies through ASP.NET Core DI Container
- Run the `dotnet run --seed` command

Let's walk through those steps.

### 1. Create a factory class

In the sample application, we created a `Todo` entity. We create a factory class by extending the base `Factory` which forces us to define a set of rules that will help generating fake data.

``` cs
public class TodoFactory : Factory<Todo>
{
    protected override Faker<Todo> BuildRules()
    {
        return new Faker<Todo>()
            .RuleFor(t => t.Title, f => f.Lorem.Sentence())
            .RuleFor(t => t.IsCompleted, f => f.Random.Bool());
    }
}
```

### 2. Create a seeder

A seeder is responsible of the database transaction that will add the relevant entities to the database. It supports dependency injection, so we can inject our dbContext to manipulate the database.

``` cs
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
```

### 3. Register the dependencies

The next step is to register the required dependencies in the DI Container.
In the service section, do the following:

``` cs
builder.Services.ConfigureSeedersEngine();
builder.Services.AddSeeder<TodoSeeder>();
```

Alternatively, we can register seeders from an assembly using the extension method `AddSeedersFromAssembly`, so that the framework automatically scans that assembly to register each seeder.

Next, we map the `--seed` and `-s` command to the pipeline as follows.

``` cs
bool appliedAny = await app.MapSeedCommandsAsync(args)
```

This method takes as parameter the `args` array, and returns wether or not a seeder was applied. We can opt to stop the application after the process, or continue. The choice is ours.

### 4. Run the command

The last step is to run a command that will apply our seeders. The command is the following:

``` sh
dotnet run --seed
```

This command runs sequentially all the registered seeders, no matter the order. We can run a specific seeder by specifying its name, or run multiple ones by separating names by spaces.

``` sh
dotnet run --seed <FirstSeederName> <SecondSeederName> ...
```

This ensures that the seeders are applied in the correct order.
