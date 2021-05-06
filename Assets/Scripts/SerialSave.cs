using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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
}