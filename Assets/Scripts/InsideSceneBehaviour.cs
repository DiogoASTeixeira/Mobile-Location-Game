using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

//TODO
// UI which leaf
// camera with half leaf
// detect leaf is correct
// return to menu

public class InsideSceneBehaviour : MonoBehaviour
{
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public GameObject camera, leafPanel, menuBtn;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    public UnityEngine.UI.Image halfLeafImage;
    public Sprite[] halfSprites;
    public UnityEngine.UI.Image[] ticks;

    private int leafSelected;

    public Leaves leaves { get; private set; }

    private void Start()
    {
        leaves = GameControl.control.Leaves;
        SetAllTicksFalse();
    }

    public void DetectedLeaf(int leafNumber)
    {
        //TODO add functionality
        //Debug.Log("You found Leaf " + leafNumber);
        if (leafNumber == leafSelected)
        {
            leaves.SetFoundLeaf(leafNumber);
            UpdateLeafFoundTicks(leafNumber);
            leaves.DebugPrint();
            OpenLeafMenu();
        }
    }

    public void SelectedLeaf(int leafSelected)
    {
        camera.SetActive(true);
        leafPanel.SetActive(false);
        menuBtn.SetActive(true);
        this.leafSelected = leafSelected;
        halfLeafImage.sprite = halfSprites[this.leafSelected];
        halfLeafImage.enabled = true;
    }

    public void OpenLeafMenu()
    {
        camera.SetActive(false);
        leafPanel.SetActive(true);
        menuBtn.SetActive(false);
        halfLeafImage.enabled = false;
    }

    private void UpdateLeafFoundTicks(int leafNumber)
    {
        ticks[leafNumber].enabled = true;
    }

    private void SetAllTicksFalse()
    {
        foreach(UnityEngine.UI.Image tick in ticks)
        {
            tick.enabled = false;
        }
    }
}