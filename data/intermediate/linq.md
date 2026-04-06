# C# LINQ (Language Integrated Query)

## LINQ Fundamentals

### LINQ Query Syntax
```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class LinqBasics
{
    public void DemonstrateQuerySyntax()
    {
        // Data source
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        
        // Query syntax
        var query = from num in numbers
                    where num % 2 == 0
                    select num * 2;
        
        Console.WriteLine("Even numbers doubled:");
        foreach (int num in query)
        {
            Console.WriteLine(num);
        }
        
        // More complex query
        var complexQuery = from num in numbers
                          where num > 3 && num < 8
                          orderby num descending
                          select new { Number = num, Square = num * num };
        
        Console.WriteLine("\nNumbers between 3 and 8 (descending):");
        foreach (var item in complexQuery)
        {
            Console.WriteLine($"{item.Number}: {item.Square}");
        }
    }
    
    public void DemonstrateMethodSyntax()
    {
        List<string> words = new List<string> 
        { 
            "apple", "banana", "cherry", "date", "elderberry", 
            "fig", "grape", "honeydew" 
        };
        
        // Method syntax
        var query = words
            .Where(word => word.Length > 5)
            .OrderBy(word => word)
            .Select(word => word.ToUpper());
        
        Console.WriteLine("Words longer than 5 characters (uppercase, sorted):");
        foreach (string word in query)
        {
            Console.WriteLine(word);
        }
        
        // Chaining methods
        var complexQuery = words
            .Where(word => word.StartsWith('a') || word.StartsWith('e'))
            .OrderByDescending(word => word.Length)
            .ThenBy(word => word)
            .Select(word => new { Word = word, Length = word.Length });
        
        Console.WriteLine("\nWords starting with 'a' or 'e' (by length desc):");
        foreach (var item in complexQuery)
        {
            Console.WriteLine($"{item.Word} ({item.Length})");
        }
    }
}
```

### LINQ Data Sources
```csharp
public class LinqDataSources
{
    // LINQ with arrays
    public void QueryArrays()
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        
        var squares = from n in numbers
                     where n % 2 == 0
                     select n * n;
        
        Console.WriteLine("Squares of even numbers:");
        foreach (int square in squares)
        {
            Console.WriteLine(square);
        }
    }
    
    // LINQ with lists
    public void QueryLists()
    {
        List<string> fruits = new List<string> 
        { 
            "Apple", "Banana", "Cherry", "Date", "Elderberry" 
        };
        
        var longFruits = from fruit in fruits
                         where fruit.Length > 5
                         select fruit;
        
        Console.WriteLine("Fruits with more than 5 characters:");
        foreach (string fruit in longFruits)
        {
            Console.WriteLine(fruit);
        }
    }
    
    // LINQ with dictionaries
    public void QueryDictionaries()
    {
        Dictionary<string, int> studentAges = new Dictionary<string, int>
        {
            ["Alice"] = 20,
            ["Bob"] = 22,
            ["Charlie"] = 21,
            ["Diana"] = 23
        };
        
        var adults = from student in studentAges
                    where student.Value >= 21
                    select student;
        
        Console.WriteLine("Students 21 or older:");
        foreach (var student in adults)
        {
            Console.WriteLine($"{student.Key}: {student.Value}");
        }
    }
    
    // LINQ with custom objects
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public List<string> Hobbies { get; set; }
    }
    
    public void QueryCustomObjects()
    {
        List<Person> people = new List<Person>
        {
            new Person { Name = "Alice", Age = 25, City = "New York", Hobbies = new List<string> { "Reading", "Swimming" } },
            new Person { Name = "Bob", Age = 30, City = "Los Angeles", Hobbies = new List<string> { "Gaming", "Cooking" } },
            new Person { Name = "Charlie", Age = 35, City = "Chicago", Hobbies = new List<string> { "Music", "Travel" } },
            new Person { Name = "Diana", Age = 28, City = "New York", Hobbies = new List<string> { "Photography", "Yoga" } }
        };
        
        var newYorkers = from person in people
                         where person.City == "New York"
                         select person;
        
        Console.WriteLine("People from New York:");
        foreach (Person person in newYorkers)
        {
            Console.WriteLine($"{person.Name}, {person.Age}");
        }
        
        // Complex query with nested collections
        var peopleWithReading = from person in people
                                where person.Hobbies.Contains("Reading")
                                select person;
        
        Console.WriteLine("\nPeople who enjoy reading:");
        foreach (Person person in peopleWithReading)
        {
            Console.WriteLine($"{person.Name} from {person.City}");
        }
    }
}
```

