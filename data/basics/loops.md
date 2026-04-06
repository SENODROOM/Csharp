# C# Loops and Iteration

## For Loops

### Basic For Loop
```csharp
public class ForLoopExamples
{
    // Basic for loop
    public void PrintNumbers()
    {
        for (int i = 1; i <= 10; i++)
        {
            Console.WriteLine($"Number: {i}");
        }
    }
    
    // For loop with custom step
    public void PrintEvenNumbers()
    {
        for (int i = 0; i <= 20; i += 2)
        {
            Console.WriteLine($"Even number: {i}");
        }
    }
    
    // Reverse for loop
    public void CountDown()
    {
        for (int i = 10; i >= 0; i--)
        {
            Console.WriteLine($"Countdown: {i}");
        }
    }
    
    // For loop with multiple variables
    public void PrintMultiplicationTable(int number)
    {
        for (int i = 1; i <= 10; i++)
        {
            int result = number * i;
            Console.WriteLine($"{number} x {i} = {result}");
        }
    }
    
    // For loop with array iteration
    public void PrintArray(int[] numbers)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            Console.WriteLine($"Index {i}: {numbers[i]}");
        }
    }
    
    // For loop with collection
    public void PrintList(List<string> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"Item {i}: {items[i]}");
        }
    }
}
```

### Nested For Loops
```csharp
public class NestedForLoops
{
    // Nested for loops for multiplication table
    public void PrintMultiplicationGrid(int rows, int columns)
    {
        for (int i = 1; i <= rows; i++)
        {
            for (int j = 1; j <= columns; j++)
            {
                Console.Write($"{i * j}\t");
            }
            Console.WriteLine();
        }
    }
    
    // Nested loops for 2D array
    public void PrintMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"[{i},{j}] = {matrix[i, j]}\t");
            }
            Console.WriteLine();
        }
    }
    
    // Nested loops for combinations
    public void GenerateCombinations(string[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            for (int j = i + 1; j < items.Length; j++)
            {
                Console.WriteLine($"{items[i]} - {items[j]}");
            }
        }
    }
    
    // Nested loops with break and continue
    public void FindPrimeNumbers(int max)
    {
        for (int i = 2; i <= max; i++)
        {
            bool isPrime = true;
            
            for (int j = 2; j <= Math.Sqrt(i); j++)
            {
                if (i % j == 0)
                {
                    isPrime = false;
                    break;
                }
            }
            
            if (isPrime)
            {
                Console.WriteLine($"Prime: {i}");
            }
        }
    }
}
```

### Enhanced For Loop (C# 8.0+)
```csharp
public class EnhancedForLoop
{
    // Using for with ranges
    public void PrintRange()
    {
        for (int i = 1; i <= 10; i++)
        {
            Console.WriteLine(i);
        }
    }
    
    // For loop with index (C# 8.0)
    public void PrintWithIndex()
    {
        string[] fruits = { "Apple", "Banana", "Cherry", "Date" };
        
        foreach ((fruit, index) in fruits.Select((value, index) => (value, index)))
        {
            Console.WriteLine($"{index}: {fruit}");
        }
    }
    
    // For loop with tuple deconstruction
    public void ProcessTuples()
    {
        var people = new[]
        {
            ("John", 25),
            ("Jane", 30),
            ("Bob", 35)
        };
        
        foreach ((name, age) in people)
        {
            Console.WriteLine($"{name} is {age} years old");
        }
    }
    
    // For loop with pattern matching (C# 9.0)
    public void ProcessObjects()
    {
        var objects = new object[] { "Hello", 42, 3.14, true };
        
        foreach (var obj in objects)
        {
            switch (obj)
            {
                case string s:
                    Console.WriteLine($"String: {s}");
                    break;
                case int i:
                    Console.WriteLine($"Integer: {i}");
                    break;
                case double d:
                    Console.WriteLine($"Double: {d}");
                    break;
                case bool b:
                    Console.WriteLine($"Boolean: {b}");
                    break;
            }
        }
    }
}
```

## While Loops

