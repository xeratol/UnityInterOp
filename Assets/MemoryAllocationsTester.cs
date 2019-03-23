using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class MemoryAllocationsTester : MonoBehaviour
{
    [Tooltip("Turn this off to simulate unmanaged memory leak per frame")]
    public bool release = true;

    [Tooltip("Number of bytes to allocate per frame")]
    public int sizeToAllocate = 1024 * 1024;

    private IntPtr _ptr = IntPtr.Zero;

    void Update()
    {
        _ptr = Marshal.AllocHGlobal(sizeToAllocate);

        if (release)
        {
            Marshal.FreeHGlobal(_ptr);
        }
    }
}
