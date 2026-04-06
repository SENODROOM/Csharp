# C# Methods and Functions

## Method Basics

### Method Declaration
```csharp
// Basic method syntax
public class Calculator
{
    // Public method with no parameters and no return value
    public void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Calculator!");
    }
    
    // Method with parameters and return value
    public int Add(int a, int b)
    {
        return a + b;
    }
    
    // Method with multiple parameters
    public double CalculateInterest(double principal, double rate, int years)
    {
        return principal * rate * years;
    }
    
    // Private method (accessible only within the class)
    private void LogCalculation(string operation, double result)
    {
        Console.WriteLine($"{operation}: {result}");
    }
    
    // Static method (belongs to class, not instance)
    public static double GetPi()
    {
        return 3.141592653589793;
    }
}
```

### Method Parameters
```csharp
public class ParameterExamples
{
    // Value type parameters (passed by value)
    public void PassByValue(int number)
    {
        number = 100; // Changes local copy only
        Console.WriteLine($"Inside method: {number}");
    }
    
    // Reference type parameters (passed by reference)
    public void PassByReference(List<string> names)
    {
        names.Add("New Name"); // Modifies original list
        Console.WriteLine($"List count: {names.Count}");
    }
    
    // ref keyword (must be initialized)
    public void RefParameter(ref int number)
    {
        number *= 2; // Modifies original variable
    }
    
    // out keyword (doesn't need to be initialized)
    public void OutParameter(out int result)
    {
        result = 42; // Must assign value
    }
    
    // in keyword (read-only reference)
    public void InParameter(in ReadOnlySpan<int> numbers)
    {
        // Cannot modify numbers
        int sum = 0;
        foreach (int num in numbers)
        {
            sum += num;
        }
        Console.WriteLine($"Sum: {sum}");
    }
    
    // Optional parameters (C# 4.0+)
    public void OptionalParameters(string name = "Default", int age = 25)
    {
        Console.WriteLine($"Name: {name}, Age: {age}");
    }
    
    // Named arguments
    public void NamedArguments()
    {
        OptionalParameters(age: 30, name: "Alice");
    }
    
    // Variable number of arguments (params)
    public int SumNumbers(params int[] numbers)
    {
        int sum = 0;
        foreach (int num in numbers)
        {
            sum += num;
        }
        return sum;
    }
}
```

### Method Overloading
```csharp
public class OverloadingExample
{
    // Method overloading by parameter count
    public int Add(int a, int b)
    {
        return a + b;
    }
    
    public int Add(int a, int b, int c)
    {
        return a + b + c;
    }
    
    // Method overloading by parameter types
    public double Add(double a, double b)
    {
        return a + b;
    }
    
    // Method overloading by parameter types (mixed)
    public double Add(int a, double b)
    {
        return a + b;
    }
    
    // Overloaded constructor
    public OverloadingExample()
    {
        Console.WriteLine("Default constructor");
    }
    
    public OverloadingExample(string name)
    {
        Console.WriteLine($"Constructor with name: {name}");
    }
    
    public OverloadingExample(string name, int age)
    {
        Console.WriteLine($"Constructor with name: {name}, age: {age}");
    }
}
```

## Advanced Method Features

### Recursion
```csharp
public class RecursionExamples
{
    // Recursive factorial
    public int Factorial(int n)
    {
        if (n <= 1)
            return 1;
        return n * Factorial(n - 1);
    }
    
    // Recursive Fibonacci
    public int Fibonacci(int n)
    {
        if (n <= 1)
            return n;
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }
    
    // Recursive directory traversal
    public void ListFiles(string path, string searchPattern = "*")
    {
        try
        {
            foreach (string file in Directory.GetFiles(path, searchPattern))
            {
                Console.WriteLine(file);
            }
            
            foreach (string directory in Directory.GetDirectories(path))
            {
                ListFiles(directory, searchPattern);
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Access denied to: {path}");
        }
    }
    
    // Tail recursion optimization (C# doesn't optimize tail calls)
    public int TailRecursiveFactorial(int n, int accumulator = 1)
    {
        if (n <= 1)
            return accumulator;
        return TailRecursiveFactorial(n - 1, n * accumulator);
    }
}
```