### Basic While Loop
```csharp
public class WhileLoopExamples
{
    // Basic while loop
    public void CountToTen()
    {
        int count = 1;
        
        while (count <= 10)
        {
            Console.WriteLine($"Count: {count}");
            count++;
        }
    }
    
    // While loop with user input
    public void GetUserInput()
    {
        string input = "";
        
        while (input.ToLower() != "exit")
        {
            Console.WriteLine("Enter text (type 'exit' to quit):");
            input = Console.ReadLine();
        }
        
        Console.WriteLine("Program ended");
    }
    
    // While loop for validation
    public int GetValidAge()
    {
        int age;
        string input;
        
        do
        {
            Console.WriteLine("Enter your age (18-100):");
            input = Console.ReadLine();
        } while (!int.TryParse(input, out age) || age < 18 || age > 100);
        
        return age;
    }
    
    // While loop for file processing
    public void ReadFileLines(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        {
            string line;
            int lineNumber = 1;
            
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine($"Line {lineNumber}: {line}");
                lineNumber++;
            }
        }
    }
    
    // While loop with condition based on calculation
    public void CalculateUntilThreshold()
    {
        double value = 1.0;
        double threshold = 1000.0;
        int iterations = 0;
        
        while (value < threshold)
        {
            value *= 1.1;
            iterations++;
            
            if (iterations % 10 == 0)
            {
                Console.WriteLine($"Iteration {iterations}: Value = {value:F2}");
            }
        }
        
        Console.WriteLine($"Reached threshold {threshold} in {iterations} iterations");
    }
}
```

### Do-While Loop
```csharp
public class DoWhileLoopExamples
{
    // Basic do-while loop
    public void GetUserInput()
    {
        string input;
        
        do
        {
            Console.WriteLine("Enter 'yes' to continue: ");
            input = Console.ReadLine();
        } while (input.ToLower() == "yes");
        
        Console.WriteLine("Program ended");
    }
    
    // Do-while for menu system
    public void ShowMenu()
    {
        int choice;
        
        do
        {
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Subtract");
            Console.WriteLine("3. Multiply");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice (1-4): ");
            
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                ProcessMenuChoice(choice);
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
            
        } while (choice != 4);
    }
    
    private void ProcessMenuChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                Console.WriteLine("Add operation");
                break;
            case 2:
                Console.WriteLine("Subtract operation");
                break;
            case 3:
                Console.WriteLine("Multiply operation");
                break;
            case 4:
                Console.WriteLine("Exiting");
                break;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
    }
    
    // Do-while for validation
    public int GetValidNumber()
    {
        int number;
        
        do
        {
            Console.Write("Enter a positive number: ");
        } while (!int.TryParse(Console.ReadLine(), out number) || number <= 0);
        
        return number;
    }
    
    // Do-while for retry logic
    public bool TryOperation()
    {
        int attempts = 0;
        bool success = false;
        
        do
        {
            attempts++;
            Console.WriteLine($"Attempt {attempts}: Trying operation...");
            
            try
            {
                // Simulate operation that might fail
                if (Random.Shared.Next(1, 4) != 1) // 75% success rate
                {
                    success = true;
                    Console.WriteLine("Operation succeeded!");
                }
                else
                {
                    Console.WriteLine("Operation failed, retrying...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
        } while (!success && attempts < 3);
        
        return success;
    }
}
```

## Foreach Loops

### Basic Foreach Loop
```csharp
public class ForEachExamples
{
    // Foreach with array
    public void PrintStringArray()
    {
        string[] fruits = { "Apple", "Banana", "Cherry", "Date" };
        
        foreach (string fruit in fruits)
        {
            Console.WriteLine(fruit);
        }
    }
    
    // Foreach with List<T>
    public void PrintList()
    {
        var numbers = new List<int> { 1, 2, 3, 4, 5 };
        
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }
    }
    
    // Foreach with Dictionary<TKey, TValue>
    public void PrintDictionary()
    {
        var scores = new Dictionary<string, int>
        {
            ["Alice"] = 95,
            ["Bob"] = 87,
            ["Charlie"] = 92
        };
        
        foreach (KeyValuePair<string, int> kvp in scores)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
    
    // Foreach with HashSet<T>
    public void PrintHashSet()
    {
        var uniqueNumbers = new HashSet<int> { 1, 2, 3, 4, 5, 5, 6 };
        
        foreach (int number in uniqueNumbers)
        {
            Console.WriteLine(number);
        }
    }
    
    // Foreach with Queue<T>
    public void ProcessQueue()
    {
        var queue = new Queue<string>();
        queue.Enqueue("First");
        queue.Enqueue("Second");
        queue.Enqueue("Third");
        
        foreach (string item in queue)
        {
            Console.WriteLine(item);
        }
    }
    
    // Foreach with Stack<T>
    public void ProcessStack()
    {
        var stack = new Stack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        
        foreach (int item in stack)
        {
            Console.WriteLine(item);
        }
    }
}
```

