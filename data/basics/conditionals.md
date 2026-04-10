# C# Conditional Statements and Control Flow

## If Statements

### Basic If Statement
```csharp
// Simple if statement
public class BasicIfExample
{
    public void CheckNumber(int number)
    {
        if (number > 0)
        {
            Console.WriteLine("Number is positive");
        }
        
        if (number < 0)
        {
            Console.WriteLine("Number is negative");
        }
        
        if (number == 0)
        {
            Console.WriteLine("Number is zero");
        }
    }
    
    // If-else statement
    public void CheckEvenOdd(int number)
    {
        if (number % 2 == 0)
        {
            Console.WriteLine($"{number} is even");
        }
        else
        {
            Console.WriteLine($"{number} is odd");
        }
    }
    
    // If-else if-else statement
    public void GradeStudent(int score)
    {
        if (score >= 90)
        {
            Console.WriteLine("Grade: A");
        }
        else if (score >= 80)
        {
            Console.WriteLine("Grade: B");
        }
        else if (score >= 70)
        {
            Console.WriteLine("Grade: C");
        }
        else if (score >= 60)
        {
            Console.WriteLine("Grade: D");
        }
        else
        {
            Console.WriteLine("Grade: F");
        }
    }
}
```

### Nested If Statements
```csharp
public class NestedIfExample
{
    public void ValidateUser(string username, string password, int age)
    {
        if (!string.IsNullOrEmpty(username))
        {
            if (!string.IsNullOrEmpty(password))
            {
                if (age >= 18)
                {
                    Console.WriteLine("User is valid");
                }
                else
                {
                    Console.WriteLine("User must be at least 18 years old");
                }
            }
            else
            {
                Console.WriteLine("Password cannot be empty");
            }
        }
        else
        {
            Console.WriteLine("Username cannot be empty");
        }
    }
    
    // Complex nested logic
    public void ProcessOrder(Order order)
    {
        if (order != null)
        {
            if (order.Items.Count > 0)
            {
                if (order.TotalAmount > 0)
                {
                    if (order.Customer != null)
                    {
                        if (order.Customer.IsActive)
                        {
                            Console.WriteLine("Order is valid and ready to process");
                        }
                        else
                        {
                            Console.WriteLine("Customer is not active");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Order has no customer");
                    }
                }
                else
                {
                    Console.WriteLine("Order total amount must be positive");
                }
            }
            else
            {
                Console.WriteLine("Order must have at least one item");
            }
        }
        else
        {
            Console.WriteLine("Order cannot be null");
        }
    }
}

public class Order
{
    public List<OrderItem> Items { get; set; }
    public decimal TotalAmount { get; set; }
    public Customer Customer { get; set; }
}

public class Customer
{
    public bool IsActive { get; set; }
}

public class OrderItem
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
```

## Switch Statements

### Basic Switch Statement
```csharp
public class SwitchExample
{
    // Switch with integer
    public void GetDayName(int dayNumber)
    {
        string dayName;
        
        switch (dayNumber)
        {
            case 1:
                dayName = "Monday";
                break;
            case 2:
                dayName = "Tuesday";
                break;
            case 3:
                dayName = "Wednesday";
                break;
            case 4:
                dayName = "Thursday";
                break;
            case 5:
                dayName = "Friday";
                break;
            case 6:
                dayName = "Saturday";
                break;
            case 7:
                dayName = "Sunday";
                break;
            default:
                dayName = "Invalid day";
                break;
        }
        
        Console.WriteLine($"Day {dayNumber} is {dayName}");
    }
    
    // Switch with string
    public void ProcessCommand(string command)
    {
        switch (command.ToLower())
        {
            case "start":
                Console.WriteLine("Starting process");
                break;
            case "stop":
                Console.WriteLine("Stopping process");
                break;
            case "pause":
                Console.WriteLine("Pausing process");
                break;
            case "resume":
                Console.WriteLine("Resuming process");
                break;
            default:
                Console.WriteLine($"Unknown command: {command}");
                break;
        }
    }
    
    // Switch with enum
    public enum Status
    {
        Pending,
        Approved,
        Rejected,
        Completed
    }
    
    public void HandleStatus(Status status)
    {
        switch (status)
        {
            case Status.Pending:
                Console.WriteLine("Request is pending");
                break;
            case Status.Approved:
                Console.WriteLine("Request is approved");
                break;
            case Status.Rejected:
                Console.WriteLine("Request is rejected");
                break;
            case Status.Completed:
                Console.WriteLine("Request is completed");
                break;
        }
    }
}
```

