# Unity Extensions

[![openupm](https://img.shields.io/npm/v/com.greener-games.unity-extensions?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.greener-games.unity-extensions/)

A comprehensive collection of C# extension methods for Unity, designed to streamline your development process, reduce boilerplate code, and make your scripts more readable and efficient.

## Why Use Unity Extensions?

This package is a valuable toolkit for any Unity developer. Here’s why you should consider adding it to your project:

*   **Write Cleaner Code:** Replace verbose, repetitive code with concise and expressive method calls.
*   **Save Time:** Stop reinventing the wheel. This library provides a wide range of pre-built, tested utility functions for common tasks.
*   **Improve Readability:** Extension methods allow you to write code that is more fluent and easier to understand at a glance.
*   **Extensive Functionality:** From `Transform` and `GameObject` manipulations to `string` and `color` utilities, this package has you covered.

## Key Features

Here is a glimpse of the extension categories available in this package.

### `Transform` Extensions

Manipulate `Transform` components with ease. Set positions, rotations, and scales on individual axes, manage child objects, and more.

**Example:**
```csharp
// Before
Vector3 pos = transform.position;
pos.x = 5f;
transform.position = pos;

// After
transform.SetX(5f);

// Destroy all children of a transform
transform.DestroyChildren();
```

### `GameObject` Extensions

Simplify common `GameObject`-related tasks like adding components and managing object hierarchies.

**Example:**
```csharp
// Before
MyComponent component = gameObject.GetComponent<MyComponent>();
if (component == null)
{
    component = gameObject.AddComponent<MyComponent>();
}

// After
MyComponent component = gameObject.GetOrAddComponent<MyComponent>();
```

### `String` Extensions

A set of powerful extensions for string manipulation, including formatting, case conversion, and complex parsing.

**Example:**
```csharp
// Convert a string to Title Case
string title = "hello world".TitleCase(); // "Hello World"

// Extract initials from a name
string initials = "John F. Kennedy".ExtractInitialsFromName(); // "JK"
```

### `Color` Extensions

Work with colors more intuitively.

**Example:**
```csharp
// Create a new color with a modified alpha value
Color myColor = Color.red;
Color transparentRed = myColor.WithAlpha(0.5f);
```

### And Many More...

This package includes a wide variety of other extensions for:
*   `Camera`
*   `Enums`
*   `Enumerables`
*   `Vectors`
*   `RectTransform`
*   `Numbers` (float, int)
*   And more!

## Installation

You can install this package in your Unity project using one of the following methods:

### 1. Unity Package Manager (via Git URL)

1.  In Unity, open the **Package Manager** (`Window > Package Manager`).
2.  Click the **+** button in the top-left corner and select **"Add package from git URL..."**.
3.  Enter the following URL:
    ```
    https://github.com/Greener-Games/Unity_Extensions.git
    ```
4.  Click **Add**. The package will be installed in your project.

### 2. OpenUPM

1.  Follow the instructions on [OpenUPM](https://openupm.com/packages/com.greener-games.unity-extensions/) to add the OpenUPM scoped registry to your project.
2.  Install the package `com.greener-games.unity-extensions` via the Unity Package Manager.

## How to Use

Once the package is installed, the extension methods will be available automatically on the relevant types. Simply add the `using GG.Extensions;` namespace to your C# scripts to start using them.

**Example Script:**
```csharp
using UnityEngine;
using GG.Extensions; // Don't forget this using statement!

public class MyPlayer : MonoBehaviour
{
    void Start()
    {
        // Example using a Transform extension
        transform.SetLocalX(0f);

        // Example using a GameObject extension
        var rb = gameObject.GetOrAddComponent<Rigidbody>();
    }
}
```

## Documentation

For a complete API reference of all available extension methods, please visit our [documentation website](https://greener-games.github.io/Unity_Extensions/).

## Contributing

Contributions are welcome! If you have a useful extension you'd like to add, or if you've found a bug, please open an issue or submit a pull request on our [GitHub repository](https://github.com/Greener-Games/Unity_Extensions).