### Foreach with Custom Collections
```csharp
public class CustomCollectionExamples
{
    // Foreach with custom collection
    public class NumberCollection : IEnumerable<int>
    {
        private readonly List<int> _numbers = new List<int>();
        
        public void Add(int number)
        {
            _numbers.Add(number);
        }
        
        public IEnumerator<int> GetEnumerator()
        {
            return _numbers.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    
    public void ProcessCustomCollection()
    {
        var collection = new NumberCollection();
        collection.Add(10);
        collection.Add(20);
        collection.Add(30);
        
        foreach (int number in collection)
        {
            Console.WriteLine(number);
        }
    }
    
    // Foreach with LINQ
    public void ProcessWithLINQ()
    {
        var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        
        // Where clause
        var evenNumbers = numbers.Where(n => n % 2 == 0);
        Console.WriteLine("Even numbers:");
        foreach (int num in evenNumbers)
        {
            Console.WriteLine(num);
        }
        
        // Select clause
        var squares = numbers.Select(n => n * n);
        Console.WriteLine("Squares:");
        foreach (int square in squares)
        {
            Console.WriteLine(square);
        }
        
        // Where and Select combined
        var evenSquares = numbers.Where(n => n % 2 == 0).Select(n => n * n);
        Console.WriteLine("Even squares:");
        foreach (int square in evenSquares)
        {
            Console.WriteLine(square);
        }
    }
    
    // Foreach with anonymous types
    public void ProcessAnonymousTypes()
    {
        var people = new[]
        {
            new { Name = "John", Age = 25, City = "New York" },
            new { Name = "Jane", Age = 30, City = "Boston" },
            new { Name = "Bob", Age = 35, City = "Chicago" }
        };
        
        foreach (var person in people)
        {
            Console.WriteLine($"{person.Name}, {person.Age}, {person.City}");
        }
    }
}
```

## Loop Control Statements

### Break Statement
```csharp
public class BreakExamples
{
    // Break in for loop
    public void FindFirstPrime(int[] numbers)
    {
        foreach (int number in numbers)
        {
            if (IsPrime(number))
            {
                Console.WriteLine($"First prime: {number}");
                break;
            }
        }
    }
    
    // Break in while loop
    public void ProcessUntilCondition()
    {
        int count = 0;
        
        while (count < 100)
        {
            count++;
            
            if (count % 7 == 0)
            {
                Console.WriteLine($"Found multiple of 7: {count}");
                break;
            }
        }
    }
    
    // Break in nested loops
    public void SearchInMatrix(int[,] matrix, int target)
    {
        bool found = false;
        
        for (int i = 0; i < matrix.GetLength(0) && !found; i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == target)
                {
                    Console.WriteLine($"Found {target} at ({i}, {j})");
                    found = true;
                    break; // Break inner loop
                }
            }
            
            // Continue outer loop search
        }
    }
    
    // Labeled break (C# 8.0+)
    public void LabeledBreakExample()
    {
        outerLoop:
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (i == 1 && j == 1)
                {
                    Console.WriteLine($"Breaking at ({i}, {j})");
                    break outerLoop; // Break outer loop
                }
                Console.WriteLine($"({i}, {j})");
            }
        }
    }
    
    private bool IsPrime(int number)
    {
        if (number < 2) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;
        
        for (int i = 3; i <= Math.Sqrt(number); i += 2)
        {
            if (number % i == 0) return false;
        }
        
        return true;
    }
}
```

