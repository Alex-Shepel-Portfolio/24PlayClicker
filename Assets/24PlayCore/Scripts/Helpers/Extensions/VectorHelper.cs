using UnityEngine;

public static class VectorHelper
{
    /// <summary>
    /// Возвращает true если расстояние между векторами меньше заданного
    /// </summary>
    /// <param name="firstVector"> Первый вектор</param>
    /// <param name="secondVector"> Второй вектор</param>
    /// <param name="distance"> Расстояние</param>
    /// <returns></returns>
    public static bool InRange(Vector3 firstVector, Vector3 secondVector, float distance)
    {
        return SqrDistance(firstVector, secondVector) < distance * distance;
    }

    /// <summary>
    /// Возвращает квадрат расстояния между векторами
    /// </summary>
    /// <param name="firstVector"> Первый вектор</param>
    /// <param name="secondVector"> Второй вектор</param>
    /// <returns></returns>
    /// 
    public static float SqrDistance(Vector3 firstVector, Vector3 secondVector)
    {
        return (firstVector - secondVector).sqrMagnitude;
    }

    /// <summary>
    /// Возвращает направление от одного вектора к другому
    /// </summary>
    /// <param name="from"> Вектор от которого считается направление</param>
    /// <param name="to"> Вектор к которому считается направление</param>
    /// <returns></returns>
    public static Vector3 Direction(Vector3 from, Vector3 to)
    {
        return to - from;
    }

    /// <summary>
    /// Возвращает true если расстояние между векторами меньше заданного
    /// </summary>
    /// <param name="firstVector"> Первый вектор</param>
    /// <param name="secondVector"> Второй вектор</param>
    /// <param name="distance"> Расстояние</param>
    /// <returns></returns>
    public static bool InRange(Vector2 firstVector, Vector2 secondVector, float distance)
    {
        return SqrDistance(firstVector, secondVector) < distance * distance;
    }

    /// <summary>
    /// Возвращает квадрат расстояния между векторами
    /// </summary>
    /// <param name="firstVector"> Первый вектор</param>
    /// <param name="secondVector"> Второй вектор</param>
    /// <returns></returns>
    public static float SqrDistance(Vector2 firstVector, Vector2 secondVector)
    {
        return (firstVector - secondVector).sqrMagnitude;
    }

    /// <summary>
    /// Возвращает направление от одного вектора к другому
    /// </summary>
    /// <param name="from"> Вектор от которого считается направление</param>
    /// <param name="to"> Вектор к которому считается направление</param>
    /// <returns></returns>
    public static Vector2 Direction(Vector2 from, Vector2 to)
    {
        return to - from;
    }
}