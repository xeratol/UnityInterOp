using System.Runtime.InteropServices;
using UnityEngine;

public class PluginInterop : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(NativeLib.GetBool() ? "True" : "False");
    }
}