### Continue Statement
```csharp
public class ContinueExamples
{
    // Continue in for loop
    public void SumPositiveNumbers(int[] numbers)
    {
        int sum = 0;
        
        foreach (int number in numbers)
        {
            if (number < 0)
            {
                continue; // Skip negative numbers
            }
            
            sum += number;
        }
        
        Console.WriteLine($"Sum of positive numbers: {sum}");
    }
    
    // Continue in while loop
    public void ProcessValidData()
    {
        int count = 0;
        
        while (count < 10)
        {
            count++;
            
            int data = GetData(count);
            
            if (!IsValid(data))
            {
                continue; // Skip invalid data
            }
            
            Console.WriteLine($"Processing valid data: {data}");
        }
    }
    
    // Continue in nested loops
    public void ProcessMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] < 0)
                {
                    continue; // Skip negative values
                }
                
                Console.WriteLine($"Processing ({i}, {j}): {matrix[i, j]}");
            }
        }
    }
    
    // Continue with label
    public void LabeledContinueExample()
    {
        outerLoop:
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (i == j)
                {
                    Console.WriteLine($"Skipping diagonal ({i}, {j})");
                    continue outerLoop; // Continue outer loop
                }
                
                Console.WriteLine($"Processing ({i}, {j})");
            }
        }
    }
    
    private int GetData(int index)
    {
        // Simulate getting data
        return index * 10 - 30;
    }
    
    private bool IsValid(int data)
    {
        return data >= 0;
    }
}
```

### Return Statement in Loops
```csharp
public class ReturnInLoops
{
    // Return in for loop
    public int FindFirstMatch(int[] numbers, int target)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] == target)
            {
                return i; // Return index of first match
            }
        }
        
        return -1; // Not found
    }
    
    // Return in foreach loop
    public bool ContainsEvenNumber(int[] numbers)
    {
        foreach (int number in numbers)
        {
            if (number % 2 == 0)
            {
                return true; // Return true if any even number found
            }
        }
        
        return false; // No even numbers found
    }
    
    // Return in while loop
    public int FindDivisibleBy(int[] numbers, int divisor)
    {
        int index = 0;
        
        while (index < numbers.Length)
        {
            if (numbers[index] % divisor == 0)
            {
                return index; // Return index of first divisible number
            }
            index++;
        }
        
        return -1; // No divisible number found
    }
    
    // Return in do-while loop
    public bool TryParseUntilValid(string[] inputs)
    {
        foreach (string input in inputs)
        {
            if (int.TryParse(input, out int result) && result > 0)
            {
                return true; // Return true if valid positive integer found
            }
        }
        
        return false; // No valid positive integer found
    }
    
    // Return with early exit
    public List<int> GetValidNumbers(int[] numbers, int maxCount)
    {
        var validNumbers = new List<int>();
        
        foreach (int number in numbers)
        {
            if (number > 0 && number < 100)
            {
                validNumbers.Add(number);
                
                // Return early if we have enough numbers
                if (validNumbers.Count >= maxCount)
                {
                    return validNumbers;
                }
            }
        }
        
        return validNumbers;
    }
}
```

## Iterator Patterns

### Yield Return
```csharp
public class YieldExamples
{
    // Basic yield return
    public IEnumerable<int> GenerateNumbers(int start, int end)
    {
        for (int i = start; i <= end; i++)
        {
            yield return i;
        }
    }
    
    // Yield return with conditions
    public IEnumerable<int> GetEvenNumbers(int start, int end)
    {
        for (int i = start; i <= end; i++)
        {
            if (i % 2 == 0)
            {
                yield return i;
            }
        }
    }
    
    // Yield return complex sequence
    public IEnumerable<string> GenerateFibonacci(int count)
    {
        int a = 0;
        int b = 1;
        
        yield return a.ToString();
        yield return b.ToString();
        
        for (int i = 2; i < count; i++)
        {
            int temp = a + b;
            a = b;
            b = temp;
            yield return b.ToString();
        }
    }
    
    // Yield return with filtering
    public IEnumerable<string> GetShortWords(string text)
    {
        string[] words = text.Split(' ');
        
        foreach (string word in words)
        {
            if (word.Length <= 5)
            {
                yield return word;
            }
        }
    }
    
    // Yield return with transformation
    public IEnumerable<double> GetSquares(int start, int end)
    {
        for (int i = start; i <= end; i++)
        {
            yield return i * i;
        }
    }
    
    // Using yield return
    public void DemonstrateYield()
    {
        Console.WriteLine("Numbers 1-5:");
        foreach (int num in GenerateNumbers(1, 5))
        {
            Console.WriteLine(num);
        }
        
        Console.WriteLine("\nEven numbers 1-10:");
        foreach (int num in GetEvenNumbers(1, 10))
        {
            Console.WriteLine(num);
        }
        
        Console.WriteLine("\nFibonacci sequence:");
        foreach (string fib in GenerateFibonacci(10))
        {
            Console.WriteLine(fib);
        }
    }
}
```

