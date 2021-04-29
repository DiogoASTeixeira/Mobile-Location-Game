using UnityEngine.UI;

public class InfoPanelBehaviour : UnityEngine.MonoBehaviour
{
    public GameControl control;
    public Image TreeImage;
    public TMPro.TextMeshProUGUI Species, Family, Information;

    public void Setup(short leafIndex)
    {
        Leaf leaf = control.Leaves[leafIndex];
        Species.text = leaf.scientificName;
        //Family.text = leaf.family; TODO
        Information.text = leaf.information;
    }
}
