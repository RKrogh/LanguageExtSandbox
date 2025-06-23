using LanguageExt;
using LanguageExtSandbox.Entities;

namespace Demos;

public static class OptionTypeDemo
{
    public static void Demo(Lst<Expense> expenses, Lst<Category> categories)
    {
        Console.WriteLine("1. Option Types Demo:");
        Console.WriteLine("----------------------");

        // Safe find operations
        var foundExpense = expenses.Find(e => e.Amount > 100);
        foundExpense.Match(
            Some: expense =>
                Console.WriteLine(
                    $"Found expensive item: {expense.Description} ({expense.Amount:C} SEK)"
                ),
            None: () => Console.WriteLine("No expensive items found")
        );

        // Safe category lookup
        var foodCategory = categories.Find(c => c.Name == "Food");
        foodCategory.Match(
            Some: cat => Console.WriteLine($"Found category: {cat.Name} ({cat.Color})"),
            None: () => Console.WriteLine("Category not found")
        );

        // Option with default value
        var totalAmount = expenses.Map(e => e.Amount).Sum();
        Console.WriteLine($"Total expenses: {totalAmount:C} SEK");

        Console.WriteLine();
    }
}
