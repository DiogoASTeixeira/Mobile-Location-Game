using UnityEngine;

public class MapTreeFocusApproach : MonoBehaviour
{
    public RectTransform MapTreeViewPort;

    [System.Serializable]
    public struct LeafViewCoords
    {
        public float left, top, right, bottom;
    }
    public LeafViewCoords[] LeafViewPort;
    // Start is called before the first frame update

    public LeafViewCoords getMapTreeCoords(int i)
    {
        return LeafViewPort[i];
    }

    public void FocusMapOnTree(int index)
    {
        Utils.SetLeft(MapTreeViewPort, LeafViewPort[index].left);
        Utils.SetTop(MapTreeViewPort, LeafViewPort[index].top);
        Utils.SetRight(MapTreeViewPort, LeafViewPort[index].right);
        Utils.SetBottom(MapTreeViewPort, LeafViewPort[index].bottom);
    }
}
