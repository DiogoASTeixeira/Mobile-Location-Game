using UnityEngine;

public class MapTreeFocusApproach : MonoBehaviour
{
    public GameObject MapTreeViewPort;
    
    public void Update()
    {
        //Debug.Log(MapTreeViewPort.transform.localPosition);
    }

    
    [System.Serializable]
    public struct LeafViewCoords
    {
        //public float left, top, right, bottom;
        public Lean.Transition.LeanManualAnimation LeafTransitor;
    }
    public LeafViewCoords[] LeafViewPort;

    public LeafViewCoords getMapTreeCoords(int i)
    {
        return LeafViewPort[i];
    }

    /*
    public void FocusMapOnTree(int index)
    {
        Utils.SetLeft(MapTreeViewPort, LeafViewPort[index].left);
        Utils.SetTop(MapTreeViewPort, LeafViewPort[index].top);
        Utils.SetRight(MapTreeViewPort, LeafViewPort[index].right);
        Utils.SetBottom(MapTreeViewPort, LeafViewPort[index].bottom);
    }
    */
    public void FocusMapOnTree(int index)
    {
        LeafViewPort[index].LeafTransitor.BeginTransitions();
    }
}
