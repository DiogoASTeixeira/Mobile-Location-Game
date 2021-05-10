using System.Collections;
using UnityEngine;

public class InitialEmptyScript : MonoBehaviour
{
    private void Awake()
    {
        while (GameControl.control == null)
        {
            StartCoroutine(WaitCoroutine());
        }
        GameControl control = GameControl.control;
        control.load_finished = false;
        control.LoadGame();

        while (!control.load_finished)
        {
            StartCoroutine(WaitCoroutine());
        }

        if (!control.HasSeenIntro())
        {
            control.SeenIntro();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Intro2");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
            control.CheckSceneTransition();
        }
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
    }

}
