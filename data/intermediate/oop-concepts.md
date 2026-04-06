# C# Object-Oriented Programming Concepts

## Classes and Objects

### Class Definition
```csharp
public class Person
{
    // Fields
    private string _firstName;
    private string _lastName;
    private int _age;
    
    // Properties
    public string FirstName 
    { 
        get => _firstName; 
        set => _firstName = value ?? throw new ArgumentNullException(nameof(value)); 
    }
    
    public string LastName 
    { 
        get => _lastName; 
        set => _lastName = value ?? throw new ArgumentNullException(nameof(value)); 
    }
    
    public int Age 
    { 
        get => _age; 
        set => _age = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(value)); 
    }
    
    // Read-only property
    public string FullName => $"{FirstName} {LastName}";
    
    // Computed property
    public bool IsAdult => Age >= 18;
    
    // Static property
    public static int Count { get; private set; }
    
    // Constructors
    public Person()
    {
        FirstName = "Unknown";
        LastName = "Unknown";
        Age = 0;
        Count++;
    }
    
    public Person(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Count++;
    }
    
    // Static constructor
    static Person()
    {
        Console.WriteLine("Person class initialized");
    }
    
    // Methods
    public void Introduce()
    {
        Console.WriteLine($"Hi, I'm {FullName} and I'm {Age} years old.");
    }
    
    public void CelebrateBirthday()
    {
        Age++;
        Console.WriteLine($"Happy birthday! I'm now {Age} years old.");
    }
    
    // Static method
    public static Person CreateChild(string firstName, string lastName)
    {
        return new Person(firstName, lastName, 0);
    }
    
    // Destructor (finalizer)
    ~Person()
    {
        Console.WriteLine($"Person object {FullName} is being finalized");
    }
}
```

### Object Creation and Usage
```csharp
public class ObjectUsage
{
    public void DemonstrateObjects()
    {
        // Create objects using constructors
        Person person1 = new Person();
        Person person2 = new Person("John", "Doe", 30);
        
        // Use properties
        person1.FirstName = "Alice";
        person1.LastName = "Smith";
        person1.Age = 25;
        
        // Call methods
        person1.Introduce();
        person2.Introduce();
        
        // Use read-only properties
        Console.WriteLine($"{person1.FullName} is {(person1.IsAdult ? "an adult" : "a minor")}");
        
        // Use static members
        Console.WriteLine($"Total persons created: {Person.Count}");
        
        // Create child using static method
        Person child = Person.CreateChild("Emma", "Johnson");
        child.Introduce();
        
        // Object initializers
        var person3 = new Person
        {
            FirstName = "Bob",
            LastName = "Wilson",
            Age = 40
        };
        
        person3.Introduce();
    }
}
```

## Inheritance

### Base and Derived Classes
```csharp
// Base class
public abstract class Animal
{
    protected string Name { get; set; }
    protected int Age { get; set; }
    
    protected Animal(string name, int age)
    {
        Name = name;
        Age = age;
    }
    
    // Virtual method (can be overridden)
    public virtual void MakeSound()
    {
        Console.WriteLine($"{Name} makes a sound");
    }
    
    // Abstract method (must be overridden)
    public abstract void Move();
    
    // Non-virtual method (cannot be overridden)
    public void Sleep()
    {
        Console.WriteLine($"{Name} is sleeping");
    }
    
    // Protected method (accessible by derived classes)
    protected void Eat()
    {
        Console.WriteLine($"{Name} is eating");
    }
    
    // Public method that uses protected members
    public void Live()
    {
        Eat();
        Move();
        MakeSound();
        Sleep();
    }
}

// Derived class
public class Dog : Animal
{
    public string Breed { get; set; }
    
    public Dog(string name, int age, string breed) : base(name, age)
    {
        Breed = breed;
    }
    
    // Override abstract method
    public override void Move()
    {
        Console.WriteLine($"{Name} the {Breed} is running");
    }
    
    // Override virtual method
    public override void MakeSound()
    {
        Console.WriteLine($"{Name} barks: Woof! Woof!");
    }
    
    // New method specific to Dog
    public void WagTail()
    {
        Console.WriteLine($"{Name} is wagging its tail");
    }
    
    // Hide base method with new keyword
    public new void Sleep()
    {
        Console.WriteLine($"{Name} is sleeping in its dog bed");
    }
}

// Another derived class
public class Cat : Animal
{
    public bool IsIndoor { get; set; }
    
    public Cat(string name, int age, bool isIndoor) : base(name, age)
    {
        IsIndoor = isIndoor;
    }
    
    public override void Move()
    {
        Console.WriteLine($"{Name} is stealthily walking");
    }
    
    public override void MakeSound()
    {
        Console.WriteLine($"{Name} meows: Meow!");
    }
    
    public void Purr()
    {
        Console.WriteLine($"{Name} is purring");
    }
}
```

