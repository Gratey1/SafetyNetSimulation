using System;
using UnityEngine;

public static class EnumExtended
{
    public static E GetRandomEnum<E>()
    {
        if (!typeof(E).IsEnum)
            return default(E);

        Array _values = Enum.GetValues(typeof(E));
        int _index = UnityEngine.Random.Range(0, _values.Length);
        return (E)_values.GetValue(_index);
    }
}
