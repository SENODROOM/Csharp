# C# Multithreading and Parallel Programming

## Thread Basics

### Thread Creation and Management
```csharp
using System;
using System.Threading;

public class ThreadBasics
{
    // Basic thread creation
    public void CreateBasicThread()
    {
        Console.WriteLine($"Main thread: {Thread.CurrentThread.ManagedThreadId}");
        
        // Create and start a thread
        Thread thread = new Thread(WorkerMethod);
        thread.Start();
        
        Console.WriteLine("Thread started");
        
        // Wait for thread to complete
        thread.Join();
        
        Console.WriteLine("Thread completed");
    }
    
    private void WorkerMethod()
    {
        Console.WriteLine($"Worker thread: {Thread.CurrentThread.ManagedThreadId}");
        Thread.Sleep(2000); // Simulate work
        Console.WriteLine("Worker method completed");
    }
    
    // Thread with parameters
    public void ThreadWithParameters()
    {
        // Thread with parameterized start
        Thread thread = new Thread(WorkerMethodWithParam);
        thread.Start("Hello from thread!");
        
        thread.Join();
    }
    
    private void WorkerMethodWithParam(object data)
    {
        Console.WriteLine($"Worker thread received: {data}");
        Thread.Sleep(1000);
    }
    
    // Thread with lambda
    public void ThreadWithLambda()
    {
        Thread thread = new Thread(() =>
        {
            Console.WriteLine($"Lambda thread: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000);
            Console.WriteLine("Lambda thread completed");
        });
        
        thread.Start();
        thread.Join();
    }
    
    // Thread properties
    public void ThreadProperties()
    {
        Thread thread = new Thread(() =>
        {
            Console.WriteLine($"Thread Name: {Thread.CurrentThread.Name}");
            Console.WriteLine($"IsAlive: {Thread.CurrentThread.IsAlive}");
            Console.WriteLine($"IsBackground: {Thread.CurrentThread.IsBackground}");
            Console.WriteLine($"Priority: {Thread.CurrentThread.Priority}");
            Console.WriteLine($"ThreadState: {Thread.CurrentThread.ThreadState}");
        });
        
        // Set thread properties before starting
        thread.Name = "WorkerThread";
        thread.IsBackground = false;
        thread.Priority = ThreadPriority.Normal;
        
        thread.Start();
        thread.Join();
    }
    
    // Thread pool usage
    public void ThreadPoolUsage()
    {
        // Queue work item to thread pool
        ThreadPool.QueueUserWorkItem(state =>
        {
            Console.WriteLine($"Thread pool thread: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"State: {state}");
        }, "Hello from thread pool");
        
        // Wait for thread pool work
        Thread.Sleep(2000);
    }
    
    // Thread with return value using Task
    public async Task ThreadWithReturnValue()
    {
        // Using Task<T> for return value
        Task<int> task = Task.Run(() =>
        {
            Thread.Sleep(1000);
            return 42;
        });
        
        int result = await task;
        Console.WriteLine($"Task result: {result}");
    }
}
```

### Thread Synchronization
```csharp
using System.Threading;

public class ThreadSynchronization
{
    // Lock statement
    private readonly object _lock = new object();
    private int _counter = 0;
    
    public void LockExample()
    {
        // Create multiple threads that modify shared state
        Thread[] threads = new Thread[5];
        
        for (int i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < 1000; j++)
                {
                    // Lock to ensure thread safety
                    lock (_lock)
                    {
                        _counter++;
                        Console.WriteLine($"Counter: {_counter} on thread {Thread.CurrentThread.ManagedThreadId}");
                    }
                }
            });
        }
        
        // Start all threads
        foreach (Thread thread in threads)
        {
            thread.Start();
        }
        
        // Wait for all threads to complete
        foreach (Thread thread in threads)
        {
            thread.Join();
        }
        
        Console.WriteLine($"Final counter value: {_counter}");
    }
    
    // Monitor class
    private readonly object _monitorLock = new object();
    private bool _conditionMet = false;
    
    public void MonitorExample()
    {
        Thread producer = new Thread(ProducerMethod);
        Thread consumer = new Thread(ConsumerMethod);
        
        producer.Start();
        consumer.Start();
        
        Thread.Sleep(5000); // Run for 5 seconds
        
        producer.Interrupt();
        consumer.Interrupt();
        
        producer.Join();
        consumer.Join();
    }
    
    private void ProducerMethod()
    {
        try
        {
            for (int i = 0; i < 10; i++)
            {
                lock (_monitorLock)
                {
                    Console.WriteLine($"Producing item {i}");
                    _conditionMet = true;
                    Monitor.Pulse(_monitorLock);
                    Monitor.Wait(_monitorLock, 1000);
                }
                
                Thread.Sleep(500);
            }
        }
        catch (ThreadInterruptedException)
        {
            Console.WriteLine("Producer interrupted");
        }
    }
    
    private void ConsumerMethod()
    {
        try
        {
            while (true)
            {
                lock (_monitorLock)
                {
                    while (!_conditionMet)
                    {
                        Console.WriteLine("Waiting for condition...");
                        Monitor.Wait(_monitorLock);
                    }
                    
                    Console.WriteLine("Consuming item");
                    _conditionMet = false;
                    Monitor.Pulse(_monitorLock);
                }
                
                Thread.Sleep(1000);
            }
        }
        catch (ThreadInterruptedException)
        {
            Console.WriteLine("Consumer interrupted");
        }
    }
    
    // Mutex example
    public void MutexExample()
    {
        // Named mutex for cross-process synchronization
        using (var mutex = new Mutex(false, "MyMutex"))
        {
            Console.WriteLine("Waiting to acquire mutex...");
            
            // Acquire mutex
            mutex.WaitOne();
            
            try
            {
                Console.WriteLine("Mutex acquired. Doing work...");
                Thread.Sleep(2000);
                Console.WriteLine("Work completed");
            }
            finally
            {
                // Release mutex
                mutex.ReleaseMutex();
                Console.WriteLine("Mutex released");
            }
        }
    }
    
    // Semaphore example
    public void SemaphoreExample()
    {
        // Semaphore allows limited concurrent access
        using (var semaphore = new Semaphore(2, 2)) // 2 max concurrent, 2 initial
        {
            Thread[] threads = new Thread[5];
            
            for (int i = 0; i < threads.Length; i++)
            {
                int threadNum = i;
                threads[i] = new Thread(() =>
                {
                    Console.WriteLine($"Thread {threadNum} waiting for semaphore");
                    
                    semaphore.WaitOne();
                    
                    try
                    {
                        Console.WriteLine($"Thread {threadNum} acquired semaphore");
                        Thread.Sleep(2000);
                        Console.WriteLine($"Thread {threadNum} releasing semaphore");
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });
            }
            
            foreach (Thread thread in threads)
            {
                thread.Start();
            }
            
            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }
    }
}
```