### Inheritance Usage
```csharp
public class InheritanceUsage
{
    public void DemonstrateInheritance()
    {
        // Create derived class objects
        Dog dog = new Dog("Buddy", 3, "Golden Retriever");
        Cat cat = new Cat("Whiskers", 2, true);
        
        // Use inherited methods
        dog.Live();
        cat.Live();
        
        // Use overridden methods
        dog.MakeSound();
        cat.MakeSound();
        
        // Use derived class specific methods
        dog.WagTail();
        cat.Purr();
        
        // Use hidden method
        dog.Sleep(); // Calls Dog's Sleep method
        
        // Polymorphism - treat derived objects as base type
        Animal[] animals = { dog, cat };
        
        foreach (Animal animal in animals)
        {
            animal.MakeSound(); // Calls appropriate override
            animal.Move(); // Calls appropriate override
        }
        
        // Type checking
        if (animal is Dog dogAnimal)
        {
            dogAnimal.WagTail();
        }
        
        // Pattern matching
        foreach (Animal animal in animals)
        {
            switch (animal)
            {
                case Dog d:
                    d.WagTail();
                    break;
                case Cat c:
                    c.Purr();
                    break;
            }
        }
    }
}
```

## Encapsulation

### Access Modifiers
```csharp
public class BankAccount
{
    // Private fields - only accessible within this class
    private string _accountNumber;
    private decimal _balance;
    private List<Transaction> _transactions;
    
    // Protected field - accessible in this class and derived classes
    protected string _accountHolderName;
    
    // Internal field - accessible within the same assembly
    internal string _bankName;
    
    // Public properties - accessible everywhere
    public string AccountNumber 
    { 
        get => _accountNumber; 
        private set => _accountNumber = value; // Private setter
    }
    
    public decimal Balance 
    { 
        get => _balance; 
        private set => _balance = value; 
    }
    
    // Protected internal property
    protected internal string AccountHolderName 
    { 
        get => _accountHolderName; 
        set => _accountHolderName = value; 
    }
    
    // Constructor
    public BankAccount(string accountNumber, string accountHolderName, string bankName)
    {
        _accountNumber = accountNumber;
        _accountHolderName = accountHolderName;
        _bankName = bankName;
        _balance = 0;
        _transactions = new List<Transaction>();
    }
    
    // Public method with internal validation
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be positive", nameof(amount));
        
        _balance += amount;
        AddTransaction("Deposit", amount);
    }
    
    // Protected method for derived classes
    protected void AddTransaction(string type, decimal amount)
    {
        _transactions.Add(new Transaction
        {
            Type = type,
            Amount = amount,
            Date = DateTime.Now,
            Balance = _balance
        });
    }
    
    // Internal method for same assembly
    internal void ApplyInterest(decimal interestRate)
    {
        decimal interest = _balance * interestRate;
        _balance += interest;
        AddTransaction("Interest", interest);
    }
    
    // Public read-only access to transactions
    public IReadOnlyList<Transaction> GetTransactions()
    {
        return _transactions.AsReadOnly();
    }
    
    // Private helper method
    private bool ValidateTransaction(decimal amount)
    {
        return amount > 0 && amount <= _balance;
    }
    
    // Public method that uses private validation
    public bool Withdraw(decimal amount)
    {
        if (!ValidateTransaction(amount))
            return false;
        
        _balance -= amount;
        AddTransaction("Withdrawal", -amount);
        return true;
    }
}

public class Transaction
{
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public decimal Balance { get; set; }
}
```