### Advanced Switch Features (C# 8.0+)
```csharp
public class AdvancedSwitchExample
{
    // Switch expressions (C# 8.0)
    public string GetDayNameExpression(int dayNumber) => dayNumber switch
    {
        1 => "Monday",
        2 => "Tuesday",
        3 => "Wednesday",
        4 => "Thursday",
        5 => "Friday",
        6 => "Saturday",
        7 => "Sunday",
        _ => "Invalid day"
    };
    
    // Pattern matching with switch
    public void ProcessShape(Shape shape) => shape switch
    {
        Circle circle when circle.Radius > 10 => 
            Console.WriteLine($"Large circle with radius {circle.Radius}"),
        Circle circle => 
            Console.WriteLine($"Small circle with radius {circle.Radius}"),
        Rectangle rect when rect.Width == rect.Height => 
            Console.WriteLine($"Square with side {rect.Width}"),
        Rectangle rect => 
            Console.WriteLine($"Rectangle {rect.Width}x{rect.Height}"),
        Triangle triangle => 
            Console.WriteLine($"Triangle with area {triangle.GetArea()}"),
        null => 
            Console.WriteLine("Shape is null"),
        _ => 
            Console.WriteLine("Unknown shape")
    };
    
    // Tuple pattern matching
    public void ProcessTuple((int x, int y) coordinates) => coordinates switch
    {
        (0, 0) => Console.WriteLine("Origin"),
        (0, var y) => Console.WriteLine($"On Y-axis at {y}"),
        (var x, 0) => Console.WriteLine($"On X-axis at {x}"),
        (var x, var y) when x == y => Console.WriteLine($"Diagonal line at ({x}, {y})"),
        (var x, var y) => Console.WriteLine($"Point at ({x}, {y})")
    };
    
    // List pattern matching (C# 11)
    public void ProcessList(List<int> numbers) => numbers switch
    {
        [] => Console.WriteLine("Empty list"),
        [var single] => Console.WriteLine($"Single element: {single}"),
        [var first, var second] => Console.WriteLine($"Two elements: {first}, {second}"),
        [var first, .., var last] => Console.WriteLine($"First: {first}, Last: {last}"),
        _ => Console.WriteLine($"List with {numbers.Count} elements")
    };
}

// Shape classes for pattern matching
public abstract class Shape
{
    public abstract double GetArea();
}

public class Circle : Shape
{
    public double Radius { get; set; }
    
    public override double GetArea() => Math.PI * Radius * Radius;
}

public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }
    
    public override double GetArea() => Width * Height;
}

public class Triangle : Shape
{
    public double Base { get; set; }
    public double Height { get; set; }
    
    public override double GetArea() => 0.5 * Base * Height;
}
```

## Ternary Operator

