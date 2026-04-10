## The .NET Ecosystem

```
.NET Platform (2024)
──────────────────────────────────────────────────────────────────
RUNTIME
  .NET 9  ──► Cross-platform CLR (Windows, Linux, macOS)
  .NET Framework ──► Windows-only legacy (4.8 final version)
  Mono    ──► Open source .NET for mobile (Xamarin/MAUI base)

WEB FRAMEWORKS
  ASP.NET Core     ──► MVC + Razor Pages
  Blazor           ──► C# in the browser (WebAssembly)
  Minimal APIs     ──► Lightweight REST APIs
  SignalR          ──► Real-time WebSockets
  gRPC             ──► High-performance RPC

DESKTOP
  WPF              ──► Windows Presentation Foundation
  WinForms         ──► Windows Forms
  .NET MAUI        ──► Cross-platform: Win, Mac, iOS, Android

CLOUD & MICROSERVICES
  Azure SDK        ──► Azure cloud services
  Orleans          ──► Distributed actor framework
  Aspire           ──► Cloud-native .NET tooling (new!)

DATA ACCESS
  Entity Framework Core ──► ORM (database-first or code-first)
  Dapper           ──► Lightweight micro-ORM
  ADO.NET          ──► Low-level database access

TESTING
  xUnit, NUnit, MSTest ──► Unit testing frameworks
  Moq, NSubstitute     ──► Mocking
  FluentAssertions     ──► Readable assertions
  Bogus                ──► Fake data generation

TOOLING
  dotnet CLI       ──► Build, run, test, publish
  Visual Studio    ──► Full IDE (Windows)
  VS Code + C# Dev Kit ──► Cross-platform editor
  JetBrains Rider  ──► Cross-platform IDE
```

---

## Performance & Span\<T>

```csharp
// Span<T> — zero-allocation slicing of contiguous memory (C# 7.2+)
void ProcessData(ReadOnlySpan<byte> data)
{
    // Work with slices without allocation
    var header = data[..4];
    var body   = data[4..];

    // Parse without string allocation
    if (int.TryParse(data[..10], out int value))
        Console.WriteLine(value);
}

// stackalloc — allocate on the stack (no GC pressure)
Span<int> numbers = stackalloc int[100];
numbers.Fill(0);

// ArrayPool — rent and return arrays (avoid allocation)
var pool   = System.Buffers.ArrayPool<byte>.Shared;
byte[] buf = pool.Rent(4096);
try
{
    // use buf
}
finally
{
    pool.Return(buf);  // return to pool for reuse
}

// Memory<T> — like Span but can be stored (not limited to stack)
public async Task ProcessFileAsync(string path)
{
    using var file = File.OpenRead(path);
    var buffer = new byte[4096];
    Memory<byte> memory = buffer;  // wrap in Memory<T>

    int bytesRead;
    while ((bytesRead = await file.ReadAsync(memory)) > 0)
    {
        await ProcessChunkAsync(memory[..bytesRead]);
    }
}

// Unsafe code for extreme performance (rare, use with care)
unsafe
{
    int* ptr = stackalloc int[10];
    for (int i = 0; i < 10; i++)
        ptr[i] = i * i;
}
```

---

## Quick Reference

```
Type Aliases          │ C# Type  │ .NET Type
──────────────────────┼──────────┼────────────────
int                   │ int      │ System.Int32
long                  │ long     │ System.Int64
float                 │ float    │ System.Single
double                │ double   │ System.Double
decimal               │ decimal  │ System.Decimal
string                │ string   │ System.String
bool                  │ bool     │ System.Boolean
object                │ object   │ System.Object

Common Collections
──────────────────────────────────────────────────
List<T>               Dynamic array
Dictionary<K,V>       Hash map
HashSet<T>            Unique collection
Queue<T>              FIFO
Stack<T>              LIFO
LinkedList<T>         Doubly linked list
ImmutableList<T>      Thread-safe immutable list
ConcurrentDictionary  Thread-safe dictionary

String Methods
──────────────────────────────────────────────────
s.Contains("x")       bool
s.StartsWith("x")     bool
s.Split(',')          string[]
s.Trim()              string
s.ToUpper()           string
s.Replace("a","b")    string
s.Substring(1, 3)     string (legacy)
s[1..4]               string (modern)
string.Join(",", arr) string
```

---

*C#: Where Java wanted to go, but Microsoft got there first — and kept going, version after version, feature after feature, never stopping. 💜*
