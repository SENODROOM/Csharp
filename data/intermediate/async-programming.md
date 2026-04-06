# C# Async Programming

## Async/Await Fundamentals

### Basic Async Methods
```csharp
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class AsyncBasics
{
    // Basic async method returning Task
    public async Task<string> DownloadWebPageAsync(string url)
    {
        using (var httpClient = new HttpClient())
        {
            Console.WriteLine($"Downloading {url}...");
            
            // Await the async operation
            string content = await httpClient.GetStringAsync(url);
            
            Console.WriteLine($"Downloaded {content.Length} characters");
            return content;
        }
    }
    
    // Async method returning Task<T>
    public async Task<int> CalculateSumAsync(int a, int b)
    {
        Console.WriteLine("Calculating sum...");
        
        // Simulate async work
        await Task.Delay(1000);
        
        int result = a + b;
        Console.WriteLine($"Sum calculated: {result}");
        
        return result;
    }
    
    // Async method with no return value (Task)
    public async Task ProcessDataAsync()
    {
        Console.WriteLine("Starting data processing...");
        
        // Simulate processing steps
        await Step1Async();
        await Step2Async();
        await Step3Async();
        
        Console.WriteLine("Data processing completed");
    }
    
    private async Task Step1Async()
    {
        Console.WriteLine("Step 1: Validating data");
        await Task.Delay(500);
    }
    
    private async Task Step2Async()
    {
        Console.WriteLine("Step 2: Transforming data");
        await Task.Delay(800);
    }
    
    private async Task Step3Async()
    {
        Console.WriteLine("Step 3: Saving data");
        await Task.Delay(300);
    }
    
    // Calling async methods
    public async Task DemonstrateAsyncCalls()
    {
        // Call async method with await
        string content = await DownloadWebPageAsync("https://example.com");
        Console.WriteLine($"Downloaded content length: {content.Length}");
        
        // Call async method returning value
        int sum = await CalculateSumAsync(10, 20);
        Console.WriteLine($"Sum: {sum}");
        
        // Call async method with no return value
        await ProcessDataAsync();
    }
}
```

### Async Main Method
```csharp
// Async Main method (C# 7.1+)
public class AsyncMain
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Async Main started");
        
        await RunAsyncOperations();
        
        Console.WriteLine("Async Main completed");
    }
    
    private static async Task RunAsyncOperations()
    {
        var downloader = new AsyncBasics();
        await downloader.DemonstrateAsyncCalls();
    }
}

// Traditional Main method calling async code
public class TraditionalMain
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Traditional Main started");
        
        // Use GetAwaiter().GetResult() for synchronous wait
        RunAsyncOperations().GetAwaiter().GetResult();
        
        Console.WriteLine("Traditional Main completed");
    }
    
    private static async Task RunAsyncOperations()
    {
        var downloader = new AsyncBasics();
        await downloader.DemonstrateAsyncCalls();
    }
}
```

## Task Creation and Manipulation

### Task Creation
```csharp
public class TaskCreation
{
    // Create completed task
    public Task CreateCompletedTask()
    {
        return Task.CompletedTask;
    }
    
    // Create task with result
    public Task<int> CreateCompletedTaskWithResult()
    {
        return Task.FromResult(42);
    }
    
    // Create task from result
    public Task<string> CreateTaskFromResult(string result)
    {
        return Task.FromResult(result);
    }
    
    // Create task from exception
    public Task CreateTaskFromException()
    {
        var exception = new InvalidOperationException("Something went wrong");
        return Task.FromException(exception);
    }
    
    // Create task using Task.Run
    public Task<int> CreateTaskWithRun()
    {
        return Task.Run(() =>
        {
            Console.WriteLine("Running on background thread");
            return ComputeExpensiveOperation();
        });
    }
    
    // Create task using Task.Factory.StartNew
    public Task<string> CreateTaskWithFactory()
    {
        return Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Started with Task.Factory");
            return "Task completed";
        });
    }
    
    // Create delayed task
    public Task CreateDelayedTask()
    {
        return Task.Delay(1000);
    }
    
    // Create task that never completes
    public Task CreateNeverCompletingTask()
    {
        return new TaskCompletionSource<bool>().Task;
    }
    
    // Create cancelled task
    public Task CreateCancelledTask()
    {
        var cts = new CancellationTokenSource();
        cts.Cancel();
        
        return Task.FromCanceled(cts.Token);
    }
    
    private int ComputeExpensiveOperation()
    {
        // Simulate expensive computation
        Thread.Sleep(2000);
        return 123;
    }
}
```