### Conditional Ternary Operator
```csharp
public class TernaryExample
{
    // Basic ternary operator
    public string CheckPositive(int number)
    {
        return number > 0 ? "Positive" : "Not positive";
    }
    
    // Nested ternary operators
    public string GradeDescription(int score)
    {
        return score >= 90 ? "Excellent" : 
               score >= 80 ? "Good" : 
               score >= 70 ? "Average" : "Poor";
    }
    
    // Ternary with method calls
    public string GetUserInfo(User user)
    {
        return user != null ? 
               user.IsActive ? $"{user.Name} (Active)" : $"{user.Name} (Inactive)" : 
               "User not found";
    }
    
    // Ternary with null coalescing
    public string SafeGetString(string text)
    {
        return text ?? "Default value";
    }
    
    // Complex ternary expression
    public decimal CalculateDiscount(decimal price, bool isPremiumCustomer, int yearsLoyal)
    {
        return isPremiumCustomer ? 
               yearsLoyal > 5 ? price * 0.8m : price * 0.9m : 
               yearsLoyal > 3 ? price * 0.95m : price;
    }
    
    // Ternary in property
    public string StatusMessage
    {
        get => _isConnected ? "Connected" : "Disconnected";
    }
    
    private bool _isConnected;
}
```

## Conditional Logical Operators

### Logical Operators
```csharp
public class LogicalOperators
{
    // AND operator (&&)
    public bool CanVote(int age, bool isCitizen)
    {
        return age >= 18 && isCitizen;
    }
    
    // OR operator (||)
    public bool IsWeekend(DayOfWeek day)
    {
        return day == DayOfWeek.Saturday || day == DayOfWeek.Sunday;
    }
    
    // NOT operator (!)
    public bool IsNotValid(string input)
    {
        return !string.IsNullOrEmpty(input);
    }
    
    // Complex logical expressions
    public bool CanDrive(Person person)
    {
        return person.Age >= 16 && 
               person.HasLicense && 
               !person.IsSuspended &&
               (person.Car != null || person.HasMotorcycleLicense);
    }
    
    // Short-circuit evaluation
    public bool SafeDivision(int numerator, int denominator, out int result)
    {
        if (denominator != 0 && numerator / denominator > 10)
        {
            result = numerator / denominator;
            return true;
        }
        
        result = 0;
        return false;
    }
    
    // XOR operation (exclusive or)
    public bool IsExclusive(bool condition1, bool condition2)
    {
        return condition1 ^ condition2; // XOR
    }
    
    // Complex boolean logic
    public bool ValidateInput(string email, int age, string password)
    {
        bool isEmailValid = !string.IsNullOrEmpty(email) && email.Contains("@");
        bool isAgeValid = age >= 18 && age <= 120;
        bool isPasswordValid = !string.IsNullOrEmpty(password) && password.Length >= 8;
        
        return isEmailValid && isAgeValid && isPasswordValid;
    }
}

public class Person
{
    public int Age { get; set; }
    public bool HasLicense { get; set; }
    public bool IsSuspended { get; set; }
    public Car Car { get; set; }
    public bool HasMotorcycleLicense { get; set; }
}

public class Car
{
    public string Make { get; set; }
    public string Model { get; set; }
}
```

## Null-Conditional Operators

### Null-Conditional and Coalescing
```csharp
public class NullConditionalOperators
{
    // Null-conditional operator (?.)
    public int GetStringLength(string text)
    {
        return text?.Length ?? 0; // Returns 0 if text is null
    }
    
    // Null-conditional with member access
    public string GetCustomerName(Customer customer)
    {
        return customer?.Name ?? "Unknown Customer";
    }
    
    // Null-conditional with array access
    public string GetFirstItem(string[] items)
    {
        return items?[0] ?? "No items";
    }
    
    // Null-conditional with method calls
    public string GetFormattedAddress(Address address)
    {
        return address?.GetFormattedAddress() ?? "No address";
    }
    
    // Null-coalescing assignment (??=)
    public void SetDefaultValues()
    {
        string name = null;
        name ??= "Default Name"; // Assign if null
        
        List<string> items = null;
        items ??= new List<string>(); // Create new list if null
    }
    
    // Combine null-conditional operators
    public string GetFullContactInfo(Person person)
    {
        return person?.Address?.Street ?? "No address";
    }
    
    // Safe navigation with method chaining
    public bool ValidateOrder(Order order)
    {
        return order?.Customer?.IsActive == true && 
               order?.Items?.Count > 0;
    }
}

public class Customer
{
    public string Name { get; set; }
    public Address Address { get; set; }
}

public class Address
{
    public string Street { get; set; }
    
    public string GetFormattedAddress()
    {
        return Street;
    }
}
```

