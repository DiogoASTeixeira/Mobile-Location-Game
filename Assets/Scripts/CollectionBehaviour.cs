using UnityEngine;
using UnityEngine.UI;

public class CollectionBehaviour : MonoBehaviour
{
    public GameObject[] LockedImage;
    public Button[] LeafButtons;

    private void Start()
    {
        GameControl control = GameControl.control;
        Leaf[] leaves = control.Leaves;
        for (short i = 0; i < leaves.Length; i++)
        {
            if (leaves[i].IsLeafFound())
            {
                LockedImage[i].SetActive(false);
                LeafButtons[i].interactable = true;
            }
            else
            {
                LockedImage[i].SetActive(true);
                LeafButtons[i].interactable = false;
            }
        }
    }
}