### Task Composition
```csharp
public class TaskComposition
{
    // Run multiple tasks sequentially
    public async Task SequentialTasks()
    {
        Console.WriteLine("Starting sequential tasks");
        
        await Task1();
        await Task2();
        await Task3();
        
        Console.WriteLine("Sequential tasks completed");
    }
    
    // Run multiple tasks in parallel
    public async Task ParallelTasks()
    {
        Console.WriteLine("Starting parallel tasks");
        
        var task1 = Task1();
        var task2 = Task2();
        var task3 = Task3();
        
        await Task.WhenAll(task1, task2, task3);
        
        Console.WriteLine("Parallel tasks completed");
    }
    
    // Run tasks with results
    public async Task<(int, string, double)> TasksWithResults()
    {
        Console.WriteLine("Starting tasks with results");
        
        var task1 = Task.Run(() => ComputeInt());
        var task2 = Task.Run(() => ComputeString());
        var task3 = Task.Run(() => ComputeDouble());
        
        await Task.WhenAll(task1, task2, task3);
        
        int intResult = await task1;
        string stringResult = await task2;
        double doubleResult = await task3;
        
        Console.WriteLine("Tasks with results completed");
        
        return (intResult, stringResult, doubleResult);
    }
    
    // Continue with task
    public Task ContinueWithTask()
    {
        var task = Task.Run(() => ComputeInt());
        
        return task.ContinueWith(result =>
        {
            Console.WriteLine($"Task completed with result: {result}");
        });
    }
    
    // Continue with multiple tasks
    public Task ContinueWithMultipleTasks()
    {
        var task1 = Task.Run(() => ComputeInt());
        var task2 = Task.Run(() => ComputeString());
        
        return Task.WhenAll(task1, task2).ContinueWith(results =>
        {
            Console.WriteLine("All tasks completed");
        });
    }
    
    // Task timeout
    public async Task<bool> TaskWithTimeout(TimeSpan timeout)
    {
        var task = Task.Run(() => LongRunningOperation());
        
        var timeoutTask = Task.Delay(timeout);
        
        var completedTask = await Task.WhenAny(task, timeoutTask);
        
        if (completedTask == task)
        {
            Console.WriteLine("Operation completed");
            return true;
        }
        else
        {
            Console.WriteLine("Operation timed out");
            return false;
        }
    }
    
    private async Task Task1()
    {
        await Task.Delay(1000);
        Console.WriteLine("Task 1 completed");
    }
    
    private async Task Task2()
    {
        await Task.Delay(1500);
        Console.WriteLine("Task 2 completed");
    }
    
    private async Task Task3()
    {
        await Task.Delay(800);
        Console.WriteLine("Task 3 completed");
    }
    
    private int ComputeInt()
    {
        Thread.Sleep(1000);
        return 42;
    }
    
    private string ComputeString()
    {
        Thread.Sleep(800);
        return "Hello, World!";
    }
    
    private double ComputeDouble()
    {
        Thread.Sleep(1200);
        return 3.14159;
    }
    
    private void LongRunningOperation()
    {
        Thread.Sleep(3000);
        Console.WriteLine("Long running operation completed");
    }
}
```

## Error Handling in Async Code