### Encapsulation Best Practices
```csharp
public class EncapsulationExamples
{
    // Good encapsulation with properties
    public class Product
    {
        private string _name;
        private decimal _price;
        private int _stock;
        
        public string Name 
        { 
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(value));
        }
        
        public decimal Price 
        { 
            get => _price;
            set => _price = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(value));
        }
        
        public int Stock 
        { 
            get => _stock;
            private set => _stock = value; // Only internal modification
        }
        
        // Computed property
        public bool InStock => Stock > 0;
        
        // Method that encapsulates business logic
        public bool Purchase(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive", nameof(quantity));
            
            if (Stock < quantity)
                return false;
            
            Stock -= quantity;
            return true;
        }
        
        // Factory method
        public static Product Create(string name, decimal price, int stock)
        {
            return new Product
            {
                Name = name,
                Price = price,
                Stock = stock
            };
        }
    }
    
    // Encapsulation with dependency injection
    public class OrderProcessor
    {
        private readonly IPaymentGateway _paymentGateway;
        private readonly INotificationService _notificationService;
        
        public OrderProcessor(IPaymentGateway paymentGateway, INotificationService notificationService)
        {
            _paymentGateway = paymentGateway ?? throw new ArgumentNullException(nameof(paymentGateway));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }
        
        public bool ProcessOrder(Order order)
        {
            // Process payment using injected dependency
            bool paymentSuccess = _paymentGateway.ProcessPayment(order.TotalAmount);
            
            if (paymentSuccess)
            {
                // Send notification using injected dependency
                _notificationService.SendNotification($"Order {order.Id} processed successfully");
                return true;
            }
            
            return false;
        }
    }
}

public interface IPaymentGateway
{
    bool ProcessPayment(decimal amount);
}

public interface INotificationService
{
    void SendNotification(string message);
}

public class Order
{
    public string Id { get; set; }
    public decimal TotalAmount { get; set; }
}
```

## Polymorphism

### Method Overriding
```csharp
public abstract class Shape
{
    public string Name { get; set; }
    public Color Color { get; set; }
    
    protected Shape(string name, Color color)
    {
        Name = name;
        Color = color;
    }
    
    // Abstract method - must be implemented by derived classes
    public abstract double CalculateArea();
    
    // Virtual method - can be overridden
    public virtual double CalculatePerimeter()
    {
        return 0; // Default implementation
    }
    
    // Virtual method with implementation
    public virtual void Draw()
    {
        Console.WriteLine($"Drawing {Name} with color {Color}");
    }
    
    // Non-virtual method - cannot be overridden
    public void GetInfo()
    {
        Console.WriteLine($"Shape: {Name}, Area: {CalculateArea():F2}, Perimeter: {CalculatePerimeter():F2}");
    }
}

public class Circle : Shape
{
    public double Radius { get; set; }
    
    public Circle(double radius, Color color) : base("Circle", color)
    {
        Radius = radius;
    }
    
    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
    
    public override double CalculatePerimeter()
    {
        return 2 * Math.PI * Radius;
    }
    
    public override void Draw()
    {
        Console.WriteLine($"Drawing a {Color} circle with radius {Radius}");
    }
    
    // New method specific to Circle
    public double GetCircumference()
    {
        return CalculatePerimeter();
    }
}

public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }
    
    public Rectangle(double width, double height, Color color) : base("Rectangle", color)
    {
        Width = width;
        Height = height;
    }
    
    public override double CalculateArea()
    {
        return Width * Height;
    }
    
    public override double CalculatePerimeter()
    {
        return 2 * (Width + Height);
    }
    
    public override void Draw()
    {
        Console.WriteLine($"Drawing a {Color} rectangle {Width}x{Height}");
    }
    
    // Method hiding with new keyword
    public new void GetInfo()
    {
        Console.WriteLine($"Rectangle: {Width}x{Height}, Area: {CalculateArea():F2}");
    }
}

public class Triangle : Shape
{
    public double Base { get; set; }
    public double Height { get; set; }
    public double SideA { get; set; }
    public double SideB { get; set; }
    public double SideC { get; set; }
    
    public Triangle(double @base, double height, double sideA, double sideB, double sideC, Color color) 
        : base("Triangle", color)
    {
        Base = @base;
        Height = height;
        SideA = sideA;
        SideB = sideB;
        SideC = sideC;
    }
    
    public override double CalculateArea()
    {
        return 0.5 * Base * Height;
    }
    
    public override double CalculatePerimeter()
    {
        return SideA + SideB + SideC;
    }
    
    public override void Draw()
    {
        Console.WriteLine($"Drawing a {Color} triangle with base {Base} and height {Height}");
    }
}
```

