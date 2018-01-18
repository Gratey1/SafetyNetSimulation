using System.Collections;
using System.Collections.Generic;

public static class DictionaryExtended
{
    public static List<T> GetKeyList<T, U>(this Dictionary<T, U> _dict)
    {
        T[] _keys = new T[_dict.Count];
        _dict.Keys.CopyTo(_keys, 0);
        return new List<T>(_keys);
    }

    public static List<U> GetValueList<T, U>(this Dictionary<T, U> _dict)
    {
        U[] _values = new U[_dict.Count];
        _dict.Values.CopyTo(_values, 0);
        return new List<U>(_values);
    }

    public static Dictionary<T, U> GetSubDictionaryFromKeys<T, U>(this Dictionary<T, U> _dict, T[] _keys)
    {
        Dictionary<T, U> _ret = new Dictionary<T, U>();
        if(_keys != null)
        {
            for(int i = 0; i < _keys.Length; i++)
            {
                T _key = _keys[i];
                if(_dict.ContainsKey(_key))
                {
                    _ret.Add(_key, _dict[_key]);
                }
            }
        }

        return _ret;
    }

    public static void AddOrReplace<T, U>(this Dictionary<T, U> _dict, T _key, U _value)
    {
        if(_dict.ContainsKey(_key))
        {
            _dict[_key] = _value;
        }
        else
        {
            _dict.Add(_key, _value);
        }
    }
}
