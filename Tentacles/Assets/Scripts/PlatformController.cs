using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlatformController 
{
    public static bool IsAndroidPlatform {
        get {
#if UNITY_ANDROID
            return true;
#endif
            return false;
        }
    }

    public static bool IsInEditor {
        get {
#if UNITY_EDITOR
            return true;
#endif
            return false;
        }
    }
}