### Polymorphism Usage
```csharp
public class PolymorphismUsage
{
    public void DemonstratePolymorphism()
    {
        // Create different shapes
        Shape[] shapes = 
        {
            new Circle(5.0, Color.Red),
            new Rectangle(4.0, 6.0, Color.Blue),
            new Triangle(3.0, 4.0, 5.0, 5.0, 6.0, Color.Green)
        };
        
        // Polymorphic behavior - same method call, different implementations
        Console.WriteLine("=== Drawing Shapes ===");
        foreach (Shape shape in shapes)
        {
            shape.Draw(); // Calls appropriate override
        }
        
        Console.WriteLine("\n=== Shape Information ===");
        foreach (Shape shape in shapes)
        {
            shape.GetInfo(); // Calls base method (non-virtual)
        }
        
        // Use specific derived class methods
        Circle circle = shapes[0] as Circle;
        if (circle != null)
        {
            Console.WriteLine($"Circle circumference: {circle.GetCircumference():F2}");
        }
        
        Rectangle rectangle = shapes[1] as Rectangle;
        if (rectangle != null)
        {
            rectangle.GetInfo(); // Calls Rectangle's new method
        }
        
        // Pattern matching with polymorphism
        Console.WriteLine("\n=== Pattern Matching ===");
        foreach (Shape shape in shapes)
        {
            switch (shape)
            {
                case Circle c:
                    Console.WriteLine($"Circle with radius {c.Radius} has area {c.CalculateArea():F2}");
                    break;
                case Rectangle r:
                    Console.WriteLine($"Rectangle {r.Width}x{r.Height} has area {r.CalculateArea():F2}");
                    break;
                case Triangle t:
                    Console.WriteLine($"Triangle with base {t.Base} has area {t.CalculateArea():F2}");
                    break;
                default:
                    Console.WriteLine($"Unknown shape: {shape.Name}");
                    break;
            }
        }
    }
    
    // Polymorphic collection processing
    public void ProcessShapes(List<Shape> shapes)
    {
        foreach (Shape shape in shapes)
        {
            // Polymorphic processing
            double area = shape.CalculateArea();
            double perimeter = shape.CalculatePerimeter();
            
            Console.WriteLine($"Processing {shape.Name}: Area={area:F2}, Perimeter={perimeter:F2}");
            
            // Type-specific processing
            if (shape is Circle circle)
            {
                ProcessCircle(circle);
            }
            else if (shape is Rectangle rectangle)
            {
                ProcessRectangle(rectangle);
            }
        }
    }
    
    private void ProcessCircle(Circle circle)
    {
        Console.WriteLine($"Circle-specific processing for radius {circle.Radius}");
    }
    
    private void ProcessRectangle(Rectangle rectangle)
    {
        Console.WriteLine($"Rectangle-specific processing for {rectangle.Width}x{rectangle.Height}");
    }
}
```

## Abstraction

