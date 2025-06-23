using Demos;
using LanguageExt;
using LanguageExtSandbox.Entities;
using static LanguageExt.Prelude;

namespace LanguageExtSandbox;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Language Ext Expense Tracker Sandbox ===\n");

        // Sample data
        var categories = List(
            new Category("Food", "#FF6B6B"),
            new Category("Transport", "#4ECDC4"),
            new Category("Entertainment", "#45B7D1"),
            new Category("Shopping", "#96CEB4")
        );

        var expenses = List(
            new Expense(Guid.NewGuid(), 25.50m, "Lunch", DateTime.Now.AddDays(-1), categories[0]),
            new Expense(
                Guid.NewGuid(),
                15.00m,
                "Bus fare",
                DateTime.Now.AddDays(-2),
                categories[1]
            ),
            new Expense(
                Guid.NewGuid(),
                45.00m,
                "Movie tickets",
                DateTime.Now.AddDays(-3),
                categories[2]
            ),
            new Expense(
                Guid.NewGuid(),
                120.00m,
                "New shoes",
                DateTime.Now.AddDays(-4),
                categories[3]
            )
        );

        // Demo 1: Option types for safe operations
        OptionTypeDemo.Demo(expenses, categories);

        // Demo 2: Either types for validation
        EitherValidationDemo.Demo();

        // Demo 3: Functional pipelines
        FunctionalPipelineDemo.Demo(expenses);

        // Demo 4: Pattern matching
        PatternMatchingDemo.Demo(expenses);

        // Demo 5: Monadic composition
        MonadicCompositionDemo.Demo(expenses);

        Console.WriteLine("\n=== Sandbox Complete ===");
    }
}
