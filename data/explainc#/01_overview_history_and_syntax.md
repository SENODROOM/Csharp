# C# (C-sharp) 💜

> *"C# is an elegant and type-safe object-oriented language that enables developers to build a variety of secure and robust applications."*
> — Microsoft Documentation

---

## Table of Contents

1. [Overview](#overview)
2. [History & Evolution](#history--evolution)
3. [Core Characteristics](#core-characteristics)
4. [Syntax & Fundamentals](#syntax--fundamentals)
5. [Type System](#type-system)
6. [Object-Oriented Programming](#object-oriented-programming)
7. [Interfaces & Abstract Classes](#interfaces--abstract-classes)
8. [Generics](#generics)
9. [LINQ](#linq)
10. [Async & Await](#async--await)
11. [Pattern Matching](#pattern-matching)
12. [Records and Immutability](#records-and-immutability)
13. [The .NET Ecosystem](#the-net-ecosystem)
14. [Performance & Span<T>](#performance--spant)

---

## Overview

C# is a **statically typed, multi-paradigm, compiled programming language** developed by Microsoft. It runs on the **.NET runtime** (CLR — Common Language Runtime), which provides garbage collection, JIT compilation, and platform independence.

```
C# Compilation Pipeline
──────────────────────────────────────────────────────────────────
C# Source (.cs)
     │
     ▼ Roslyn Compiler (csc / dotnet build)
IL Bytecode (.dll / .exe)   ← "Intermediate Language" (like JVM bytecode)
     │
     ▼ CLR Just-In-Time (JIT) Compiler
Native Machine Code
     │
     ▼
Execution  +  Garbage Collection  +  Runtime Services
──────────────────────────────────────────────────────────────────
```

**Key Stats:**
- Created: 2000 by Anders Hejlsberg at Microsoft
- Current version: C# 13 (.NET 9, 2024)
- Paradigms: OOP, Functional, Generic, Concurrent, Component-based
- Typing: Static, strong, with type inference (`var`, `dynamic`)
- Platforms: Windows, Linux, macOS, Android, iOS, Web, Cloud

---

## History & Evolution

```
C# Version History
────────────────────────────────────────────────────────────────
C# 1.0  (2002)  — Classes, interfaces, delegates, events
C# 2.0  (2005)  — Generics, iterators (yield), nullable types
C# 3.0  (2007)  — LINQ, lambda expressions, var, extension methods
C# 4.0  (2010)  — Dynamic binding, named/optional parameters
C# 5.0  (2012)  — async / await (game changer!)
C# 6.0  (2015)  — String interpolation, null-conditional ?.
C# 7.0  (2017)  — Tuples, pattern matching, local functions
C# 8.0  (2019)  — Nullable reference types, switch expressions
C# 9.0  (2020)  — Records, init properties, top-level statements
C# 10.0 (2021)  — Global usings, file-scoped namespaces
C# 11.0 (2022)  — Required members, raw string literals
C# 12.0 (2023)  — Primary constructors, collection expressions
C# 13.0 (2024)  — params collections, lock object, new escape sequences
────────────────────────────────────────────────────────────────
```

---

## Core Characteristics

| Feature | Description |
|---------|-------------|
| **Static Typing** | Types checked at compile time |
| **Garbage Collection** | Automatic memory management via CLR |
| **Unified Type System** | Everything (including primitives) derives from `object` |
| **Generics** | Type-safe reusable code (better than Java generics) |
| **Delegates & Events** | First-class functions and observer pattern built in |
| **LINQ** | Integrated query language for any data source |
| **Properties** | Syntax sugar for getters/setters |
| **Nullable Reference Types** | Opt-in null safety (C# 8+) |
| **Async/Await** | First-class async programming model |
| **Expression Trees** | Inspect code as data (used by ORMs like EF Core) |

---

## Syntax & Fundamentals

```csharp
// Modern C# (top-level statements — no class boilerplate needed!)
using System;
using System.Collections.Generic;

Console.WriteLine("Hello, World! 💜");

// Variables
int    age     = 30;
double pi      = 3.14159;
string name    = "Alice";
bool   active  = true;
var    inferred = 42;     // compiler infers type (still statically typed!)

// String interpolation (C# 6+)
Console.WriteLine($"Hello, {name}! You are {age} years old.");

// Raw string literals (C# 11+) — no escape sequences needed
string json = """
    {
        "name": "Alice",
        "path": "C:\Users\Alice"
    }
    """;

// Nullable value types
int? nullableInt = null;
if (nullableInt.HasValue) Console.WriteLine(nullableInt.Value);
int result = nullableInt ?? -1;  // null-coalescing

// Nullable reference types (C# 8+, opt-in via project settings)
string? maybeNull = null;
string definite   = maybeNull!;  // null-forgiving operator (use sparingly)
int len = maybeNull?.Length ?? 0;  // safe access

// Constants and readonly
const double Pi = 3.14159265358979;  // compile-time constant
static readonly DateTime AppStart = DateTime.Now;  // runtime constant
```

### Operators

```csharp
// Arithmetic
int a = 17 / 5;    // 3 (integer division!)
double b = 17.0 / 5;  // 3.4
int mod = 17 % 5;  // 2

// Null-related operators
var x = obj?.Property?.SubProperty;   // null-conditional
var y = value ?? defaultValue;         // null-coalescing
obj ??= new MyClass();                 // null-coalescing assignment

// Pattern operators
if (obj is string s && s.Length > 5) { ... }  // is pattern
if (obj is not null) { ... }                   // negation pattern

// Range and index (C# 8+)
var arr = new[] { 1, 2, 3, 4, 5 };
var last  = arr[^1];      // 5 (last element)
var slice = arr[1..4];    // [2, 3, 4]
var tail  = arr[^2..];    // [4, 5]
```

---

