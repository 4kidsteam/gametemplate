using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringUtils
{
    public static string ToString(string[] values)
    {
        string str = "";
        for (int i = 0; i < values.Length; i++)
        {
            str += values[i];
        }
        return str;
    }

    public static string ToString(int[] values)
    {
        string str = "";
        for (int i = 0; i < values.Length; i++)
        {
            str += string.Format("{0}, ", values[i]);
        }
        return str;
    }
}