## Parallel Programming

### Parallel.For and Parallel.ForEach
```csharp
using System.Threading.Tasks;

public class ParallelBasics
{
    // Parallel.For example
    public void ParallelForExample()
    {
        int[] numbers = new int[100];
        
        // Initialize array
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = i;
        }
        
        // Parallel processing
        Parallel.For(0, numbers.Length, i =>
        {
            numbers[i] = numbers[i] * numbers[i];
            Console.WriteLine($"Processed index {i} on thread {Thread.CurrentThread.ManagedThreadId}");
        });
        
        Console.WriteLine("Parallel.For completed");
        
        // Display results
        Console.WriteLine("Squared values:");
        for (int i = 0; i < Math.Min(10, numbers.Length); i++)
        {
            Console.WriteLine($"{numbers[i]}");
        }
    }
    
    // Parallel.ForEach example
    public void ParallelForEachExample()
    {
        List<string> items = new List<string>();
        
        for (int i = 0; i < 10; i++)
        {
            items.Add($"Item {i}");
        }
        
        // Parallel processing
        Parallel.ForEach(items, item =>
        {
            Console.WriteLine($"Processing {item} on thread {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000); // Simulate work
            Console.WriteLine($"Completed {item}");
        });
        
        Console.WriteLine("Parallel.ForEach completed");
    }
    
    // Parallel.For with loop options
    public void ParallelForWithOptions()
    {
        ParallelOptions options = new ParallelOptions
        {
            MaxDegreeOfParallelism = 2 // Limit to 2 threads
        };
        
        Parallel.For(0, 10, options, i =>
        {
            Console.WriteLine($"Processing {i} on thread {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(500);
        });
    }
    
    // Parallel.For with stop condition
    public void ParallelForWithStop()
    {
        ParallelLoopResult result = Parallel.For(0, 20, (i, state) =>
        {
            Console.WriteLine($"Processing {i}");
            
            // Stop after processing 10
            if (i >= 10)
            {
                state.Stop();
            }
            
            Thread.Sleep(100);
        });
        
        Console.WriteLine($"Parallel.For stopped early: {result.IsCompleted}");
    }
    
    // Parallel.For with break
    public void ParallelForWithBreak()
    {
        ParallelLoopResult result = Parallel.For(0, 20, (i, state) =>
        {
            Console.WriteLine($"Processing {i}");
            
            // Break when i >= 15
            if (i >= 15)
            {
                state.Break();
            }
            
            Thread.Sleep(100);
        });
        
        Console.WriteLine($"Parallel.For broke early: {result.LowestBreakIteration}");
    }
}
```

### Parallel LINQ (PLINQ)
```csharp
using System.Linq;
using System.Threading.Tasks;

public class ParallelLinq
{
    // Parallel LINQ basics
    public void ParallelLinqBasics()
    {
        int[] numbers = Enumerable.Range(1, 1000000).ToArray();
        
        // Sequential LINQ
        var sequentialResult = numbers
            .Where(n => n % 2 == 0)
            .Select(n => n * n)
            .Take(100);
        
        // Parallel LINQ
        var parallelResult = numbers
            .AsParallel()
            .Where(n => n % 2 == 0)
            .Select(n => n * n)
            .Take(100);
        
        Console.WriteLine($"Sequential result count: {sequentialResult.Count()}");
        Console.WriteLine($"Parallel result count: {parallelResult.Count()}");
    }
    
    // Parallel LINQ with degree of parallelism
    public void ParallelLinqWithDegreeOfParallelism()
    {
        int[] numbers = Enumerable.Range(1, 100000).ToArray();
        
        var result = numbers
            .AsParallel()
            .WithDegreeOfParallelism(4) // Use 4 threads
            .Where(n => n % 3 == 0)
            .Select(n => n * n)
            .Take(50);
        
        Console.WriteLine($"Parallel LINQ result count: {result.Count()}");
    }
    
    // Parallel LINQ with ordering
    public void ParallelLinqOrdering()
    {
        string[] words = { "apple", "banana", "cherry", "date", "elderberry", "fig", "grape" };
        
        var result = words
            .AsParallel()
            .OrderBy(word => word.Length)
            .ThenBy(word => word)
            .Select(word => word.ToUpper());
        
        Console.WriteLine("Parallel LINQ ordered words:");
        foreach (string word in result)
        {
            Console.WriteLine(word);
        }
    }
    
    // Parallel LINQ with aggregation
    public void ParallelLinqAggregation()
    {
        int[] numbers = Enumerable.Range(1, 100000).ToArray();
        
        var sum = numbers
            .AsParallel()
            .Sum();
        
        var average = numbers
            .AsParallel()
            .Average();
        
        var min = numbers
            .AsParallel()
            .Min();
        
        var max = numbers
            .AsParallel()
            .Max();
        
        Console.WriteLine($"Sum: {sum}, Average: {average}, Min: {min}, Max: {max}");
    }
    
    // Parallel LINQ with grouping
    public void ParallelLinqGrouping()
    {
        var people = new[]
        {
            new { Name = "Alice", Age = 25, City = "New York" },
            new { Name = "Bob", Age = 30, City = "Los Angeles" },
            new { Name = "Charlie", Age = 25, City = "New York" },
            new { Name = "Diana", Age = 30, City = "Los Angeles" },
            new { Name = "Eve", Age = 35, City = "Chicago" }
        };
        
        var grouped = people
            .AsParallel()
            .GroupBy(p => p.City)
            .Select(g => new { City = g.Key, Count = g.Count(), AvgAge = g.Average(p => p.Age) });
        
        Console.WriteLine("Parallel LINQ grouping:");
        foreach (var group in grouped)
        {
            Console.WriteLine($"{group.City}: {group.Count} people, Avg Age: {group.AvgAge:F1}");
        }
    }
}
```

