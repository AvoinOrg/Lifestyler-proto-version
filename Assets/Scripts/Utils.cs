using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    /// <summary>
    /// Incease index by one
    /// </summary>
    /// <param name="current">Current value for int</param>
    /// <param name="max">Exclusive max value for int</param>
    /// <returns>Value increased by one or swapped to zero</returns>
    public static int IncreaseIndex(int current, int max)
    {
        current++;
        if (current > max)
        {
            current = 0;
        }
        return current;
    }

    public static Vector3 GetTouchWorldPos(int touchIndex = 0)
    {
        return Camera.main.ScreenToWorldPoint(Input.GetTouch(touchIndex).position);
    }
}
