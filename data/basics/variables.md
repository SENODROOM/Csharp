# C# Variables and Data Types

## Variables in C#

### Variable Declaration and Initialization
```csharp
// Variable declaration with initialization
int age = 25;
string name = "John Doe";
double salary = 50000.50;
bool isActive = true;
char grade = 'A';

// Multiple variable declaration
int x = 10, y = 20, z = 30;
string firstName = "John", lastName = "Doe";

// Variable without explicit initialization (will be default value)
int uninitializedInt; // Default: 0
bool uninitializedBool; // Default: false
string uninitializedString; // Default: null

// Using var for implicit typing
var implicitInt = 42; // Compiler infers int
var implicitString = "Hello"; // Compiler infers string
var implicitDouble = 3.14; // Compiler infers double
```

### Naming Conventions
```csharp
// Camel case for local variables
int studentAge = 20;
string firstName = "Alice";
bool hasGraduated = false;

// Pascal case for public members
public int MaxValue { get; set; }
public string ClassName { get; set; }

// Constants use Pascal case
const double PI = 3.14159;
const int MaxUsers = 1000;

// Private fields use camel case with underscore
private int _userId;
private string _userName;

// Static fields use Pascal case
public static readonly string AppVersion = "1.0.0";
```

### Variable Scope
```csharp
public class VariableScopeExample
{
    private int _classField = 100; // Class-level scope
    
    public void MethodScope()
    {
        int localVariable = 50; // Method-level scope
        
        if (localVariable > 0)
        {
            int blockVariable = 25; // Block-level scope
            Console.WriteLine($"Block variable: {blockVariable}");
        }
        
        // blockVariable is not accessible here
        Console.WriteLine($"Local variable: {localVariable}");
    }
    
    public void NestedScopes()
    {
        int outer = 10;
        
        for (int i = 0; i < 5; i++)
        {
            int inner = i * 2;
            Console.WriteLine($"Outer: {outer}, Inner: {inner}");
        }
        
        // inner is not accessible here
    }
}
```

## Data Types

### Primitive Types
```csharp
// Integer types
sbyte signedByte = -128;        // -128 to 127
byte unsignedByte = 255;         // 0 to 255
short shortInt = -32768;         // -32,768 to 32,767
ushort unsignedShort = 65535;    // 0 to 65,535
int normalInt = -2147483648;      // -2,147,483,648 to 2,147,483,647
uint unsignedInt = 4294967295U;   // 0 to 4,294,967,295
long longInt = -9223372036854775808L; // -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807
ulong unsignedLong = 18446744073709551615UL; // 0 to 18,446,744,073,709,551,615

// Floating-point types
float singlePrecision = 3.14f;     // 7 digits of precision
double doublePrecision = 3.141592653589793; // 15-16 digits of precision
decimal decimalType = 3.141592653589793238462643383279m; // 28-29 digits of precision

// Other primitive types
bool booleanValue = true;          // true or false
char character = 'A';              // 16-bit Unicode character
string text = "Hello, World!";     // Sequence of characters

// Special types
object obj = 42;                   // Can hold any type
dynamic dynamicVar = "Hello";       // Runtime type checking
var anonymous = new { Name = "John", Age = 30 }; // Anonymous type
```

### Type Conversion
```csharp
// Implicit conversion (safe)
int intValue = 42;
long longValue = intValue;         // int to long (no data loss)
float floatValue = intValue;        // int to float (possible precision loss)
double doubleValue = floatValue;   // float to double (no data loss)

// Explicit conversion (cast)
double doubleVal = 3.99;
int intVal = (int)doubleVal;        // double to int (truncates to 3)
long longVal = (long)intVal;        // int to long (no data loss)

// Convert class methods
string strNumber = "123";
int parsedInt = Convert.ToInt32(strNumber);
double parsedDouble = Convert.ToDouble("3.14");
bool parsedBool = Convert.ToBoolean("true");

// Parse methods (with error handling)
string numberString = "456";
if (int.TryParse(numberString, out int result))
{
    Console.WriteLine($"Parsed successfully: {result}");
}

// ToString() method
int number = 42;
string numberString = number.ToString();
string formattedString = number.ToString("C2"); // Currency format
```