### Abstract Classes and Interfaces
```csharp
// Abstract class
public abstract class Vehicle
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public double Speed { get; protected set; }
    
    protected Vehicle(string make, string model, int year)
    {
        Make = make;
        Model = model;
        Year = year;
        Speed = 0;
    }
    
    // Abstract properties
    public abstract int NumberOfWheels { get; }
    public abstract string VehicleType { get; }
    
    // Abstract methods
    public abstract void Accelerate(double amount);
    public abstract void Brake(double amount);
    
    // Virtual method with default implementation
    public virtual void Start()
    {
        Console.WriteLine($"{Make} {Model} is starting");
    }
    
    // Concrete method
    public void Stop()
    {
        Speed = 0;
        Console.WriteLine($"{Make} {Model} has stopped");
    }
    
    // Abstract method with implementation
    public abstract void DisplayInfo();
}

// Interface
public interface IElectric
{
    double BatteryLevel { get; }
    void Charge(double amount);
    bool IsCharging { get; }
}

// Another interface
public interface IAutonomous
{
    void EnableAutonomousMode();
    void DisableAutonomousMode();
    bool IsAutonomousMode { get; }
}

// Concrete class implementing abstract class and interfaces
public class ElectricCar : Vehicle, IElectric, IAutonomous
{
    public double BatteryLevel { get; private set; }
    public bool IsCharging { get; private set; }
    public bool IsAutonomousMode { get; private set; }
    
    public override int NumberOfWheels => 4;
    public override string VehicleType => "Electric Car";
    
    public ElectricCar(string make, string model, int year) : base(make, model, year)
    {
        BatteryLevel = 100.0;
    }
    
    public override void Accelerate(double amount)
    {
        if (BatteryLevel > 0)
        {
            Speed += amount;
            BatteryLevel -= amount * 0.1; // Consume battery
            Console.WriteLine($"Electric car accelerating to {Speed} km/h");
        }
        else
        {
            Console.WriteLine("Cannot accelerate: Battery is empty");
        }
    }
    
    public override void Brake(double amount)
    {
        Speed = Math.Max(0, Speed - amount);
        Console.WriteLine($"Electric car braking to {Speed} km/h");
    }
    
    public override void Start()
    {
        if (BatteryLevel > 0)
        {
            base.Start();
            Console.WriteLine("Electric motor started");
        }
        else
        {
            Console.WriteLine("Cannot start: Battery is empty");
        }
    }
    
    public override void DisplayInfo()
    {
        Console.WriteLine($"Electric Car: {Make} {Model} ({Year})");
        Console.WriteLine($"Speed: {Speed} km/h, Battery: {BatteryLevel:F1}%");
        Console.WriteLine($"Autonomous mode: {(IsAutonomousMode ? "Enabled" : "Disabled")}");
    }
    
    // IElectric implementation
    public void Charge(double amount)
    {
        if (!IsCharging)
        {
            IsCharging = true;
            BatteryLevel = Math.Min(100, BatteryLevel + amount);
            IsCharging = false;
            Console.WriteLine($"Charged to {BatteryLevel:F1}%");
        }
        else
        {
            Console.WriteLine("Already charging");
        }
    }
    
    // IAutonomous implementation
    public void EnableAutonomousMode()
    {
        IsAutonomousMode = true;
        Console.WriteLine("Autonomous mode enabled");
    }
    
    public void DisableAutonomousMode()
    {
        IsAutonomousMode = false;
        Console.WriteLine("Autonomous mode disabled");
    }
}

// Another concrete class
public class Motorcycle : Vehicle
{
    public bool HasSidecar { get; set; }
    
    public override int NumberOfWheels => HasSidecar ? 3 : 2;
    public override string VehicleType => "Motorcycle";
    
    public Motorcycle(string make, string model, int year, bool hasSidecar) 
        : base(make, model, year)
    {
        HasSidecar = hasSidecar;
    }
    
    public override void Accelerate(double amount)
    {
        Speed += amount * 1.2; // Motorcycles accelerate faster
        Console.WriteLine($"Motorcycle accelerating to {Speed} km/h");
    }
    
    public override void Brake(double amount)
    {
        Speed = Math.Max(0, Speed - amount * 1.1); // Motorcycles brake faster
        Console.WriteLine($"Motorcycle braking to {Speed} km/h");
    }
    
    public override void DisplayInfo()
    {
        Console.WriteLine($"Motorcycle: {Make} {Model} ({Year})");
        Console.WriteLine($"Speed: {Speed} km/h, Wheels: {NumberOfWheels}");
        Console.WriteLine($"Has sidecar: {HasSidecar}");
    }
    
    // Motorcycle-specific method
    public void DoWheelie()
    {
        if (Speed > 20 && Speed < 60)
        {
            Console.WriteLine("Doing a wheelie!");
        }
        else
        {
            Console.WriteLine("Cannot do wheelie at this speed");
        }
    }
}
```

### Abstraction Usage
```csharp
public class AbstractionUsage
{
    public void DemonstrateAbstraction()
    {
        // Create instances of concrete classes
        Vehicle[] vehicles = 
        {
            new ElectricCar("Tesla", "Model 3", 2023),
            new Motorcycle("Harley-Davidson", "Sportster", 2022, false),
            new ElectricCar("Nissan", "Leaf", 2022)
        };
        
        // Use abstraction - treat all as Vehicle
        Console.WriteLine("=== Vehicle Operations ===");
        foreach (Vehicle vehicle in vehicles)
        {
            vehicle.Start();
            vehicle.Accelerate(50);
            vehicle.DisplayInfo();
            vehicle.Brake(20);
            vehicle.Stop();
            Console.WriteLine();
        }
        
        // Use interface-specific functionality
        Console.WriteLine("=== Electric Vehicle Operations ===");
        foreach (Vehicle vehicle in vehicles)
        {
            if (vehicle is IElectric electric)
            {
                electric.Charge(10.0);
                Console.WriteLine($"Battery level: {electric.BatteryLevel:F1}%");
            }
        }
        
        // Use multiple interfaces
        Console.WriteLine("=== Autonomous Vehicle Operations ===");
        ElectricCar tesla = (ElectricCar)vehicles[0];
        tesla.EnableAutonomousMode();
        tesla.Accelerate(30);
        tesla.DisplayInfo();
        tesla.DisableAutonomousMode();
        
        // Use derived class specific methods
        Console.WriteLine("=== Motorcycle Operations ===");
        Motorcycle motorcycle = (Motorcycle)vehicles[1];
        motorcycle.Accelerate(40);
        motorcycle.DoWheelie();
    }
    
    // Factory method pattern with abstraction
    public Vehicle CreateVehicle(string type, string make, string model, int year)
    {
        return type.ToLower() switch
        {
            "electric" => new ElectricCar(make, model, year),
            "motorcycle" => new Motorcycle(make, model, year, false),
            _ => throw new ArgumentException($"Unknown vehicle type: {type}")
        };
    }
    
    public void DemonstrateFactory()
    {
        Vehicle vehicle1 = CreateVehicle("electric", "Tesla", "Model S", 2023);
        Vehicle vehicle2 = CreateVehicle("motorcycle", "Yamaha", "R1", 2022);
        
        vehicle1.DisplayInfo();
        vehicle2.DisplayInfo();
    }
}
```

