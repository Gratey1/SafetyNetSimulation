using System.Collections;
using System.Collections.Generic;

public static class ListExtended
{
    /// <summary>
    /// Creates a shallow copy of the list.
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <param name="_list">List to be copied</param>
    /// <returns>Shallow copy of the list</returns>
    public static List<T> ShallowCopy<T>(this List<T> _list)
    {
        List<T> _l = new List<T>();
        for(int i = 0; i < _list.Count; i++)
        {
            _l.Add(_list[i]);
        }

        return _l;
    }

    /// <summary>
    /// Removes the first element from the list and places it as the last element.
    /// </summary>
    /// <typeparam name="T">List Type</typeparam>
    /// <param name="_list">Self</param>
    public static void MoveFirstItemToLast<T>(this List<T> _list)
    {
        if (_list == null || _list.Count < 2) return;

        var _item = _list[0];
        _list.RemoveAt(0);
        _list.Add(_item);
    }

    /// <summary>
    /// Removes the last element from the list and places it as the first element.
    /// </summary>
    /// <typeparam name="T">List Type</typeparam>
    /// <param name="_list">Self</param>
    public static void MoveLastItemToFirst<T>(this List<T> _list)
    {
        if (_list == null || _list.Count < 2) return;

        int _i = _list.Count - 1;
        var _item = _list[_i];
        _list.RemoveAt(_i);
        _list.Insert(0, _item);
    }

    /// <summary>
    /// Adds elemtents from a variable number of arrays to the list.
    /// </summary>
    /// <typeparam name="T">List Type</typeparam>
    /// <param name="_list">Self</param>
    /// <param name="_arrays">Arrays whose elements are to be added to the list.</param>
    public static void AddArrays<T>(this List<T> _list, params T[][] _arrays)
    {
        if (_arrays != null)
        {
            for (int _i = 0; _i < _arrays.Length; _i++)
            {
                var _array = _arrays[_i];
                if (_array != null)
                {
                    for (int _j = 0; _j < _array.Length; _j++)
                    {
                        var _element = _array[_j];
                        _list.Add(_element);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Removes all elements in a variable number of arrays from the list.
    /// </summary>
    /// <typeparam name="T">List Type</typeparam>
    /// <param name="_list">Self</param>
    /// <param name="_arrays">Arrays whose elements are to be removed from the list.</param>
    public static void RemoveArrays<T>(this List<T> _list, params T[][] _arrays)
    {
        if (_arrays != null)
        {
            for (int _i = 0; _i < _arrays.Length; _i++)
            {
                var _array = _arrays[_i];
                if (_array != null)
                {
                    for (int _j = 0; _j < _array.Length; _j++)
                    {
                        var _element = _array[_j];
                        _list.Remove(_element);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Adds elements from a variable number of lists to the list.
    /// </summary>
    /// <typeparam name="T">List Type</typeparam>
    /// <param name="_list">Self</param>
    /// <param name="_lists">Lists whose elements are to be added to the list.</param>
    public static void AddLists<T>(this List<T> _list, params List<T>[] _lists)
    {
        if (_lists != null)
        {
            for (int _i = 0; _i < _lists.Length; _i++)
            {
                var _list2 = _lists[_i];
                if (_list2 != null)
                {
                    for (int _j = 0; _j < _list2.Count; _j++)
                    {
                        var _element = _list2[_j];
                        _list.Add(_element);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Removes elements in a variable number of lists from the list.
    /// </summary>
    /// <typeparam name="T">List Type</typeparam>
    /// <param name="_list">Self</param>
    /// <param name="_lists">Lists whose elements are to be removed to the list.</param>
    public static void RemoveLists<T>(this List<T> _list, params List<T>[] _lists)
    {
        if (_lists != null)
        {
            for (int _i = 0; _i < _lists.Length; _i++)
            {
                var _list2 = _lists[_i];
                if (_list2 != null)
                {
                    for (int _j = 0; _j < _list2.Count; _j++)
                    {
                        var _element = _list2[_j];
                        _list.Remove(_element);
                    }
                }
            }
        }
    }
}
