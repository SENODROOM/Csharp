# C# Arrays and Collections

## Arrays

### Single-Dimensional Arrays
```csharp
public class ArrayBasics
{
    // Array declaration and initialization
    public void ArrayDeclaration()
    {
        // Declaration with size
        int[] numbers = new int[5];
        
        // Declaration with initialization
        int[] initializedNumbers = { 1, 2, 3, 4, 5 };
        
        // Declaration with new keyword
        string[] names = new string[] { "Alice", "Bob", "Charlie" };
        
        // Mixed types in object array
        object[] mixedArray = { 1, "Hello", true, 3.14, 'A' };
        
        Console.WriteLine($"Numbers length: {numbers.Length}");
        Console.WriteLine($"Names length: {names.Length}");
    }
    
    // Array element access
    public void ArrayElementAccess()
    {
        int[] numbers = { 10, 20, 30, 40, 50 };
        
        // Access elements
        int first = numbers[0];      // 10
        int last = numbers[4];       // 50
        int middle = numbers[2];     // 30
        
        // Modify elements
        numbers[1] = 25;
        numbers[3] = 45;
        
        Console.WriteLine($"First: {first}, Last: {last}, Middle: {middle}");
        Console.WriteLine($"Modified array: [{string.Join(", ", numbers)}]");
    }
    
    // Array iteration
    public void ArrayIteration()
    {
        string[] fruits = { "Apple", "Banana", "Cherry", "Date" };
        
        // For loop
        Console.WriteLine("For loop:");
        for (int i = 0; i < fruits.Length; i++)
        {
            Console.WriteLine($"{i}: {fruits[i]}");
        }
        
        // Foreach loop
        Console.WriteLine("\nForeach loop:");
        foreach (string fruit in fruits)
        {
            Console.WriteLine(fruit);
        }
        
        // LINQ
        Console.WriteLine("\nLINQ:");
        var upperFruits = fruits.Select(f => f.ToUpper());
        foreach (string fruit in upperFruits)
        {
            Console.WriteLine(fruit);
        }
    }
    
    // Array methods
    public void ArrayMethods()
    {
        int[] numbers = { 5, 2, 8, 1, 9, 3, 7, 4, 6 };
        
        // Sort
        Array.Sort(numbers);
        Console.WriteLine($"Sorted: [{string.Join(", ", numbers)}]");
        
        // Reverse
        Array.Reverse(numbers);
        Console.WriteLine($"Reversed: [{string.Join(", ", numbers)}]");
        
        // Find element
        int index = Array.IndexOf(numbers, 8);
        Console.WriteLine($"Index of 8: {index}");
        
        // Check if element exists
        bool contains = Array.Exists(numbers, x => x > 5);
        Console.WriteLine($"Contains element > 5: {contains}");
        
        // Find all matching elements
        int[] largeNumbers = Array.FindAll(numbers, x => x > 5);
        Console.WriteLine($"Numbers > 5: [{string.Join(", ", largeNumbers)}]");
        
        // Copy array
        int[] copy = new int[numbers.Length];
        Array.Copy(numbers, copy, numbers.Length);
        Console.WriteLine($"Copy: [{string.Join(", ", copy)}]");
    }
}
```

### Multi-Dimensional Arrays
```csharp
public class MultiDimensionalArrays
{
    // Two-dimensional array
    public void TwoDArray()
    {
        // Declaration
        int[,] matrix = new int[3, 3];
        
        // Initialization
        int[,] initializedMatrix = 
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };
        
        // Access elements
        int element = initializedMatrix[1, 2]; // 6
        Console.WriteLine($"Element at [1,2]: {element}");
        
        // Iterate through 2D array
        Console.WriteLine("2D Array elements:");
        for (int i = 0; i < initializedMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < initializedMatrix.GetLength(1); j++)
            {
                Console.Write($"[{i},{j}] = {initializedMatrix[i, j]}\t");
            }
            Console.WriteLine();
        }
        
        // Modify elements
        initializedMatrix[0, 0] = 10;
        initializedMatrix[2, 2] = 20;
        
        Console.WriteLine("\nModified matrix:");
        PrintMatrix(initializedMatrix);
    }
    
    // Three-dimensional array
    public void ThreeDArray()
    {
        // Declaration
        int[,,] cube = new int[2, 2, 2];
        
        // Initialization
        int[,,] initializedCube = 
        {
            { { 1, 2 }, { 3, 4 } },
            { { 5, 6 }, { 7, 8 } }
        };
        
        // Access elements
        int element = initializedCube[1, 0, 1]; // 6
        Console.WriteLine($"Element at [1,0,1]: {element}");
        
        // Iterate through 3D array
        Console.WriteLine("3D Array elements:");
        for (int x = 0; x < initializedCube.GetLength(0); x++)
        {
            for (int y = 0; y < initializedCube.GetLength(1); y++)
            {
                for (int z = 0; z < initializedCube.GetLength(2); z++)
                {
                    Console.Write($"[{x},{y},{z}] = {initializedCube[x, y, z]}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
    
    // Matrix operations
    public void MatrixOperations()
    {
        int[,] matrixA = 
        {
            { 1, 2, 3 },
            { 4, 5, 6 }
        };
        
        int[,] matrixB = 
        {
            { 7, 8 },
            { 9, 10 },
            { 11, 12 }
        };
        
        // Matrix multiplication
        int[,] result = MatrixMultiply(matrixA, matrixB);
        
        Console.WriteLine("Matrix A:");
        PrintMatrix(matrixA);
        
        Console.WriteLine("\nMatrix B:");
        PrintMatrix(matrixB);
        
        Console.WriteLine("\nMatrix A × B:");
        PrintMatrix(result);
    }
    
    private int[,] MatrixMultiply(int[,] a, int[,] b)
    {
        int rowsA = a.GetLength(0);
        int colsA = a.GetLength(1);
        int colsB = b.GetLength(1);
        
        int[,] result = new int[rowsA, colsB];
        
        for (int i = 0; i < rowsA; i++)
        {
            for (int j = 0; j < colsB; j++)
            {
                for (int k = 0; k < colsA; k++)
                {
                    result[i, j] += a[i, k] * b[k, j];
                }
            }
        }
        
        return result;
    }
    
    private void PrintMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}
```

