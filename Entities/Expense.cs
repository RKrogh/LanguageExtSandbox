namespace LanguageExtSandbox.Entities;

public record Expense(
    Guid Id,
    decimal Amount,
    string Description,
    DateTime Date,
    Category Category
);
