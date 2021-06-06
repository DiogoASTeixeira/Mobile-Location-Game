using UnityEngine.UI;

public class InfoPanelBehaviour : UnityEngine.MonoBehaviour
{
    public Image TreeImage;
    public TMPro.TextMeshProUGUI Species, Family, Information;

    private void Awake()
    {
    }

    public void Setup(short leafIndex)
    {
        Leaf leaf = GameControl.control.Leaves[leafIndex];
        Species.text = leaf.scientificName;
        //Family.text = leaf.family; TODO
        //Information.text = leaf.information;
    }
}
