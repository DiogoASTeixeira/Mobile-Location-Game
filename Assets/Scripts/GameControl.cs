using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    // Static Reference
    public static GameControl control;
    public GameObject NavBar;

    // data to persist between scenes
    [HideInInspector]
    public Leaf[] Leaves;


    
    public bool seen_intro;
    public bool seen_tutorial;
    public bool load_finished;
    public bool bordo_found;
    public bool pilriteiro_found;
    public bool azevinho_found;
    public bool ambar_found;
    public bool carvalho_found;
    public bool eugenia_found;
    

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

    private string GetSupposedScene()
    {
        GPSLocation gps = GPSLocation.Instance;
        // coordinates that surround Jardim Botanico
        MyVector2 a_coords = new MyVector2(41.15434233891927, -8.643349215167602),
            b_coords = new MyVector2(41.153878879571764, -8.641774344324698),
            c_coords = new MyVector2(41.15307068904232, -8.643918733955989),
            d_coords = new MyVector2(41.15200534835579, -8.641775566711743);

        MyVector2 self_coords = new MyVector2(gps.selfLatitude, gps.selfLongitude);

        // the sum of all triangles with self coords as one of the points of the triangle 
        double sum = getTriangleArea(a_coords, b_coords, self_coords)
         + getTriangleArea(b_coords, c_coords, self_coords)
         + getTriangleArea(c_coords, d_coords, self_coords)
         + getTriangleArea(d_coords, a_coords, self_coords);


        return "";
    }

    private double getTriangleArea(MyVector2 point_A, MyVector2 point_B, MyVector2 point_C)
    {
        double l1 = Math.Sqrt(Math.Pow((point_A.x - point_B.x), 2) + Math.Pow((point_A.y - point_B.y), 2));
        double l2 = Math.Sqrt(Math.Pow((point_B.x - point_C.x), 2) + Math.Pow((point_B.y - point_C.y), 2));
        double l3 = Math.Sqrt(Math.Pow((point_C.x - point_A.x), 2) + Math.Pow((point_C.y - point_A.y), 2));

        return 0.0f;
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
            data.SeenIntro = seen_intro;
            data.SeenTutorial = seen_tutorial;
            data.foundBordo = bordo_found;
            data.foundpilriteiro = pilriteiro_found;
            data.foundAzevinho = azevinho_found;
            data.foundCarvalho = carvalho_found;
            data.foundEugenia = eugenia_found;
            data.foundAmbar = ambar_found;

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
                

                // Debug.Log(i + " " + data.savedFoundInside[i] + " " + data.savedFoundOutside[i]);
                 Debug.Log(data.foundBordo);
            }
            if (data.SeenIntro) seen_intro = true;
            if (data.foundBordo) bordo_found = true;
            if (data.foundpilriteiro) pilriteiro_found = true;
            if (data.foundAzevinho) azevinho_found = true;
            if (data.foundCarvalho) carvalho_found = true;
            if (data.foundEugenia) eugenia_found = true;
            if (data.foundAmbar) ambar_found = true;
            if (data.SeenTutorial) seen_tutorial = true;

            Debug.Log("Loaded Data.");
            load_finished = true;
        }
        else
        {
            Debug.LogWarning("No save data.");
            load_finished = true;
        }
    }

    public bool HasSeenIntro() => seen_intro;
    public void SeenIntro() => seen_intro = true;
}

[System.Serializable]
class SaveData
{
    public static readonly string FILE_NAME = "FloRA.dat";
    public bool[] savedFoundInside;
    public bool[] savedFoundOutside;
    public bool SeenIntro;
    public bool SeenTutorial;
    public bool foundBordo;
    public bool foundpilriteiro;
    public bool foundAzevinho;
    public bool foundCarvalho;
    public bool foundEugenia;
    public bool foundAmbar;

    public SaveData(int length)
    {
        savedFoundInside = new bool[length];
        savedFoundOutside = new bool[length];
    }
}
