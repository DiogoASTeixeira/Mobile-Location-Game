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
    private void CheckSceneTransition()
    {
        // string s = GetSupposedScene();
        string s = "";
        if(s != SceneManager.GetActiveScene().name)
        {
            SceneManager.LoadScene(s);
        }
    }

}
