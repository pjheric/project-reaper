using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockPlayerManager : MonoBehaviour
{
    public static bool morriganLock;
    public static bool ganglimLock;
    public static void toggleMorriganLock()
    {
        morriganLock = !morriganLock;
    }
    public static void toggleGanglimLock()
    {
        ganglimLock = !ganglimLock;
    }
}
