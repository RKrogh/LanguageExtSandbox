using LanguageExt;

namespace LanguageExtSandbox.Entities;

public record ExpenseSummary(decimal Total, int Count, Lst<Category> Categories);
