using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectionBehaviour : MonoBehaviour
{
    public GameObject[] LockedImage;
    public Button[] LeafButtons;
    public GameObject[] FoundInsideIndicator;
    public GameObject[] FoundOutsideIndicator;
    public TextMeshProUGUI[] treeCounterText;
    public Image[] counterColor;


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
            FoundInsideIndicator[0].SetActive(true);
            counterColor[0].color = new Color32(106, 142, 78, 255);
            treeCounterText[0].text = "1/2";
            if (control.TreeOutside.bordo_foundOutside == true) { FoundOutsideIndicator[0].SetActive(true); treeCounterText[0].text = "2/2"; }

        }
        else
        {
            LockedImage[0].SetActive(true);
            LeafButtons[0].interactable = false;
            FoundInsideIndicator[0].SetActive(false);
        } 
        //eugenia
        if (control.eugenia_found == true)
        {
            LockedImage[5].SetActive(false);
            LeafButtons[5].interactable = true;
            FoundInsideIndicator[5].SetActive(true);
            counterColor[5].color = new Color32(106, 142, 78, 255);
            treeCounterText[5].text = "1/2";
            if (control.TreeOutside.eugenia_foundOutside == true) { FoundOutsideIndicator[5].SetActive(true); treeCounterText[5].text = "2/2"; }

        }
        else
        {
            LockedImage[5].SetActive(true);
            LeafButtons[5].interactable = false;
            FoundInsideIndicator[5].SetActive(false);

        }
        //carvalho
        if (control.carvalho_found == true)
        {
            LockedImage[4].SetActive(false);
            LeafButtons[4].interactable = true;
            FoundInsideIndicator[4].SetActive(true);
            counterColor[4].color = new Color32(106, 142, 78, 255);
            treeCounterText[4].text = "1/2";
            if (control.TreeOutside.carvalho_foundOutside == true) { FoundOutsideIndicator[4].SetActive(true); treeCounterText[4].text = "2/2"; }
        }
        else
        {
            LockedImage[4].SetActive(true);
            LeafButtons[4].interactable = false;
            FoundInsideIndicator[4].SetActive(false);

        }
        //pilriteiro
        if (control.pilriteiro_found == true)
        {
            LockedImage[1].SetActive(false);
            LeafButtons[1].interactable = true;
            FoundInsideIndicator[1].SetActive(true);
            counterColor[1].color = new Color32(106, 142, 78, 255);
            treeCounterText[1].text = "1/2";
            if (control.TreeOutside.pilriteiro_foundOutside == true) { FoundOutsideIndicator[1].SetActive(true); treeCounterText[1].text = "2/2"; }
        }
        else
        {
            LockedImage[1].SetActive(true);
            LeafButtons[1].interactable = false;
            FoundInsideIndicator[1].SetActive(false);
        }
        //ambar
        if (control.ambar_found == true)
        {
            LockedImage[3].SetActive(false);
            LeafButtons[3].interactable = true;
            FoundInsideIndicator[3].SetActive(true);
            counterColor[3].color = new Color32(106, 142, 78, 255);
            treeCounterText[3].text = "1/2";
            if (control.TreeOutside.ambar_foundOutside == true) { FoundOutsideIndicator[3].SetActive(true); treeCounterText[3].text = "2/2"; }
        }
        else
        {
            LockedImage[3].SetActive(true);
            LeafButtons[3].interactable = false;
            FoundInsideIndicator[3].SetActive(false);

        }
        //azevinho
        if (control.azevinho_found == true)
        {
            LockedImage[2].SetActive(false);
            LeafButtons[2].interactable = true;
            FoundInsideIndicator[2].SetActive(true);
            counterColor[2].color = new Color32(106, 142, 78, 255);
            treeCounterText[2].text = "1/2";
            if (control.TreeOutside.azevinho_foundOutside == true) { FoundOutsideIndicator[2].SetActive(true); treeCounterText[2].text = "2/2"; }
        }
        else
        {
            LockedImage[2].SetActive(true);
            LeafButtons[2].interactable = false;
            FoundInsideIndicator[2].SetActive(false);

        }
    }
}
