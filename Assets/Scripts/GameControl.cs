using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    // Static Reference
    public static GameControl control;

    // data to persist between scenes
    [HideInInspector]
    public Leaf[] Leaves;

    //Leaf Struct to allow settings values in Unity Editor
    public Leaf.LeafStruct[] leafStruct;

    private void Awake()
    {
        //Let the gameobject persist over the scenes
        DontDestroyOnLoad(gameObject);

        //Check if the control instance is null
        if (control == null)
        {
            //This instance becomes the single instance available
            control = this;
        }
        else if( control != this)
        {
            //In case there is a different instance destroy this one.
            Destroy(gameObject);
        }
    }

     private void Start()
    {
        CreateLeaves();
    }

    private void CreateLeaves()
    {
        Leaves = new Leaf[leafStruct.Length];

        for ( int i = 0; i < leafStruct.Length; i++)
        {
            Leaves[i] = new Leaf(leafStruct[i]);
        }
    }

    public int NumberOfFoundLeaves()
    {
        int n = 0;
        for(int i = 0; i < Leaves.Length; i++)
        {
            if (Leaves[i].IsLeafFound()) n++;
        }
        return n;
    }

    public bool HasFoundAllLeaves()
    {
        foreach(Leaf leaf in Leaves)
            if (!leaf.IsLeafFound()) return false;
        return true;
    }

    //TODO prepare localisation with GPS and BT
    public void CheckSceneTransition()
    {
        // string s = GetSupposedScene();
         string s = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Menu");
        if (s != SceneManager.GetActiveScene().name)
        {
            SceneManager.LoadScene(s);
        }
    }


    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + SaveData.FILE_NAME);
        Leaf[] leaves = control.Leaves;
        SaveData data = new SaveData(leaves.Length);
        for(short i = 0; i < leaves.Length; i++)
        {
            data.savedFoundInside[i] = leaves[i].IsLeafFound();
            data.savedFoundOutside[i] = leaves[i].IsTreeFound();
        }
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Saved Game Data: " + Application.persistentDataPath + "/" + SaveData.FILE_NAME);
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/" + SaveData.FILE_NAME))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + SaveData.FILE_NAME, FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            Leaf[] leaves = control.Leaves;
            for (short i = 0; i < leaves.Length; i++)
            {
                if (data.savedFoundInside[i]) leaves[i].FoundLeaf();
                if (data.savedFoundOutside[i]) leaves[i].FoundTree();

                Debug.Log(i + " " + data.savedFoundInside[i] + " " + data.savedFoundOutside[i]);
            }
            Debug.Log("Loaded Data.");
        }
        else Debug.LogWarning("No save data.");
    }
}

[System.Serializable]
class SaveData
{
    public static readonly string FILE_NAME = "FloRA.dat";
    public bool[] savedFoundInside;
    public bool[] savedFoundOutside;

    public SaveData(int length)
    {
        savedFoundInside = new bool[6];
        savedFoundOutside = new bool[6];
    }
}
