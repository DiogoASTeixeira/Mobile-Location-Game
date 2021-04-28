using UnityEngine;

public class ToggleBehaviour : MonoBehaviour
{
    private UnityEngine.UI.Toggle toggle;
    public GameObject SceneController;
    public InsideController.Difficulty difficultyValue;

    public void ToggleValueChanged(bool isOn)
    {
        if (isOn) {
            InsideController controller = SceneController.GetComponent<InsideController>();
            controller.SetDifficulty(difficultyValue);
        }
    }
}