### Task Parallel Library (TPL)
```csharp
using System.Threading.Tasks;
using System.Collections.Concurrent;

public class TaskParallelLibrary
{
    // Task.Factory.StartNew
    public async Task TaskFactoryExample()
    {
        // Create multiple tasks
        Task<int>[] tasks = new Task<int>[5];
        
        for (int i = 0; i < tasks.Length; i++)
        {
            int index = i;
            tasks[i] = Task.Run(() =>
            {
                Console.WriteLine($"Task {index} starting on thread {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(1000 * (index + 1));
                Console.WriteLine($"Task {index} completed");
                return index * 10;
            });
        }
        
        // Wait for all tasks
        int[] results = await Task.WhenAll(tasks);
        
        Console.WriteLine("All tasks completed. Results:");
        foreach (int result in results)
        {
            Console.WriteLine(result);
        }
    }
    
    // Task.WhenAll
    public async Task TaskWhenAllExample()
    {
        var task1 = Task.Run(() => { Thread.Sleep(1000); return "Task 1"; });
        var task2 = Task.Run(() => { Thread.Sleep(2000); return "Task 2"; });
        var task3 = Task.Run(() => { Thread.Sleep(1500); return "Task 3"; });
        
        string[] results = await Task.WhenAll(task1, task2, task3);
        
        Console.WriteLine("All tasks completed:");
        foreach (string result in results)
        {
            Console.WriteLine(result);
        }
    }
    
    // Task.WhenAny
    public async Task TaskWhenAnyExample()
    {
        var task1 = Task.Run(() => { Thread.Sleep(3000); return "Task 1"; });
        var task2 = Task.Run(() => { Thread.Sleep(1000); return "Task 2"; });
        var task3 = Task.Run(() => { Thread.Sleep(2000); return "Task 3"; });
        
        Task<string> firstCompleted = await Task.WhenAny(task1, task2, task3);
        
        Console.WriteLine($"First completed: {await firstCompleted}");
    }
    
    // Task.ContinueWith
    public void TaskContinueWithExample()
    {
        Task<int> task = Task.Run(() =>
        {
            Thread.Sleep(1000);
            return 42;
        });
        
        Task continuation = task.ContinueWith(result =>
        {
            Console.WriteLine($"Previous task result: {result}");
            Console.WriteLine("Continuation task completed");
        });
        
        continuation.Wait();
    }
    
    // Task.ContinueWith for multiple continuations
    public void MultipleContinueWithExample()
    {
        Task<int> task = Task.Run(() =>
        {
            Thread.Sleep(1000);
            return 42;
        });
        
        Task continuation1 = task.ContinueWith(result =>
            Console.WriteLine($"Continuation 1: {result}"));
        
        Task continuation2 = task.ContinueWith(result =>
            Console.WriteLine($"Continuation 2: {result}"));
        
        Task continuation3 = task.ContinueWith(result =>
            Console.WriteLine($"Continuation 3: {result}"));
        
        Task.WaitAll(continuation1, continuation2, continuation3);
    }
    
    // TaskCompletionSource
    public void TaskCompletionSourceExample()
    {
        var tcs = new TaskCompletionSource<string>();
        
        // Simulate async operation
        Task.Run(() =>
        {
            Thread.Sleep(2000);
            tcs.SetResult("Operation completed");
        });
        
        Task<string> task = tcs.Task;
        
        try
        {
            string result = task.Result;
            Console.WriteLine(result);
        }
        catch (AggregateException ex)
        {
            Console.WriteLine($"Error: {ex.InnerException?.Message}");
        }
    }
    
    // Concurrent collections
    public void ConcurrentCollectionsExample()
    {
        // ConcurrentBag
        ConcurrentBag<int> bag = new ConcurrentBag<int>();
        
        Parallel.For(0, 10, i =>
        {
            bag.Add(i * i);
        });
        
        Console.WriteLine("ConcurrentBag contents:");
        foreach (int item in bag)
        {
            Console.WriteLine(item);
        }
        
        // ConcurrentDictionary
        ConcurrentDictionary<string, int> dictionary = new ConcurrentDictionary<string, int>();
        
        Parallel.For(0, 5, i =>
        {
            dictionary.TryAdd($"Key{i}", i * i);
        });
        
        Console.WriteLine("\nConcurrentDictionary contents:");
        foreach (var kvp in dictionary)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
        
        // ConcurrentQueue
        ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
        
        Parallel.For(0, 5, i =>
        {
            queue.Enqueue($"Item {i}");
        });
        
        Console.WriteLine("\nConcurrentQueue contents:");
        while (queue.TryDequeue(out string item))
        {
            Console.WriteLine(item);
        }
        
        // ConcurrentStack
        ConcurrentStack<string> stack = new ConcurrentStack<string>();
        
        Parallel.For(0, 5, i =>
        {
            stack.Push($"Item {i}");
        });
        
        Console.WriteLine("\nConcurrentStack contents:");
        while (stack.TryPop(out string item))
        {
            Console.WriteLine(item);
        }
    }
}
```