### Jagged Arrays
```csharp
public class JaggedArrays
{
    // Jagged array declaration
    public void JaggedArrayBasics()
    {
        // Declaration
        int[][] jagged = new int[3][];
        
        // Initialize each sub-array
        jagged[0] = new int[] { 1, 2, 3 };
        jagged[1] = new int[] { 4, 5 };
        jagged[2] = new int[] { 6, 7, 8, 9 };
        
        // Access elements
        int element = jagged[1][0]; // 4
        Console.WriteLine($"Element at [1][0]: {element}");
        
        // Iterate through jagged array
        Console.WriteLine("Jagged array elements:");
        for (int i = 0; i < jagged.Length; i++)
        {
            for (int j = 0; j < jagged[i].Length; j++)
            {
                Console.Write(jagged[i][j] + " ");
            }
            Console.WriteLine();
        }
        
        // Modify elements
        jagged[0][1] = 20;
        jagged[2][3] = 50;
        
        Console.WriteLine("\nModified jagged array:");
        foreach (int[] subArray in jagged)
        {
            Console.WriteLine($"[{string.Join(", ", subArray)}]");
        }
    }
    
    // Jagged array with different lengths
    public void VariableLengthJaggedArray()
    {
        string[][] names = new string[4][];
        
        names[0] = new string[] { "John" };
        names[1] = new string[] { "Alice", "Bob" };
        names[2] = new string[] { "Charlie", "Diana", "Eve" };
        names[3] = new string[] { "Frank", "Grace", "Henry", "Ivy" };
        
        Console.WriteLine("Variable length jagged array:");
        for (int i = 0; i < names.Length; i++)
        {
            Console.WriteLine($"Row {i}: [{string.Join(", ", names[i])}]");
        }
    }
    
    // Jagged array operations
    public void JaggedArrayOperations()
    {
        int[][] jagged = 
        {
            new int[] { 1, 2, 3 },
            new int[] { 4, 5 },
            new int[] { 6, 7, 8, 9 }
        };
        
        // Sort each sub-array
        foreach (int[] subArray in jagged)
        {
            Array.Sort(subArray);
        }
        
        Console.WriteLine("Sorted sub-arrays:");
        foreach (int[] subArray in jagged)
        {
            Console.WriteLine($"[{string.Join(", ", subArray)}]");
        }
        
        // Find total elements
        int totalElements = 0;
        foreach (int[] subArray in jagged)
        {
            totalElements += subArray.Length;
        }
        
        Console.WriteLine($"\nTotal elements: {totalElements}");
        
        // Flatten jagged array
        int[] flattened = new int[totalElements];
        int index = 0;
        
        foreach (int[] subArray in jagged)
        {
            foreach (int element in subArray)
            {
                flattened[index++] = element;
            }
        }
        
        Console.WriteLine($"Flattened: [{string.Join(", ", flattened)}]");
    }
}
```

## Generic Collections

