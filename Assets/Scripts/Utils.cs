using System.Collections;
using UnityEngine;

public class Utils
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
}
