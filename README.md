# UnityInterOp

The purpose of this repo is to demonstrate the various marshaling behaviors of C# for C++. This demonstrates the following:
- passing simple data types by value, address, and reference
- returning simple data types
- passing arrays of simple data types as const and non-const
- passing C# strings as C-strings
- returning C-strings of different lengths
- passing structs by value, address, and reference
  - structs with simple data type members
  - structs with struct members
  - structs with pointer members
  - structs with array members
- returning structs
- passing and executing delegates (function pointers) in the same thread
  - with simple data type parameters
  - with string parameters
  - with struct parameters
- passing and executing a delegate array in the same thread
- passing, storing, and executing delegates for later
- passing struct with delegate members, storing, and executing delegates
- passing and executing delegates in a separate thread

This repo contains 2 projects:
- [**C++ Dynamic Link Library**](NativeLib) with [**Visual Studio Solution**](NativeLib/Visual%20Studio)
- [**Unity3D C#**](../..)

Tested on Visual Studio Community 2017 Version 15.8.5 and Unity Version 2018.3.7f1

## How To Use
1. Build the C++ DLL using Visual Studio
2. Copy the `NativeLib.dll` from the output folder and paste it into the `Assets\Plugins\x86_64` folder
3. Open Unity3D (or restart it if it was opened while the dll was being copied)
4. Open `Assets\Scenes\SampleScene`
5. Navigate to the `Tester` Game Object
6. Enable the Tester script you wish to execute
7. Play the scene and check the logs for result

## References
[**InterOp Marshaling**](https://docs.microsoft.com/en-us/dotnet/framework/interop/interop-marshaling?view=netframework-4.8) Official MS Documentation

[**Marshaling with C#**](https://www.codeproject.com/Articles/66245/Marshaling-with-Csharp-Chapter-1-Introducing-Marsh.aspx) Old reference but has good explanation on BOOL vs BOOLEAN

## Want to help?
I need help in testing this for macOS. I don't have an Apple machine.
