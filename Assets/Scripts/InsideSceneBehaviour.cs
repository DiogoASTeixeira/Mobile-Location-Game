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
    public GameObject camera, leafPanel, menuBtn;

    private int leafSelected;

    public void DetectedLeaf(int leafNumber)
    {
        //TODO add functionality
        Debug.Log("You found Leaf " + leafNumber);
        GameControl.control.Leaves.SetFoundLeaf(leafNumber);
        GameControl.control.Leaves.DebugPrint();
    }

    public void SelectedLeaf(int leafSelected)
    {
        camera.SetActive(true);
        leafPanel.SetActive(false);
        menuBtn.SetActive(true);
        this.leafSelected = leafSelected;
    }

    public void OpenLeafMenu()
    {
        camera.SetActive(false);
        leafPanel.SetActive(true);
        menuBtn.SetActive(false);
    }
}