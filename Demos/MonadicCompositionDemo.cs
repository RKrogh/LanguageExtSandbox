using LanguageExt;
using LanguageExtSandbox.Entities;
using static LanguageExt.Prelude;

namespace Demos;

public static class MonadicCompositionDemo
{
    public static void Demo(Lst<Expense> expenses)
    {
        Console.WriteLine("5. Monadic Composition Demo:");
        Console.WriteLine("-----------------------------");

        // Compose operations using monadic bind
        var result = Some(expenses)
            .Bind(exp => exp.Count > 0 ? Some(exp) : None)
            .Map(exp => exp.Sum(e => e.Amount))
            .Map(total => total * 0.1m) // 10% tax
            .Match(
                Some: tax => Console.WriteLine($"Estimated tax on expenses: {tax:C} SEK"),
                None: () => Console.WriteLine("No expenses to calculate tax on")
            );

        // Chain Either operations
        var budgetAnalysis = AnalyzeBudget(expenses, 500m);
        budgetAnalysis.Match(
            Right: analysis => Console.WriteLine($"Budget analysis: {analysis}"),
            Left: error => Console.WriteLine($"Analysis error: {error.Message}")
        );

        Console.WriteLine();
    }

    private static Either<ValidationError, string> AnalyzeBudget(
        Lst<Expense> expenses,
        decimal budget
    )
    {
        var total = expenses.Sum(e => e.Amount);
        var remaining = budget - total;

        return remaining switch
        {
            < 0 => Left<ValidationError, string>(
                new ValidationError($"Over budget by {Math.Abs(remaining):C} SEK")
            ),
            < 100 => Right<ValidationError, string>(
                $"Close to budget limit. Remaining: {remaining:C} SEK"
            ),
            _ => Right<ValidationError, string>(
                $"Well within budget. Remaining: {remaining:C} SEK"
            ),
        };
    }
}
