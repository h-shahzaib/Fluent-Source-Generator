# Flynth.CSharp

> **Flynth.CSharp** is a fluent, zero-dependency source code generator for C#.
> It helps you create structured C# source code through a clean, builder-style API — so you can focus on _what_ to generate, not _how_ to format it.

---

## 📦 Installation

Install via the .NET CLI:

```
dotnet add package Flynth.CSharp
```

---

## 🚀 Overview

Built on **.NET Standard 2.0**, Flynth.CSharp works across **all .NET versions** with **no external dependencies**.
It handles indentation, structure, formatting, and character escaping — all out of the box.

---

## ✨ Key Features

### ✅ Fluent API — Code That Writes Code

Write source generation logic that looks nearly identical to the output.
No manual formatting, indentation, or brace management required.

### 🧠 Token-Aware Formatting

The builder system knows its context — it places line breaks and indentation only where needed, making your generated code clean and readable.

### 🔄 Character Replacement System

Write code with backticks instead of escaped double quotes:

```csharp
_method.Line("Console.WriteLine(`Hello World!`);");
```

You can customize or remove replacements:

```csharp
_root.ChildOptions.RegisterCharReplacement('`', '"');
_root.ChildOptions.RemoveCharReplacement('`');
```

---

## 🧪 Example

This C# code:

```csharp
using System.IO;
using System.Collections.Generic;

namespace MyNamespace
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Hello World!");

            var employees = await m_DbContext.Employees
               .Where(i => i.Age >= 30)
               .ToListAsync();

            Console.WriteLine($"Total employees count: {employees.Count}");
        }
    }
}
```

Can be generated like this:

```csharp
using Flynth.CSharp;

var _root = new SourceBuilder();
_root.Using("System.IO");
_root.Using("System.Collections.Generic");

_root.Namespace("MyNamespace", _namespace =>
{
    _namespace.Class("public", "Program", _class =>
    {
        _class.Method("public static async", "Task", "Main", "string[] args", _method =>
        {
            _method.Line("Console.WriteLine(`Hello World!`);");
            _method.Line("Console.WriteLine(`Hello World!`);");

            _method.Lines(
                "var employees = await m_DbContext.Employees",
                "   .Where(i => i.Age >= 30)",
                "   .ToListAsync();"
            );

            _method.Line("Console.WriteLine($`Total employees count: {employees.Count}`);");
        });
    });
});

var result = _root.ToString();
```

---

## ⚠️ Compatibility Note

Flynth works with all .NET targets that support **.NET Standard 2.0**.
If you're using older .NET versions like .NET Core < 3.0 or .NET Framework, be aware of this:

### 🔁 Variable Name Conflicts in Nested Scopes

Older compilers don't allow reuse of the same variable name inside nested lambdas:

```csharp
_class.Method("public", "void", "Main", "", _method =>
{
    _method.Line("var number = 10;");

    _method.Method("", "void", "InnerMethod", "", _method => // ❌ Conflict
    {
        _method.Line("var number = 20;");
    });
});
```

#### ✅ Solution:

Use a different name in inner scopes (e.g. `_inner_method`).

---

## 🤔 Why Flynth?

- ✅ Fluent, readable, and chainable API
- ✅ No need to manage syntax or formatting manually
- ✅ Output closely mirrors your generation code
- ✅ No dependencies
- ✅ Works with any .NET project

---

## 📚 Getting Started

1. Install the package
2. Use `SourceBuilder` to define your structure
3. Call `.ToString()` to get the generated code

---

## 🔗 Links

- 📦 NuGet: [https://www.nuget.org/packages/Flynth.CSharp/](https://www.nuget.org/packages/Flynth.CSharp/)
- 💻 GitHub: [https://github.com/h-shahzaib/Flynth](https://github.com/h-shahzaib/Flynth)
