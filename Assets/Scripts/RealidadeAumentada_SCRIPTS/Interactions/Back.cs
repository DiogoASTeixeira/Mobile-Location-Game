using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Back : MonoBehaviour
{


    [SerializeField]
    string sceneName;

    string[] files = null;

    void Start()
    {
        files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
        
    }

    void GetPictureAndDeleteIt()
    {
        string pathToFile = files[0];
        DeleteFile(pathToFile);
    }

    void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public void BackScene()
    {
        if (files.Length > 0)
        {
            GetPictureAndDeleteIt();
        }
        SceneManager.LoadScene(sceneName);
    }

   
}
