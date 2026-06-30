# DavidGroup.Core.Utilities

#### [![Release](https://github.com/david-group-solutions/utilities/actions/workflows/release.yml/badge.svg?branch=main)](https://github.com/david-group-solutions/utilities/actions/workflows/release.yml) [![Nuget](https://img.shields.io/nuget/vpre/DavidGroup.Core.Utilities)](https://www.nuget.org/packages/DavidGroup.Core.Utilities/)

A productivity library that accelerates .NET project development by providing ready-to-use helpers for
common application needs.

---

## 🚀 Getting Started

### Install NuGet Package

Using the .NET CLI:

```bash
dotnet add package DavidGroup.Core.Utilities
```

Or via the Package Manager Console:

```bash
Install-Package DavidGroup.Core.Utilities
```

### How to use it?

Feel free to explore the [samples](https://github.com/david-group-solutions/utilities/tree/main/samples) to find
practical examples for each feature.
New samples are added continuously as more features are developed.

## 📦 Key Features

### Code generator

```csharp
string code = CodeGenerator.GenerateCode(); // Example output: "A7kP2mX9"
```

### String helpers

```csharp
string propertyName = StringHelper.FirstCharToLower("UserName"); // Output: "userName"
```

### Enum helpers

```csharp
public enum Status
{
    Pending,
    Completed
}

Status status = "completed".ToEnum(Status.Pending);

Console.WriteLine(status); // Output: Completed
```

### Other helpers

**CalculateAge()**

```csharp
DateTime birthDate = new(1995, 8, 15);
int age = birthDate.CalculateAge();

Console.WriteLine(age);
// Output: 30 (depending on the current date)
```

---

**Map()**

```csharp
decimal progress = 50m.Map(0m, 100m, 0m, 1m);

Console.WriteLine(progress);
// Output: 0.5
```

---

**IsInRange()**

```csharp
bool isWithin = CommonUtilities.IsInRange(1, 10, 3, 7);

Console.WriteLine(isWithin);
// Output: True
```

## 🤝 Contributing

Found a bug? Have an idea? Want to contribute?

* Submit an issue:
  https://github.com/david-group-solutions/utilities/issues
* Create a pull request:
  https://github.com/david-group-solutions/utilities/pulls

Contributions of any size are appreciated!

## 📝 License

Distributed under the **MIT license**.
See [License](https://github.com/david-group-solutions/utilities/blob/main/LICENSE.txt) for more information.

Copyright © 2025-2026 David Khachatryan (David Group Solutions)