### Extension Methods
```csharp
// Extension methods (must be in static class)
public static class StringExtensions
{
    // Extension method for string
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }
    
    // Extension method for string with parameter
    public static string Reverse(this string str)
    {
        char[] charArray = str.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
    
    // Extension method for IEnumerable<T>
    public static double Average<T>(this IEnumerable<T> source, Func<T, double> selector)
    {
        double sum = 0;
        int count = 0;
        
        foreach (T item in source)
        {
            sum += selector(item);
            count++;
        }
        
        return count > 0 ? sum / count : 0;
    }
}

// Using extension methods
public class ExtensionMethodUsage
{
    public void DemonstrateExtensions()
    {
        string text = "Hello, World!";
        
        // Using string extension
        bool isEmpty = text.IsNullOrEmpty();
        string reversed = text.Reverse();
        
        Console.WriteLine($"Is empty: {isEmpty}");
        Console.WriteLine($"Reversed: {reversed}");
        
        // Using IEnumerable extension
        var numbers = new List<int> { 1, 2, 3, 4, 5 };
        double average = numbers.Average(x => x * 2);
        
        Console.WriteLine($"Average: {average}");
    }
}
```

### Lambda Expressions and Delegates
```csharp
public class LambdaExamples
{
    // Delegate declaration
    public delegate int MathOperation(int a, int b);
    
    // Using lambda expressions
    public void DemonstrateLambdas()
    {
        // Lambda expression with no parameters
        Func<string> greet = () => "Hello, World!";
        Console.WriteLine(greet());
        
        // Lambda expression with parameters
        Func<int, int, int> add = (a, b) => a + b;
        Console.WriteLine($"5 + 3 = {add(5, 3)}");
        
        // Lambda expression with statement block
        Func<int, string> formatNumber = (num) =>
        {
            return $"Number: {num:N2}";
        };
        Console.WriteLine(formatNumber(1234.5678));
        
        // Lambda expression in LINQ
        var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var evenNumbers = numbers.Where(x => x % 2 == 0).ToList();
        
        Console.WriteLine($"Even numbers: {string.Join(", ", evenNumbers)}");
    }
    
    // Action delegate (no return value)
    public void DemonstrateActions()
    {
        Action<string> printMessage = message => Console.WriteLine(message);
        printMessage("Hello from Action!");
        
        Action<int, int> performOperation = (a, b) =>
        {
            Console.WriteLine($"Sum: {a + b}");
            Console.WriteLine($"Product: {a * b}");
        };
        
        performOperation(5, 3);
    }
    
    // Predicate delegate (returns bool)
    public void DemonstratePredicates()
    {
        Predicate<int> isEven = x => x % 2 == 0;
        Predicate<string> isLong = s => s.Length > 10;
        
        Console.WriteLine($"Is 4 even? {isEven(4)}");
        Console.WriteLine($"Is 'Hello World' long? {isLong("Hello World")}");
    }
}
```

### Generic Methods
```csharp
public class GenericMethods
{
    // Generic method
    public T Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }
    
    // Generic method with constraints
    public T Max<T>(T a, T b) where T : IComparable<T>
    {
        return a.CompareTo(b) > 0 ? a : b;
    }
    
    // Generic method with multiple constraints
    public T CreateInstance<T>() where T : new(), IDisposable
    {
        T instance = new T();
        // Use instance
        instance.Dispose();
        return instance;
    }
    
    // Generic method with out parameter
    public bool TryParse<T>(string input, out T result) where T : IParsable<T>
    {
        return T.TryParse(input, out result);
    }
    
    // Generic method for collections
    public void PrintCollection<T>(IEnumerable<T> collection)
    {
        foreach (T item in collection)
        {
            Console.WriteLine(item);
        }
    }
    
    // Generic method with multiple type parameters
    public TResult Convert<TSource, TResult>(TSource source, Func<TSource, TResult> converter)
    {
        return converter(source);
    }
}
```

