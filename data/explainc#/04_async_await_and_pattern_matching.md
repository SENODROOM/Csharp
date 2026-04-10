## Async & Await

```csharp
using System.Net.Http;

// async method — returns Task or Task<T>
public async Task<string> FetchDataAsync(string url)
{
    using var client = new HttpClient();

    // await suspends the method without blocking the thread!
    var response = await client.GetAsync(url);
    response.EnsureSuccessStatusCode();

    return await response.Content.ReadAsStringAsync();
}

// Parallel execution
public async Task<(User user, List<Post> posts)> GetDashboardAsync(int userId)
{
    // Run both concurrently — not sequentially!
    var userTask  = GetUserAsync(userId);
    var postsTask = GetPostsAsync(userId);

    // Await both — total time = max(userTime, postsTime) not sum!
    await Task.WhenAll(userTask, postsTask);

    return (await userTask, await postsTask);
}

// CancellationToken — cooperative cancellation
public async Task<string> SearchAsync(string query, CancellationToken ct = default)
{
    ct.ThrowIfCancellationRequested();

    var results = await database.QueryAsync(query, ct);  // pass token down

    // Check periodically in loops
    foreach (var item in results)
    {
        ct.ThrowIfCancellationRequested();
        await ProcessAsync(item, ct);
    }
    return results.ToString();
}

// IAsyncEnumerable — streaming async sequences (C# 8+)
public async IAsyncEnumerable<int> GenerateAsync(
    [EnumeratorCancellation] CancellationToken ct = default)
{
    for (int i = 0; i < 100; i++)
    {
        await Task.Delay(10, ct);  // simulate async work
        yield return i;
    }
}

// Consume with await foreach
await foreach (var item in GenerateAsync())
{
    Console.WriteLine(item);
}
```

---

## Pattern Matching

```csharp
// Switch expression (C# 8+)
string Classify(object obj) => obj switch
{
    int n when n < 0    => "negative integer",
    int n when n == 0   => "zero",
    int n               => $"positive integer: {n}",
    string { Length: 0 } => "empty string",
    string s when s.Length < 5 => $"short string: {s}",
    string s            => $"long string ({s.Length} chars)",
    null                => "null",
    _                   => "unknown type",
};

// Property patterns
string DescribePoint(Point p) => p switch
{
    { X: 0, Y: 0 }         => "Origin",
    { X: 0 }               => $"On Y-axis at {p.Y}",
    { Y: 0 }               => $"On X-axis at {p.X}",
    { X: > 0, Y: > 0 }     => "First quadrant",
    { X: < 0, Y: > 0 }     => "Second quadrant",
    _                       => "Other quadrant",
};

// Tuple patterns
string RPS(string p1, string p2) => (p1, p2) switch
{
    ("Rock",     "Scissors") => "Player 1 wins",
    ("Scissors", "Paper")    => "Player 1 wins",
    ("Paper",    "Rock")     => "Player 1 wins",
    var (a, b) when a == b   => "Draw",
    _                        => "Player 2 wins",
};

// List patterns (C# 11+)
string DescribeList(int[] arr) => arr switch
{
    []               => "empty",
    [var single]     => $"single element: {single}",
    [var first, ..]  => $"starts with {first}",
};

// is pattern with declaration
if (shape is Circle { Radius: > 10 } bigCircle)
{
    Console.WriteLine($"Big circle with radius {bigCircle.Radius}");
}
```

---

## Records and Immutability

```csharp
// Record (C# 9+) — immutable reference type with value equality
public record Person(string FirstName, string LastName, int Age);

var alice  = new Person("Alice", "Smith", 30);
var alice2 = new Person("Alice", "Smith", 30);

Console.WriteLine(alice == alice2);   // true (value equality!)
Console.WriteLine(alice.ToString());  // Person { FirstName = Alice, LastName = Smith, Age = 30 }

// Non-destructive mutation with 'with'
var olderAlice = alice with { Age = 31 };  // new record, alice unchanged

// Record struct (C# 10+) — value type record
public record struct Coordinate(double Lat, double Lon);

// Record with custom members
public record Order(string Id, List<OrderItem> Items, decimal Total)
{
    // Computed property
    public int ItemCount => Items.Count;

    // Validation in init
    public decimal Total { get; init; } =
        Total >= 0 ? Total : throw new ArgumentException("Total cannot be negative");
}

// Positional records support deconstruction
var (first, last, age) = alice;
Console.WriteLine($"{first} {last}, age {age}");
```

---