## Thread Safety

### Thread-Safe Collections
```csharp
using System.Collections.Concurrent;

public class ThreadSafeCollections
{
    // Thread-safe counter
    public class ThreadSafeCounter
    {
        private int _value = 0;
        private readonly object _lock = new object();
        
        public int Value
        {
            get
            {
                lock (_lock)
                {
                    return _value;
                }
            }
        }
        
        public void Increment()
        {
            lock (_lock)
            {
                _value++;
            }
        }
        
        public void Decrement()
        {
            lock (_lock)
            {
                _value--;
            }
        }
    }
    
    // Thread-safe list using ConcurrentBag
    public class ThreadSafeList<T>
    {
        private readonly ConcurrentBag<T> _items = new ConcurrentBag<T>();
        
        public void Add(T item)
        {
            _items.Add(item);
        }
        
        public bool TryTake(out T item)
        {
            return _items.TryTake(out item);
        }
        
        public IEnumerable<T> GetAll()
        {
            return _items.ToArray();
        }
    }
    
    // Thread-safe dictionary
    public class ThreadSafeDictionary<TKey, TValue>
    {
        private readonly ConcurrentDictionary<TKey, TValue> _dictionary = new ConcurrentDictionary<TKey, TValue>();
        
        public bool TryAdd(TKey key, TValue value)
        {
            return _dictionary.TryAdd(key, value);
        }
        
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }
        
        public bool TryRemove(TKey key, out TValue value)
        {
            return _dictionary.TryRemove(key, out value);
        }
        
        public bool TryUpdate(TKey key, Func<TKey, TValue, TValue> updateFactory)
        {
            return _dictionary.TryUpdate(key, updateFactory);
        }
        
        public IEnumerable<KeyValuePair<TKey, TValue>> GetAll()
        {
            return _dictionary.ToArray();
        }
    }
    
    // Thread-safe queue
    public class ThreadSafeQueue<T>
    {
        private readonly ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();
        
        public void Enqueue(T item)
        {
            _queue.Enqueue(item);
        }
        
        public bool TryDequeue(out T item)
        {
            return _queue.TryDequeue(out item);
        }
        
        public int Count => _queue.Count;
        
        public bool IsEmpty => _queue.IsEmpty;
        
        public IEnumerable<T> GetAll()
        {
            return _queue.ToArray();
        }
    }
    
    // Thread-safe stack
    public class ThreadSafeStack<T>
    {
        private readonly ConcurrentStack<T> _stack = new ConcurrentStack<T>();
        
        public void Push(T item)
        {
            _stack.Push(item);
        }
        
        public bool TryPop(out T item)
        {
            return _stack.TryPop(out item);
        }
        
        public bool TryPeek(out T item)
        {
            return _stack.TryPeek(out item);
        }
        
        public int Count => _stack.Count;
        
        public bool IsEmpty => _stack.IsEmpty;
        
        public IEnumerable<T> GetAll()
        {
            return _stack.ToArray();
        }
    }
    
    // Demonstrate thread-safe collections
    public void DemonstrateThreadSafeCollections()
    {
        // Thread-safe counter
        var counter = new ThreadSafeCounter();
        
        Thread[] threads = new Thread[10];
        for (int i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < 1000; j++)
                {
                    counter.Increment();
                    Thread.Sleep(1);
                }
            });
        }
        
        foreach (Thread thread in threads)
        {
            thread.Start();
        }
        
        foreach (Thread thread in threads)
        {
            thread.Join();
        }
        
        Console.WriteLine($"Final counter value: {counter.Value}");
        
        // Thread-safe list
        var list = new ThreadSafeList<int>();
        
        Parallel.For(0, 100, i => list.Add(i));
        
        Console.WriteLine($"\nThread-safe list count: {list.GetAll().Count()}");
        
        // Thread-safe dictionary
        var dictionary = new ThreadSafeDictionary<string, int>();
        
        Parallel.For(0, 50, i => dictionary.TryAdd($"Key{i}", i * i));
        
        Console.WriteLine($"\nThread-safe dictionary count: {dictionary.GetAll().Count()}");
        
        // Thread-safe queue
        var queue = new ThreadSafeQueue<string>();
        
        Parallel.For(0, 20, i => queue.Enqueue($"Item {i}"));
        
        Console.WriteLine($"\nThread-safe queue count: {queue.Count}");
        
        // Thread-safe stack
        var stack = new ThreadSafeStack<string>();
        
        Parallel.For(0, 15, i => stack.Push($"Item {i}"));
        
        Console.WriteLine($"\nThread-safe stack count: {stack.Count}");
    }
}
```

