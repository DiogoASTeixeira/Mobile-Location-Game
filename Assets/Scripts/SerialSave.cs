using UnityEngine;

public class SerialSave : MonoBehaviour
{
    public void Save()
    {
        GameControl.control.SaveGame();
    }

    public void Load()
    {
        GameControl.control.LoadGame();
    }

    public void ResetData()
    {
        GameControl.control.ResetGame();
    }
}