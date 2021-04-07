using UnityEngine;

public class GameControl : MonoBehaviour
{
    // Static Reference
    public static GameControl control;

    // data to persist between scenes
    public Leaf[] Leaves;
    public string words;
    public int nLeaves;


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

     void Start()
    {
        CreateLeaves();
    }

    private void CreateLeaves()
    {
        Leaves = new Leaf[leafStruct.Length];

        int i = 0;
        Debug.Log(Leaves[i]);
        Debug.Log(leafStruct[i]);

        for ( i = 0; i < leafStruct.Length; i++)
        {
            Leaves[i] = new Leaf(leafStruct[i].treeCoordinates);
            Debug.Log(Leaves[i]);
            Debug.Log(leafStruct[i]);
            DontDestroyOnLoad(Leaves[i].gameObject);
        }
    }

    public int NumberOfFoundLeaves()
    {
        int n = 0;
        for(int i = 0; i < Leaves.Length; i++)
        {
            Debug.Log(Leaves[i]);
            Debug.Log(i);
            if (Leaves[i].IsLeafFound()) n++;
        }
        return n;
    }
}
