using UnityEngine;
using TMPro;

public class MapTreeFocusApproach : MonoBehaviour
{
    public GameObject MapTreeViewPort;
    public GameObject[] treeFoundCircle;
    public CanvasGroup[] treeAlpha;
    public TMPro.TextMeshProUGUI treeCounter;
    private GameControl Control;

    public void Start()
    {
        Control = GameControl.control;
        Debug.Log(Control.NumberOfFoundTrees() + " / " + Control.NumberOfFoundLeaves());

    }

    public void Update()
    {
        treeCounter.text = Control.NumberOfFoundTrees() + " / " + Control.NumberOfFoundLeaves();

        for (short i = 0; i < Control.Leaves.Length; i++)
        {
            if (Control.Leaves[i].IsTreeFound())
            {

                if (treeFoundCircle[i] != null )treeFoundCircle[i].SetActive(true);
                treeAlpha[i].alpha = 0.5f;
                
                Debug.Log(Control.NumberOfFoundTrees() + " / " + Control.NumberOfFoundLeaves());
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
