using UnityEngine;
using System.Collections;

public static class MonoBehaviourExtended
{
    
}

public static class ObjectExtended
{
    public static void SafeDestroy(this Object mb, Object obj)
    {
        if (Application.isEditor)
        {
            Object.DestroyImmediate(obj);
        }
        else
        {
            Object.Destroy(obj);
        }
    }
}