### Exception Handling
```csharp
public class AsyncErrorHandling
{
    // Try-catch in async methods
    public async Task<string> SafeDownloadAsync(string url)
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                return await httpClient.GetStringAsync(url);
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP error: {ex.Message}");
            return null;
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("Request was cancelled");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            return null;
        }
    }
    
    // Handle multiple exceptions
    public async Task HandleMultipleExceptions()
    {
        var tasks = new List<Task>
        {
            Task.Run(() => ThrowException<InvalidOperationException>("Invalid operation")),
            Task.Run(() => ThrowException<ArgumentException>("Invalid argument")),
            Task.Run(() => ThrowException<NullReferenceException>("Null reference"))
        };
        
        try
        {
            await Task.WhenAll(tasks);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Caught exception: {ex.GetType().Name}: {ex.Message}");
            
            // Handle aggregate exceptions
            if (ex is AggregateException aggregateEx)
            {
                foreach (var innerEx in aggregateEx.InnerExceptions)
                {
                    Console.WriteLine($"Inner exception: {innerEx.GetType().Name}: {innerEx.Message}");
                }
            }
        }
    }
    
    // Retry logic
    public async Task<string> RetryDownloadAsync(string url, int maxRetries = 3)
    {
        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            try
            {
                Console.WriteLine($"Attempt {attempt} of {maxRetries}");
                
                using (var httpClient = new HttpClient())
                {
                    return await httpClient.GetStringAsync(url);
                }
            }
            catch (Exception ex) when (attempt < maxRetries)
            {
                Console.WriteLine($"Attempt {attempt} failed: {ex.Message}. Retrying...");
                await Task.Delay(1000 * attempt); // Exponential backoff
            }
        }
        
        throw new Exception($"Failed to download after {maxRetries} attempts");
    }
    
    // Exception filtering (C# 6.0+)
    public async Task ExceptionFiltering()
    {
        try
        {
            await Task.Run(() => throw new InvalidOperationException("Invalid operation"));
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Invalid"))
        {
            Console.WriteLine($"Filtered exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General exception: {ex.Message}");
        }
    }
    
    private Task ThrowException<T>(string message) where T : Exception, new()
    {
        return Task.FromException<T>(new T { Message = message });
    }
}
```

### Cancellation
```csharp
public class AsyncCancellation
{
    // Cancellable operation
    public async Task CancellableOperation(CancellationToken cancellationToken)
    {
        for (int i = 0; i < 10; i++)
        {
            // Check for cancellation
            cancellationToken.ThrowIfCancellationRequested();
            
            Console.WriteLine($"Processing step {i + 1}/10");
            
            // Simulate work
            await Task.Delay(1000, cancellationToken);
        }
        
        Console.WriteLine("Operation completed successfully");
    }
    
    // Cancellable operation with manual cancellation checking
    public async Task ManualCancellationCheck(CancellationToken cancellationToken)
    {
        for (int i = 0; i < 10; i++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Cancellation requested, cleaning up...");
                // Perform cleanup
                cancellationToken.ThrowIfCancellationRequested();
            }
            
            Console.WriteLine($"Processing step {i + 1}/10");
            await Task.Delay(1000);
        }
    }
    
    // Demonstrate cancellation
    public async Task DemonstrateCancellation()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;
        
        // Start cancellable operation
        var operation = CancellableOperation(token);
        
        // Cancel after 3 seconds
        _ = Task.Delay(3000).ContinueWith(_ => cts.Cancel());
        
        try
        {
            await operation;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation was cancelled");
        }
    }
    
    // Timeout as cancellation
    public async Task TimeoutAsCancellation()
    {
        using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2)))
        {
            try
            {
                await LongRunningOperation(cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation timed out");
            }
        }
    }
    
    // Multiple cancellation tokens
    public async Task MultipleCancellationTokens()
    {
        var cts1 = new CancellationTokenSource();
        var cts2 = new CancellationTokenSource();
        
        var combinedToken = CancellationTokenSource.CreateLinkedTokenSource(cts1.Token, cts2.Token).Token;
        
        try
        {
            await CancellableOperation(combinedToken);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation was cancelled by one of the cancellation sources");
        }
    }
    
    private async Task LongRunningOperation(CancellationToken cancellationToken)
    {
        for (int i = 0; i < 20; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Console.WriteLine($"Long operation step {i + 1}/20");
            await Task.Delay(500, cancellationToken);
        }
    }
}
```

## Async Streams (IAsyncEnumerable)

