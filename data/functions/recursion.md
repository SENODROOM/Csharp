# Recursion

A method calling itself.

## Example
```csharp
int Factorial(int n) {
    if(n == 0) return 1;
    return n * Factorial(n - 1);
}
Practice

Write a recursive function for sum of numbers.