### List<T>
```csharp
public class ListExamples
{
    // List<T> basics
    public void ListBasics()
    {
        // Create list
        List<string> fruits = new List<string>();
        
        // Add elements
        fruits.Add("Apple");
        fruits.Add("Banana");
        fruits.Add("Cherry");
        
        // Add range
        string[] moreFruits = { "Date", "Elderberry" };
        fruits.AddRange(moreFruits);
        
        // Access elements
        string first = fruits[0]; // Apple
        string last = fruits[fruits.Count - 1]; // Elderberry
        
        Console.WriteLine($"First: {first}, Last: {last}");
        Console.WriteLine($"Count: {fruits.Count}");
        Console.WriteLine($"Capacity: {fruits.Capacity}");
        
        // Check if contains
        bool contains = fruits.Contains("Banana");
        Console.WriteLine($"Contains 'Banana': {contains}");
        
        // Find element
        int index = fruits.IndexOf("Cherry");
        Console.WriteLine($"Index of 'Cherry': {index}");
        
        // Remove element
        fruits.Remove("Banana");
        fruits.RemoveAt(0); // Remove first element
        
        Console.WriteLine($"After removals: [{string.Join(", ", fruits)}]");
    }
    
    // List<T> with objects
    public void ListWithObjects()
    {
        List<Person> people = new List<Person>();
        
        // Add objects
        people.Add(new Person { Name = "John", Age = 25 });
        people.Add(new Person { Name = "Jane", Age = 30 });
        people.Add(new Person { Name = "Bob", Age = 35 });
        
        // Iterate and display
        Console.WriteLine("People:");
        foreach (Person person in people)
        {
            Console.WriteLine($"{person.Name}, {person.Age}");
        }
        
        // Find with condition
        Person john = people.Find(p => p.Name == "John");
        Console.WriteLine($"\nFound John: {john?.Name}");
        
        // Find all with condition
        List<Person> adults = people.FindAll(p => p.Age >= 30);
        Console.WriteLine("\nAdults (age >= 30):");
        foreach (Person person in adults)
        {
            Console.WriteLine($"{person.Name}, {person.Age}");
        }
        
        // Sort
        people.Sort((p1, p2) => p1.Age.CompareTo(p2.Age));
        Console.WriteLine("\nSorted by age:");
        foreach (Person person in people)
        {
            Console.WriteLine($"{person.Name}, {person.Age}");
        }
    }
    
    // List<T> operations
    public void ListOperations()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        
        // Insert at specific position
        numbers.Insert(2, 99); // Insert 99 at index 2
        Console.WriteLine($"After insert: [{string.Join(", ", numbers)}]");
        
        // Remove range
        numbers.RemoveRange(1, 2); // Remove 2 elements starting at index 1
        Console.WriteLine($"After remove range: [{string.Join(", ", numbers)}]");
        
        // Convert to array
        int[] array = numbers.ToArray();
        Console.WriteLine($"Array: [{string.Join(", ", array)}]");
        
        // Clear list
        numbers.Clear();
        Console.WriteLine($"After clear - Count: {numbers.Count}");
        
        // Check if empty
        bool isEmpty = !numbers.Any();
        Console.WriteLine($"Is empty: {isEmpty}");
    }
}

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}
```

### Dictionary<TKey, TValue>
```csharp
public class DictionaryExamples
{
    // Dictionary basics
    public void DictionaryBasics()
    {
        // Create dictionary
        Dictionary<string, int> ages = new Dictionary<string, int>();
        
        // Add key-value pairs
        ages["Alice"] = 25;
        ages["Bob"] = 30;
        ages["Charlie"] = 35;
        
        // Alternative add method
        ages.Add("Diana", 28);
        
        // Access values
        int aliceAge = ages["Alice"]; // 25
        Console.WriteLine($"Alice's age: {aliceAge}");
        
        // TryGetValue (safe access)
        if (ages.TryGetValue("Bob", out int bobAge))
        {
            Console.WriteLine($"Bob's age: {bobAge}");
        }
        
        // Check if key exists
        bool hasKey = ages.ContainsKey("Eve");
        Console.WriteLine($"Contains key 'Eve': {hasKey}");
        
        // Check if value exists
        bool hasValue = ages.ContainsValue(30);
        Console.WriteLine($"Contains value 30: {hasValue}");
        
        // Count items
        Console.WriteLine($"Count: {ages.Count}");
        
        // Iterate through dictionary
        Console.WriteLine("\nDictionary items:");
        foreach (KeyValuePair<string, int> kvp in ages)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
        
        // Iterate through keys
        Console.WriteLine("\nKeys:");
        foreach (string key in ages.Keys)
        {
            Console.WriteLine(key);
        }
        
        // Iterate through values
        Console.WriteLine("\nValues:");
        foreach (int value in ages.Values)
        {
            Console.WriteLine(value);
        }
    }
    
    // Dictionary with complex types
    public void ComplexDictionary()
    {
        Dictionary<int, List<string>> categories = new Dictionary<int, List<string>>();
        
        // Add categories
        categories[1] = new List<string> { "Apple", "Banana", "Cherry" };
        categories[2] = new List<string> { "Car", "Bike", "Bus" };
        categories[3] = new List<string> { "Red", "Blue", "Green" };
        
        // Access and modify
        categories[1].Add("Date");
        categories[2].Remove("Bike");
        
        Console.WriteLine("Categories:");
        foreach (KeyValuePair<int, List<string>> kvp in categories)
        {
            Console.WriteLine($"Category {kvp.Key}: [{string.Join(", ", kvp.Value)}]");
        }
        
        // Find specific item
        bool hasApple = categories[1].Contains("Apple");
        Console.WriteLine($"Category 1 contains 'Apple': {hasApple}");
    }
    
    // Dictionary operations
    public void DictionaryOperations()
    {
        Dictionary<string, double> prices = new Dictionary<string, double>
        {
            ["Apple"] = 1.99,
            ["Banana"] = 0.99,
            ["Cherry"] = 2.99
        };
        
        // Update value
        prices["Apple"] = 1.79;
        
        // Add or update
        prices["Date"] = 3.49; // Add
        prices["Banana"] = 1.09; // Update
        
        // Remove item
        bool removed = prices.Remove("Cherry");
        Console.WriteLine($"Removed 'Cherry': {removed}");
        
        // Try remove
        bool tryRemoved = prices.TryRemove("Orange", out double removedValue);
        Console.WriteLine($"Try removed 'Orange': {tryRemoved}");
        
        // Clear dictionary
        prices.Clear();
        Console.WriteLine($"After clear - Count: {prices.Count}");
        
        // Dictionary initialization
        var newPrices = new Dictionary<string, double>
        {
            ["Mango"] = 2.99,
            ["Peach"] = 1.99,
            ["Pear"] = 1.49
        };
        
        Console.WriteLine($"\nNew dictionary: {string.Join(", ", newPrices.Select(kvp => $"{kvp.Key}: ${kvp.Value}"))}");
    }
}
```