### Constants and Read-only Variables
```csharp
// Compile-time constants
const double PI = 3.14159;
const int DaysInWeek = 7;
const string WelcomeMessage = "Hello, World!";

// Runtime constants (read-only)
public class ReadOnlyExample
{
    public readonly int ReadOnlyField;
    
    public ReadOnlyExample(int value)
    {
        ReadOnlyField = value; // Can only be set in constructor
    }
}

// Static readonly
public static class Configuration
{
    public static readonly string AppName = "MyApplication";
    public static readonly Version AppVersion = new Version(1, 0, 0);
}
```

## Nullable Types

### Nullable Value Types
```csharp
// Nullable syntax for value types
int? nullableInt = null;
double? nullableDouble = null;
bool? nullableBool = null;

// Using Nullable<T>
Nullable<int> anotherNullableInt = null;
Nullable<double> anotherNullableDouble = null;

// Check if nullable has value
if (nullableInt.HasValue)
{
    Console.WriteLine($"Value: {nullableInt.Value}");
}

// GetValueOrDefault()
int safeValue = nullableInt.GetValueOrDefault(0);

// Null coalescing operator
int result = nullableInt ?? 0;
```

### Reference Types and Null
```csharp
// Reference types can be null
string nullString = null;
object nullObject = null;
CustomClass nullInstance = null;

// Null-conditional operator (?.)
string text = null;
int length = text?.Length ?? 0; // Safe navigation

// Null coalescing assignment
string name = null;
name ??= "Default Name";
```

## Type Inference

### var Keyword
```csharp
// var allows implicit typing but variable is still strongly typed
var number = 42;              // Compiler infers int
var text = "Hello";           // Compiler infers string
var list = new List<int>();   // Compiler infers List<int>

// var must be initialized
// var uninitialized; // Error: Implicitly-typed variables must be initialized

// var cannot change type
var count = 10;
// count = "Hello"; // Error: Cannot implicitly convert type 'string' to 'int'

// Good uses of var
var customers = new List<Customer>();
var dictionary = new Dictionary<string, int>();
var query = from c in customers select c.Name;
```

### dynamic Type
```csharp
// dynamic bypasses compile-time type checking
dynamic dynamicVar = 10;
dynamicVar = "Hello";
dynamicVar = new List<int>();

// Dynamic operations resolved at runtime
dynamic result = dynamicVar + 5; // Works if operation is valid at runtime

// Dynamic with anonymous types
var person = new { Name = "John", Age = 30 };
dynamic dynamicPerson = person;
Console.WriteLine(dynamicPerson.Name); // Works at runtime

// Be careful with dynamic - no compile-time checking
// dynamic.WrongProperty; // Compiles but may fail at runtime
```

## Arrays and Collections

### Arrays
```csharp
// Single-dimensional arrays
int[] numbers = new int[5];
int[] initializedNumbers = { 1, 2, 3, 4, 5 };

// Multi-dimensional arrays
int[,] matrix = new int[3, 3];
int[,] initializedMatrix = { {1, 2, 3}, {4, 5, 6}, {7, 8, 9} };

// Jagged arrays (array of arrays)
int[][] jagged = new int[3][];
jagged[0] = new int[] { 1, 2, 3 };
jagged[1] = new int[] { 4, 5 };
jagged[2] = new int[] { 6, 7, 8, 9 };

// Array operations
int[] array = { 1, 2, 3, 4, 5 };
int length = array.Length;
int first = array[0];
int last = array[array.Length - 1];

// Array methods
Array.Sort(array);
Array.Reverse(array);
int index = Array.IndexOf(array, 3);
bool contains = Array.Exists(array, x => x > 3);
```

### Collections
```csharp
// List<T>
List<string> names = new List<string>();
names.Add("Alice");
names.Add("Bob");
names.Remove("Alice");
bool contains = names.Contains("Bob");

// Dictionary<TKey, TValue>
Dictionary<string, int> ages = new Dictionary<string, int>();
ages["Alice"] = 25;
ages["Bob"] = 30;
int aliceAge = ages["Alice"];

// HashSet<T>
HashSet<int> uniqueNumbers = new HashSet<int>();
uniqueNumbers.Add(1);
uniqueNumbers.Add(2);
uniqueNumbers.Add(1); // Duplicate ignored

// Queue<T>
Queue<string> queue = new Queue<string>();
queue.Enqueue("First");
queue.Enqueue("Second");
string first = queue.Dequeue();

// Stack<T>
Stack<int> stack = new Stack<int>();
stack.Push(1);
stack.Push(2);
int top = stack.Pop();
```