## Async Methods

### Asynchronous Programming
```csharp
public class AsyncExamples
{
    // Async method with Task<T>
    public async Task<int> CalculateSumAsync(int a, int b)
    {
        await Task.Delay(1000); // Simulate async operation
        return a + b;
    }
    
    // Async method with no return value
    public async Task ProcessDataAsync()
    {
        await Task.Delay(500);
        Console.WriteLine("Data processed");
    }
    
    // Async method with multiple awaits
    public async Task<string> FetchAndProcessDataAsync(string url)
    {
        // Fetch data
        string data = await FetchDataAsync(url);
        
        // Process data
        string processedData = await ProcessDataAsync(data);
        
        return processedData;
    }
    
    // Async method with exception handling
    public async Task<bool> SafeOperationAsync()
    {
        try
        {
            await RiskyOperationAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }
    
    // Async method with cancellation
    public async Task LongRunningOperationAsync(CancellationToken cancellationToken)
    {
        for (int i = 0; i < 100; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Delay(100, cancellationToken);
            Console.WriteLine($"Progress: {i}%");
        }
    }
    
    // Helper methods
    private async Task<string> FetchDataAsync(string url)
    {
        await Task.Delay(1000);
        return $"Data from {url}";
    }
    
    private async Task<string> ProcessDataAsync(string data)
    {
        await Task.Delay(500);
        return $"Processed {data}";
    }
    
    private async Task RiskyOperationAsync()
    {
        await Task.Delay(100);
        throw new InvalidOperationException("Something went wrong");
    }
}
```

### Async/Await Patterns
```csharp
public class AsyncPatterns
{
    // Parallel async operations
    public async Task<List<string>> FetchMultipleUrlsAsync(string[] urls)
    {
        var tasks = urls.Select(url => FetchDataAsync(url)).ToArray();
        var results = await Task.WhenAll(tasks);
        return results.ToList();
    }
    
    // Async with timeout
    public async Task<string> FetchWithTimeoutAsync(string url, TimeSpan timeout)
    {
        var task = FetchDataAsync(url);
        var timeoutTask = Task.Delay(timeout);
        
        var completedTask = await Task.WhenAny(task, timeoutTask);
        
        if (completedTask == task)
        {
            return await task;
        }
        else
        {
            throw new TimeoutException($"Operation timed out after {timeout.TotalSeconds} seconds");
        }
    }
    
    // Async with retry logic
    public async Task<string> RetryOperationAsync(Func<Task<string>> operation, int maxRetries)
    {
        for (int i = 0; i < maxRetries; i++)
        {
            try
            {
                return await operation();
            }
            catch (Exception ex)
            {
                if (i == maxRetries - 1)
                    throw;
                
                Console.WriteLine($"Attempt {i + 1} failed: {ex.Message}. Retrying...");
                await Task.Delay(1000 * (i + 1));
            }
        }
        
        throw new InvalidOperationException("All retry attempts failed");
    }
    
    // Async stream (IAsyncEnumerable)
    public async IAsyncEnumerable<int> GenerateNumbersAsync(int count)
    {
        for (int i = 0; i < count; i++)
        {
            await Task.Delay(100);
            yield return i;
        }
    }
    
    // Consume async stream
    public async Task ConsumeAsyncStream()
    {
        await foreach (int number in GenerateNumbersAsync(10))
        {
            Console.WriteLine($"Received: {number}");
        }
    }
    
    private async Task<string> FetchDataAsync(string url)
    {
        await Task.Delay(1000);
        return $"Data from {url}";
    }
}
```

## Method Attributes

