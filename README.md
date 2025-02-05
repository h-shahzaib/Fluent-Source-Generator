## Overview

Built on **.NET Standard 2.0**, this library provides seamless compatibility across all .NET versions, with zero external dependencies. Simply import the project as a reference, and you're ready to go!

This library simplifies source code generation with a fluent, user-friendly API that ensures your code structure remains clean, readable, and intuitive. It takes care of all the tedious formatting, indentation, and syntax, so you can focus on the logic of generating code.

---

## Example

To demonstrate the library's usage, here's how you can generate C# code:

### **Generated Code**

This is the generated code that will be output by the library:

```csharp
using System.IO;
using System.Collections.Generic;

namespace MyNamespace
{
    public class Program
    {
        public async Task Main(string[] args)
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

### **Code to Generate the Above Output**

Now, using the library's `SourceBuilder` API, you can generate this code as follows:

```csharp
var _root = new SourceBuilder();
_root.Using("System.IO");
_root.Using("System.Collections.Generic");

_root.Namespace("MyNamespace", _namespace =>
{
    _namespace.Class("public", "Program", _class =>
    {
        _class.Method("public async", "Task", "Main", "string[] args", _method =>
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

var str = _root.ToString();
```

---

## Key Features

### **1. Code that Writes Code**

One of the key strengths of this library is that the code used to generate other code is **nearly identical** to the output that gets generated. You don't have to worry about indentation, braces, or high-level syntax like `usings`, `namespaces`, or `classes`—the library handles it all for you.

### **2. Char Replacement System**

Notice that when writing strings with double quotes (`"`) inside, you can use backticks (`` ` ``) for convenience. The library has a **character replacement system** in place to handle this.

For example:
```csharp
_method.Line("Console.WriteLine(`Hello World!`);");
```

This uses backticks instead of double quotes to avoid escaping characters. The library automatically registers backticks as a replacement for double quotes. You can also configure custom replacements if necessary.

To **register a custom character replacement**:
```csharp
_root.ChildOptions.RegisterCharReplacement('`', '"');
```

This ensures any backtick in the `SourceBuilder` will be replaced by double quotes when generating the code.

To **remove a character replacement**:
```csharp
_root.ChildOptions.RemoveCharReplacement('`');
```

---

## Getting Started

1. Add the library to your project by importing it as a reference.
2. Use the fluent API to start building your code structure.
3. Call `ToString()` on the `SourceBuilder` instance to retrieve the generated C# code.

---

## Limitation with Older .NET Versions

### **Potential Name Conflict In Nested Scopes**

When using this library with earlier versions of .NET Core (< 3.0) or with .NET Framework, you cannot reuse the same variable name in nested scopes. For example:

```csharp
_class.Method("public", "void", "Main", "", _method =>
{
    _method.Line("var number = 10;");

    _method.Method("", "void", "InnerMethod", "", _method => // <-- Will give `A local or parameter named '_method' cannot be declared in this scope because that name is used in an enclosing local scope to define a local or parameter` syntax error.
    {
        _method.Line("var number = 20;");
    });
});
```

#### **Solution:**

You'll have to use a different name like maybe `_inner_method` for inner scope.

### **Recommendation**

For better flexibility, use **.NET Core 3.0+** or **.NET Standard 2.1+** to avoid this limitation, as these versions allow variable name reuse in nested scopes.

---
