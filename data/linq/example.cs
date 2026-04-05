using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        List<int> numbers = new List<int> { 1, 5, 8, 12, 15, 20 };

        // Using LINQ to find numbers greater than 10
        var largeNumbers = numbers.Where(n => n > 10).ToList();

        Console.WriteLine("Numbers greater than 10:");
        largeNumbers.ForEach(n => Console.WriteLine(n));
    }
}