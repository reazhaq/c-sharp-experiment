# Project site
* [C# Language Design](https://github.com/dotnet/csharplang)
* [.NET Runtime](https://github.com/dotnet/runtime)
* [Roslyn](https://github.com/dotnet/roslyn)

# Timeline (very high level)

* 1 (~2002-03): first release
* 2 (~2005-06): Generics, Nullable Types, Anonymous methods, etc.
* 3 (~2007-08): Lambda, Extension Methods, Anonymous types, Expression tree, Auto-Property, etc.
* 4 (~2010): Covariance-Contravariance, Optional Paramters, Dynamic Binding, COM interop, Task, etc.
* 5 (~2011-2012) async-await, caller information
* 6 (~2014): Null conditional Op, Auto Property initializer, Name Of, Expression body, string interpolation, etc.
* [7._ (2017-2018)](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7): value tuple, deconstructor (not destructor/finalizer), out variable, pattern matching, local function, ref. return, expression body (enhancement), value task, etc.
  * async main, `default` literal, some tuple enhancements, some patter matching improvements, ref local in returns, in parameter (more like readonly), generic constraint (enhancement)
* [8 (2019)](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8): readonly members, default interface method, pattern matching (enhancement), using declaration, static local function, disposable ref structure, nullable ref types, async stream, async disposable, indices and ranges, null-coalescing assignment, unmanaged constructed types, stackalloc in nested expression, enhancement of interpolated verbatim strings
* [9 (2020)](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9): records, init-only setters, pattern matching (enhancement), native sized int, function pointers, suppress emitting localsinit flag, target-type new expression, covariant return type, get-enumerator extension, lambda discard parameters, attributes on local function, module initializers, new feature for partial methods
* [10 (2021)](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10): record structs, improvements of structure types, global using directive, file-scoped namespace, extended property patterns, allow constant interpolated string, seal to-string override for record type, both assignment and declaration in same desconstruction, async-method-bulider attribute
 
for details - look at [The History of C#](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history)