## LINQ Operators

### Filtering Operators
```csharp
public class FilteringOperators
{
    public void DemonstrateWhere()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        
        // Single condition
        var evenNumbers = numbers.Where(n => n % 2 == 0);
        
        // Multiple conditions
        var filteredNumbers = numbers.Where(n => n > 3 && n < 8);
        
        // Complex condition
        var complexFilter = numbers.Where(n => n % 2 == 0 && n * n < 50);
        
        Console.WriteLine("Even numbers: " + string.Join(", ", evenNumbers));
        Console.WriteLine("Numbers between 3 and 8: " + string.Join(", ", filteredNumbers));
        Console.WriteLine("Complex filter: " + string.Join(", ", complexFilter));
    }
    
    public void DemonstrateOfType()
    {
        object[] mixed = { 1, "two", 3.0, "four", 5, "six", 7.0 };
        
        var integers = mixed.OfType<int>();
        var strings = mixed.OfType<string>();
        var doubles = mixed.OfType<double>();
        
        Console.WriteLine("Integers: " + string.Join(", ", integers));
        Console.WriteLine("Strings: " + string.Join(", ", strings));
        Console.WriteLine("Doubles: " + string.Join(", ", doubles));
    }
    
    public void DemonstrateFilterByIndex()
    {
        List<string> words = new List<string> { "one", "two", "three", "four", "five" };
        
        // Filter by index (even indices)
        var evenIndices = words.Where((word, index) => index % 2 == 0);
        
        Console.WriteLine("Words at even indices: " + string.Join(", ", evenIndices));
    }
}
```

### Projection Operators
```csharp
public class ProjectionOperators
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }
    
    public void DemonstrateSelect()
    {
        List<Person> people = new List<Person>
        {
            new Person { FirstName = "John", LastName = "Doe", Age = 30, City = "New York" },
            new Person { FirstName = "Jane", LastName = "Smith", Age = 25, City = "Los Angeles" },
            new Person { FirstName = "Bob", LastName = "Johnson", Age = 35, City = "Chicago" }
        };
        
        // Select single property
        var names = people.Select(p => p.FirstName);
        
        // Select anonymous type
        var personInfo = people.Select(p => new { p.FirstName, p.LastName, FullName = p.FirstName + " " + p.LastName });
        
        // Select with transformation
        var agesPlusFive = people.Select(p => new { p.FirstName, AgePlusFive = p.Age + 5 });
        
        Console.WriteLine("First names: " + string.Join(", ", names));
        
        foreach (var info in personInfo)
        {
            Console.WriteLine($"{info.FirstName} {info.LastName} ({info.FullName})");
        }
        
        foreach (var ageInfo in agesPlusFive)
        {
            Console.WriteLine($"{ageInfo.FirstName} will be {ageInfo.AgePlusFive} in 5 years");
        }
    }
    
    public void DemonstrateSelectMany()
    {
        // Flatten nested collections
        List<List<string>> categories = new List<List<string>>
        {
            new List<string> { "Apple", "Banana", "Cherry" },
            new List<string> { "Dog", "Cat", "Bird" },
            new List<string> { "Red", "Green", "Blue" }
        };
        
        var allItems = categories.SelectMany(category => category);
        
        Console.WriteLine("All items: " + string.Join(", ", allItems));
        
        // SelectMany with indexes
        var indexedItems = categories.SelectMany((category, index) => 
            category.Select(item => new { Category = index, Item = item }));
        
        Console.WriteLine("\nIndexed items:");
        foreach (var item in indexedItems)
        {
            Console.WriteLine($"Category {item.Category}: {item.Item}");
        }
        
        // Complex SelectMany with people and hobbies
        List<Person> people = new List<Person>
        {
            new Person { FirstName = "John", LastName = "Doe", Age = 30, City = "New York", Hobbies = new List<string> { "Reading", "Swimming" } },
            new Person { FirstName = "Jane", LastName = "Smith", Age = 25, City = "Los Angeles", Hobbies = new List<string> { "Gaming", "Cooking" } }
        };
        
        var allHobbies = people.SelectMany(p => p.Hobbies);
        
        Console.WriteLine("\nAll hobbies: " + string.Join(", ", allHobbies));
        
        // SelectMany with transformation
        var personHobbies = people.SelectMany(p => p.Hobbies, (p, hobby) => new { p.FirstName, Hobby = hobby });
        
        Console.WriteLine("\nPerson-hobby pairs:");
        foreach (var ph in personHobbies)
        {
            Console.WriteLine($"{ph.FirstName}: {ph.Hobby}");
        }
    }
}
```

