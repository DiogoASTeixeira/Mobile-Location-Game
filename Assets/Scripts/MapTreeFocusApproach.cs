using UnityEngine;
using TMPro;

public class MapTreeFocusApproach : MonoBehaviour
{
    public GameObject MapTreeViewPort;
    public GameObject[] treeFoundCircle;
    public CanvasGroup[] treeAlpha;
    public TMPro.TextMeshProUGUI treeCounter;
    
    public void Start()
    {
        
    }

    public void Update()
    {
        //Debug.Log(MapTreeViewPort.transform.localPosition);

        for (short i = 0; i < GameControl.control.Leaves.Length; i++)
        {
            if (GameControl.control.Leaves[i].IsTreeFound())
            {

                if (treeFoundCircle[i] != null )treeFoundCircle[i].SetActive(true);
                treeAlpha[i].alpha = 0.5f;
                treeCounter.text = GameControl.control.NumberOfFoundTrees() + " / " + GameControl.control.NumberOfFoundLeaves();

            }
        }
            

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