### Built-in Attributes
```csharp
public class AttributeExamples
{
    // Obsolete attribute
    [Obsolete("This method is deprecated. Use NewMethod instead.")]
    public void OldMethod()
    {
        Console.WriteLine("Old method");
    }
    
    // Conditional attribute
    [Conditional("DEBUG")]
    public void DebugOnlyMethod()
    {
        Console.WriteLine("This method only runs in DEBUG mode");
    }
    
    // Web API attributes
    [HttpGet]
    [Route("api/[controller]")]
    public IActionResult GetData()
    {
        return Ok("Data");
    }
    
    // Serialization attributes
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonIgnore]
    public string InternalProperty { get; set; }
    
    // Validation attributes
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    
    [Range(0, 120)]
    public int Age { get; set; }
}

// Custom attribute
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class LogAttribute : Attribute
{
    public string Message { get; }
    
    public LogAttribute(string message)
    {
        Message = message;
    }
}

// Using custom attribute
public class LoggingExample
{
    [Log("Method started")]
    public void MethodWithLogging()
    {
        Console.WriteLine("Method execution");
    }
    
    [Log("Method completed")]
    public void AnotherMethod()
    {
        Console.WriteLine("Another method");
    }
}
```

## Method Performance

### Performance Considerations
```csharp
public class PerformanceExamples
{
    // Method inlining (small methods may be inlined by JIT)
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int FastAdd(int a, int b)
    {
        return a + b;
    }
    
    // Avoid boxing/unboxing
    public void AvoidBoxing(int value)
    {
        // Bad: object obj = value; (boxing)
        // Good: Keep as int
        Console.WriteLine(value);
    }
    
    // Use StringBuilder for string concatenation in loops
    public string BuildString(string[] parts)
    {
        var sb = new StringBuilder();
        foreach (string part in parts)
        {
            sb.Append(part);
        }
        return sb.ToString();
    }
    
    // Use Span<T> for performance-critical operations
    public void ProcessSpan(ReadOnlySpan<char> text)
    {
        // Efficient text processing without allocations
        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            // Process character
        }
    }
    
    // Use ValueTask for async methods that may complete synchronously
    public ValueTask<int> GetValueAsync(int value)
    {
        if (value < 0)
            return new ValueTask<int>(0); // Synchronous completion
        
        return new ValueTask<int>(ComputeValueAsync(value));
    }
    
    private async Task<int> ComputeValueAsync(int value)
    {
        await Task.Delay(100);
        return value * 2;
    }
}
```

## Best Practices

### Method Design
```csharp
public class MethodDesign
{
    // Good: Single Responsibility Principle
    public int CalculateTotalPrice(decimal price, int quantity, decimal discount)
    {
        return price * quantity * (1 - discount);
    }
    
    // Good: Clear method name
    public bool IsValidEmail(string email)
    {
        return email.Contains("@") && email.Contains(".");
    }
    
    // Good: Proper parameter validation
    public void ProcessOrder(Order order)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order));
        
        if (order.Items.Count == 0)
            throw new ArgumentException("Order must have at least one item");
        
        // Process order
    }
    
    // Good: Use appropriate return types
    public bool TryParseInt(string input, out int result)
    {
        return int.TryParse(input, out result);
    }
    
    // Good: Use async/await for I/O operations
    public async Task<string> ReadFileAsync(string path)
    {
        return await File.ReadAllTextAsync(path);
    }
    
    // Bad: Too many parameters
    public void BadMethod(string a, int b, double c, bool d, char e, float f, decimal g)
    {
        // Too many parameters - consider using a parameter object
    }
    
    // Good: Use parameter object
    public void GoodMethod(OrderParameters parameters)
    {
        // Clean parameter handling
    }
}

// Parameter object
public class OrderParameters
{
    public string CustomerName { get; set; }
    public List<OrderItem> Items { get; set; }
    public decimal Discount { get; set; }
    public DateTime OrderDate { get; set; }
}
```