### Ordering Operators
```csharp
public class OrderingOperators
{
    public class Student
    {
        public string Name { get; set; }
        public int Grade { get; set; }
        public int Age { get; set; }
        public string Subject { get; set; }
    }
    
    public void DemonstrateOrderBy()
    {
        List<Student> students = new List<Student>
        {
            new Student { Name = "Alice", Grade = 85, Age = 20, Subject = "Math" },
            new Student { Name = "Bob", Grade = 92, Age = 22, Subject = "Science" },
            new Student { Name = "Charlie", Grade = 78, Age = 19, Subject = "Math" },
            new Student { Name = "Diana", Grade = 88, Age = 21, Subject = "English" },
            new Student { Name = "Eve", Grade = 95, Age = 20, Subject = "Science" }
        };
        
        // Order by single property
        var byGrade = students.OrderBy(s => s.Grade);
        
        // Order by multiple properties
        var byGradeThenAge = students.OrderBy(s => s.Grade).ThenBy(s => s.Age);
        
        // Order by descending
        var byGradeDesc = students.OrderByDescending(s => s.Grade);
        
        // Order by multiple with different directions
        var complexOrder = students.OrderBy(s => s.Subject).ThenByDescending(s => s.Grade).ThenBy(s => s.Name);
        
        Console.WriteLine("Students by grade (ascending):");
        foreach (var student in byGrade)
        {
            Console.WriteLine($"{student.Name}: {student.Grade}");
        }
        
        Console.WriteLine("\nStudents by grade then age:");
        foreach (var student in byGradeThenAge)
        {
            Console.WriteLine($"{student.Name}: Grade {student.Grade}, Age {student.Age}");
        }
        
        Console.WriteLine("\nStudents by grade (descending):");
        foreach (var student in byGradeDesc)
        {
            Console.WriteLine($"{student.Name}: {student.Grade}");
        }
        
        Console.WriteLine("\nComplex ordering:");
        foreach (var student in complexOrder)
        {
            Console.WriteLine($"{student.Name}: {student.Subject}, {student.Grade}");
        }
    }
    
    public void DemonstrateCustomComparer()
    {
        List<string> words = new List<string> { "apple", "Banana", "cherry", "Date", "elderberry" };
        
        // Case-insensitive ordering
        var caseInsensitive = words.OrderBy(w => w, StringComparer.OrdinalIgnoreCase);
        
        // Custom length comparer
        var byLength = words.OrderBy(w => w.Length).ThenBy(w => w);
        
        Console.WriteLine("Case-insensitive ordering:");
        foreach (string word in caseInsensitive)
        {
            Console.WriteLine(word);
        }
        
        Console.WriteLine("\nBy length then alphabetically:");
        foreach (string word in byLength)
        {
            Console.WriteLine(word);
        }
    }
}
```