### HashSet<T>
```csharp
public class HashSetExamples
{
    // HashSet basics
    public void HashSetBasics()
    {
        // Create HashSet
        HashSet<int> numbers = new HashSet<int>();
        
        // Add elements
        numbers.Add(1);
        numbers.Add(2);
        numbers.Add(3);
        numbers.Add(2); // Duplicate - will be ignored
        numbers.Add(4);
        
        Console.WriteLine($"Count: {numbers.Count}"); // 4, not 5
        
        // Check if contains
        bool contains = numbers.Contains(3);
        Console.WriteLine($"Contains 3: {contains}");
        
        // Remove element
        bool removed = numbers.Remove(2);
        Console.WriteLine($"Removed 2: {removed}");
        
        // Iterate
        Console.WriteLine("HashSet elements:");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }
        
        // Union with another set
        HashSet<int> otherNumbers = new HashSet<int> { 3, 4, 5, 6 };
        numbers.UnionWith(otherNumbers);
        
        Console.WriteLine($"\nAfter union: [{string.Join(", ", numbers)}]");
        
        // Intersection
        HashSet<int> intersection = new HashSet<int> { 4, 5, 6, 7 };
        numbers.IntersectWith(intersection);
        
        Console.WriteLine($"After intersection: [{string.Join(", ", numbers)}]");
    }
    
    // HashSet with strings
    public void StringHashSet()
    {
        HashSet<string> words = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        
        // Add words (case-insensitive)
        words.Add("Hello");
        words.Add("World");
        words.Add("hello"); // Duplicate (case-insensitive)
        words.Add("CSharp");
        
        Console.WriteLine($"Count: {words.Count}"); // 3
        
        // Check if contains (case-insensitive)
        bool contains = words.Contains("HELLO");
        Console.WriteLine($"Contains 'HELLO': {contains}");
        
        // Find all words starting with 'H'
        var hWords = words.Where(w => w.StartsWith("H", StringComparison.OrdinalIgnoreCase));
        Console.WriteLine($"Words starting with 'H': {string.Join(", ", hWords)}");
    }
    
    // HashSet operations
    public void HashSetOperations()
    {
        HashSet<int> set1 = new HashSet<int> { 1, 2, 3, 4, 5 };
        HashSet<int> set2 = new HashSet<int> { 4, 5, 6, 7, 8 };
        
        // Union
        HashSet<int> union = new HashSet<int>(set1);
        union.UnionWith(set2);
        Console.WriteLine($"Union: [{string.Join(", ", union)}]");
        
        // Intersection
        HashSet<int> intersection = new HashSet<int>(set1);
        intersection.IntersectWith(set2);
        Console.WriteLine($"Intersection: [{string.Join(", ", intersection)}]");
        
        // Difference
        HashSet<int> difference = new HashSet<int>(set1);
        difference.ExceptWith(set2);
        Console.WriteLine($"Difference (set1 - set2): [{string.Join(", ", difference)}]");
        
        // Symmetric difference
        HashSet<int> symmetricDifference = new HashSet<int>(set1);
        symmetricDifference.SymmetricExceptWith(set2);
        Console.WriteLine($"Symmetric difference: [{string.Join(", ", symmetricDifference)}]");
        
        // Check if subset
        bool isSubset = new HashSet<int> { 1, 2 }.IsSubsetOf(set1);
        Console.WriteLine($"{{1, 2}} is subset of set1: {isSubset}");
        
        // Check if superset
        bool isSuperset = set1.IsSupersetOf(new HashSet<int> { 1, 2 });
        Console.WriteLine($"set1 is superset of {{1, 2}}: {isSuperset}");
    }
}
```