## Pattern Matching (C# 7.0+)

### Pattern Matching with Is
```csharp
public class PatternMatchingExample
{
    // Pattern matching with is operator
    public void ProcessValue(object value)
    {
        if (value is int intValue)
        {
            Console.WriteLine($"Integer: {intValue}");
        }
        else if (value is string stringValue)
        {
            Console.WriteLine($"String: {stringValue}");
        }
        else if (value is double doubleValue)
        {
            Console.WriteLine($"Double: {doubleValue}");
        }
        else
        {
            Console.WriteLine($"Unknown type: {value.GetType().Name}");
        }
    }
    
    // Pattern matching with type and condition
    public void ProcessNumber(object number)
    {
        if (number is int intValue && intValue > 0)
        {
            Console.WriteLine($"Positive integer: {intValue}");
        }
        else if (number is int intValue && intValue < 0)
        {
            Console.WriteLine($"Negative integer: {intValue}");
        }
        else if (number is double doubleValue && doubleValue > 0.0)
        {
            Console.WriteLine($"Positive double: {doubleValue}");
        }
    }
    
    // Pattern matching with deconstruction
    public void ProcessPoint(Point point)
    {
        if (point is (int x, int y))
        {
            Console.WriteLine($"Point coordinates: ({x}, {y})");
        }
    }
    
    // Pattern matching with switch statement
    public string GetShapeDescription(Shape shape)
    {
        switch (shape)
        {
            case Circle circle when circle.Radius > 10:
                return $"Large circle with radius {circle.Radius}";
            case Circle circle:
                return $"Small circle with radius {circle.Radius}";
            case Rectangle rect when rect.Width == rect.Height:
                return $"Square with side {rect.Width}";
            case Rectangle rect:
                return $"Rectangle {rect.Width}x{rect.Height}";
            default:
                return "Unknown shape";
        }
    }
}

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    
    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }
}
```

## Loop Control

### Break and Continue
```csharp
public class LoopControl
{
    // Break statement
    public void FindFirstEven(int[] numbers)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] % 2 == 0)
            {
                Console.WriteLine($"First even number found at index {i}: {numbers[i]}");
                break; // Exit loop when found
            }
        }
    }
    
    // Continue statement
    public void SumOddNumbers(int[] numbers)
    {
        int sum = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] % 2 == 0)
            {
                continue; // Skip even numbers
            }
            
            sum += numbers[i];
        }
        
        Console.WriteLine($"Sum of odd numbers: {sum}");
    }
    
    // Break with nested loops
    public void FindElement(int[,] matrix, int target)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == target)
                {
                    Console.WriteLine($"Found {target} at position ({i}, {j})");
                    break; // Break inner loop
                }
            }
            
            // Check if we should break outer loop too
            bool found = false;
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == target)
                {
                    found = true;
                    break;
                }
            }
            
            if (found)
            {
                break; // Break outer loop
            }
        }
    }
    
    // Return statement in loops
    public List<int> FindAllEvenNumbers(int[] numbers)
    {
        var evenNumbers = new List<int>();
        
        foreach (int number in numbers)
        {
            if (number % 2 == 0)
            {
                evenNumbers.Add(number);
            }
            
            // Early return if we have enough numbers
            if (evenNumbers.Count >= 5)
            {
                return evenNumbers;
            }
        }
        
        return evenNumbers;
    }
}
```

## Best Practices

