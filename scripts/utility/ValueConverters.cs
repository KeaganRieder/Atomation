namespace  Atomation;

using System;

/// <summary>
/// class which contains methods used to convert between value type
/// </summary>
public static class TypeConverter
{
    public static int ToInt(string toConvert){
        int val = 0;
        foreach (char letter in toConvert)
        {
            var num = letter + 0;
            val += num;
        }

        return val;
    }
}