### Queue<T> and Stack<T>
```csharp
public class QueueAndStackExamples
{
    // Queue<T> - FIFO (First In, First Out)
    public void QueueExample()
    {
        Queue<string> queue = new Queue<string>();
        
        // Enqueue (add to end)
        queue.Enqueue("First");
        queue.Enqueue("Second");
        queue.Enqueue("Third");
        queue.Enqueue("Fourth");
        
        Console.WriteLine($"Count: {queue.Count}");
        
        // Peek (look at first element without removing)
        string first = queue.Peek();
        Console.WriteLine($"First element: {first}");
        
        // Dequeue (remove from beginning)
        string dequeued = queue.Dequeue();
        Console.WriteLine($"Dequeued: {dequeued}");
        
        Console.WriteLine($"After dequeue - Count: {queue.Count}");
        
        // Iterate through queue
        Console.WriteLine("Queue elements:");
        foreach (string item in queue)
        {
            Console.WriteLine(item);
        }
        
        // Clear queue
        queue.Clear();
        Console.WriteLine($"After clear - Count: {queue.Count}");
        
        // Try dequeue
        bool tryDequeue = queue.TryDequeue(out string result);
        Console.WriteLine($"Try dequeue from empty queue: {tryDequeue}");
    }
    
    // Stack<T> - LIFO (Last In, First Out)
    public void StackExample()
    {
        Stack<int> stack = new Stack<int>();
        
        // Push (add to top)
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        stack.Push(4);
        
        Console.WriteLine($"Count: {stack.Count}");
        
        // Peek (look at top element without removing)
        int top = stack.Peek();
        Console.WriteLine($"Top element: {top}");
        
        // Pop (remove from top)
        int popped = stack.Pop();
        Console.WriteLine($"Popped: {popped}");
        
        Console.WriteLine($"After pop - Count: {stack.Count}");
        
        // Iterate through stack
        Console.WriteLine("Stack elements:");
        foreach (int item in stack)
        {
            Console.WriteLine(item);
        }
        
        // Clear stack
        stack.Clear();
        Console.WriteLine($"After clear - Count: {stack.Count}");
        
        // Try pop
        bool tryPop = stack.TryPop(out int result);
        Console.WriteLine($"Try pop from empty stack: {tryPop}");
    }
    
    // Practical examples
    public void PracticalExamples()
    {
        // Queue for task processing
        Queue<string> tasks = new Queue<string>();
        tasks.Enqueue("Process order #1");
        tasks.Enqueue("Process order #2");
        tasks.Enqueue("Process order #3");
        
        Console.WriteLine("Processing tasks:");
        while (tasks.Count > 0)
        {
            string task = tasks.Dequeue();
            Console.WriteLine($"Processing: {task}");
        }
        
        // Stack for undo functionality
        Stack<string> undoStack = new Stack<string>();
        undoStack.Push("Added item A");
        undoStack.Push("Added item B");
        undoStack.Push("Added item C");
        
        Console.WriteLine("\nUndo operations:");
        while (undoStack.Count > 0)
        {
            string action = undoStack.Pop();
            Console.WriteLine($"Undo: {action}");
        }
    }
}
```

## LINQ and Collections

### LINQ Operations
```csharp
public class LinqExamples
{
    // LINQ filtering
    public void LinqFiltering()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        
        // Where clause
        var evenNumbers = numbers.Where(n => n % 2 == 0);
        Console.WriteLine($"Even numbers: [{string.Join(", ", evenNumbers)}]");
        
        // Multiple conditions
        var filtered = numbers.Where(n => n > 3 && n < 8);
        Console.WriteLine($"Numbers between 3 and 8: [{string.Join(", ", filtered)}]");
        
        // OfType
        object[] mixed = { 1, "two", 3.0, "four", 5 };
        var strings = mixed.OfType<string>();
        Console.WriteLine($"Strings: [{string.Join(", ", strings)}]");
    }
    
    // LINQ projection
    public void LinqProjection()
    {
        List<Person> people = new List<Person>
        {
            new Person { Name = "John", Age = 25 },
            new Person { Name = "Jane", Age = 30 },
            new Person { Name = "Bob", Age = 35 }
        };
        
        // Select
        var names = people.Select(p => p.Name);
        Console.WriteLine($"Names: [{string.Join(", ", names)}]");
        
        // Select with transformation
        var agesPlusFive = people.Select(p => new { p.Name, AgePlusFive = p.Age + 5 });
        Console.WriteLine("Ages plus 5:");
        foreach (var item in agesPlusFive)
        {
            Console.WriteLine($"{item.Name}: {item.AgePlusFive}");
        }
        
        // SelectMany
        var departments = new List<Department>
        {
            new Department 
            { 
                Name = "IT", 
                Employees = new List<Person> 
                { 
                    new Person { Name = "Alice", Age = 28 },
                    new Person { Name = "Bob", Age = 32 }
                }
            },
            new Department 
            { 
                Name = "HR", 
                Employees = new List<Person> 
                { 
                    new Person { Name = "Charlie", Age = 35 }
                }
            }
        };
        
        var allEmployees = departments.SelectMany(d => d.Employees);
        Console.WriteLine($"\nAll employees: {string.Join(", ", allEmployees.Select(e => e.Name))}");
    }
    
    // LINQ ordering
    public void LinqOrdering()
    {
        List<Person> people = new List<Person>
        {
            new Person { Name = "John", Age = 25 },
            new Person { Name = "Jane", Age = 30 },
            new Person { Name = "Bob", Age = 20 },
            new Person { Name = "Alice", Age = 35 }
        };
        
        // OrderBy
        var byAge = people.OrderBy(p => p.Age);
        Console.WriteLine("Ordered by age:");
        foreach (var person in byAge)
        {
            Console.WriteLine($"{person.Name}: {person.Age}");
        }
        
        // OrderByDescending
        var byNameDesc = people.OrderByDescending(p => p.Name);
        Console.WriteLine("\nOrdered by name (descending):");
        foreach (var person in byNameDesc)
        {
            Console.WriteLine($"{person.Name}: {person.Age}");
        }
        
        // ThenBy
        var ordered = people.OrderBy(p => p.Age).ThenBy(p => p.Name);
        Console.WriteLine("\nOrdered by age then name:");
        foreach (var person in ordered)
        {
            Console.WriteLine($"{person.Name}: {person.Age}");
        }
    }
    
    // LINQ aggregation
    public void LinqAggregation()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        
        // Count
        int count = numbers.Count();
        Console.WriteLine($"Count: {count}");
        
        // Count with condition
        int evenCount = numbers.Count(n => n % 2 == 0);
        Console.WriteLine($"Even count: {evenCount}");
        
        // Sum
        int sum = numbers.Sum();
        Console.WriteLine($"Sum: {sum}");
        
        // Average
        double average = numbers.Average();
        Console.WriteLine($"Average: {average}");
        
        // Min and Max
        int min = numbers.Min();
        int max = numbers.Max();
        Console.WriteLine($"Min: {min}, Max: {max}");
        
        // Aggregate
        string concatenated = numbers.Aggregate("", (acc, n) => acc + n);
        Console.WriteLine($"Concatenated: {concatenated}");
    }
    
    // LINQ grouping
    public void LinqGrouping()
    {
        List<Person> people = new List<Person>
        {
            new Person { Name = "John", Age = 25, Department = "IT" },
            new Person { Name = "Jane", Age = 30, Department = "HR" },
            new Person { Name = "Bob", Age = 25, Department = "IT" },
            new Person { Name = "Alice", Age = 30, Department = "HR" },
            new Person { Name = "Charlie", Age = 35, Department = "IT" }
        };
        
        // GroupBy
        var groupedByAge = people.GroupBy(p => p.Age);
        Console.WriteLine("Grouped by age:");
        foreach (var group in groupedByAge)
        {
            Console.WriteLine($"Age {group.Key}: {string.Join(", ", group.Select(p => p.Name))}");
        }
        
        // GroupBy with multiple keys
        var groupedByAgeAndDept = people.GroupBy(p => new { p.Age, p.Department });
        Console.WriteLine("\nGrouped by age and department:");
        foreach (var group in groupedByAgeAndDept)
        {
            Console.WriteLine($"Age {group.Key.Age}, Dept {group.Key.Department}: {string.Join(", ", group.Select(p => p.Name))}");
        }
    }
    
    // LINQ set operations
    public void LinqSetOperations()
    {
        List<int> set1 = new List<int> { 1, 2, 3, 4, 5 };
        List<int> set2 = new List<int> { 4, 5, 6, 7, 8 };
        
        // Union
        var union = set1.Union(set2);
        Console.WriteLine($"Union: [{string.Join(", ", union)}]");
        
        // Intersection
        var intersection = set1.Intersect(set2);
        Console.WriteLine($"Intersection: [{string.Join(", ", intersection)}]");
        
        // Difference
        var difference = set1.Except(set2);
        Console.WriteLine($"Difference (set1 - set2): [{string.Join(", ", difference)}]");
        
        // Distinct
        List<int> withDuplicates = new List<int> { 1, 2, 2, 3, 3, 3, 4 };
        var distinct = withDuplicates.Distinct();
        Console.WriteLine($"Distinct: [{string.Join(", ", distinct)}]");
    }
}

public class Department
{
    public string Name { get; set; }
    public List<Person> Employees { get; set; }
}
```

