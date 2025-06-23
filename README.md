# Language Ext Expense Tracker Sandbox

A functional programming sandbox using Language Ext for C# to explore key FP concepts through a personal expense tracker application.

## What's Included

This sandbox demonstrates 5 core functional programming concepts using Language Ext:

### 1. **Option Types** (`Option<T>`)
- Safe operations that might not return a value
- Eliminates null reference exceptions
- Pattern matching with `Some`/`None`

```csharp
var foundExpense = expenses.Find(e => e.Amount > 100);
foundExpense.Match(
    Some: expense => Console.WriteLine($"Found: {expense.Description}"),
    None: () => Console.WriteLine("No expensive items found")
);
```

### 2. **Either Types** (`Either<L, R>`)
- Error handling without exceptions
- Railway-oriented programming
- Validation pipelines

```csharp
var result = CreateExpense("Coffee", "5.50", "Food");
result.Match(
    Right: expense => Console.WriteLine($"Created: {expense.Description}"),
    Left: error => Console.WriteLine($"Error: {error.Message}")
);
```

### 3. **Functional Pipelines**
- Immutable data transformations
- Method chaining with `Map`, `Filter`, `Reduce`
- Declarative data processing

```csharp
var expensiveItems = expenses
    .Filter(e => e.Amount > 20)
    .Map(e => $"{e.Description}: {e.Amount:C} SEK")
    .ToList();
```

### 4. **Pattern Matching**
- Switch expressions with functional style
- Exhaustive matching
- Type-safe branching logic

```csharp
var categoryType = expense.Category.Name switch
{
    "Food" => "Essential",
    "Transport" => "Essential", 
    "Entertainment" => "Luxury",
    _ => "Other"
};
```

### 5. **Monadic Composition**
- Chaining operations with `Bind`
- Railway-oriented programming
- Error propagation through the chain

```csharp
var result = Some(expenses)
    .Bind(exp => exp.Count > 0 ? Some(exp) : None)
    .Map(exp => exp.Sum(e => e.Amount))
    .Map(total => total * 0.1m);
```

## Domain Entities

The sandbox uses a simple but realistic domain model for expense tracking. Understanding these entities will help you follow the code examples:

### **Expense** (`Entities/Expense.cs`)
The core domain object representing a single expense transaction:

```csharp
public record Expense(
    Guid Id,           // Unique identifier
    decimal Amount,    // Transaction amount in SEK
    string Description, // Human-readable description
    DateTime Date,     // When the expense occurred
    Category Category  // Categorization
);
```

**Key characteristics:**
- **Immutable**: Once created, an expense cannot be modified
- **Value-based equality**: Two expenses with same values are considered equal
- **Rich domain**: Contains business logic and validation rules

### **Category** (`Entities/Category.cs`)
Represents expense categories for organization and analysis:

```csharp
public record Category(string Name, string Color);
```

**Usage examples:**
- `new Category("Food", "#FF6B6B")` - Red color for food expenses
- `new Category("Transport", "#4ECDC4")` - Teal for transport
- Categories enable grouping, filtering, and pattern matching

### **ValidationError** (`Entities/ValidationError.cs`)
Represents domain validation failures in a functional way:

```csharp
public record ValidationError(string Message);
```

**Why this matters:**
- **No exceptions**: Errors are values, not thrown exceptions
- **Composable**: Can be chained with `Either<ValidationError, T>`
- **Descriptive**: Clear error messages for user feedback

### **ExpenseSummary** (`Entities/ExpenseSummary.cs`)
Aggregated view of expense data for reporting:

```csharp
public record ExpenseSummary(
    decimal Total,           // Sum of all expenses
    int Count,              // Number of expenses
    Lst<Category> Categories // Unique categories used
);
```

**Functional benefits:**
- **Immutable aggregation**: Safe to share and cache
- **Rich data**: Contains both calculated and raw data
- **Composable**: Can be further transformed or combined

### **Why These Entities Work Well for FP**

1. **Immutability**: Records are immutable by default, preventing side effects
2. **Value semantics**: Natural equality and comparison behavior
3. **Composability**: Easy to combine and transform using functional operations
4. **Type safety**: Strong typing prevents invalid states
5. **Domain clarity**: Clear business concepts that map to real-world usage

### **Entity Relationships**

```
Expense ──────┐
  │           │
  ├─ Category │
  │           │
  └─ ValidationError (when validation fails)
     │
     └─ ExpenseSummary (aggregated view)
```

This domain model provides a realistic foundation for exploring functional programming patterns while remaining simple enough to understand quickly.

## Key Language Ext Features Used

- **`Option<T>`**: Safe optional values
- **`Either<L, R>`**: Error handling
- **`Lst<T>`**: Immutable lists
- **`List()`**: Functional list creation
- **`Map()`**: Transform collections
- **`Filter()`**: Filter collections
- **`Match()`**: Pattern matching
- **`Bind()`**: Monadic composition
- **`Some()`/`None()`**: Option constructors
- **`Left()`/`Right()`**: Either constructors

## Domain Models

### Immutable Records
```csharp
public record Expense(
    Guid Id,
    decimal Amount,
    string Description,
    DateTime Date,
    Category Category);

public record Category(string Name, string Color);
```

### Validation Error
```csharp
public record ValidationError(string Message);
```

## Running the Sandbox

```bash
dotnet run
```

## Exploring Further

### Try These Modifications:

1. **Add new expense categories** and update the pattern matching
2. **Implement expense deletion** using `Option<T>` for safe removal
3. **Add date filtering** with functional pipelines
4. **Create budget alerts** using `Either<L, R>` for threshold checking
5. **Implement expense search** with functional composition
6. **Add expense splitting** between categories using monadic operations

### Advanced Concepts to Explore:

- **Applicative Functors**: Parallel validation
- **Monad Transformers**: Combining different monadic contexts
- **Free Monads**: Building domain-specific languages
- **Lens**: Immutable updates to nested structures
- **Validation**: Accumulating multiple errors

### Language Ext Collections to Try:

- **`Map<K, V>`**: Immutable dictionaries
- **`Set<T>`**: Immutable sets
- **`Seq<T>`**: Lazy sequences
- **`Arr<T>`**: Immutable arrays

## Why This Works Well for Learning

1. **Real-world domain**: Expense tracking is familiar and practical
2. **Clear data flow**: Each operation transforms immutable data
3. **Error handling**: Natural validation scenarios
4. **Composability**: Easy to chain operations together
5. **Immutability**: No side effects, predictable behavior

## Next Steps

After exploring this sandbox:

1. Try building a web API using these patterns
2. Implement persistence with functional error handling
3. Add real-time updates using functional reactive programming
4. Explore other functional libraries like F# or Haskell
5. Study category theory and monad laws

## Resources

- [Language Ext Documentation](https://github.com/louthy/language-ext)
- [Functional Programming in C#](https://www.manning.com/books/functional-programming-in-c-sharp)
- [Railway Oriented Programming](https://fsharpforfunandprofit.com/rop/)
- [Category Theory for Programmers](https://bartoszmilewski.com/2014/10/28/category-theory-for-programmers-the-preface/) 