### Custom Iterators
```csharp
public class CustomIterators
{
    // Iterator for tree traversal
    public IEnumerable<T> InOrderTraversal<T>(TreeNode<T> root)
    {
        if (root == null) yield break;
        
        var stack = new Stack<TreeNode<T>>();
        var current = root;
        
        while (stack.Count > 0 || current != null)
        {
            if (current != null)
            {
                stack.Push(current);
                current = current.Left;
            }
            else
            {
                current = stack.Pop();
                yield return current.Value;
                current = current.Right;
            }
        }
    }
    
    // Iterator for pagination
    public IEnumerable<T> Paginate<T>(IEnumerable<T> source, int pageSize)
    {
        var page = new List<T>();
        
        foreach (T item in source)
        {
            page.Add(item);
            
            if (page.Count >= pageSize)
            {
                yield return page;
                page = new List<T>();
            }
        }
        
        if (page.Count > 0)
        {
            yield return page;
        }
    }
    
    // Iterator with state
    public IEnumerable<int> RunningTotal(IEnumerable<int> numbers)
    {
        int total = 0;
        
        foreach (int number in numbers)
        {
            total += number;
            yield return total;
        }
    }
    
    // Iterator with filtering and transformation
    public IEnumerable<string> ProcessWords(string text)
    {
        string[] words = text.Split(' ');
        
        foreach (string word in words)
        {
            // Filter out empty words
            if (string.IsNullOrWhiteSpace(word))
                continue;
            
            // Transform to lowercase
            yield return word.ToLower();
        }
    }
    
    // Using custom iterators
    public void DemonstrateCustomIterators()
    {
        // Create a sample tree
        var tree = new TreeNode<int>(1,
            new TreeNode<int>(2,
                new TreeNode<int>(4),
                new TreeNode<int>(5)),
            new TreeNode<int>(3,
                new TreeNode<int>(6),
                new TreeNode<int>(7))
        );
        
        Console.WriteLine("In-order traversal:");
        foreach (int value in InOrderTraversal(tree))
        {
            Console.WriteLine(value);
        }
        
        // Pagination example
        var numbers = Enumerable.Range(1, 25);
        Console.WriteLine("\nPaginated numbers:");
        int page = 1;
        foreach (var pageItems in Paginate(numbers, 5))
        {
            Console.WriteLine($"Page {page++}: {string.Join(", ", pageItems)}");
        }
        
        // Running total example
        var numbersList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        Console.WriteLine("\nRunning total:");
        foreach (int total in RunningTotal(numbersList))
        {
            Console.WriteLine(total);
        }
    }
}

public class TreeNode<T>
{
    public T Value { get; set; }
    public TreeNode<T> Left { get; set; }
    public TreeNode<T> Right { get; set; }
    
    public TreeNode(T value)
    {
        Value = value;
    }
    
    public TreeNode(T value, TreeNode<T> left, TreeNode<T> right)
    {
        Value = value;
        Left = left;
        Right = right;
    }
}
```

## Performance Considerations

