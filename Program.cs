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
        DemoOptionTypes(expenses, categories);

        // Demo 2: Either types for validation
        DemoEitherValidation();

        // Demo 3: Functional pipelines
        DemoFunctionalPipelines(expenses);

        // Demo 4: Pattern matching
        DemoPatternMatching(expenses);

        // Demo 5: Monadic composition
        DemoMonadicComposition(expenses);

        Console.WriteLine("\n=== Sandbox Complete ===");
    }

    private static void DemoOptionTypes(Lst<Expense> expenses, Lst<Category> categories)
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

    private static void DemoEitherValidation()
    {
        Console.WriteLine("2. Either Validation Demo:");
        Console.WriteLine("---------------------------");

        // Validation functions that return Either
        var validExpense = CreateExpense("Coffee", "5.50", "Food");
        var invalidExpense = CreateExpense("", "-10.00", "InvalidCategory");

        validExpense.Match(
            Right: expense =>
                Console.WriteLine(
                    $"Valid expense created: {expense.Description} ({expense.Amount:C} SEK)"
                ),
            Left: error => Console.WriteLine($"Validation error: {error.Message}")
        );

        invalidExpense.Match(
            Right: expense =>
                Console.WriteLine(
                    $"Valid expense created: {expense.Description} ({expense.Amount:C} SEK)"
                ),
            Left: error => Console.WriteLine($"Validation error: {error.Message}")
        );

        Console.WriteLine();
    }

    private static void DemoFunctionalPipelines(Lst<Expense> expenses)
    {
        Console.WriteLine("3. Functional Pipelines Demo:");
        Console.WriteLine("------------------------------");

        // Pipeline: filter -> map -> reduce
        var expensiveItems = expenses
            .Filter(e => e.Amount > 20)
            .Map(e => $"{e.Description}: {e.Amount:C} SEK")
            .ToList();

        Console.WriteLine("Expensive items (>20 SEK):");
        expensiveItems.Iter(item => Console.WriteLine($"  - {item}"));

        // Group by category and calculate totals
        var categoryTotals = expenses
            .GroupBy(e => e.Category.Name)
            .Map(group => (Category: group.Key, Total: group.Sum(e => e.Amount)))
            .OrderByDescending(x => x.Total);

        Console.WriteLine("\nTotals by category:");
        categoryTotals.Iter(item => Console.WriteLine($"  {item.Category}: {item.Total:C} SEK"));

        Console.WriteLine();
    }

    private static void DemoPatternMatching(Lst<Expense> expenses)
    {
        Console.WriteLine("4. Pattern Matching Demo:");
        Console.WriteLine("--------------------------");

        expenses.Iter(expense =>
        {
            var categoryType = expense.Category.Name switch
            {
                "Food" => "Essential",
                "Transport" => "Essential",
                "Entertainment" => "Luxury",
                "Shopping" => "Luxury",
                _ => "Other",
            };

            var priority = expense.Amount switch
            {
                > 100 => "High",
                > 50 => "Medium",
                > 20 => "Low",
                _ => "Very Low",
            };

            Console.WriteLine(
                $"{expense.Description}: {categoryType} expense, {priority} priority"
            );
        });

        Console.WriteLine();
    }

    private static void DemoMonadicComposition(Lst<Expense> expenses)
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

    // Helper functions demonstrating functional patterns
    private static Either<ValidationError, Expense> CreateExpense(
        string description,
        string amountStr,
        string categoryName
    )
    {
        return from desc in ValidateDescription(description)
            from amount in ParseAmount(amountStr)
            from category in ValidateCategory(categoryName)
            select new Expense(Guid.NewGuid(), amount, desc, DateTime.Now, category);
    }

    private static Either<ValidationError, string> ValidateDescription(string description)
    {
        return string.IsNullOrWhiteSpace(description)
            ? Left<ValidationError, string>(new ValidationError("Description cannot be empty"))
            : Right<ValidationError, string>(description.Trim());
    }

    private static Either<ValidationError, decimal> ParseAmount(string amountStr)
    {
        return decimal.TryParse(amountStr, out var amount) && amount > 0
            ? Right<ValidationError, decimal>(amount)
            : Left<ValidationError, decimal>(
                new ValidationError("Amount must be a positive number")
            );
    }

    private static Either<ValidationError, Category> ValidateCategory(string categoryName)
    {
        var validCategories = List("Food", "Transport", "Entertainment", "Shopping");
        return validCategories.Contains(categoryName)
            ? Right<ValidationError, Category>(new Category(categoryName, "#000000"))
            : Left<ValidationError, Category>(
                new ValidationError($"Invalid category: {categoryName}")
            );
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