## Best Practices

### Variable Naming
```csharp
// Good: Descriptive, meaningful names
int customerAge;
string emailAddress;
bool hasCompletedOrder;
double totalAmount;

// Bad: Non-descriptive names
int a;
string s;
bool b;
double d;

// Good: Follow conventions
private int _userId;
public string FirstName { get; set; }
const int MAX_ATTEMPTS = 3;

// Bad: Violate conventions
private int userid;
public string firstName { get; set; }
const int max_attempts = 3;
```

### Type Selection
```csharp
// Choose appropriate types for the domain
public class Person
{
    public int Age { get; set; }                    // int for age
    public string Name { get; set; }                 // string for name
    public decimal Salary { get; set; }               // decimal for money
    public bool IsActive { get; set; }               // bool for status
    public DateTime BirthDate { get; set; }           // DateTime for dates
}

// Use specific types when precision matters
public class FinancialCalculations
{
    public decimal CalculateInterest(decimal principal, decimal rate)
    {
        // Use decimal for financial calculations
        return principal * rate;
    }
    
    public double CalculatePhysics(double mass, double velocity)
    {
        // Use double for scientific calculations
        return 0.5 * mass * velocity * velocity;
    }
}
```

### Initialization
```csharp
// Good: Initialize variables when declared
int count = 0;
string name = string.Empty;
List<string> items = new List<string>();

// Good: Use object initializers
var person = new Person
{
    Name = "John",
    Age = 30,
    IsActive = true
};

// Good: Use collection initializers
var numbers = new List<int> { 1, 2, 3, 4, 5 };
var dictionary = new Dictionary<string, int>
{
    ["One"] = 1,
    ["Two"] = 2,
    ["Three"] = 3
};
```

## Common Pitfalls

### Type Conversion Issues
```csharp
// Pitfall: Implicit conversion that loses data
double largeDouble = 1.7976931348623157E+308; // Max double value
float smallFloat = (float)largeDouble; // May result in infinity

// Pitfall: Overflow/underflow
int maxInt = int.MaxValue;
int overflow = maxInt + 1; // Wraps around to min value

// Solution: Use checked blocks
checked
{
    int result = maxInt + 1; // Throws OverflowException
}
catch (OverflowException)
{
    Console.WriteLine("Overflow occurred");
}
```

### Null Reference Issues
```csharp
// Pitfall: Null reference exception
string text = null;
int length = text.Length; // Throws NullReferenceException

// Solution: Null checking
if (text != null)
{
    int length = text.Length;
}

// Solution: Null-conditional operator
int safeLength = text?.Length ?? 0;
```

### Type Safety Issues
```csharp
// Pitfall: Using dynamic when not needed
dynamic value = GetSomeValue();
int result = value.SomeMethod(); // No compile-time checking

// Solution: Use strong typing
SomeType value = GetSomeValue();
int result = value.SomeMethod(); // Compile-time checking
```

## Summary

C# variables and data types provide:

**Variable Types:**
- Value types (int, double, bool, char, struct, enum)
- Reference types (string, object, class, interface, delegate, array)
- Nullable types for handling null values
- Dynamic type for runtime type checking

**Data Types:**
- Signed and unsigned integers of various sizes
- Floating-point types with different precision levels
- Boolean, character, and string types
- Object and dynamic for flexibility

**Type Conversion:**
- Implicit conversion for safe conversions
- Explicit conversion with casting
- Convert class for type conversions
- Parse methods for string to type conversion

**Collections:**
- Arrays for fixed-size collections
- List<T> for dynamic lists
- Dictionary<TKey, TValue> for key-value pairs
- HashSet<T> for unique items
- Queue<T> and Stack<T> for FIFO/LIFO operations

**Best Practices:**
- Follow naming conventions (camelCase, PascalCase)
- Choose appropriate types for the domain
- Initialize variables properly
- Use var judiciously
- Handle null values safely

**Common Pitfalls:**
- Type conversion errors
- Null reference exceptions
- Type safety issues
- Performance considerations

C# provides a rich type system with strong typing, garbage collection, and extensive built-in types that make it suitable for a wide range of applications from desktop to web to mobile development.
