using System.Collections;
using UnityEngine;

public class InitialEmptyScript : MonoBehaviour
{
    private void Awake()
    {
        do
        {
            StartCoroutine(WaitCoroutine());
        } while (GameControl.control == null);
        GameControl.control.LoadGame();
        GameControl.control.CheckSceneTransition();
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
    }

}
