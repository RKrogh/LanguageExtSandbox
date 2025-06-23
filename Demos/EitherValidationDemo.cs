using System.Globalization;
using LanguageExt;
using LanguageExtSandbox.Entities;
using static LanguageExt.Prelude;

namespace Demos;

public static class EitherValidationDemo
{
    public static void Demo()
    {
        Console.WriteLine("2. Either Validation Demo:");
        Console.WriteLine("---------------------------");

        // Validation functions that return Either
        var validExpense = CreateExpense("Coffee", "5.50", "Food");
        var invalidDescriptionExpense = CreateExpense("", "-10.00", "InvalidCategory");
        var invalidAmountExpense = CreateExpense("Unkown", "-10.00", "InvalidCategory");
        var invalidCategoryExpense = CreateExpense("Unkown", "10.00", "InvalidCategory");

        validExpense.Match(
            Right: expense =>
                Console.WriteLine(
                    $"Valid expense created: {expense.Description} ({expense.Amount:C} SEK)"
                ),
            Left: error => Console.WriteLine($"Validation error: {error.Message}")
        );

        invalidDescriptionExpense.Match(
            Right: expense =>
                Console.WriteLine(
                    $"Valid expense created: {expense.Description} ({expense.Amount:C} SEK)"
                ),
            Left: error => Console.WriteLine($"Validation error: {error.Message}")
        );

        invalidAmountExpense.Match(
            Right: expense =>
                Console.WriteLine(
                    $"Valid expense created: {expense.Description} ({expense.Amount:C} SEK)"
                ),
            Left: error => Console.WriteLine($"Validation error: {error.Message}")
        );

        invalidCategoryExpense.Match(
            Right: expense =>
                Console.WriteLine(
                    $"Valid expense created: {expense.Description} ({expense.Amount:C} SEK)"
                ),
            Left: error => Console.WriteLine($"Validation error: {error.Message}")
        );

        Console.WriteLine();
    }

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
        return
            decimal.TryParse(
                amountStr,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var amount
            )
            && amount > 0
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
}
