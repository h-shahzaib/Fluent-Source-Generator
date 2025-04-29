⚠ **Note:** Currently, this library is designed to generate C# source code, but it can easily be extended to support source code generation for virtually any programming or scripting language.  

## **Overview** 

Built on **.NET Standard 2.0**, this library provides seamless compatibility across all .NET versions, with zero external dependencies. Simply import the project as a reference, and you're ready to go!

This library simplifies source code generation with a fluent, user-friendly API (inspired by **QuestPDF**) that ensures your code structure remains clean, readable, and intuitive. It takes care of all the tedious formatting, indentation, and syntax, so you can focus on the logic of generating code.


---


## **Example**

To demonstrate the library's usage, lets generate the following C# code:

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

Using the library's `CSharp_SourceBuilder`, you can generate the above code as follows:

```csharp
var _root = new CSharp_SourceBuilder();
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

var str = _root.ToString();
```


---


## **Key Features**

### 1. **Code that Writes Code**  
One of the key strengths of this library is that the code used to generate other code is nearly identical to the generated output. You no longer need to worry about line breaks, indentation, braces, or high-level syntax like `usings`, `namespaces`, or `classes`—the library takes care of all that for you.


### 2. **Char Replacement System**  
When writing strings with double quotes (`"`) inside, you can use backticks (`` ` ``) for convenience. The library has a built-in character replacement system to handle this.

For example:
```csharp
_method.Line("Console.WriteLine(`Hello World!`);");
```
This uses backticks instead of double quotes to avoid escaping characters. The library automatically registers backticks as replacements for double quotes. You can also register custom character replacements as needed.

To register a custom character replacement:
```csharp
_root.ChildOptions.RegisterCharReplacement('`', '"');
```
This ensures that any backtick in the `SourceBuilder` will be replaced by double quotes when generating the code.

To remove a character replacement:
```csharp
_root.ChildOptions.RemoveCharReplacement('`');
```


### 3. **Token-Based System**  
Every element is aware of the elements before and after it, thanks to a token-based system. For example, when calling `.Line()` twice, no line break is inserted between them. However, when using `.Lines()`, a different token is used that understands that multiple lines should be preceded by a line break. Furthermore, if the `.Line()` token is the last in a block with no subsequent content, it automatically knows that the block is about to end and prevents adding an unnecessary line break.

This system ensures that code generation remains structured and easy to control, helping maintain consistency and readability across your generated output.


---


## **Getting Started**

1. Add the library to your project by importing it as a reference.
2. Use the fluent API to start building your code structure.
3. Call `ToString()` on the `SourceBuilder` instance to retrieve the generated C# code.


---


## **Limitation with Older .NET Versions**


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