### Async Streams Basics
```csharp
public class AsyncStreams
{
    // Generate async sequence
    public async IAsyncEnumerable<int> GenerateNumbersAsync(int count)
    {
        for (int i = 0; i < count; i++)
        {
            await Task.Delay(100); // Simulate async work
            yield return i;
        }
    }
    
    // Generate async sequence with cancellation
    public async IAsyncEnumerable<int> GenerateNumbersWithCancellationAsync(
        int count, 
        CancellationToken cancellationToken = default)
    {
        for (int i = 0; i < count; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Delay(100, cancellationToken);
            yield return i;
        }
    }
    
    // Consume async stream
    public async Task ConsumeAsyncStream()
    {
        Console.WriteLine("Consuming async stream:");
        
        await foreach (int number in GenerateNumbersAsync(10))
        {
            Console.WriteLine($"Received: {number}");
        }
    }
    
    // Consume async stream with cancellation
    public async Task ConsumeAsyncStreamWithCancellation()
    {
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
        
        try
        {
            await foreach (int number in GenerateNumbersWithCancellationAsync(20, cts.Token))
            {
                Console.WriteLine($"Received: {number}");
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Async stream consumption was cancelled");
        }
    }
    
    // Transform async stream
    public async IAsyncEnumerable<string> TransformAsyncStream(
        IAsyncEnumerable<int> numbers)
    {
        await foreach (int number in numbers)
        {
            await Task.Delay(50); // Simulate async transformation
            yield return $"Number: {number}";
        }
    }
    
    // Filter async stream
    public async IAsyncEnumerable<int> FilterAsyncStream(
        IAsyncEnumerable<int> numbers,
        Func<int, bool> predicate)
    {
        await foreach (int number in numbers)
        {
            if (predicate(number))
            {
                yield return number;
            }
        }
    }
    
    // Demonstrate async stream operations
    public async Task DemonstrateAsyncStreams()
    {
        // Generate and consume
        await ConsumeAsyncStream();
        
        // Transform stream
        Console.WriteLine("\nTransformed stream:");
        var numbers = GenerateNumbersAsync(5);
        var transformed = TransformAsyncStream(numbers);
        
        await foreach (string item in transformed)
        {
            Console.WriteLine(item);
        }
        
        // Filter stream
        Console.WriteLine("\nFiltered stream (even numbers only):");
        var allNumbers = GenerateNumbersAsync(10);
        var evenNumbers = FilterAsyncStream(allNumbers, n => n % 2 == 0);
        
        await foreach (int number in evenNumbers)
        {
            Console.WriteLine(number);
        }
    }
}
```

## Async Synchronization Context

### ConfigureAwait
```csharp
public class ConfigureAwaitExamples
{
    // Without ConfigureAwait (captures context)
    public async Task WithoutConfigureAwait()
    {
        Console.WriteLine($"Before await: Thread {Thread.CurrentThread.ManagedThreadId}");
        
        await Task.Delay(1000);
        
        Console.WriteLine($"After await: Thread {Thread.CurrentThread.ManagedThreadId}");
    }
    
    // With ConfigureAwait(false) (doesn't capture context)
    public async Task WithConfigureAwaitFalse()
    {
        Console.WriteLine($"Before await: Thread {Thread.CurrentThread.ManagedThreadId}");
        
        await Task.Delay(1000).ConfigureAwait(false);
        
        Console.WriteLine($"After await: Thread {Thread.CurrentThread.ManagedThreadId}");
    }
    
    // ConfigureAwait in UI context
    public async Task UiContextExample()
    {
        // In UI applications, ConfigureAwait(false) can prevent deadlocks
        // when calling async code from UI thread
        
        Console.WriteLine("Starting UI context example");
        
        // This would typically be called from UI thread
        await Task.Run(() => 
        {
            // Background work
            Thread.Sleep(1000);
            Console.WriteLine("Background work completed");
        }).ConfigureAwait(false);
        
        // Back on UI thread (if ConfigureAwait(false) wasn't used)
        Console.WriteLine("Back on UI thread");
    }
    
    // ConfigureAwait with exception handling
    public async Task ConfigureAwaitWithExceptionHandling()
    {
        try
        {
            await Task.Run(() => throw new InvalidOperationException("Test exception"))
                .ConfigureAwait(false);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Caught exception: {ex.Message}");
        }
    }
    
    // Multiple ConfigureAwait calls
    public async Task MultipleConfigureAwait()
    {
        await Task.Delay(100).ConfigureAwait(false);
        await Task.Run(() => Console.WriteLine("Task 1")).ConfigureAwait(false);
        await Task.Run(() => Console.WriteLine("Task 2")).ConfigureAwait(false);
        
        Console.WriteLine("All tasks completed");
    }
}
```