### Grouping Operators
```csharp
public class GroupingOperators
{
    public class Employee
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public int YearsOfService { get; set; }
    }
    
    public void DemonstrateGroupBy()
    {
        List<Employee> employees = new List<Employee>
        {
            new Employee { Name = "Alice", Department = "IT", Salary = 75000, YearsOfService = 5 },
            new Employee { Name = "Bob", Department = "HR", Salary = 65000, YearsOfService = 3 },
            new Employee { Name = "Charlie", Department = "IT", Salary = 80000, YearsOfService = 7 },
            new Employee { Name = "Diana", Department = "Finance", Salary = 85000, YearsOfService = 10 },
            new Employee { Name = "Eve", Department = "HR", Salary = 60000, YearsOfService = 2 },
            new Employee { Name = "Frank", Department = "IT", Salary = 90000, YearsOfService = 12 }
        };
        
        // Group by single property
        var byDepartment = employees.GroupBy(e => e.Department);
        
        Console.WriteLine("Employees grouped by department:");
        foreach (var group in byDepartment)
        {
            Console.WriteLine($"\n{group.Key} Department:");
            foreach (var employee in group)
            {
                Console.WriteLine($"  {employee.Name}: ${employee.Salary:N0}");
            }
        }
        
        // Group by multiple keys
        var byDeptAndService = employees.GroupBy(e => new { e.Department, e.YearsOfService });
        
        Console.WriteLine("\nEmployees grouped by department and years of service:");
        foreach (var group in byDeptAndService)
        {
            Console.WriteLine($"{group.Key.Department}, {group.Key.YearsOfService} years:");
            foreach (var employee in group)
            {
                Console.WriteLine($"  {employee.Name}");
            }
        }
        
        // Group with transformation
        var deptStats = employees.GroupBy(e => e.Department)
                               .Select(g => new 
                               { 
                                   Department = g.Key, 
                                   Count = g.Count(), 
                                   AvgSalary = g.Average(e => e.Salary),
                                   MaxSalary = g.Max(e => e.Salary)
                               });
        
        Console.WriteLine("\nDepartment statistics:");
        foreach (var stat in deptStats)
        {
            Console.WriteLine($"{stat.Department}: {stat.Count} employees, " +
                             $"Avg: ${stat.AvgSalary:N0}, Max: ${stat.MaxSalary:N0}");
        }
    }
    
    public void DemonstrateGroupJoin()
    {
        // Departments
        var departments = new[]
        {
            new { DeptId = 1, DeptName = "IT" },
            new { DeptId = 2, DeptName = "HR" },
            new { DeptId = 3, DeptName = "Finance" },
            new { DeptId = 4, DeptName = "Marketing" }
        };
        
        // Employees
        var employees = new[]
        {
            new { EmpId = 1, Name = "Alice", DeptId = 1 },
            new { EmpId = 2, Name = "Bob", DeptId = 2 },
            new { EmpId = 3, Name = "Charlie", DeptId = 1 },
            new { EmpId = 4, Name = "Diana", DeptId = 3 }
        };
        
        // Group join - departments with their employees
        var deptWithEmployees = departments.GroupJoin(
            employees,
            dept => dept.DeptId,
            emp => emp.DeptId,
            (dept, emps) => new { Department = dept.DeptName, Employees = emps }
        );
        
        Console.WriteLine("Departments with employees:");
        foreach (var dept in deptWithEmployees)
        {
            Console.WriteLine($"\n{dept.Department}:");
            foreach (var emp in dept.Employees.DefaultIfEmpty())
            {
                Console.WriteLine($"  {emp.Name}");
            }
        }
    }
}
```