## Performance Considerations

### Collection Performance
```csharp
public class CollectionPerformance
{
    // Choosing the right collection
    public void ChooseRightCollection()
    {
        // List<T> - Fast access by index, fast iteration
        List<int> list = new List<int>();
        
        // Dictionary<TKey, TValue> - Fast key lookup
        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        
        // HashSet<T> - Fast uniqueness checking
        HashSet<int> hashSet = new HashSet<int>();
        
        // Queue<T> - FIFO operations
        Queue<string> queue = new Queue<string>();
        
        // Stack<T> - LIFO operations
        Stack<int> stack = new Stack<int>();
        
        // Array - Fixed size, fastest access
        int[] array = new int[100];
    }
    
    // Performance tips
    public void PerformanceTips()
    {
        // Use initial capacity when possible
        List<int> list = new List<int>(1000); // Avoids resizing
        
        // Use AddRange instead of multiple Add calls
        int[] numbers = new int[100];
        list.AddRange(numbers);
        
        // Use LINQ judiciously - it can create intermediate collections
        var filtered = list.Where(x => x > 50).ToList(); // Creates new list
        
        // Use Span<T> for performance-critical operations
        ReadOnlySpan<int> span = numbers.AsSpan();
        
        // Use HashSet<T> for uniqueness checking
        HashSet<int> uniqueNumbers = new HashSet<int>(list);
        
        // Use Dictionary.TryGetValue instead of checking ContainsKey
        Dictionary<string, int> dict = new Dictionary<string, int>();
        if (dict.TryGetValue("key", out int value))
        {
            // Use value
        }
    }
    
    // Memory considerations
    public void MemoryConsiderations()
    {
        // Arrays are stored contiguously in memory
        int[] array = new int[1000];
        
        // List<T> uses an array internally
        List<int> list = new List<int>(1000);
        
        // Dictionary<TKey, TValue> uses more memory for hash table
        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        
        // HashSet<T> also uses hash table
        HashSet<int> hashSet = new HashSet<int>();
        
        // Clear collections when done to free memory
        list.Clear();
        dictionary.Clear();
        hashSet.Clear();
    }
    
    // Large collection handling
    public void LargeCollections()
    {
        // Use streaming with IEnumerable<T> for large datasets
        IEnumerable<int> largeNumbers = GenerateLargeNumbers(1000000);
        
        // Process in chunks
        const int chunkSize = 1000;
        var chunks = largeNumbers.Chunk(chunkSize);
        
        foreach (var chunk in chunks)
        {
            ProcessChunk(chunk);
        }
        
        // Use PLINQ for parallel processing
        var parallelResult = largeNumbers.AsParallel()
            .Where(x => x % 2 == 0)
            .Select(x => x * x);
    }
    
    private IEnumerable<int> GenerateLargeNumbers(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return i;
        }
    }
    
    private void ProcessChunk(int[] chunk)
    {
        // Process chunk of data
    }
}
```