### Synchronization Context
```csharp
public class SynchronizationContext
{
    // Custom synchronization context
    public class CustomSynchronizationContext : SynchronizationContext
    {
        public override void Post(SendOrPostCallback callback, object state)
        {
            // Post to the synchronization context
            ThreadPool.QueueUserWorkItem(_ => callback(state), null);
        }
        
        public override void Send(SendOrPostCallback callback, object state)
        {
            // Send to the synchronization context (synchronous)
            callback(state);
        }
    }
    
    // Run with custom synchronization context
    public async Task RunWithCustomContext()
    {
        var customContext = new CustomSynchronizationContext();
        
        SynchronizationContext.SetSynchronizationContext(customContext);
        
        try
        {
            Console.WriteLine("Running with custom synchronization context");
            
            await Task.Run(() => 
            {
                Console.WriteLine("Background work in custom context");
                Thread.Sleep(1000);
            });
            
            Console.WriteLine("Back in custom context");
        }
        finally
        {
            SynchronizationContext.SetSynchronizationContext(null);
        }
    }
    
    // Async void method (avoid in production)
    public async void AsyncVoidMethod()
    {
        Console.WriteLine("Starting async void method");
        
        await Task.Delay(1000);
        
        Console.WriteLine("Async void method completed");
        
        // Note: async void methods can't be awaited and can cause issues
        // Use Task or Task<T> instead
    }
    
    // Fire and forget (with error handling)
    public void FireAndForget()
    {
        Task.Run(async () =>
        {
            try
            {
                await Task.Delay(1000);
                Console.WriteLine("Fire and forget completed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in fire and forget: {ex.Message}");
            }
        });
    }
    
    // Async lambda
    public Func<Task<int>> CreateAsyncLambda()
    {
        return async () =>
        {
            await Task.Delay(500);
            return 42;
        };
    }
    
    // Use async lambda
    public async Task UseAsyncLambda()
    {
        var asyncLambda = CreateAsyncLambda();
        
        int result = await asyncLambda();
        Console.WriteLine($"Async lambda result: {result}");
    }
}
```

## Async Design Patterns

### Repository Pattern with Async
```csharp
public interface IRepository<T>
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}

public class Repository<T> : IRepository<T> where T : class
{
    private readonly List<T> _entities = new List<T>();
    
    public async Task<T> GetByIdAsync(int id)
    {
        await Task.Delay(100); // Simulate database access
        return _entities.FirstOrDefault(e => GetId(e) == id);
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        await Task.Delay(200); // Simulate database access
        return _entities.ToList();
    }
    
    public async Task AddAsync(T entity)
    {
        await Task.Delay(50); // Simulate database access
        _entities.Add(entity);
    }
    
    public async Task UpdateAsync(T entity)
    {
        await Task.Delay(75); // Simulate database access
        // Update logic here
    }
    
    public async Task DeleteAsync(int id)
    {
        await Task.Delay(50); // Simulate database access
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _entities.Remove(entity);
        }
    }
    
    private int GetId(T entity)
    {
        // Simplified - in real scenario, use reflection or interfaces
        return 0;
    }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public class UserRepository : Repository<User>
{
    public async Task<User> GetByEmailAsync(string email)
    {
        await Task.Delay(100);
        return (await GetAllAsync()).FirstOrDefault(u => u.Email == email);
    }
    
    public async Task<IEnumerable<User>> GetUsersByCityAsync(string city)
    {
        await Task.Delay(150);
        return (await GetAllAsync()).Where(u => u.City == city);
    }
}
```

