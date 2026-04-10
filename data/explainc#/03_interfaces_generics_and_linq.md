## Interfaces & Abstract Classes

```csharp
// Interface — pure contract, no state (C# 8+ allows default implementations)
public interface IRepository<T, TId> where T : class
{
    Task<T?>           GetByIdAsync(TId id);
    Task<IList<T>>     GetAllAsync();
    Task<T>            CreateAsync(T entity);
    Task<T>            UpdateAsync(T entity);
    Task<bool>         DeleteAsync(TId id);

    // Default implementation (C# 8+)
    async Task<bool> ExistsAsync(TId id) =>
        await GetByIdAsync(id) is not null;
}

// Extension methods — add methods to existing types without inheritance
public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? s) =>
        string.IsNullOrEmpty(s);

    public static string Truncate(this string s, int maxLength) =>
        s.Length <= maxLength ? s : s[..maxLength] + "...";

    public static string ToTitleCase(this string s) =>
        System.Globalization.CultureInfo.CurrentCulture
              .TextInfo.ToTitleCase(s.ToLower());
}

// Usage — looks like a native method!
"hello world".ToTitleCase();  // "Hello World"
"very long string".Truncate(5);  // "very ..."
```

---

## Generics

```csharp
// Generic class
public class Stack<T>
{
    private readonly List<T> _items = [];

    public void Push(T item) => _items.Add(item);

    public T Pop()
    {
        if (_items.Count == 0) throw new InvalidOperationException("Stack is empty");
        var item = _items[^1];
        _items.RemoveAt(_items.Count - 1);
        return item;
    }

    public T Peek() => _items.Count > 0 ? _items[^1]
        : throw new InvalidOperationException("Stack is empty");

    public int Count => _items.Count;
}

// Generic method with constraints
public static T Max<T>(T a, T b) where T : IComparable<T>
    => a.CompareTo(b) >= 0 ? a : b;

// Multiple constraints
public static void Process<T>(T item)
    where T : class,           // must be reference type
              IDisposable,     // must implement IDisposable
              new()            // must have parameterless constructor
{
    using var resource = item;  // auto-dispose
    // ...
}

// Covariance and Contravariance
IEnumerable<string> strings = new List<string>();
IEnumerable<object> objects = strings;  // covariant (out T)

Action<object> objAction = o => Console.WriteLine(o);
Action<string> strAction = objAction;   // contravariant (in T)
```

---

## LINQ

Language Integrated Query — query any data source with a unified syntax.

```csharp
var people = new List<Person>
{
    new("Alice", 30, "Engineering"),
    new("Bob",   25, "Marketing"),
    new("Carol", 35, "Engineering"),
    new("Dave",  28, "Marketing"),
    new("Eve",   32, "Engineering"),
};

// Query syntax (SQL-like)
var engineers = from p in people
                where p.Department == "Engineering"
                orderby p.Age descending
                select new { p.Name, p.Age };

// Method syntax (fluent) — same result, preferred by most C# devs
var engineers2 = people
    .Where(p => p.Department == "Engineering")
    .OrderByDescending(p => p.Age)
    .Select(p => new { p.Name, p.Age });

// Aggregation
double avgAge = people.Average(p => p.Age);
int    oldest = people.Max(p => p.Age);
int    total  = people.Count(p => p.Age > 28);

// Grouping
var byDept = people
    .GroupBy(p => p.Department)
    .Select(g => new {
        Department = g.Key,
        Count = g.Count(),
        AvgAge = g.Average(p => p.Age),
        Members = g.Select(p => p.Name).ToList()
    });

// Joining
var result = people
    .Join(departments,
          person => person.Department,
          dept => dept.Name,
          (person, dept) => new { person.Name, dept.Manager });

// Deferred execution — LINQ is lazy!
var query = people.Where(p => {
    Console.WriteLine($"Checking {p.Name}");  // only runs when iterated!
    return p.Age > 28;
});
// Nothing printed yet...
var list = query.ToList();  // NOW it executes
```

---