## Composition over Inheritance

### Composition Example
```csharp
// Instead of deep inheritance, use composition
public class Engine
{
    public string Type { get; set; }
    public int Horsepower { get; set; }
    public bool IsRunning { get; private set; }
    
    public Engine(string type, int horsepower)
    {
        Type = type;
        Horsepower = horsepower;
        IsRunning = false;
    }
    
    public void Start()
    {
        if (!IsRunning)
        {
            IsRunning = true;
            Console.WriteLine($"{Type} engine started ({Horsepower} HP)");
        }
    }
    
    public void Stop()
    {
        if (IsRunning)
        {
            IsRunning = false;
            Console.WriteLine($"{Type} engine stopped");
        }
    }
}

public class Transmission
{
    public int Gears { get; set; }
    public int CurrentGear { get; private set; }
    
    public Transmission(int gears)
    {
        Gears = gears;
        CurrentGear = 1;
    }
    
    public void ShiftUp()
    {
        if (CurrentGear < Gears)
        {
            CurrentGear++;
            Console.WriteLine($"Shifted to gear {CurrentGear}");
        }
    }
    
    public void ShiftDown()
    {
        if (CurrentGear > 1)
        {
            CurrentGear--;
            Console.WriteLine($"Shifted to gear {CurrentGear}");
        }
    }
}

public class GPS
{
    public string CurrentLocation { get; private set; }
    public bool IsActive { get; private set; }
    
    public void Activate()
    {
        IsActive = true;
        CurrentLocation = "Unknown";
        Console.WriteLine("GPS activated");
    }
    
    public void UpdateLocation(string location)
    {
        if (IsActive)
        {
            CurrentLocation = location;
            Console.WriteLine($"GPS: Current location - {CurrentLocation}");
        }
    }
}

// Car using composition
public class ModernCar
{
    private Engine _engine;
    private Transmission _transmission;
    private GPS _gps;
    
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    
    public ModernCar(string make, string model, int year)
    {
        Make = make;
        Model = model;
        Year = year;
        
        // Compose with other objects
        _engine = new Engine("V6", 300);
        _transmission = new Transmission(6);
        _gps = new GPS();
    }
    
    public void StartCar()
    {
        Console.WriteLine($"Starting {Make} {Model}");
        _engine.Start();
        _gps.Activate();
    }
    
    public void StopCar()
    {
        _engine.Stop();
        Console.WriteLine($"{Make} {Model} stopped");
    }
    
    public void Accelerate()
    {
        if (_engine.IsRunning)
        {
            _transmission.ShiftUp();
            Console.WriteLine("Accelerating");
        }
    }
    
    public void Brake()
    {
        _transmission.ShiftDown();
        Console.WriteLine("Braking");
    }
    
    public void NavigateTo(string destination)
    {
        _gps.UpdateLocation($"Navigating to {destination}");
    }
    
    public void DisplayStatus()
    {
        Console.WriteLine($"Car: {Make} {Model} ({Year})");
        Console.WriteLine($"Engine: {_engine.Type} ({_engine.Horsepower} HP) - Running: {_engine.IsRunning}");
        Console.WriteLine($"Transmission: {_transmission.CurrentGear}/{_transmission.Gears} gears");
        Console.WriteLine($"GPS: {(_gps.IsActive ? "Active" : "Inactive")} - Location: {_gps.CurrentLocation}");
    }
}
```