### Factory Pattern with Async
```csharp
public interface IAsyncFactory<T>
{
    Task<T> CreateAsync();
}

public class DatabaseConnectionFactory : IAsyncFactory<IDbConnection>
{
    private readonly string _connectionString;
    
    public DatabaseConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task<IDbConnection> CreateAsync()
    {
        await Task.Delay(100); // Simulate connection setup
        // In real scenario, create actual database connection
        return new MockDbConnection(_connectionString);
    }
}

public interface IDbConnection
{
    Task OpenAsync();
    Task CloseAsync();
    Task ExecuteAsync(string sql);
}

public class MockDbConnection : IDbConnection
{
    private readonly string _connectionString;
    
    public MockDbConnection(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task OpenAsync()
    {
        await Task.Delay(200);
        Console.WriteLine($"Database connection opened: {_connectionString}");
    }
    
    public async Task CloseAsync()
    {
        await Task.Delay(100);
        Console.WriteLine("Database connection closed");
    }
    
    public async Task ExecuteAsync(string sql)
    {
        await Task.Delay(150);
        Console.WriteLine($"Executing: {sql}");
    }
}
```

### Observer Pattern with Async
```csharp
public interface IAsyncObserver<T>
{
    Task OnNextAsync(T value);
    Task OnErrorAsync(Exception error);
    Task OnCompletedAsync();
}

public interface IAsyncObservable<T>
{
    IDisposable Subscribe(IAsyncObserver<T> observer);
}

public class AsyncObservable<T> : IAsyncObservable<T>
{
    private readonly List<IAsyncObserver<T>> _observers = new List<IAsyncObserver<T>>();
    
    public IDisposable Subscribe(IAsyncObserver<T> observer)
    {
        _observers.Add(observer);
        return new Disposable(() => _observers.Remove(observer));
    }
    
    public async Task NotifyObserversAsync(T value)
    {
        var tasks = _observers.Select(observer => observer.OnNextAsync(value));
        await Task.WhenAll(tasks);
    }
    
    public async Task NotifyErrorAsync(Exception error)
    {
        var tasks = _observers.Select(observer => observer.OnErrorAsync(error));
        await Task.WhenAll(tasks);
    }
    
    public async Task NotifyCompletedAsync()
    {
        var tasks = _observers.Select(observer => observer.OnCompletedAsync());
        await Task.WhenAll(tasks);
    }
    
    private class Disposable : IDisposable
    {
        private readonly Action _dispose;
        
        public Disposable(Action dispose)
        {
            _dispose = dispose;
        }
        
        public void Dispose()
        {
            _dispose();
        }
    }
}

public class AsyncConsoleObserver<T> : IAsyncObserver<T>
{
    public async Task OnNextAsync(T value)
    {
        await Task.Delay(50);
        Console.WriteLine($"Received: {value}");
    }
    
    public async Task OnErrorAsync(Exception error)
    {
        await Task.Delay(50);
        Console.WriteLine($"Error: {error.Message}");
    }
    
    public async Task OnCompletedAsync()
    {
        await Task.Delay(50);
        Console.WriteLine("Completed");
    }
}
```

## Best Practices

### Async Best Practices
```csharp
public class AsyncBestPractices
{
    // Always use ConfigureAwait(false) in library code
    public class LibraryCode
    {
        public async Task<string> GetDataAsync()
        {
            using var httpClient = new HttpClient();
            return await httpClient.GetStringAsync("https://example.com").ConfigureAwait(false);
        }
    }
    
    // Avoid async void in library code
    public class LibraryCodeGood
    {
        public Task ProcessDataAsync()
        {
            return ProcessDataInternalAsync();
        }
        
        private async Task ProcessDataInternalAsync()
        {
            // Implementation
            await Task.Delay(1000).ConfigureAwait(false);
        }
    }
    
    // Use ValueTask for synchronous completion
    public class ValueTaskExample
    {
        private int _value = 42;
        
        public ValueTask<int> GetValueAsync()
        {
            // If the value is already available, return ValueTask
            return new ValueTask<int>(_value);
        }
        
        public async ValueTask<int> ComputeValueAsync()
        {
            // If async work is needed, use ValueTask
            await Task.Delay(1000);
            return new ValueTask<int>(42);
        }
    }
    
    // Handle cancellation properly
    public class CancellationExample
    {
        public async Task ProcessWithCancellationAsync(CancellationToken cancellationToken)
        {
            // Pass cancellation token to all async methods
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
            
            // Check for cancellation periodically
            cancellationToken.ThrowIfCancellationRequested();
            
            // Use cancellation token in LINQ
            var numbers = Enumerable.Range(1, 100);
            var result = await numbers
                .Where(n => n % 2 == 0)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
    
    // Use async/await instead of Task.Wait
    public class AvoidBlocking
    {
        public async Task<int> GetSumAsync()
        {
            var task1 = Task.Run(() => 1);
            var task2 = Task.Run(() => 2);
            
            // Good: Use await
            int result = await task1 + await task2;
            return result;
            
            // Bad: Use Wait (can cause deadlocks)
            // int badResult = task1.Result + task2.Result;
        }
    }
    
    // Use async streams for large datasets
    public class AsyncStreamExample
    {
        public async IAsyncEnumerable<int> GetLargeDatasetAsync()
        {
            for (int i = 0; i < 1000000; i++)
            {
                yield return i;
                await Task.Delay(1); // Simulate async work
            }
        }
        
        public async Task ProcessLargeDatasetAsync()
        {
            await foreach (int item in GetLargeDatasetAsync())
            {
                // Process item without loading entire dataset into memory
                if (item % 100000 == 0)
                {
                    Console.WriteLine($"Processed {item:N0} items");
                }
            }
        }
    }
    
    // Use ConfigureAwait(false) consistently
    public class ConsistentConfigureAwait
    {
        public async Task<string> GetDataAsync()
        {
            // Use ConfigureAwait(false) consistently
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://example.com").ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return content;
        }
    }
}
```