### Conditional Best Practices
```csharp
public class ConditionalBestPractices
{
    // Good: Clear and readable conditions
    public bool IsValidUser(User user)
    {
        return user != null && 
               user.Age >= 18 && 
               user.Email.Contains("@") && 
               user.IsActive;
    }
    
    // Good: Use early returns to reduce nesting
    public bool ValidateOrder(Order order)
    {
        if (order == null)
            return false;
        
        if (order.Items == null || order.Items.Count == 0)
            return false;
        
        if (order.TotalAmount <= 0)
            return false;
        
        return true;
    }
    
    // Good: Use meaningful variable names
    public bool IsEligibleForDiscount(Customer customer, Order order)
    {
        bool isPremiumCustomer = customer.IsPremium;
        bool isLargeOrder = order.TotalAmount > 1000;
        bool isLongTermCustomer = customer.YearsAsCustomer > 5;
        
        return isPremiumCustomer && (isLargeOrder || isLongTermCustomer);
    }
    
    // Good: Use switch for multiple conditions
    public string GetStatusMessage(OrderStatus status)
    {
        switch (status)
        {
            case OrderStatus.Pending:
                return "Order is pending approval";
            case OrderStatus.Approved:
                return "Order has been approved";
            case OrderStatus.Shipped:
                return "Order has been shipped";
            case OrderStatus.Delivered:
                return "Order has been delivered";
            case OrderStatus.Cancelled:
                return "Order has been cancelled";
            default:
                return "Unknown status";
        }
    }
    
    // Good: Use ternary for simple conditions
    public string GetDisplayName(User user)
    {
        return user.DisplayName ?? user.Email;
    }
    
    // Bad: Too complex ternary
    public string BadComplexTernary(bool a, bool b, bool c)
    {
        return a ? "A" : b ? "B" : c ? "C" : "D"; // Hard to read
    }
    
    // Good: Use if-else for complex conditions
    public string GoodComplexConditional(bool a, bool b, bool c)
    {
        if (a) return "A";
        if (b) return "B";
        if (c) return "C";
        return "D";
    }
}

public enum OrderStatus
{
    Pending,
    Approved,
    Shipped,
    Delivered,
    Cancelled
}
```

### Performance Considerations
```csharp
public class PerformanceConsiderations
{
    // Good: Use switch for performance with multiple string comparisons
    public string ProcessCommandOptimized(string command)
    {
        switch (command)
        {
            case "start":
                return "Starting";
            case "stop":
                return "Stopping";
            case "pause":
                return "Pausing";
            case "resume":
                return "Resuming";
            default:
                return "Unknown";
        }
    }
    
    // Bad: Multiple if-else for string comparison (slower)
    public string ProcessCommandUnoptimized(string command)
    {
        if (command == "start")
            return "Starting";
        else if (command == "stop")
            return "Stopping";
        else if (command == "pause")
            return "Pausing";
        else if (command == "resume")
            return "Resuming";
        else
            return "Unknown";
    }
    
    // Good: Use dictionary lookup for many conditions
    private static readonly Dictionary<string, string> CommandHandlers = new()
    {
        ["start"] = "Starting",
        ["stop"] = "Stopping",
        ["pause"] = "Pausing",
        ["resume"] = "Resuming"
    };
    
    public string ProcessCommandWithDictionary(string command)
    {
        return CommandHandlers.TryGetValue(command, out string result) 
            ? result 
            : "Unknown";
    }
    
    // Good: Short-circuit evaluation
    public bool IsEligibleForPromotion(Employee employee)
    {
        return employee.YearsOfService > 5 && 
               employee.PerformanceRating >= 4 && 
               employee.HasRequiredCertifications;
    }
    
    // Good: Use pattern matching for type checking
    public void ProcessObject(object obj)
    {
        switch (obj)
        {
            case string s:
                Console.WriteLine($"String: {s}");
                break;
            case int i:
                Console.WriteLine($"Integer: {i}");
                break;
            case bool b:
                Console.WriteLine($"Boolean: {b}");
                break;
            default:
                Console.WriteLine($"Unknown type: {obj.GetType().Name}");
                break;
        }
    }
}

public class Employee
{
    public int YearsOfService { get; set; }
    public int PerformanceRating { get; set; }
    public bool HasRequiredCertifications { get; set; }
}
```

## Common Pitfalls