### Loop Performance
```csharp
public class LoopPerformance
{
    // Good: Use for loop for indexed access
    public void ProcessArrayWithFor(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = array[i] * 2;
        }
    }
    
    // Good: Use foreach for collections
    public void ProcessListWithForEach(List<string> list)
    {
        foreach (string item in list)
        {
            Console.WriteLine(item.ToUpper());
        }
    }
    
    // Bad: Avoid repeated property access
    public void BadPropertyAccess(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine(list[i]); // Accesses Count property each iteration
        }
    }
    
    // Good: Cache property access
    public void GoodPropertyAccess(List<string> list)
    {
        int count = list.Count;
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine(list[i]);
        }
    }
    
    // Good: Use StringBuilder for string concatenation in loops
    public string BuildStringWithBuilder(string[] parts)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < parts.Length; i++)
        {
            sb.Append(parts[i]);
            if (i < parts.Length - 1)
                sb.Append(", ");
        }
        return sb.ToString();
    }
    
    // Bad: String concatenation in loops
    public string BuildStringBad(string[] parts)
    {
        string result = "";
        for (int i = 0; i < parts.Length; i++)
        {
            result += parts[i];
            if (i < parts.Length - 1)
                result += ", ";
        }
        return result;
    }
    
    // Good: Use Span<T> for performance-critical operations
    public void ProcessSpan(ReadOnlySpan<char> text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            // Process character without bounds checking
        }
    }
    
    // Good: Use LINQ for complex operations
    public List<int> GetFilteredAndSorted(List<int> numbers)
    {
        return numbers
            .Where(n => n > 0)
            .OrderBy(n => n)
            .ToList();
    }
    
    // Bad: Manual filtering and sorting
    public List<int> GetFilteredAndSortedBad(List<int> numbers)
    {
        var filtered = new List<int>();
        
        // Filter
        foreach (int n in numbers)
        {
            if (n > 0)
            {
                filtered.Add(n);
            }
        }
        
        // Sort
        filtered.Sort();
        
        return filtered;
    }
}
```

### Memory Management
```csharp
public class LoopMemoryManagement
{
    // Good: Use using statement for disposable resources
    public void ProcessFiles(string[] filePaths)
    {
        foreach (string filePath in filePaths)
        {
            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Process line
                    Console.WriteLine(line);
                }
            }
        }
    }
    
    // Good: Reuse objects in loops
    public void ProcessWithReuse(List<string> items)
    {
        var processor = new ItemProcessor();
        
        foreach (string item in items)
        {
            processor.Process(item);
        }
    }
    
    // Bad: Create objects in loops unnecessarily
    public void ProcessWithoutReuse(List<string> items)
    {
        foreach (string item in items)
        {
            var processor = new ItemProcessor(); // Creates new object each iteration
            processor.Process(item);
        }
    }
    
    // Good: Use object pooling for expensive objects
    public void ProcessWithPool(List<string> items)
    {
        var pool = new ObjectPool<ItemProcessor>();
        
        foreach (string item in items)
        {
            var processor = pool.Get();
            try
            {
                processor.Process(item);
            }
            finally
            {
                pool.Return(processor);
            }
        }
    }
    
    // Good: Clear collections in loops
    public void ProcessAndClear<T>(List<T> items)
    {
        foreach (T item in items)
        {
            // Process item
        }
        
        items.Clear(); // Clear collection when done
    }
    
    // Bad: Create new collection unnecessarily
    public List<T> ProcessAndCreateNew<T>(List<T> items)
    {
        var result = new List<T>(); // Creates new collection
        
        foreach (T item in items)
        {
            // Process item
            result.Add(item);
        }
        
        return result;
    }
}

public class ItemProcessor
{
    public void Process(string item)
    {
        // Process item
    }
}

public class ObjectPool<T> where T : new()
{
    private readonly ConcurrentQueue<T> _items = new ConcurrentQueue<T>();
    
    public T Get()
    {
        if (_items.TryDequeue(out T item))
        {
            return item;
        }
        
        return new T();
    }
    
    public void Return(T item)
    {
        _items.Enqueue(item);
    }
}
```

## Best Practices

