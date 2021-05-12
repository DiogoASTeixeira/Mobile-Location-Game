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
        //bordo
        if (control.bordo_found == true)
        {
            LockedImage[0].SetActive(false);
            LeafButtons[0].interactable = true;

        }
        else
        {
            LockedImage[0].SetActive(true);
            LeafButtons[0].interactable = false;
        }
        //eugenia
        if (control.eugenia_found == true)
        {
            LockedImage[1].SetActive(false);
            LeafButtons[1].interactable = true;
        }
        else
        {
            LockedImage[1].SetActive(true);
            LeafButtons[1].interactable = false;
        }
        //carvalho
        if (control.carvalho_found == true)
        {
            LockedImage[2].SetActive(false);
            LeafButtons[2].interactable = true;
        }
        else
        {
            LockedImage[2].SetActive(true);
            LeafButtons[2].interactable = false;
        }
        //pilriteiro
        if (control.pilriteiro_found == true)
        {
            LockedImage[3].SetActive(false);
            LeafButtons[3].interactable = true;
        }
        else
        {
            LockedImage[3].SetActive(true);
            LeafButtons[3].interactable = false;
        }
        //ambar
        if (control.ambar_found == true)
        {
            LockedImage[4].SetActive(false);
            LeafButtons[4].interactable = true;
        }
        else
        {
            LockedImage[4].SetActive(true);
            LeafButtons[4].interactable = false;
        }
        //azevinho
        if (control.azevinho_found == true)
        {
            LockedImage[5].SetActive(false);
            LeafButtons[5].interactable = true;
        }
        else
        {
            LockedImage[5].SetActive(true);
            LeafButtons[5].interactable = false;
        }
    }
}