### Set Operators
```csharp
public class SetOperators
{
    public void DemonstrateUnion()
    {
        List<int> set1 = new List<int> { 1, 2, 3, 4, 5 };
        List<int> set2 = new List<int> { 4, 5, 6, 7, 8 };
        
        var union = set1.Union(set2);
        var unionWithComparer = set1.Union(set2, new CustomComparer());
        
        Console.WriteLine("Union: " + string.Join(", ", union));
    }
    
    public void DemonstrateIntersection()
    {
        List<string> list1 = new List<string> { "apple", "banana", "cherry", "date" };
        List<string> list2 = new List<string> { "cherry", "date", "elderberry", "fig" };
        
        var intersection = list1.Intersect(list2);
        
        Console.WriteLine("Intersection: " + string.Join(", ", intersection));
    }
    
    public void DemonstrateExcept()
    {
        List<int> set1 = new List<int> { 1, 2, 3, 4, 5 };
        List<int> set2 = new List<int> { 4, 5, 6, 7, 8 };
        
        var difference = set1.Except(set2);
        
        Console.WriteLine("Set1 - Set2: " + string.Join(", ", difference));
    }
    
    public void DemonstrateDistinct()
    {
        List<int> numbers = new List<int> { 1, 2, 2, 3, 3, 3, 4, 5, 5 };
        
        var distinct = numbers.Distinct();
        var distinctByLength = numbers.DistinctBy(n => n.ToString().Length);
        
        Console.WriteLine("Distinct numbers: " + string.Join(", ", distinct));
        Console.WriteLine("Distinct by length: " + string.Join(", ", distinctByLength));
    }
    
    public void DemonstrateConcat()
    {
        List<string> list1 = new List<string> { "apple", "banana" };
        List<string> list2 = new List<string> { "cherry", "date" };
        
        var concatenated = list1.Concat(list2);
        
        Console.WriteLine("Concatenated: " + string.Join(", ", concatenated));
    }
    
    private class CustomComparer : IEqualityComparer<int>
    {
        public bool Equals(int x, int y) => x == y;
        public int GetHashCode(int obj) => obj.GetHashCode();
    }
}
```

### Aggregation Operators
```csharp
public class AggregationOperators
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
    }
    
    public void DemonstrateBasicAggregations()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        
        int count = numbers.Count();
        int sum = numbers.Sum();
        double average = numbers.Average();
        int min = numbers.Min();
        int max = numbers.Max();
        
        Console.WriteLine($"Count: {count}");
        Console.WriteLine($"Sum: {sum}");
        Console.WriteLine($"Average: {average}");
        Console.WriteLine($"Min: {min}");
        Console.WriteLine($"Max: {max}");
    }
    
    public void DemonstrateConditionalAggregations()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        
        int evenCount = numbers.Count(n => n % 2 == 0);
        int evenSum = numbers.Sum(n => n % 2 == 0 ? n : 0);
        double evenAverage = numbers.Average(n => n % 2 == 0 ? n : double.NaN);
        
        Console.WriteLine($"Even count: {evenCount}");
        Console.WriteLine($"Even sum: {evenSum}");
        Console.WriteLine($"Even average: {evenAverage}");
    }
    
    public void DemonstrateProductAggregations()
    {
        List<Product> products = new List<Product>
        {
            new Product { Name = "Laptop", Price = 999.99m, Quantity = 5, Category = "Electronics" },
            new Product { Name = "Mouse", Price = 25.99m, Quantity = 20, Category = "Electronics" },
            new Product { Name = "Book", Price = 19.99m, Quantity = 15, Category = "Books" },
            new Product { Name = "Pen", Price = 2.99m, Quantity = 50, Category = "Office" }
        };
        
        decimal totalValue = products.Sum(p => p.Price * p.Quantity);
        int totalQuantity = products.Sum(p => p.Quantity);
        decimal averagePrice = products.Average(p => p.Price);
        
        Console.WriteLine($"Total value: ${totalValue:N2}");
        Console.WriteLine($"Total quantity: {totalQuantity}");
        Console.WriteLine($"Average price: ${averagePrice:N2}");
    }
    
    public void DemonstrateAggregate()
    {
        List<string> words = new List<string> { "Hello", "World", "LINQ", "is", "awesome" };
        
        // Custom aggregation
        string concatenated = words.Aggregate((acc, word) => acc + " " + word);
        
        // Aggregate with seed
        int product = numbers.Aggregate(1, (acc, n) => acc * n);
        
        // Aggregate with seed and result selector
        string result = words.Aggregate(
            "Words: ",
            (acc, word) => acc + word + ", ",
            acc => acc.TrimEnd(',', ' ')
        );
        
        Console.WriteLine($"Concatenated: {concatenated}");
        Console.WriteLine($"Product: {product}");
        Console.WriteLine($"Result: {result}");
    }
}
```

