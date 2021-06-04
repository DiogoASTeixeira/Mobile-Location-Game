using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    // Static Reference
    public static GameControl control;
    public GameObject NavBar;
    public Button RA;
    public GameObject RALockedToolTip;

    // data to persist between scenes
    [HideInInspector]
    public Leaf[] Leaves;

    
    public bool seen_intro;
    public bool seen_tutorial;
    public bool load_finished;
    public GameObject continueGarden;
    /*
    public bool bordo_found;
    public bool pilriteiro_found;
    public bool azevinho_found;
    public bool ambar_found;
    public bool carvalho_found;
    public bool eugenia_found;
    
    //Lista das folhas encontradas lá fora
    [Serializable]
    public struct FoundTreeOutside
    {
        public bool bordo_foundOutside;
        public bool pilriteiro_foundOutside;
        public bool azevinho_foundOutside;
        public bool ambar_foundOutside;
        public bool carvalho_foundOutside;
        public bool eugenia_foundOutside;
    }
    
    [SerializeField]
    public FoundTreeOutside TreeOutside;
    */
    //Leaf Struct to allow settings values in Unity Editor
    public Leaf.LeafStruct[] leafStruct;
    public PointCounter PointCounter;

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
    
    private void Update()
    {

        foreach (Leaf leaf in Leaves)
            if (NumberOfFoundTrees() >= 1)
            {
                
                RA.interactable = true;
                RALockedToolTip.SetActive(false);
                

            }
            else

            {
                RA.interactable = false;
                RALockedToolTip.SetActive(true);

            }

        if (NumberOfFoundTrees() >= 1 || NumberOfFoundLeaves() >= 1)
        {
            //continueGarden.SetActive(false);
        }
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
    public int NumberOfFoundTrees()
    {
        int n = 0;
        for(int i = 0; i < Leaves.Length; i++)
        {
            if (Leaves[i].IsTreeFound()) n++;
        }
        return n;
    }

    public bool HasFoundAllLeaves()
    {
        foreach(Leaf leaf in Leaves)
            if (!leaf.IsLeafFound()) return false;
        return true;
    }

    public bool HasFoundAllTrees()
    {
        foreach (Leaf leaf in Leaves)
            if (!leaf.IsTreeFound()) return false;
        return true;
    }
    
    

    //TODO prepare localisation with GPS and BT
    public void CheckSceneTransition()
    {
        // string s = GetSupposedScene();
         string s = SceneManager.GetActiveScene().name;
        //SceneManager.LoadScene("Inside");
        if (s != SceneManager.GetActiveScene().name)
        {
            //SceneManager.LoadScene(s);
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
        FileStream file = File.Create(SaveData.FILE_PATH);
        Leaf[] leaves = control.Leaves;
        SaveData data = new SaveData(leaves.Length);
        data.SeenIntro = seen_intro;
        data.SeenTutorial = seen_tutorial;
        for (short i = 0; i < leaves.Length; i++)
        {
            data.savedFoundInside[i] = leaves[i].IsLeafFound();
            data.savedFoundOutside[i] = leaves[i].IsTreeFound();

            /*
            data.foundBordo = bordo_found;
            data.foundpilriteiro = pilriteiro_found;
            data.foundAzevinho = azevinho_found;
            data.foundCarvalho = carvalho_found;
            data.foundEugenia = eugenia_found;
            data.foundAmbar = ambar_found;

            data.foundBordoOutside = TreeOutside.bordo_foundOutside;
            data.foundpilriteiroOutside = TreeOutside.pilriteiro_foundOutside;
            data.foundAzevinhoOutside = TreeOutside.azevinho_foundOutside;
            data.foundCarvalhoOutside = TreeOutside.carvalho_foundOutside;
            data.foundEugeniaOutside = TreeOutside.eugenia_foundOutside;
            data.foundAmbarOutside = TreeOutside.ambar_foundOutside;
            */
        }
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Saved Game Data: " + SaveData.FILE_PATH);
    }

    public void LoadGame()
    {
        if (File.Exists(SaveData.FILE_PATH))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(SaveData.FILE_PATH, FileMode.Open);

            SaveData data = new SaveData(control.Leaves.Length);
            data = (SaveData)bf.Deserialize(file);
            file.Close();
            for (short i = 0; i < control.Leaves.Length; i++)
            {
                if (data.savedFoundInside[i])
                    control.Leaves[i].FoundLeaf();
                if (data.savedFoundOutside[i])
                    control.Leaves[i].FoundTree();
            }
            
            if (data.SeenIntro) seen_intro = true;
            if (data.SeenTutorial) seen_tutorial = true;
            /*
            if (data.foundBordo) bordo_found = true;
            if (data.foundpilriteiro) pilriteiro_found = true;
            if (data.foundAzevinho) azevinho_found = true;
            if (data.foundCarvalho) carvalho_found = true;
            if (data.foundEugenia) eugenia_found = true;
            if (data.foundAmbar) ambar_found = true;

            if (data.foundBordoOutside) TreeOutside.bordo_foundOutside = true;
            if (data.foundpilriteiroOutside) TreeOutside.pilriteiro_foundOutside = true;
            if (data.foundAzevinhoOutside) TreeOutside.azevinho_foundOutside = true;
            if (data.foundCarvalhoOutside) TreeOutside.carvalho_foundOutside = true;
            if (data.foundEugeniaOutside) TreeOutside.eugenia_foundOutside = true;
            if (data.foundAmbarOutside) TreeOutside.ambar_foundOutside = true;
            */

            Debug.Log("Loaded Data from: " + SaveData.FILE_PATH);
        }
        else
        {
            Debug.LogWarning("No save data on: " + SaveData.FILE_PATH);
        }
        load_finished = true;
    }

    public void ResetGame()
    {
            if (File.Exists(SaveData.FILE_PATH))
            {
                File.Delete(SaveData.FILE_PATH);
                for (short i = 0; i < control.Leaves.Length; i++)
                {
                        control.Leaves[i].ResetLeaf();

                }
                Debug.Log("Successfully Reset Data.");
            }
            else
                Debug.LogWarning("No save data found to delete.");
    }

    public bool HasSeenIntro() => seen_intro;
    public void SeenIntro() => seen_intro = true;
}

[System.Serializable]
class SaveData
{
    public static readonly string FILE_PATH = Application.persistentDataPath + "/FloRA.dat";
    public bool[] savedFoundInside;
    public bool[] savedFoundOutside;
    public bool SeenIntro;
    public bool SeenTutorial;

    /*
    public bool foundBordo;
    public bool foundpilriteiro;
    public bool foundAzevinho;
    public bool foundCarvalho;
    public bool foundEugenia;
    public bool foundAmbar;
    public bool foundBordoOutside;
    public bool foundpilriteiroOutside;
    public bool foundAzevinhoOutside;
    public bool foundCarvalhoOutside;
    public bool foundEugeniaOutside;
    public bool foundAmbarOutside;
    */

    public SaveData(int length)
    {
        savedFoundInside = new bool[length];
        savedFoundOutside = new bool[length];
    }
}
