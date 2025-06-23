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