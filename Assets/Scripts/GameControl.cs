using UnityEngine;

public class GameControl : MonoBehaviour
{
    // Static Reference
    public static GameControl control;
    public Leaves Leaves;

    // data to persist between scenes

    private void Awake()
    {
        //Let the gameobject persist over the scenes
        DontDestroyOnLoad(gameObject);

        Leaves = new Leaves();

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
}

public class Leaves
{
    private const int NUMBER_OF_LEAVES = 2;
    private readonly bool[] Found;
    public Leaves()
    {
        Found = new bool[NUMBER_OF_LEAVES];
        for (int i = 0; i < NUMBER_OF_LEAVES; i++)
            Found[i] = false;
    }

    public void SetFoundLeaf(int leafNumber)
    {
        Found[leafNumber] = true;
    }

    public void DebugPrint()
    {
        for (int i = 0; i < NUMBER_OF_LEAVES; i++)
            Debug.Log("Leaf " + i + " : " + Found[i]);
    }
}