## Best Practices

### Collection Best Practices
```csharp
public class CollectionBestPractices
{
    // Use appropriate collection type
    public void UseAppropriateCollection()
    {
        // Use List<T> when you need indexed access
        List<string> names = new List<string>();
        
        // Use Dictionary<TKey, TValue> for key-value pairs
        Dictionary<string, int> scores = new Dictionary<string, int>();
        
        // Use HashSet<T> for uniqueness
        HashSet<string> uniqueNames = new HashSet<string>();
        
        // Use Queue<T> for FIFO
        Queue<Task> taskQueue = new Queue<Task>();
        
        // Use Stack<T> for LIFO
        Stack<Operation> operationStack = new Stack<Operation>();
    }
    
    // Initialize with capacity when possible
    public void InitializeWithCapacity()
    {
        // Good: Specify initial capacity
        List<int> numbers = new List<int>(1000);
        Dictionary<string, int> dictionary = new Dictionary<string, int>(500);
        
        // Bad: Let collection resize multiple times
        List<int> badNumbers = new List<int>();
        for (int i = 0; i < 1000; i++)
        {
            badNumbers.Add(i); // May cause multiple resizes
        }
    }
    
    // Use readonly interfaces when possible
    public void UseReadOnlyInterfaces()
    {
        List<string> internalList = new List<string> { "A", "B", "C" };
        
        // Expose as read-only
        IReadOnlyList<string> readOnlyList = internalList;
        IReadOnlyCollection<string> readOnlyCollection = internalList;
        
        // Cannot modify through these interfaces
        // readOnlyList.Add("D"); // Compile error
    }
    
    // Use object initializers
    public void UseObjectInitializers()
    {
        // Good: Object initializer
        var person = new Person
        {
            Name = "John",
            Age = 25
        };
        
        // Good: Collection initializer
        List<string> names = new List<string> { "Alice", "Bob", "Charlie" };
        
        Dictionary<string, int> ages = new Dictionary<string, int>
        {
            ["Alice"] = 25,
            ["Bob"] = 30
        };
    }
    
    // Handle null collections safely
    public void HandleNullCollections()
    {
        List<string> names = null;
        
        // Use null-coalescing operator
        List<string> safeNames = names ?? new List<string>();
        
        // Use null-conditional operator
        int count = names?.Count ?? 0;
        
        // Use Any() to check if collection has items
        bool hasItems = safeNames.Any();
        
        // Use Empty<T>() instead of null
        List<string> emptyList = Enumerable.Empty<string>();
    }
    
    // Use LINQ effectively
    public void UseLinqEffectively()
    {
        List<Person> people = new List<Person>();
        
        // Good: Chain LINQ operators
        var result = people
            .Where(p => p.Age >= 18)
            .OrderBy(p => p.Name)
            .Select(p => new { p.Name, p.Age })
            .Take(10);
        
        // Good: Use method syntax for complex queries
        var complexResult = people
            .GroupBy(p => p.Department)
            .Select(g => new { Department = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count);
        
        // Bad: Create unnecessary intermediate collections
        var badResult = people.Where(p => p.Age >= 18).ToList(); // Unnecessary ToList()
        var badResult2 = badResult.OrderBy(p => p.Name).ToList(); // Another ToList()
    }
}
```

### Error Handling
```csharp
public class CollectionErrorHandling
{
    // Handle index out of range
    public void HandleIndexOutOfRange()
    {
        List<string> names = new List<string> { "Alice", "Bob", "Charlie" };
        
        // Bad: May throw exception
        // string name = names[10];
        
        // Good: Check bounds first
        if (names.Count > 10)
        {
            string name = names[10];
        }
        
        // Good: Use TryGetValue for Dictionary
        Dictionary<string, int> ages = new Dictionary<string, int>();
        if (ages.TryGetValue("Alice", out int age))
        {
            Console.WriteLine($"Alice's age: {age}");
        }
        
        // Good: Use TryDequeue/TryPop for Queue/Stack
        Queue<string> queue = new Queue<string>();
        if (queue.TryDequeue(out string item))
        {
            Console.WriteLine($"Dequeued: {item}");
        }
    }
    
    // Handle null collections
    public void HandleNullCollections()
    {
        List<string> names = null;
        
        // Bad: NullReferenceException
        // int count = names.Count;
        
        // Good: Null check
        int count = names?.Count ?? 0;
        
        // Good: Use null-coalescing
        List<string> safeNames = names ?? new List<string>();
        
        // Good: Use Any() to check for emptiness
        bool isEmpty = !safeNames.Any();
    }
    
    // Handle collection modification during iteration
    public void HandleModificationDuringIteration()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        
        // Bad: Throws InvalidOperationException
        // foreach (int num in numbers)
        // {
        //     if (num % 2 == 0)
        //         numbers.Remove(num);
        // }
        
        // Good: Create a copy or use ToList()
        foreach (int num in numbers.ToList())
        {
            if (num % 2 == 0)
                numbers.Remove(num);
        }
        
        // Good: Use for loop with index
        for (int i = numbers.Count - 1; i >= 0; i--)
        {
            if (numbers[i] % 2 == 0)
                numbers.RemoveAt(i);
        }
    }
}
```

