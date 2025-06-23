using LanguageExt;
using LanguageExtSandbox.Entities;

namespace Demos;

public static class FunctionalPipelineDemo
{
    public static void Demo(Lst<Expense> expenses)
    {
        Console.WriteLine("3. Functional Pipelines Demo:");
        Console.WriteLine("------------------------------");

        // Pipeline: filter -> map -> reduce
        Console.WriteLine("Expensive items (>20 SEK):");
        var expensiveItems = expenses
            .Filter(e => e.Amount > 20)
            .Map(e => $"{e.Description}: {e.Amount:C} SEK")
            .ToList();
        expensiveItems.Iter(item => Console.WriteLine($"  - {item}"));

        Console.WriteLine("\nTotals by category:");
        var categoryTotals = expenses
            .GroupBy(e => e.Category.Name)
            .Map(group => (Category: group.Key, Total: group.Sum(e => e.Amount)))
            .OrderByDescending(x => x.Total);
        categoryTotals.Iter(item => Console.WriteLine($"  {item.Category}: {item.Total:C} SEK"));

        Console.WriteLine();
    }
}
