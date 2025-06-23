using LanguageExt;
using LanguageExtSandbox.Entities;

namespace Demos;

public static class PatternMatchingDemo
{
    public static void Demo(Lst<Expense> expenses)
    {
        Console.WriteLine("4. Pattern Matching Demo:");
        Console.WriteLine("--------------------------");

        expenses
            .Map(expense =>
                (
                    Expense: expense,
                    CategoryType: expense.Category.Name switch
                    {
                        "Food" => "Essential",
                        "Transport" => "Essential",
                        "Entertainment" => "Luxury",
                        "Shopping" => "Luxury",
                        _ => "Other",
                    },
                    Priority: expense.Amount switch
                    {
                        > 100 => "High",
                        > 50 => "Medium",
                        > 20 => "Low",
                        _ => "Very Low",
                    }
                )
            )
            .OrderBy(x => x.CategoryType)
            .ThenByDescending(x => x.Priority)
            .Iter(item =>
            {
                Console.WriteLine(
                    $"{item.Expense.Description}: {item.CategoryType} expense, {item.Priority} priority"
                );
            });

        Console.WriteLine();
    }
}
