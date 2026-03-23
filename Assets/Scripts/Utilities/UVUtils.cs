using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UVUtils
{
    public static Vector4[] ConvertVector2ToVector4(Vector2[] source)
    {
        if (source == null) return null;

        Vector4[] result = new Vector4[source.Length];

        for (int i = 0; i < source.Length; i++)
        {
            result[i] = new Vector4(source[i].x, source[i].y, 0f, 0f);
        }

        return result;
    }

    public static Vector2[] ConvertVector4ToVector2(Vector4[] source)
    {
        if (source == null) return null;

        Vector2[] result = new Vector2[source.Length];

        for (int i = 0; i < source.Length; i++)
        {
            result[i] = new Vector2(source[i].x, source[i].y);
        }

        return result;
    }
}