### Synchronization Primitives
```csharp
using System.Threading;

public class SynchronizationPrimitives
{
    // Barrier example
    public void BarrierExample()
    {
        int participants = 3;
        var barrier = new Barrier(participants, (signal) =>
        {
            Console.WriteLine($"All {participants} participants completed phase {signal.CurrentPhaseNumber}");
        });
        
        Action<int> participantAction = (id) =>
        {
            Console.WriteLine($"Participant {id} starting phase 1");
            Thread.Sleep(1000 * id);
            barrier.SignalAndWait(); // Phase 1
            
            Console.WriteLine($"Participant {id} starting phase 2");
            Thread.Sleep(500 * id);
            barrier.SignalAndWait(); // Phase 2
            
            Console.WriteLine($"Participant {id} completed");
        };
        
        Thread[] threads = new Thread[participants];
        for (int i = 0; i < participants; i++)
        {
            threads[i] = new Thread(() => participantAction(i));
        }
        
        foreach (Thread thread in threads)
        {
            thread.Start();
        }
        
        foreach (Thread thread in threads)
        {
            thread.Join();
        }
    }
    
    // CountdownEvent example
    public void CountdownEventExample()
    {
        var countdown = new CountdownEvent(5);
        
        Thread[] threads = new Thread[5];
        for (int i = 0; i < threads.Length; i++)
        {
            int id = i;
            threads[i] = new Thread(() =>
            {
                Console.WriteLine($"Thread {id} starting work");
                Thread.Sleep(1000 * (id + 1));
                Console.WriteLine($"Thread {id} completed work");
                countdown.Signal();
            });
        }
        
        foreach (Thread thread in threads)
        {
            thread.Start();
        }
        
        countdown.Wait(); // Wait for all threads to signal
        Console.WriteLine("All threads completed");
        
        foreach (Thread thread in threads)
        {
            thread.Join();
        }
    }
    
    // ManualResetEventSlim example
    public void ManualResetEventSlimExample()
    {
        var mres = new ManualResetEventSlim(false);
        
        Thread producer = new Thread(() =>
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Producing item {i}");
                mres.Set(); // Signal that item is available
                Thread.Sleep(1000);
                mres.Reset(); // Reset signal
                Thread.Sleep(500);
            }
        });
        
        Thread consumer = new Thread(() =>
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Waiting for item...");
                mres.Wait(); // Wait for signal
                Console.WriteLine("Item received");
                Thread.Sleep(1500);
            }
        });
        
        producer.Start();
        consumer.Start();
        
        producer.Join();
        consumer.Join();
    }
    
    // AutoResetEvent example
    public void AutoResetEventExample()
    {
        var are = new AutoResetEvent(false);
        
        Thread producer = new Thread(() =>
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Producing item {i}");
                Thread.Sleep(1000);
                are.Set(); // Signal that item is available
            }
        });
        
        Thread consumer = new Thread(() =>
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Waiting for item...");
                are.WaitOne(); // Wait for signal
                Console.WriteLine("Item received");
                Thread.Sleep(1500);
            }
        });
        
        producer.Start();
        consumer.Start();
        
        producer.Join();
        consumer.Join();
    }
    
    // ReaderWriterLockSlim example
    public void ReaderWriterLockSlimExample()
    {
        var rwLock = new ReaderWriterLockSlim();
        int sharedData = 0;
        
        Thread[] readers = new Thread[3];
        Thread[] writers = new Thread[2];
        
        // Create reader threads
        for (int i = 0; i < readers.Length; i++)
        {
            int readerId = i;
            readers[i] = new Thread(() =>
            {
                for (int j = 0; j < 10; j++)
                {
                    rwLock.EnterReadLock();
                    try
                    {
                        Console.WriteLine($"Reader {readerId} reading: {sharedData}");
                        Thread.Sleep(500);
                    }
                    finally
                    {
                        rwLock.ExitReadLock();
                    }
                }
            });
        }
        
        // Create writer threads
        for (int i = 0; i < writers.Length; i++)
        {
            int writerId = i;
            writers[i] = new Thread(() =>
            {
                for (int j = 0; j < 5; j++)
                {
                    rwLock.EnterWriteLock();
                    try
                    {
                        sharedData++;
                        Console.WriteLine($"Writer {writerId} writing: {sharedData}");
                        Thread.Sleep(1000);
                    }
                    finally
                    {
                        rwLock.ExitWriteLock();
                    }
                }
            });
        }
        
        // Start all threads
        foreach (Thread reader in readers)
        {
            reader.Start();
        }
        
        foreach (Thread writer in writers)
        {
            writer.Start();
        }
        
        // Wait for all threads to complete
        foreach (Thread thread in readers)
        {
            thread.Join();
        }
        
        foreach (Thread thread in writers)
        {
            thread.Join();
        }
        
        Console.WriteLine($"Final shared data value: {sharedData}");
    }
}
```

## Performance Optimization

