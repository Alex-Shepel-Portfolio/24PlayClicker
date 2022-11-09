using UnityEngine;

public static partial class Extensions
{
    /// <summary>Линейное преобразование значения в новый диапазон</summary>
    /// <param name="oldMin">Старое минимальное значение</param>
    /// <param name="oldMax">Старое максимальное значение</param>
    /// <param name="newMin">Новое минимальное значение</param>
    /// <param name="newMax">Новое максимальное значение</param>
    public static int Remap(this int value, int oldMin, int oldMax, int newMin, int newMax)
    {
        float t = Mathf.InverseLerp(oldMin, oldMax, value);
        value = (int)Mathf.Lerp(newMin, newMax, t);
        return value;
    }
    
    /// <summary>Линейное преобразование значения в новый диапазон</summary>
    /// <param name="oldMin">Старое минимальное значение</param>
    /// <param name="oldMax">Старое максимальное значение</param>
    /// <param name="newMin">Новое минимальное значение</param>
    /// <param name="newMax">Новое максимальное значение</param>
    public static float Remap(this float value, float oldMin, float oldMax, float newMin, float newMax)
    {
        float t = Mathf.InverseLerp(oldMin, oldMax, value);
        value = Mathf.Lerp(newMin, newMax, t);
        return value;
    }
}