## Advanced LINQ

### LINQ with XML
```csharp
using System.Xml.Linq;

public class LinqWithXml
{
    public void DemonstrateLinqToXml()
    {
        // Create XML
        XElement root = new XElement("People",
            new XElement("Person",
                new XAttribute("Id", 1),
                new XElement("Name", "John Doe"),
                new XElement("Age", 30),
                new XElement("City", "New York")
            ),
            new XElement("Person",
                new XAttribute("Id", 2),
                new XElement("Name", "Jane Smith"),
                new XElement("Age", 25),
                new XElement("City", "Los Angeles")
            )
        );
        
        // Query XML
        var people = from person in root.Elements("Person")
                     where (int)person.Element("Age") > 25
                     select new
                     {
                         Id = (int)person.Attribute("Id"),
                         Name = (string)person.Element("Name"),
                         Age = (int)person.Element("Age"),
                         City = (string)person.Element("City")
                     };
        
        Console.WriteLine("People over 25:");
        foreach (var person in people)
        {
            Console.WriteLine($"{person.Name}, {person.Age}, {person.City}");
        }
    }
}
```

### LINQ with SQL
```csharp
using System.Data.Linq;
using System.Data.SqlClient;

public class LinqToSql
{
    public class DataContext : DataContext
    {
        public Table<Customer> Customers;
        public Table<Order> Orders;
        
        public DataContext(string connectionString) : base(connectionString) { }
    }
    
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }
    
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public DateTime OrderDate { get; set; }
    }
    
    public void DemonstrateLinqToSql()
    {
        string connectionString = "Data Source=.;Initial Catalog=MyDatabase;Integrated Security=True";
        
        using (var db = new DataContext(connectionString))
        {
            // Query customers
            var customers = from customer in db.Customers
                           where customer.City == "New York"
                           select customer;
            
            // Query with join
            var orders = from customer in db.Customers
                         join order in db.Orders on customer.CustomerId equals order.CustomerId
                         where customer.City == "New York"
                         select new { customer.Name, order.Amount, order.OrderDate };
            
            Console.WriteLine("New York customers:");
            foreach (var customer in customers)
            {
                Console.WriteLine(customer.Name);
            }
        }
    }
}
```

### Parallel LINQ (PLINQ)
```csharp
using System.Threading.Tasks;

public class ParallelLinq
{
    public void DemonstratePlinq()
    {
        List<int> numbers = Enumerable.Range(1, 1000000).ToList();
        
        // Sequential LINQ
        var sequentialResult = numbers.Where(n => n % 2 == 0)
                                   .Select(n => n * n)
                                   .Take(100);
        
        // Parallel LINQ
        var parallelResult = numbers.AsParallel()
                                   .Where(n => n % 2 == 0)
                                   .Select(n => n * n)
                                   .Take(100);
        
        Console.WriteLine($"Sequential result count: {sequentialResult.Count()}");
        Console.WriteLine($"Parallel result count: {parallelResult.Count()}");
    }
    
    public void DemonstrateParallelOptions()
    {
        List<string> words = new List<string>();
        for (int i = 0; i < 10000; i++)
        {
            words.Add($"Word{i}");
        }
        
        // With degree of parallelism
        var result = words.AsParallel()
                        .WithDegreeOfParallelism(4)
                        .Where(w => w.Contains("5"))
                        .OrderBy(w => w);
        
        Console.WriteLine($"Words containing '5': {result.Count()}");
    }
}
```