### Parallel Performance Tips
```csharp
using System.Threading.Tasks;
using System.Diagnostics;

public class ParallelPerformance
{
    // Measure parallel vs sequential performance
    public void PerformanceComparison()
    {
        const int iterations = 1000000;
        
        // Sequential processing
        var stopwatch = Stopwatch.StartNew();
        int sequentialSum = 0;
        for (int i = 0; i < iterations; i++)
        {
            sequentialSum += i * i;
        }
        stopwatch.Stop();
        
        Console.WriteLine($"Sequential sum: {sequentialSum}, Time: {stopwatch.ElapsedMilliseconds}ms");
        
        // Parallel processing
        stopwatch.Restart();
        int parallelSum = 0;
        object lockObj = new object();
        
        Parallel.For(0, iterations, i =>
        {
            int localSum = i * i;
            lock (lockObj)
            {
                parallelSum += localSum;
            }
        });
        stopwatch.Stop();
        
        Console.WriteLine($"Parallel sum: {parallelSum}, Time: {stopwatch.ElapsedMilliseconds}ms");
    }
    
    // Optimize degree of parallelism
    public void OptimizeParallelism()
    {
        const int iterations = 100000;
        
        // Test different degrees of parallelism
        for (int degree = 1; degree <= Environment.ProcessorCount; degree++)
        {
            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = degree
            };
            
            var stopwatch = Stopwatch.StartNew();
            
            Parallel.For(0, iterations, options, i =>
            {
                // Simulate work
                Thread.Sleep(10);
            });
            
            stopwatch.Stop();
            
            Console.WriteLine($"Degree {degree}: {stopwatch.ElapsedMilliseconds}ms");
        }
    }
    
    // Use Partitioner for load balancing
    public void PartitionerExample()
    {
        int[] numbers = Enumerable.Range(1, 100000).ToArray();
        
        var stopwatch = Stopwatch.StartNew();
        
        // Use Partitioner to divide work
        var partitioner = Partitioner.Create(numbers);
        Parallel.ForEach(partitioner, range =>
        {
            foreach (int number in range)
            {
                // Simulate work
                Thread.Sleep(1);
            }
        });
        
        stopwatch.Stop();
        
        Console.WriteLine($"Partitioner processing: {stopwatch.ElapsedMilliseconds}ms");
    }
    
    // Use thread-local storage
    public void ThreadLocalStorageExample()
    {
        int[] numbers = Enumerable.Range(1, 10000).ToArray();
        
        var stopwatch = Stopwatch.StartNew();
        
        // Use thread-local storage for expensive objects
        Parallel.ForEach(numbers, number =>
        {
            ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random());
            
            // Each thread gets its own Random instance
            random.Value.NextDouble();
        });
        
        stopwatch.Stop();
        
        Console.WriteLine($"Thread-local storage: {stopwatch.ElapsedMilliseconds}ms");
    }
    
    // Avoid false sharing
    public void AvoidFalseSharing()
    {
        // Bad: Shared array can cause false sharing
        int[] sharedArray = new int[1000000];
        
        var stopwatch = Stopwatch.StartNew();
        
        Parallel.For(0, sharedArray.Length, i =>
        {
            sharedArray[i] = i * i;
        });
        
        stopwatch.Stop();
        Console.WriteLine($"False sharing (bad): {stopwatch.ElapsedMilliseconds}ms");
        
        // Good: Use thread-local storage to avoid false sharing
        stopwatch.Restart();
        
        Parallel.For(0, sharedArray.Length, i =>
        {
            ThreadLocal<int[]> localArray = new ThreadLocal<int[]>(() => new int[1000000]);
            localArray.Value[i] = i * i;
        });
        
        stopwatch.Stop();
        Console.WriteLine($"No false sharing (good): {stopwatch.ElapsedMilliseconds}ms");
    }
    
    // Use async patterns for I/O bound operations
    public async Task AsyncVsSync()
    {
        const int iterations = 100;
        
        // Synchronous I/O (bad)
        var stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)
        {
            // Simulate synchronous I/O
            Thread.Sleep(100);
        }
        stopwatch.Stop();
        Console.WriteLine($"Synchronous I/O: {stopwatch.ElapsedMilliseconds}ms");
        
        // Asynchronous I/O (good)
        stopwatch.Restart();
        var tasks = new Task[iterations];
        for (int i = 0; i < iterations; i++)
        {
            tasks[i] = Task.Delay(100);
        }
        
        await Task.WhenAll(tasks);
        stopwatch.Stop();
        Console.WriteLine($"Asynchronous I/O: {stopwatch.ElapsedMilliseconds}ms");
    }
}
```

## Best Practices