### Composition Usage
```csharp
public class CompositionUsage
{
    public void DemonstrateComposition()
    {
        // Create car using composition
        var car = new ModernCar("Toyota", "Camry", 2023);
        
        // Use composed objects
        car.StartCar();
        car.DisplayStatus();
        
        car.Accelerate();
        car.Accelerate();
        car.NavigateTo("123 Main Street");
        
        car.Brake();
        car.StopCar();
        
        // Composition allows easy modification
        Console.WriteLine("\n=== Upgrading Car ===");
        var sportsCar = new ModernCar("Ferrari", "488", 2023);
        sportsCar.StartCar();
        sportsCar.Accelerate();
        sportsCar.Accelerate();
        sportsCar.Accelerate();
        sportsCar.DisplayStatus();
    }
    
    // Composition with dependency injection
    public class CarFactory
    {
        private readonly IEngineProvider _engineProvider;
        private readonly ITransmissionProvider _transmissionProvider;
        
        public CarFactory(IEngineProvider engineProvider, ITransmissionProvider transmissionProvider)
        {
            _engineProvider = engineProvider;
            _transmissionProvider = transmissionProvider;
        }
        
        public ModernCar CreateCar(string make, string model, int year, string engineType, int horsepower, int gears)
        {
            var car = new ModernCar(make, model, year);
            
            // Inject dependencies
            car.SetEngine(_engineProvider.GetEngine(engineType, horsepower));
            car.SetTransmission(_transmissionProvider.GetTransmission(gears));
            
            return car;
        }
    }
}

public interface IEngineProvider
{
    Engine GetEngine(string type, int horsepower);
}

public interface ITransmissionProvider
{
    Transmission GetTransmission(int gears);
}

// Extension to ModernCar for dependency injection
public static class CarExtensions
{
    public static void SetEngine(this ModernCar car, Engine engine)
    {
        // Reflection or property injection would be used in real scenario
        Console.WriteLine($"Engine set to {engine.Type} ({engine.Horsepower} HP)");
    }
    
    public static void SetTransmission(this ModernCar car, Transmission transmission)
    {
        Console.WriteLine($"Transmission set to {transmission.Gears} gears");
    }
}
```

## Best Practices

### OOP Best Practices
```csharp
public class OOPBestPractices
{
    // Single Responsibility Principle
    public class UserValidator
    {
        public bool Validate(User user)
        {
            return !string.IsNullOrWhiteSpace(user.Email) && 
                   user.Email.Contains("@") && 
                   user.Age >= 18;
        }
    }
    
    public class UserRepository
    {
        public void Save(User user)
        {
            // Save user to database
            Console.WriteLine($"User {user.Email} saved");
        }
    }
    
    public class UserService
    {
        private readonly UserValidator _validator;
        private readonly UserRepository _repository;
        
        public UserService(UserValidator validator, UserRepository repository)
        {
            _validator = validator;
            _repository = repository;
        }
        
        public bool Register(User user)
        {
            if (_validator.Validate(user))
            {
                _repository.Save(user);
                return true;
            }
            
            return false;
        }
    }
    
    // Open/Closed Principle
    public abstract class NotificationProvider
    {
        public abstract void Send(string message);
    }
    
    public class EmailNotificationProvider : NotificationProvider
    {
        public override void Send(string message)
        {
            Console.WriteLine($"Email sent: {message}");
        }
    }
    
    public class SMSNotificationProvider : NotificationProvider
    {
        public override void Send(string message)
        {
            Console.WriteLine($"SMS sent: {message}");
        }
    }
    
    // Liskov Substitution Principle
    public abstract class Bird
    {
        public abstract void MakeSound();
        public virtual void Fly()
        {
            Console.WriteLine("Bird is flying");
        }
    }
    
    public class Sparrow : Bird
    {
        public override void MakeSound()
        {
            Console.WriteLine("Chirp chirp");
        }
    }
    
    public class Penguin : Bird
    {
        public override void MakeSound()
        {
            Console.WriteLine("Squawk squawk");
        }
        
        public override void Fly()
        {
            Console.WriteLine("Penguins cannot fly");
        }
    }
    
    // Interface Segregation Principle
    public interface IPrinter
    {
        void Print(string content);
    }
    
    public interface IScanner
    {
        void Scan();
    }
    
    public interface IFax
    {
        void Send(string content);
    }
    
    public class SimplePrinter : IPrinter
    {
        public void Print(string content)
        {
            Console.WriteLine($"Printing: {content}");
        }
    }
    
    public class AllInOnePrinter : IPrinter, IScanner, IFax
    {
        public void Print(string content)
        {
            Console.WriteLine($"Printing: {content}");
        }
        
        public void Scan()
        {
            Console.WriteLine("Scanning document");
        }
        
        public void Send(string content)
        {
            Console.WriteLine($"Sending fax: {content}");
        }
    }
    
    // Dependency Inversion Principle
    public interface ILogger
    {
        void Log(string message);
    }
    
    public class FileLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"File log: {message}");
        }
    }
    
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"Console log: {message}");
        }
    }
    
    public class BusinessLogic
    {
        private readonly ILogger _logger;
        
        public BusinessLogic(ILogger logger)
        {
            _logger = logger;
        }
        
        public void DoWork()
        {
            _logger.Log("Work started");
            // Do some work
            _logger.Log("Work completed");
        }
    }
}

public class User
{
    public string Email { get; set; }
    public int Age { get; set; }
}
```