## LINQ Best Practices

### Performance Considerations
```csharp
public class LinqPerformance
{
    // Good: Cache LINQ results
    public void CacheLinqResults()
    {
        List<int> numbers = Enumerable.Range(1, 100).ToList();
        
        // Bad: Multiple enumerations
        if (numbers.Any(n => n > 50))
        {
            int first = numbers.First(n => n > 50);
            int count = numbers.Count(n => n > 50);
        }
        
        // Good: Cache the result
        var greaterThan50 = numbers.Where(n => n > 50);
        if (greaterThan50.Any())
        {
            int first = greaterThan50.First();
            int count = greaterThan50.Count();
        }
    }
    
    // Good: Use appropriate methods
    public void UseAppropriateMethods()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        
        // Use Any() instead of Count() > 0
        bool hasElements = numbers.Any(); // Good
        // bool hasElements = numbers.Count() > 0; // Bad
        
        // Use FirstOrDefault() instead of First() with default
        int first = numbers.FirstOrDefault(); // Good
        // int first = numbers.Count() > 0 ? numbers.First() : 0; // Bad
        
        // Use Contains() for existence check
        bool contains = numbers.Contains(3); // Good
        // bool contains = numbers.Any(n => n == 3); // Less efficient
    }
    
    // Good: Avoid LINQ for simple operations
    public void AvoidLinqForSimpleOperations()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        
        // Bad: Using LINQ for simple operations
        int sum = numbers.Sum(); // OK for complex, but for simple...
        int count = numbers.Count(); // Use Count property instead
        
        // Good: Use direct access for simple operations
        int directCount = numbers.Count; // Property access
        int directSum = 0;
        foreach (int n in numbers)
        {
            directSum += n;
        }
    }
}
```

### LINQ Patterns
```csharp
public class LinqPatterns
{
    // Builder pattern with LINQ
    public class QueryBuilder<T>
    {
        private IQueryable<T> _query;
        
        public QueryBuilder(IQueryable<T> query)
        {
            _query = query;
        }
        
        public QueryBuilder<T> Where(Func<T, bool> predicate)
        {
            _query = _query.Where(predicate);
            return this;
        }
        
        public QueryBuilder<T> OrderBy<TKey>(Func<T, TKey> keySelector)
        {
            _query = _query.OrderBy(keySelector);
            return this;
        }
        
        public QueryBuilder<T> OrderByDescending<TKey>(Func<T, TKey> keySelector)
        {
            _query = _query.OrderByDescending(keySelector);
            return this;
        }
        
        public IQueryable<T> Build() => _query;
    }
    
    public void DemonstrateQueryBuilder()
    {
        List<string> words = new List<string> { "apple", "banana", "cherry", "date" };
        
        var query = new QueryBuilder<string>(words.AsQueryable())
            .Where(w => w.Length > 5)
            .OrderBy(w => w)
            .Build();
        
        foreach (string word in query)
        {
            Console.WriteLine(word);
        }
    }
    
    // Specification pattern with LINQ
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> ToExpression();
    }
    
    public class NameContainsSpecification : ISpecification<string>
    {
        private readonly string _contains;
        
        public NameContainsSpecification(string contains)
        {
            _contains = contains;
        }
        
        public Expression<Func<string, bool>> ToExpression()
        {
            return name => name.Contains(_contains);
        }
    }
    
    public void DemonstrateSpecification()
    {
        List<string> names = new List<string> { "Alice", "Bob", "Charlie", "Diana" };
        
        var spec = new NameContainsSpecification("a");
        var filteredNames = names.Where(spec.ToExpression().Compile());
        
        Console.WriteLine("Names containing 'a':");
        foreach (string name in filteredNames)
        {
            Console.WriteLine(name);
        }
    }
}
```

## Common Pitfalls