## Common Pitfalls

### Common Collection Mistakes
```csharp
public class CommonPitfalls
{
    // Pitfall: Using wrong collection type
    public void WrongCollectionType()
    {
        // Bad: Using List<T> for uniqueness checking
        List<string> names = new List<string>();
        if (!names.Contains("Alice"))
            names.Add("Alice"); // O(n) operation
        
        // Good: Using HashSet<T> for uniqueness
        HashSet<string> uniqueNames = new HashSet<string>();
        uniqueNames.Add("Alice"); // O(1) operation
    }
    
    // Pitfall: Not specifying initial capacity
    public void NotSpecifyingCapacity()
    {
        // Bad: Multiple resizes
        List<int> numbers = new List<int>();
        for (int i = 0; i < 10000; i++)
        {
            numbers.Add(i); // May cause multiple resizes
        }
        
        // Good: Specify initial capacity
        List<int> goodNumbers = new List<int>(10000);
        for (int i = 0; i < 10000; i++)
        {
            goodNumbers.Add(i);
        }
    }
    
    // Pitfall: Modifying collection during iteration
    public void ModifyingDuringIteration()
    {
        List<string> names = new List<string> { "Alice", "Bob", "Charlie" };
        
        // Bad: Throws exception
        // foreach (string name in names)
        // {
        //     if (name == "Bob")
        //         names.Remove(name);
        // }
        
        // Good: Use ToList() or iterate backwards
        foreach (string name in names.ToList())
        {
            if (name == "Bob")
                names.Remove(name);
        }
    }
    
    // Pitfall: Not handling null collections
    public void NotHandlingNullCollections()
    {
        List<string> names = null;
        
        // Bad: NullReferenceException
        // foreach (string name in names)
        // {
        //     Console.WriteLine(name);
        // }
        
        // Good: Null check
        if (names != null)
        {
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
        }
        
        // Better: Use null-coalescing
        foreach (string name in names ?? Enumerable.Empty<string>())
        {
            Console.WriteLine(name);
        }
    }
    
    // Pitfall: Using LINQ inefficiently
    public void InefficientLinq()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        
        // Bad: Multiple enumerations
        if (numbers.Any(n => n > 3))
        {
            int first = numbers.First(n => n > 3); // Enumerates again
            int count = numbers.Count(n => n > 3); // Enumerates again
        }
        
        // Good: Cache the result
        var greaterThanThree = numbers.Where(n => n > 3);
        if (greaterThanThree.Any())
        {
            int first = greaterThanThree.First();
            int count = greaterThanThree.Count();
        }
    }
    
    // Pitfall: Not disposing collections that implement IDisposable
    public void NotDisposingCollections()
    {
        // Bad: Not disposing
        // var stream = new MemoryStream();
        // // Use stream but don't dispose
        
        // Good: Use using statement
        using (var stream = new MemoryStream())
        {
            // Use stream
        } // Automatically disposed
    }
}
```

## Summary

C# arrays and collections provide:

**Arrays:**
- Single-dimensional arrays for fixed-size collections
- Multi-dimensional arrays for matrix-like structures
- Jagged arrays for arrays of arrays with different lengths
- Efficient memory layout and fast access

**Generic Collections:**
- List<T> for dynamic lists with indexed access
- Dictionary<TKey, TValue> for key-value pairs with fast lookup
- HashSet<T> for unique elements with fast operations
- Queue<T> for FIFO (First In, First Out) operations
- Stack<T> for LIFO (Last In, First Out) operations

**LINQ Operations:**
- Filtering with Where clause
- Projection with Select and SelectMany
- Ordering with OrderBy, OrderByDescending, ThenBy
- Aggregation with Count, Sum, Average, Min, Max
- Grouping with GroupBy
- Set operations with Union, Intersect, Except, Distinct

**Performance Considerations:**
- Choose appropriate collection type for the use case
- Specify initial capacity when possible
- Use streaming for large datasets
- Consider memory usage and garbage collection

**Best Practices:**
- Use appropriate collection types
- Initialize with capacity when possible
- Use readonly interfaces for exposure
- Handle null collections safely
- Use LINQ effectively and efficiently

**Common Pitfalls:**
- Using wrong collection type for the task
- Not specifying initial capacity
- Modifying collections during iteration
- Not handling null collections
- Using LINQ inefficiently
- Not disposing disposable collections

C# provides a rich set of collection types that are optimized for different use cases, making it easy to work with data in various scenarios while maintaining good performance and memory efficiency.
