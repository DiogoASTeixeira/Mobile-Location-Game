using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using Vuforia;

public class InitialEmptyScript : MonoBehaviour
{
    public GameObject panel;
    public Vuforia.VuforiaBehaviour vuforia;

    private static bool clicked_request_btn = false;
    private static bool started = false;
    void Awake()
    {
       
    }

  
    private void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera) || !Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            panel.SetActive(true);
        }
    }
    private void Update()
    {
        if (!started && Permission.HasUserAuthorizedPermission(Permission.Camera) && Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            started = true;
            LateStart();
        }
        StartCoroutine(WaitCoroutine());
    }

    public void RequestCameraPermission()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera) || !Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            string[] permissions = [Permission.Camera, Permission.FineLocation];
            Permission.RequestUserPermissions(permissions);

            clicked_request_btn = true;
        }
    }

    private void LateStart()
    {

        //vuforia.enabled = true;
        while (GameControl.control == null)
        {
            StartCoroutine(WaitCoroutine());

        }

        GameControl control = GameControl.control;
        control.NavBar.SetActive(false);
        control.load_finished = false;
        control.LoadGame();
        VuforiaRuntime.Instance.InitVuforia();

        while (!control.load_finished)
        {
            StartCoroutine(WaitCoroutine());
        }

        if (!control.HasSeenIntro())
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene("Intro2");
            control.SeenIntro();

        }
        else
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
            control.CheckSceneTransition();

        }
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
    }

    public void CloseApplication()
    {
        Application.Quit();
    }
}