### Common LINQ Mistakes
```csharp
public class CommonLinqMistakes
{
    // Mistake: Multiple enumeration
    public void MultipleEnumeration()
    {
        List<int> numbers = Enumerable.Range(1, 100).ToList();
        
        // Bad: Enumerates multiple times
        var evenNumbers = numbers.Where(n => n % 2 == 0);
        if (evenNumbers.Any())
        {
            int first = evenNumbers.First(); // Enumerates again
            int count = evenNumbers.Count();   // Enumerates again
        }
        
        // Good: Cache the result
        var evenNumbersCached = numbers.Where(n => n % 2 == 0).ToList();
        if (evenNumbersCached.Any())
        {
            int first = evenNumbersCached.First();
            int count = evenNumbersCached.Count();
        }
    }
    
    // Mistake: Using LINQ for simple operations
    public void UnnecessaryLinq()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        
        // Bad: Using LINQ for simple operations
        int sum = numbers.Sum(); // Could be faster with a simple loop
        bool any = numbers.Any(n => n > 3); // Could be faster with a simple check
        
        // Good: Use LINQ for complex operations
        var complexResult = numbers
            .Where(n => n % 2 == 0)
            .Select(n => n * n)
            .OrderBy(n => n)
            .Take(5);
    }
    
    // Mistake: Not handling null collections
    public void NullCollections()
    {
        List<string> names = null;
        
        // Bad: Will throw NullReferenceException
        // var count = names.Count();
        
        // Good: Check for null or use null-coalescing
        int count = names?.Count() ?? 0;
        
        // Better: Use Enumerable.Empty()
        List<string> safeNames = names ?? Enumerable.Empty<string>();
        int safeCount = safeNames.Count();
    }
    
    // Mistake: Inefficient queries
    public void InefficientQueries()
    {
        List<Person> people = new List<Person>();
        
        // Bad: Inefficient nested queries
        var result = people
            .Where(p => p.Hobbies.Any(h => h.StartsWith("A")))
            .Select(p => new 
            { 
                p.Name, 
                AHobbies = p.Hobbies.Where(h => h.StartsWith("A")).ToList() 
            });
        
        // Good: Use SelectMany or optimize the query
        var efficientResult = people
            .SelectMany(p => p.Hobbies.Where(h => h.StartsWith("A")), (p, h) => new { p.Name, Hobby = h });
    }
    
    public class Person
    {
        public string Name { get; set; }
        public List<string> Hobbies { get; set; }
    }
}
```

## Summary

C# LINQ provides:

**Fundamentals:**
- Query syntax vs method syntax
- Deferred execution
- Extension methods
- Lambda expressions

**Filtering Operators:**
- Where for conditional filtering
- OfType for type filtering
- FilterByIndex for index-based filtering

**Projection Operators:**
- Select for transformation
- SelectMany for flattening
- Anonymous types for custom projections

**Ordering Operators:**
- OrderBy and OrderByDescending
- ThenBy and ThenByDescending
- Custom comparers

**Grouping Operators:**
- GroupBy for grouping
- GroupJoin for hierarchical data
- Lookup for optimized lookups

**Set Operators:**
- Union for combining collections
- Intersect for finding common elements
- Except for finding differences
- Distinct for unique elements
- Concat for concatenation

**Aggregation Operators:**
- Count, Sum, Average, Min, Max
- Conditional aggregations
- Aggregate for custom aggregations

**Advanced Features:**
- LINQ to XML
- LINQ to SQL
- Parallel LINQ (PLINQ)
- Custom LINQ providers

**Best Practices:**
- Cache LINQ results to avoid multiple enumeration
- Use appropriate methods for the task
- Avoid LINQ for simple operations
- Handle null collections properly
- Consider performance implications

**Common Pitfalls:**
- Multiple enumeration of the same query
- Using LINQ for simple operations
- Not handling null collections
- Inefficient nested queries
- Not understanding deferred execution

LINQ provides a powerful, consistent way to query and manipulate data from various sources, making data manipulation code more readable and maintainable.