### Loop Best Practices
```csharp
public class LoopBestPractices
{
    // Good: Use meaningful loop variable names
    public void GoodVariableNames()
    {
        for (int customerIndex = 0; customerIndex < customers.Count; customerIndex++)
        {
            Console.WriteLine($"Processing customer {customerIndex}");
        }
        
        int retryCount = 0;
        while (retryCount < maxRetries)
        {
            // Retry logic
            retryCount++;
        }
    }
    
    // Good: Limit loop scope
    public void LimitedScope()
    {
        int total = 0;
        
        for (int i = 0; i < 10; i++)
        {
            total += i;
        }
        
        Console.WriteLine($"Total: {total}");
    }
    
    // Good: Use appropriate loop type
    public void ChooseRightLoop()
    {
        // Use for when you know the number of iterations
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine(i);
        }
        
        // Use foreach when iterating over collections
        var numbers = new List<int> { 1, 2, 3, 4, 5 };
        foreach (int num in numbers)
        {
            Console.WriteLine(num);
        }
        
        // Use while when condition-based
        string input = "";
        while (input != "quit")
        {
            input = Console.ReadLine();
        }
        
        // Use do-while when you need at least one iteration
        int number;
        do
        {
            number = GetValidNumber();
        } while (number < 0);
    }
    
    // Good: Avoid magic numbers
    private const int MaxRetries = 3;
    private const int MaxCount = 100;
    
    public void RetryOperation()
    {
        for (int attempt = 1; attempt <= MaxRetries; attempt++)
        {
            // Retry logic
        }
    }
    
    // Good: Handle empty collections
    public void ProcessSafely(List<string> items)
    {
        if (items == null || items.Count == 0)
        {
            Console.WriteLine("No items to process");
            return;
        }
        
        foreach (string item in items)
        {
            Console.WriteLine(item);
        }
    }
    
    // Good: Use LINQ for complex operations
    public void UseLinqWhenAppropriate()
    {
        var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        
        // Complex filtering and transformation
        var result = numbers
            .Where(n => n % 2 == 0)
            .OrderBy(n => n)
            .Select(n => n * n)
            .Take(5)
            .ToList();
        
        foreach (int num in result)
        {
            Console.WriteLine(num);
        }
    }
    
    private int GetValidNumber()
    {
        int number;
        do
        {
            Console.Write("Enter a positive number: ");
        } while (!int.TryParse(Console.ReadLine(), out number) || number <= 0);
        
        return number;
    }
    
    private List<Customer> customers = new List<Customer>();
}
```

### Error Handling in Loops
```csharp
public class LoopErrorHandling
{
    // Good: Handle exceptions in loops
    public void ProcessWithExceptionHandling(string[] filePaths)
    {
        foreach (string filePath in filePaths)
        {
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File not found: {filePath}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"IO error: {ex.Message}");
            }
        }
    }
    
    // Good: Validate input before processing
    public void ValidateAndProcess(List<string> inputs)
    {
        foreach (string input in inputs)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Skipping empty input");
                continue;
            }
            
            if (input.Length > 100)
            {
                Console.WriteLine($"Input too long: {input.Substring(0, 20)}...");
                continue;
            }
            
            // Process valid input
            Console.WriteLine($"Processing: {input}");
        }
    }
    
    // Good: Use timeout for long-running operations
    public async Task ProcessWithTimeoutAsync(CancellationToken cancellationToken)
    {
        var tasks = new List<Task>();
        
        for (int i = 0; i < 5; i++)
        {
            int taskNumber = i;
            var task = Task.Run(async () =>
            {
                await Task.Delay(1000, cancellationToken);
                Console.WriteLine($"Task {taskNumber} completed");
            });
            
            tasks.Add(task);
        }
        
        await Task.WhenAll(tasks);
    }
    
    // Good: Provide cancellation support
    public void ProcessWithCancellation(CancellationToken cancellationToken)
    {
        int count = 0;
        
        while (!cancellationToken.IsCancellationRequested)
        {
            Console.WriteLine($"Processing item {count}");
            count++;
            
            // Simulate work
            Thread.Sleep(100);
        }
        
        Console.WriteLine("Processing cancelled or completed");
    }
}
```

## Common Pitfalls