### Common Conditional Errors
```csharp
public class CommonPitfalls
{
    // Pitfall: Assignment instead of comparison
    public bool PitfallAssignment(int a, int b)
    {
        if (a = b) // Assignment instead of comparison!
        {
            return true;
        }
        return false;
    }
    
    // Solution: Use comparison operator
    public bool SolutionComparison(int a, int b)
    {
        return a == b;
    }
    
    // Pitfall: Forgetting break in switch
    public string PitfallMissingBreak(int value)
    {
        string result;
        switch (value)
        {
            case 1:
                result = "One";
                // Missing break!
            case 2:
                result = "Two";
                break;
            default:
                result = "Other";
                break;
        }
        return result; // Always returns "Two" for value 1
    }
    
    // Solution: Always include break or use fall-through intentionally
    public string SolutionCorrectSwitch(int value)
    {
        return value switch
        {
            1 => "One",
            2 => "Two",
            _ => "Other"
        };
    }
    
    // Pitfall: Not handling null in conditions
    public bool PitfallNullReference(string text)
    {
        if (text.Length > 10) // NullReferenceException if text is null
        {
            return true;
        }
        return false;
    }
    
    // Solution: Check for null first
    public bool SolutionNullCheck(string text)
    {
        return !string.IsNullOrEmpty(text) && text.Length > 10;
    }
    
    // Pitfall: Complex nested conditions
    public bool PitfallComplexCondition(User user, Order order, Payment payment)
    {
        if (user != null)
        {
            if (user.IsActive)
            {
                if (order != null)
                {
                    if (order.TotalAmount > 0)
                    {
                        if (payment != null)
                        {
                            if (payment.Amount == order.TotalAmount)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
    
    // Solution: Use early returns and guard clauses
    public bool SolutionGuardClauses(User user, Order order, Payment payment)
    {
        if (user == null || !user.IsActive)
            return false;
        
        if (order == null || order.TotalAmount <= 0)
            return false;
        
        if (payment == null)
            return false;
        
        return payment.Amount == order.TotalAmount;
    }
    
    // Pitfall: Using ternary for complex logic
    public string PitfallComplexTernary(bool a, bool b, bool c, bool d)
    {
        return a ? (b ? "AB" : "A") : (c ? (d ? "ACD" : "AC") : "A");
    }
    
    // Solution: Use if-else for complex logic
    public string SolutionComplexConditional(bool a, bool b, bool c, bool d)
    {
        if (a)
        {
            return b ? "AB" : "A";
        }
        else
        {
            return c ? (d ? "ACD" : "AC") : "A";
        }
    }
}
```

## Summary

C# conditional statements and control flow provide:

**Conditional Statements:**
- If, if-else, if-else if-else structures
- Nested if statements
- Complex boolean logic with &&, ||, and !
- Short-circuit evaluation

**Switch Statements:**
- Basic switch with different data types
- Pattern matching with switch
- Switch expressions (C# 8.0+)
- Tuple and list pattern matching

**Ternary Operator:**
- Conditional ternary (?:) syntax
- Nested ternary expressions
- Null-coalescing operator (??)
- Null-conditional operator (?.)

**Pattern Matching:**
- Pattern matching with is operator
- Type patterns and condition patterns
- Deconstruction patterns
- Switch pattern matching

**Loop Control:**
- Break statement to exit loops early
- Continue statement to skip iterations
- Return statements in loops
- Labeled break for nested loops

**Null Handling:**
- Null-conditional operators
- Null-coalescing operators
- Safe navigation patterns
- Null-checking best practices

**Best Practices:**
- Clear and readable conditions
- Early returns to reduce nesting
- Appropriate use of switch vs if-else
- Performance considerations

**Common Pitfalls:**
- Assignment vs comparison errors
        - Missing break statements
        - Null reference exceptions
        - Complex nested conditions
        - Overly complex ternary expressions

C# conditional statements provide powerful and flexible control flow mechanisms, with modern language features like pattern matching and switch expressions making code more readable and maintainable.