### Error Handling
```csharp
public class ErrorHandling
{
    // Good: Use specific exceptions
    public void ProcessPayment(Payment payment)
    {
        if (payment == null)
            throw new ArgumentNullException(nameof(payment));
        
        if (payment.Amount <= 0)
            throw new ArgumentException("Payment amount must be positive");
        
        if (payment.Amount > 10000)
            throw new PaymentAmountExceededException("Payment amount exceeds limit");
        
        // Process payment
    }
    
    // Good: Use try-catch for expected exceptions
    public bool TryConnectToDatabase(string connectionString)
    {
        try
        {
            // Attempt connection
            return true;
        }
        catch (SqlException ex)
        {
            LogError(ex);
            return false;
        }
    }
    
    // Good: Use exception filters (C# 6.0+)
    public void HandleSpecificExceptions()
    {
        try
        {
            // Risky operation
        }
        catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
        {
            // Handle specific exceptions
            LogError(ex);
        }
    }
    
    // Good: Use finally for cleanup
    public void ProcessWithCleanup()
    {
        var resource = new SomeResource();
        try
        {
            // Use resource
        }
        finally
        {
            resource?.Dispose();
        }
    }
    
    // Better: Use using statement
    public void ProcessWithUsing()
    {
        using (var resource = new SomeResource())
        {
            // Resource automatically disposed
        }
    }
    
    private void LogError(Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

public class PaymentAmountExceededException : Exception
{
    public PaymentAmountExceededException(string message) : base(message)
    {
    }
}

public class SomeResource : IDisposable
{
    public void Dispose()
    {
        // Cleanup resources
    }
}
```

## Common Pitfalls

### Method Implementation Errors
```csharp
public class CommonPitfalls
{
    // Pitfall: Not checking for null
    public void PitfallNullCheck(string text)
    {
        int length = text.Length; // NullReferenceException if text is null
    }
    
    // Solution: Check for null
    public void SolutionNullCheck(string text)
    {
        if (text != null)
        {
            int length = text.Length;
        }
    }
    
    // Pitfall: Not handling exceptions
    public void PitfallExceptionHandling()
    {
        int.Parse("not a number"); // FormatException
    }
    
    // Solution: Handle exceptions
    public bool SolutionExceptionHandling(string input)
    {
        try
        {
            int.Parse(input);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
    
    // Pitfall: Infinite recursion
    public int PitfallInfiniteRecursion(int n)
    {
        return PitfallInfiniteRecursion(n + 1); // StackOverflowException
    }
    
    // Solution: Base case
    public int SolutionRecursion(int n)
    {
        if (n <= 0)
            return 0;
        return SolutionRecursion(n - 1) + 1;
    }
    
    // Pitfall: Side effects in property getters
    private int _counter;
    public int PitfallSideEffect
    {
        get { return _counter++; } // Side effect in getter
    }
    
    // Solution: Use method for side effects
    public int SolutionSideEffect()
    {
        return _counter++;
    }
    
    // Pitfall: Not disposing resources
    public void PitfallResourceLeak()
    {
        var file = new FileStream("test.txt", FileMode.Open);
        file.WriteByte(1);
        // File not disposed - resource leak
    }
    
    // Solution: Use using statement
    public void SolutionResourceManagement()
    {
        using (var file = new FileStream("test.txt", FileMode.Open))
        {
            file.WriteByte(1);
        } // File automatically disposed
    }
}
```

## Summary

C# methods and functions provide:

**Method Basics:**
- Declaration syntax with access modifiers
- Parameter types and return values
- Method overloading
- Static and instance methods
- Constructors

**Advanced Features:**
- Recursion and tail recursion
- Extension methods
- Lambda expressions and delegates
- Generic methods with constraints
- Async/await patterns

**Asynchronous Programming:**
- Task and Task<T> return types
- Async/await syntax
- Exception handling in async methods
- Cancellation support
- Async streams

**Attributes:**
- Built-in attributes (Obsolete, Conditional, etc.)
- Custom attributes
- Web API and serialization attributes
- Validation attributes

**Performance:**
- Method inlining optimization
- Avoiding boxing/unboxing
- StringBuilder for string operations
- Span<T> for performance
- ValueTask for async optimization

**Best Practices:**
- Single Responsibility Principle
- Clear naming conventions
- Proper parameter validation
- Appropriate error handling
- Resource management

**Common Pitfalls:**
- Null reference exceptions
- Exception handling issues
- Resource leaks
- Infinite recursion
    - Side effects in properties

C# methods provide a powerful and flexible way to structure code, supporting both object-oriented and functional programming paradigms, with extensive language features for modern application development.
