using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneBehaviour : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Debug.Log("Changing Scene to " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
