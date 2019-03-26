#define DllExport __declspec (dllexport)

#ifdef __cplusplus
// Use C-based naming convention - No name nangling (no overloading)
extern "C"
{
#endif

    typedef int (*SimpleCallback)(void);

    DllExport int ExecuteCallback(SimpleCallback callback)
    {
        return callback();
    }

    // TODO Store callback for later
    // TODO Array of callbacks
    // TODO Struct with callbacks
    // TODO Callbacks with simple data type parameters
    // TODO Callbacks with arrays of simple data type parameters
    // TODO Callbacks with string parameters
    // TODO Callbacks with struct parameters

#ifdef __cplusplus
}
#endif