### Multithreading Best Practices
```csharp
public class MultithreadingBestPractices
{
    // Use thread-safe collections
    public class ThreadSafeCollections
    {
        // Good: Use ConcurrentDictionary for thread-safe operations
        private readonly ConcurrentDictionary<string, int> _cache = new ConcurrentDictionary<string, int>();
        
        public void AddToCache(string key, int value)
        {
            _cache.TryAdd(key, value);
        }
        
        // Bad: Use regular Dictionary with manual locking
        private readonly Dictionary<string, int> _unsafeCache = new Dictionary<string, int>();
        private readonly object _lock = new object();
        
        public void AddToUnsafeCache(string key, int value)
        {
            lock (_lock)
            {
                _unsafeCache[key] = value;
            }
        }
    }
    
    // Use proper synchronization
    public class ProperSynchronization
    {
        private readonly object _lock = new object();
        private int _value;
        
        // Good: Use lock for short operations
        public int GetValue()
        {
            lock (_lock)
            {
                return _value;
            }
        }
        
        // Good: Use ReaderWriterLock for read-heavy operations
        private readonly ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();
        
        public int GetValueReaderWriter()
        {
            _rwLock.EnterReadLock();
            try
            {
                return _value;
            }
            finally
            {
                _rwLock.ExitReadLock();
            }
        }
        
        public void SetValueReaderWriter(int value)
        {
            _rwLock.EnterWriteLock();
            try
            {
                _value = value;
            }
            finally
            {
                _rwLock.ExitWriteLock();
            }
        }
    }
    
    // Avoid deadlocks
    public class AvoidDeadlocks
    {
        private readonly object _lock1 = new object();
        private readonly object _lock2 = new object();
        
        // Bad: Can cause deadlock
        public void BadDeadlockMethod()
        {
            lock (_lock1)
            {
                lock (_lock2)
                Console.WriteLine("Bad deadlock method");
            }
        }
        
        // Bad: Can cause deadlock
        public void BadDeadlockMethod2()
        {
            lock (_lock2)
            {
                lock (_lock1)
                    Console.WriteLine("Bad deadlock method 2");
            }
        }
        
        // Good: Always acquire locks in the same order
        public void GoodDeadlockMethod()
        {
            lock (_lock1)
            {
                lock (_lock2)
                    Console.WriteLine("Good deadlock method");
            }
        }
        
        // Good: Use timeout to avoid deadlock
        public bool TryDeadlockMethod()
        {
            bool lock1Taken = false;
            try
            {
                Monitor.TryEnter(_lock1, 1000, ref lock1Taken);
                if (lock1Taken)
                {
                    Monitor.Enter(_lock2);
                    try
                    {
                        Console.WriteLine("Try deadlock method");
                        return true;
                    }
                    finally
                    {
                        Monitor.Exit(_lock2);
                    }
                }
            }
            finally
            {
                if (lock1Taken)
                {
                    Monitor.Exit(_lock1);
                }
            }
            
            return false;
        }
    }
    
    // Use cancellation tokens
    public class CancellationBestPractices
    {
        // Good: Accept cancellation token
        public async Task DoWorkAsync(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 100; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                
                // Do work
                await Task.Delay(100, cancellationToken);
                
                // Check cancellation periodically
                if (i % 10 == 0)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
        }
        
        // Good: Pass cancellation token to all async methods
        public async Task NestedAsyncWorkAsync(CancellationToken cancellationToken)
        {
            await DoWorkAsync(cancellationToken);
            await DoMoreWorkAsync(cancellationToken);
        }
        
        private async Task DoMoreWorkAsync(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 50; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(50, cancellationToken);
            }
        }
    }
    
    // Use ConfigureAwait(false) in library code
    public class ConfigureAwaitBestPractices
    {
        // Good: Library code doesn't capture context
        public async Task<string> LibraryMethodAsync()
        {
            using var httpClient = new HttpClient();
            return await httpClient.GetStringAsync("https://example.com").ConfigureAwait(false);
        }
        
        // Good: UI code can capture context if needed
        public async Task UiMethodAsync()
        {
            // UI work
            string result = await LibraryMethodAsync();
            
            // Update UI
            Console.WriteLine($"Result: {result}");
        }
        
        // Bad: Library code that captures context
        public async Task BadLibraryMethodAsync()
        {
            using var httpClient = new HttpClient();
            return await httpClient.GetStringAsync("https://example.com"); // Captures context
        }
    }
    
    // Use ValueTask for optimization
    public class ValueTaskBestPractices
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
    
    // Use async streams for large datasets
    public class AsyncStreamBestPractices
    {
        // Good: Use IAsyncEnumerable for large datasets
        public async IAsyncEnumerable<int> GetLargeDatasetAsync()
        {
            for (int i = 0; i < 1000000; i++)
            {
                yield return i;
                await Task.Delay(1);
            }
        }
        
        // Good: Process without loading all data into memory
        public async Task ProcessLargeDatasetAsync()
        {
            await foreach (int item in GetLargeDatasetAsync())
            {
                // Process item without loading entire dataset
                if (item % 100000 == 0)
                {
                    Console.WriteLine($"Processed {item:N0} items");
                }
            }
        }
        
        // Bad: Load all data into memory
        public async Task<List<int>> BadGetLargeDatasetAsync()
        {
            var allData = new List<int>();
            
            for (int i = 0; i < 1000000; i++)
            {
                allData.Add(await GetDataItemAsync(i));
            }
            
            return allData;
        }
        
        private async Task<int> GetDataItemAsync(int index)
        {
            await Task.Delay(1);
            return index;
        }
    }
}
```

### Error Handling
```csharp
public class MultithreadingErrorHandling
{
    // Handle exceptions in parallel operations
    public void HandleParallelExceptions()
    {
        try
        {
            Parallel.For(0, 10, i =>
            {
                if (i == 5)
                throw new InvalidOperationException($"Error at iteration {i}");
                
                Console.WriteLine($"Processing {i}");
            });
        }
        catch (AggregateException ex)
        {
            Console.WriteLine($"Parallel operation failed: {ex.InnerExceptions.Count} exceptions");
            
            foreach (Exception innerEx in ex.InnerExceptions)
            {
                Console.WriteLine($"  {innerEx.GetType().Name}: {innerEx.Message}");
            }
        }
    }
    
    // Handle exceptions in Task operations
    public async Task HandleTaskExceptions()
    {
        var task1 = Task.Run(() => { throw new InvalidOperationException("Task 1 error"); });
        var task2 = Task.Run(() => { throw new ArgumentException("Task 2 error"); });
        var task3 = Task.Run(() => { return "Task 3 success"; });
        
        try
        {
            await Task.WhenAll(task1, task2, task3);
        }
        catch (AggregateException ex)
        {
            Console.WriteLine($"Task operations failed: {ex.InnerExceptions.Count} exceptions");
            
            foreach (Exception innerEx in ex.InnerExceptions)
            {
                Console.WriteLine($"  {innerEx.GetType().Name}: {innerEx.Message}");
            }
        }
    }
    
    // Handle cancellation gracefully
    public async Task HandleCancellationGracefully()
    {
        using (var cts = new CancellationTokenSource())
        {
            var task = LongRunningOperationAsync(cts.Token);
            
            // Cancel after 2 seconds
            _ = Task.Delay(2000).ContinueWith(_ => cts.Cancel());
            
            try
            {
                await task;
                Console.WriteLine("Operation completed successfully");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation was cancelled");
            }
        }
    }
    
    private async Task LongRunningOperationAsync(CancellationToken cancellationToken)
    {
        for (int i = 0; i < 100; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Delay(100, cancellationToken);
            Console.WriteLine($"Step {i + 1}/100");
        }
    }
    
    // Handle timeout scenarios
    public async Task HandleTimeout()
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
        
        try
        {
            await LongRunningOperationAsync(cts.Token);
            Console.WriteLine("Operation completed within timeout");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation timed out");
        }
    }
    
    // Handle deadlocks with timeout
    public bool TryDeadlockOperation()
    {
        var lock1 = new object();
        var lock2 = new object();
        
        bool lock1Taken = false;
        try
        {
            Monitor.TryEnter(lock1, 1000, ref lock1Taken);
            if (lock1Taken)
            {
                Monitor.TryEnter(lock2, 1000, ref bool lock2Taken);
                if (lock2Taken)
                {
                    Console.WriteLine("Both locks acquired");
                    return true;
                }
            }
        }
        finally
        {
            if (lock1Taken)
            {
                Monitor.Exit(lock1);
            }
        }
        
        return false;
    }
}
```

