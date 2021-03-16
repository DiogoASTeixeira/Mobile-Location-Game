public class GameControl : UnityEngine.MonoBehaviour
{
    // Static Reference
    public static GameControl control;

    // datat to persist between scenes
    public float info;

    private void Awake()
    {
        //Let the gameobject persist over the scenes
        DontDestroyOnLoad(gameObject);

        //Check if the control instance is null
        if(control == null)
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