### Performance Considerations
```csharp
public class AsyncPerformance
{
    // Avoid async over-synchronization
    public class AsyncOverSynchronization
    {
        // Bad: Too many small async operations
        public async Task BadExample()
        {
            await Task.Delay(1);
            await Task.Delay(1);
            await Task.Delay(1);
            await Task.Delay(1);
            await Task.Delay(1);
        }
        
        // Good: Batch small operations
        public async Task GoodExample()
        {
            await Task.Delay(5); // Single delay
        }
        
        // Good: Use Task.WhenAll for parallel operations
        public async Task ParallelExample()
        {
            var task1 = Task.Delay(1000);
            var task2 = Task.Delay(1000);
            var task3 = Task.Delay(1000);
            
            await Task.WhenAll(task1, task2, task3);
        }
    }
    
    // Use ValueTask for optimization
    public class ValueTaskOptimization
    {
        private readonly Dictionary<string, string> _cache = new Dictionary<string, string>();
        
        // Good: Use ValueTask for potentially synchronous operations
        public ValueTask<string> GetCachedValueAsync(string key)
        {
            if (_cache.TryGetValue(key, out string value))
            {
                return new ValueTask<string>(value);
            }
            
            return new ValueTask<string>(GetValueFromDatabaseAsync(key));
        }
        
        private async Task<string> GetValueFromDatabaseAsync(string key)
        {
            await Task.Delay(1000);
            string value = $"Value for {key}";
            _cache[key] = value;
            return value;
        }
    }
    
    // Use async streams for memory efficiency
    public class MemoryEfficientAsync
    {
        // Bad: Load all data into memory
        public async Task<List<int>> BadLoadAllDataAsync()
        {
            var allData = new List<int>();
            
            for (int i = 0; i < 1000000; i++)
            {
                allData.Add(await GetDataItemAsync(i));
            }
            
            return allData;
        }
        
        // Good: Use async stream
        public async IAsyncEnumerable<int> GoodLoadDataAsync()
        {
            for (int i = 0; i < 1000000; i++)
            {
                yield return await GetDataItemAsync(i);
            }
        }
        
        private async Task<int> GetDataItemAsync(int index)
        {
            await Task.Delay(1);
            return index * 2;
        }
    }
}
```

## Common Pitfalls

