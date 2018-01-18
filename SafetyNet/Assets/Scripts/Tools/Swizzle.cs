using UnityEngine;
using System.Collections;

public static class Swizzle
{
    public static Vector2 vector2(this Vector3 me)
    {
        return new Vector2(me.x, me.y);
    }

    public static Vector2 vector2(this Vector4 me)
    {
        return new Vector2(me.x, me.y);
    }

    public static Vector3 vector3(this Vector4 me)
    {
        return new Vector3(me.x, me.y, me.z);
    }

    public static Vector4 vector4(this Vector3 me, float w)
    {
        return new Vector4(me.x, me.y, me.z, w);
    }
}