## Common Pitfalls

### Common OOP Mistakes
```csharp
public class CommonPitfalls
{
    // Pitfall: God class (too many responsibilities)
    public class GodClass
    {
        public void ValidateUser() { }
        public void SaveToDatabase() { }
        public void SendEmail() { }
        public void GenerateReport() { }
        public void CalculateTaxes() { }
        public void ProcessPayment() { }
        // ... many more responsibilities
    }
    
    // Solution: Single Responsibility
    public class UserValidator
    {
        public bool Validate(User user) => true;
    }
    
    public class UserRepository
    {
        public void Save(User user) { }
    }
    
    public class EmailService
    {
        public void SendEmail(string to, string message) { }
    }
    
    // Pitfall: Inheritance abuse
    public class Employee : Person
    {
        public void Work() { }
    }
    
    public class Manager : Employee
    {
        public void Manage() { }
    }
    
    public class CEO : Manager
    {
        public void LeadCompany() { }
    }
    
    public class Intern : Employee
    {
        public void Learn() { }
    }
    
    // Solution: Composition over inheritance
    public class Employee
    {
        private readonly IWorkBehavior _workBehavior;
        
        public Employee(IWorkBehavior workBehavior)
        {
            _workBehavior = workBehavior;
        }
        
        public void Work()
        {
            _workBehavior.PerformWork();
        }
    }
    
    // Pitfall: Tight coupling
    public class TightCoupling
    {
        private readonly DatabaseConnection _db = new DatabaseConnection();
        
        public void GetData()
        {
            _db.Connect();
            // Get data
            _db.Disconnect();
        }
    }
    
    // Solution: Dependency injection
    public class LooseCoupling
    {
        private readonly IDataRepository _repository;
        
        public LooseCoupling(IDataRepository repository)
        {
            _repository = repository;
        }
        
        public void GetData()
        {
            _repository.GetData();
        }
    }
    
    // Pitfall: Breaking Liskov Substitution
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        
        public virtual int GetArea() => Width * Height;
    }
    
    public class Square : Rectangle
    {
        public override int Width 
        { 
            get => base.Width; 
            set => base.Width = base.Height = value; // Breaks LSP
        }
        
        public override int Height 
        { 
            get => base.Height; 
            set => base.Height = base.Width = value; // Breaks LSP
        }
    }
    
    // Solution: Use separate classes or interfaces
    public interface IShape
    {
        int GetArea();
    }
    
    public class Rectangle : IShape
    {
        public int Width { get; set; }
        public int Height { get; set; }
        
        public int GetArea() => Width * Height;
    }
    
    public class Square : IShape
    {
        public int Side { get; set; }
        
        public int GetArea() => Side * Side;
    }
}

public interface IDataRepository
{
    void GetData();
}

public interface IWorkBehavior
{
    void PerformWork();
}

public class DatabaseConnection
{
    public void Connect() { }
    public void Disconnect() { }
}

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}
```

## Summary

C# object-oriented programming provides:

**Classes and Objects:**
- Class definition with fields, properties, methods
- Constructors and destructors
- Static and instance members
- Object creation and initialization

**Inheritance:**
- Base and derived class relationships
- Method overriding with virtual and abstract
- Method hiding with new keyword
- Protected and internal access modifiers

**Encapsulation:**
- Access modifiers (public, private, protected, internal)
- Property getters and setters
- Validation and business logic encapsulation
- Dependency injection and loose coupling

**Polymorphism:**
- Method overriding and virtual methods
- Abstract classes and methods
- Interface implementation
- Runtime polymorphism with base class references

**Abstraction:**
- Abstract classes with abstract members
- Interfaces for contracts
- Multiple interface implementation
- Factory patterns and dependency injection

**Composition over Inheritance:**
- Object composition for flexibility
- Dependency injection
- Interface segregation
- Loose coupling design

**Best Practices:**
- SOLID principles
- Single Responsibility
- Open/Closed Principle
- Liskov Substitution
- Interface Segregation
- Dependency Inversion

**Common Pitfalls:**
- God classes with too many responsibilities
- Inheritance abuse
- Tight coupling
- Breaking Liskov Substitution
- Violating encapsulation

C# provides powerful OOP features that enable clean, maintainable, and extensible code when used correctly with proper design principles and patterns.