### Common Loop Errors
```csharp
public class CommonPitfalls
{
    // Pitfall: Off-by-one errors
    public void OffByOneError()
    {
        int[] array = { 1, 2, 3, 4, 5 };
        
        // Wrong: i < array.Length instead of i <= array.Length - 1
        for (int i = 0; i < array.Length; i++)
        {
            Console.WriteLine(array[i]);
        }
        
        // Correct: i < array.Length
        for (int i = 0; i < array.Length; i++)
        {
            Console.WriteLine(array[i]);
        }
    }
    
    // Pitfall: Modifying collection while iterating
    public void ModifyDuringIteration()
    {
        var numbers = new List<int> { 1, 2, 3, 4, 5 };
        
        // Wrong: Modifying collection while iterating
        foreach (int num in numbers)
        {
            if (num % 2 == 0)
            {
                numbers.Remove(num); // This will cause an exception
            }
        }
        
        // Correct: Use ToList() to create a copy
        foreach (int num in numbers.ToList())
        {
            if (num % 2 == 0)
            {
                numbers.Remove(num);
            }
        }
    }
    
    // Pitfall: Infinite loop
    public void InfiniteLoop()
    {
        int count = 0;
        
        // Wrong: No way to exit loop
        while (true)
        {
            Console.WriteLine(count);
            count++;
        }
        
        // Correct: Include exit condition
        while (count < 100)
        {
            Console.WriteLine(count);
            count++;
        }
    }
    
    // Pitfall: Unnecessary loops
    public void UnnecessaryLoop()
    {
        var list = new List<string> { "A", "B", "C" };
        
        // Wrong: Loop when you can use built-in method
        string result = "";
        foreach (string item in list)
        {
            result += item + " ";
        }
        
        // Correct: Use built-in method
        string correctResult = string.Join(" ", list);
    }
    
    // Pitfall: Not handling empty collections
    public void EmptyCollectionPitfall()
    {
        var list = new List<string>();
        
        // Wrong: This will throw an exception
        string first = list[0];
        
        // Correct: Check if collection is empty
        string firstSafe = list.Count > 0 ? list[0] : "Default";
    }
    
    // Pitfall: Repeated expensive operations
    public void ExpensiveOperationInLoop()
    {
        var data = new List<string>();
        
        // Wrong: Expensive operation in loop condition
        for (int i = 0; i < 100; i++)
        {
            if (data.Count > 50) // Expensive Count() call
            {
                Console.WriteLine("Too many items");
            }
        }
        
        // Correct: Cache expensive operation
        int count = data.Count;
        for (int i = 0; i < 100; i++)
        {
            if (count > 50)
            {
                Console.WriteLine("Too many items");
            }
        }
    }
    
    // Pitfall: Not disposing resources
    public void ResourceLeakPitfall()
    {
        var files = Directory.GetFiles("C:\\Temp", "*.txt");
        
        // Wrong: Not disposing StreamReader
        foreach (string file in files)
        {
            var reader = new StreamReader(file);
            string content = reader.ReadToEnd();
            Console.WriteLine(content);
            // Reader not disposed
        }
        
        // Correct: Use using statement
        foreach (string file in files)
        {
            using (var reader = new StreamReader(file))
            {
                string content = reader.ReadToEnd();
                Console.WriteLine(content);
            }
        }
    }
}
```

## Summary

C# loops and iteration provide:

**Loop Types:**
- For loops for indexed iteration
- While loops for condition-based iteration
    - Do-while loops for at-least-once execution
- Foreach loops for collection iteration

**Loop Control:**
- Break statement to exit loops early
- Continue statement to skip iterations
- Return statement to exit methods
- Labeled break and continue for nested loops

**Advanced Features:**
- Yield return for custom iterators
    - Custom iterator patterns
    - LINQ integration
    - Stateful iterators

**Performance Considerations:**
- Choose appropriate loop type for the task
- Cache expensive operations
- Use StringBuilder for string concatenation
    - Use Span<T> for performance-critical code
- Avoid unnecessary object creation in loops

**Memory Management:**
- Use using statements for disposable resources
    - Reuse objects when possible
    - Use object pooling for expensive objects
    - Clear collections when appropriate

**Best Practices:**
- Use meaningful variable names
    - Limit variable scope
    - Handle empty collections safely
    - Use LINQ for complex operations
    - Provide cancellation support for async operations

**Common Pitfalls:**
- Off-by-one errors in array bounds
- Modifying collections while iterating
- Infinite loops without exit conditions
- Unnecessary loops when built-in methods exist
- Not handling empty collections
- Repeated expensive operations in loop conditions
- Resource leaks without proper disposal

C# loops provide powerful and flexible iteration mechanisms, with modern language features like LINQ and pattern matching making code more readable and efficient.
