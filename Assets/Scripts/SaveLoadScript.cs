using UnityEngine;

public class SaveLoadScript: MonoBehaviour
{

    public void SaveGame()
    {
        GameControl.control.SaveGame();
    }

    public void LoadGame()
    {
        GameControl.control.LoadGame();
    }
}
