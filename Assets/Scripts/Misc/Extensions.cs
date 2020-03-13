using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T GetLast<T>(this List<T> list)
    {
        return list[list.Count - 1];
    }

    public static Vector3 DivideWithDivisor(this Vector3 v, float divisor)
    {
        return new Vector3(v.x == 0 ? 0 : (divisor / v.x), v.y == 0 ? 0 : (divisor / v.y), v.z == 0 ? 0 : (divisor / v.z));
    }

    public static Vector3 MagnetForce(this Vector3 v, float divisor)
    {
        Vector3 normalized = v.normalized;
        return new Vector3(v.x < 1 ? normalized.x : (divisor / v.x), v.y < 1 ? normalized.y : (divisor / v.y), v.z < 1 ? normalized.z : (divisor / v.z));
    }

    public static Vector2 ToVector2(this Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    public static Vector3 ToVector3(this Vector2 v)
    {
        return new Vector3(v.x, v.y);
    }

    public static void RemoveNullAndThis<T>(this List<T> l, T t)
    {
        List<T> tempL = new List<T>(l);
        foreach (T component in tempL)
        {
            if (component == null || component.Equals(t))
            {
                l.Remove(t);
                break;
            }
        }
    }

    public static void RemoveNull<T>(this List<T> l)
    {
        List<T> tempL = new List<T>(l);
        foreach (T component in tempL)
        {
            if (component == null)
            {
                l.Remove(component);
                break;
            }
        }
    }

    //public static void TryRemove<T>(this List<T> l, T t)
    //{
    //    foreach (T component in l)
    //        if (component == t)
    //        {

    //        }

    //}
}