## Common Pitfalls

### Common Multithreading Mistakes
```csharp
public class CommonMultithreadingMistakes
{
    // Mistake: Race conditions
    public class RaceConditionMistake
    {
        private int _counter = 0;
        
        // Bad: Shared state without synchronization
        public void BadIncrement()
        {
            _counter++; // Race condition!
        }
        
        // Good: Use synchronization
        private readonly object _lock = new object();
        
        public void GoodIncrement()
        {
            lock (_lock)
            {
                _counter++;
            }
        }
        
        // Better: Use Interlocked for simple operations
        public void BetterIncrement()
        {
            Interlocked.Increment(ref _counter);
        }
    }
    
    // Mistake: Deadlock
    public class DeadlockMistake
    {
        private readonly object _lock1 = new object();
        private readonly object _lock2 = new object();
        
        // Bad: Can cause deadlock
        public void BadMethod()
        {
            lock (_lock1)
            {
                lock (_lock2)
                {
                    // Potential deadlock if another thread locks in reverse order
                    Console.WriteLine("In deadlock");
                }
            }
        }
        
        // Good: Always acquire locks in the same order
        public void GoodMethod()
        {
            lock (_lock1)
            {
                lock (_lock2)
                {
                    Console.WriteLine("No deadlock");
                }
            }
        }
    }
    
    // Mistake: Blocking on async code
    public class BlockingMistake
    {
        // Bad: Blocking on async code can cause deadlocks
        public void BadBlocking()
        {
            var task = BadAsyncMethod();
            task.Wait(); // Can cause deadlock
        }
        
        private async Task<string> BadAsyncMethod()
        {
            await Task.Delay(1000);
            return "Result";
        }
        
        // Good: Use async all the way
        public async Task GoodAsync()
        {
            string result = await BadAsyncMethod();
            Console.WriteLine(result);
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
    
    // Mistake: Not using thread-safe collections
    public class CollectionMistake
    {
        // Bad: Using non-thread-safe collections
        private readonly List<string> _list = new List<string>();
        
        public void BadAdd(string item)
        {
            _list.Add(item); // Not thread-safe!
        }
        
        // Good: Use thread-safe collections
        private readonly ConcurrentBag<string> _safeList = new ConcurrentBag<string>();
        
        public void GoodAdd(string item)
        {
            _safeList.Add(item); // Thread-safe
        }
    }
    
    // Mistake: Over-synchronization
    public class OverSynchronizationMistake
    {
        private readonly object _lock = new object();
        private int _value;
        
        // Bad: Unnecessary synchronization for simple operations
        public int BadGetValue()
        {
            lock (_lock)
            {
                return _value; // Overkill for simple read
            }
        }
        
        // Good: Use volatile for simple reads
        private volatile int _volatileValue;
        
        public int GoodGetValue()
        {
            return _volatileValue;
        }
        
        // Still need lock for write operations
        public void SetValue(int value)
        {
            lock (_lock)
            {
                _value = value;
                _volatileValue = value;
            }
        }
    }
    
    // Mistake: Not disposing resources
    public class ResourceMistake
    {
        // Bad: Not disposing resources
        public void BadResourceUsage()
        {
            var resource = new ExpensiveResource();
            resource.DoWork();
            // Resource not disposed!
        }
        
        // Good: Use using statement
        public void GoodResourceUsage()
        {
            using (var resource = new ExpensiveResource())
            {
                resource.DoWork();
            } // Automatically disposed
        }
    }
    
    private class ExpensiveResource : IDisposable
    {
        public void DoWork()
        {
            Console.WriteLine("Doing work");
        }
        
        public void Dispose()
        {
            Console.WriteLine("Resource disposed");
        }
    }
}
```

## Summary

C# multithreading and parallel programming provide:

**Thread Basics:**
- Thread creation and management
- Thread properties and configuration
- Thread pool usage
- Thread synchronization with lock, Monitor, Mutex, Semaphore

**Parallel Programming:**
- Parallel.For and Parallel.ForEach
- Parallel LINQ (PLINQ) for data parallelism
- Task Parallel Library (TPL) with Task.Run, Task.WhenAll, Task.WhenAny
- Task continuation patterns
- Concurrent collections (ConcurrentDictionary, ConcurrentQueue, etc.)

**Thread Safety:**
- Thread-safe collections and patterns
- Synchronization primitives (Barrier, CountdownEvent, ManualResetEventSlim, AutoResetEvent)
- ReaderWriterLockSlim for read-write scenarios
- Avoiding race conditions and deadlocks

**Performance Optimization:**
- Parallel vs sequential performance comparison
- Optimizing degree of parallelism
- Using Partitioner for load balancing
- Thread-local storage to avoid false sharing
- Async vs synchronous I/O patterns

**Best Practices:**
- Use thread-safe collections
- Proper synchronization techniques
- Avoiding deadlocks
- Proper cancellation handling
- ConfigureAwait(false) usage in library code
- ValueTask optimization
- Async streams for large datasets

**Error Handling:**
- Exception handling in parallel operations
- AggregateException handling
- Cancellation handling
- Timeout scenarios
- Deadlock prevention

**Common Pitfalls:**
- Race conditions with shared state
- Deadlock scenarios
- Blocking on async code
- Not handling cancellation
- Not using thread-safe collections
- Over-synchronization
- Resource disposal issues

C# multithreading provides powerful tools for building high-performance, responsive applications while requiring careful attention to thread safety, synchronization, and proper error handling to avoid common pitfalls.