### Common Async Mistakes
```csharp
public class CommonAsyncMistakes
{
    // Mistake: Async void methods
    public class AsyncVoidMistake
    {
        // Bad: Async void can't be awaited and can crash the application
        public async void BadAsyncVoid()
        {
            await Task.Delay(1000);
            throw new InvalidOperationException("Something went wrong");
        }
        
        // Good: Return Task
        public async Task GoodAsync()
        {
            await Task.Delay(1000);
            // Exceptions can be properly handled
        }
    }
    
    // Mistake: Blocking on async code
    public class BlockingMistake
    {
        // Bad: Blocking can cause deadlocks
        public void BadBlocking()
        {
            var result = BadAsync().Result; // Can deadlock
            Console.WriteLine(result);
        }
        
        // Good: Use async all the way
        public async Task GoodAsync()
        {
            var result = await GoodAsync();
            Console.WriteLine(result);
        }
        
        private async Task<string> BadAsync()
        {
            await Task.Delay(1000);
            return "Result";
        }
        
        private async Task<string> GoodAsync()
        {
            await Task.Delay(1000);
            return "Result";
        }
    }
    
    // Mistake: Not handling cancellation
    public class CancellationMistake
    {
        // Bad: No cancellation support
        public async Task BadLongOperation()
        {
            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(1000); // Can't be cancelled
            }
        }
        
        // Good: Support cancellation
        public async Task GoodLongOperation(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 100; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(1000, cancellationToken);
            }
        }
    }
    
    // Mistake: Not using ConfigureAwait(false) in library code
    public class ConfigureAwaitMistake
    {
        // Bad: Library code captures context
        public async Task<string> BadLibraryMethod()
        {
            using var httpClient = new HttpClient();
            return await httpClient.GetStringAsync("https://example.com");
        }
        
        // Good: Library code doesn't capture context
        public async Task<string> GoodLibraryMethod()
        {
            using var httpClient = new HttpClient();
            return await httpClient.GetStringAsync("https://example.com").ConfigureAwait(false);
        }
    }
    
    // Mistake: Async lambda issues
    public class AsyncLambdaMistake
    {
        // Bad: Async lambda in non-async context
        public void BadAsyncLambda()
        {
            // This won't work as expected
            var task = Task.Run(async () =>
            {
                await Task.Delay(1000);
                return "Done";
            });
            
            task.Wait(); // Blocking
        }
        
        // Good: Proper async lambda usage
        public async Task GoodAsyncLambda()
        {
            var result = await Task.Run(async () =>
            {
                await Task.Delay(1000);
                return "Done";
            });
            
            Console.WriteLine(result);
        }
    }
    
    // Mistake: Exception handling issues
    public class ExceptionHandlingMistake
    {
        // Bad: Not catching exceptions properly
        public async Task BadExceptionHandling()
        {
            await Task.Run(() => throw new InvalidOperationException("Error"));
            // Exception will be lost
        }
        
        // Good: Proper exception handling
        public async Task GoodExceptionHandling()
        {
            try
            {
                await Task.Run(() => throw new InvalidOperationException("Error"));
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Caught: {ex.Message}");
            }
        }
    }
}
```

## Summary

C# async programming provides:

**Async/Await Fundamentals:**
- Task and Task<T> return types
- Async method syntax
- Async Main method
- Deferred execution

**Task Creation:**
- Task.FromResult for completed tasks
- Task.Run for CPU-bound work
- Task.Factory.StartNew
- Task.Delay for delays

**Task Composition:**
- Task.WhenAll for parallel execution
- Task.WhenAny for waiting for first completion
- ContinueWith for chaining operations
- Task timeout handling

**Error Handling:**
- Try-catch in async methods
- AggregateException handling
- Exception filtering
- Retry logic implementation

**Cancellation:**
- CancellationToken and CancellationTokenSource
- Cooperative cancellation
- Timeout as cancellation
- Multiple cancellation tokens

**Async Streams:**
- IAsyncEnumerable for async sequences
- await foreach for consuming streams
- Stream transformation and filtering
- Cancellation in streams

**Synchronization Context:**
- ConfigureAwait(false) for performance
- Custom synchronization contexts
- Async void method issues
- Fire and forget patterns

**Design Patterns:**
- Async repository pattern
- Async factory pattern
- Async observer pattern
- Async command pattern

**Best Practices:**
- Use ConfigureAwait(false) in library code
- Avoid async void methods
- Handle cancellation properly
- Use ValueTask for optimization
- Use async streams for large datasets

**Performance Considerations:**
- Avoid async over-synchronization
- Use Task.WhenAll for parallel operations
- Use ValueTask for optimization
- Memory-efficient async streams

**Common Pitfalls:**
- Async void methods
- Blocking on async code
- Not handling cancellation
- Not using ConfigureAwait(false)
- Exception handling issues

C# async programming provides powerful tools for writing scalable, responsive applications while maintaining code readability and maintainability.
