## Type System

```
.NET Unified Type System
──────────────────────────────────────────────────────────
System.Object (object)
│
├── Value Types (stored on stack, copied by value)
│   ├── Numeric: int, long, short, byte, uint...
│   │            float, double, decimal
│   ├── char, bool
│   ├── Structs (value-type user-defined types)
│   └── Enums
│
└── Reference Types (stored on heap, copied by reference)
    ├── string (immutable!)
    ├── Arrays
    ├── Classes
    ├── Interfaces
    ├── Delegates
    └── Records (C# 9+)
──────────────────────────────────────────────────────────
```

```csharp
// Enums
enum DayOfWeek { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday }
enum Permission : byte { None = 0, Read = 1, Write = 2, Execute = 4 }

// [Flags] attribute for bitwise enums
[Flags]
enum FilePermissions { None = 0, Read = 1, Write = 2, Execute = 4, All = 7 }

var perms = FilePermissions.Read | FilePermissions.Write;
bool canRead = perms.HasFlag(FilePermissions.Read);  // true

// Structs — value types (good for small, immutable data)
public readonly struct Point(double X, double Y) // primary constructor (C# 12)
{
    public double DistanceTo(Point other) =>
        Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
}

// Tuples (C# 7+)
(string Name, int Age) person = ("Alice", 30);
Console.WriteLine(person.Name);  // Alice

var (name, age) = person;  // deconstruction

// ValueTuple vs Tuple (prefer ValueTuple = the () syntax)
Tuple<string, int> old = Tuple.Create("Bob", 25);       // heap allocation
(string, int)      new_ = ("Bob", 25);                  // stack allocation
```

---

## Object-Oriented Programming

```csharp
// Abstract base class
public abstract class Animal
{
    // Properties (C# auto-properties)
    public string Name { get; init; }     // init = only settable during construction
    public int    Age  { get; private set; }

    // Constructor
    protected Animal(string name, int age)
    {
        Name = name;
        Age  = age;
    }

    // Abstract method — must be implemented by subclasses
    public abstract string MakeSound();

    // Virtual method — can be overridden
    public virtual string Describe() =>
        $"I am {Name}, a {GetType().Name}, {Age} years old.";

    // Sealed method — cannot be overridden further
    public sealed override string ToString() =>
        $"{GetType().Name}({Name})";
}

// Derived class
public class Dog : Animal
{
    public string Breed { get; init; }
    private readonly List<string> _tricks = [];

    public Dog(string name, int age, string breed)
        : base(name, age)  // call base constructor
    {
        Breed = breed;
    }

    public override string MakeSound() => "Woof! 🐕";

    public override string Describe() =>
        $"{base.Describe()} I'm a {Breed}.";

    public Dog Learn(string trick)
    {
        _tricks.Add(trick);
        return this;  // fluent interface
    }

    public IReadOnlyList<string> Tricks => _tricks.AsReadOnly();
}

// Usage
var dog = new Dog("Rex", 3, "Labrador")
{
    // Object initializer syntax (only init properties can be set here)
};

dog.Learn("sit").Learn("shake").Learn("roll over");
Console.WriteLine(dog.Describe());
Console.WriteLine(dog.MakeSound());
```

### Access Modifiers

```
Modifier          │ Same Class │ Same Assembly │ Derived Class │ Anywhere
──────────────────┼────────────┼───────────────┼───────────────┼─────────
private           │     ✅     │      ❌       │      ❌       │   ❌
private protected │     ✅     │      ❌       │  ✅ (same asm)│   ❌
protected         │     ✅     │      ❌       │      ✅       │   ❌
internal          │     ✅     │      ✅       │      ❌       │   ❌
protected internal│     ✅     │      ✅       │      ✅       │   ❌
public            │     ✅     │      ✅       │      ✅       │   ✅
```

---

