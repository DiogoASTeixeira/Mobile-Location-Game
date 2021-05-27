using System.Collections;
using UnityEngine;

public static class Utils
{
    public delegate void WaitDelegate();

    /**
     * Waits secondsToWait seconds and afterwards calls method store in methodsToCall
     * **/
    public static IEnumerator WaitAndCall(int secondsToWait, WaitDelegate methodToCall)
    {
        Debug.LogWarning("WAITING AND CALLING");
        yield return new WaitForSeconds(secondsToWait);
        methodToCall();
    }

    public static void SetLeft(this RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }

    public static void SetRight(this RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }

    public static void SetTop(this RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }

    public static void SetBottom(this RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }
}
