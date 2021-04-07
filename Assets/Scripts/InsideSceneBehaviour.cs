﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class InsideSceneBehaviour : MonoBehaviour
{
    public Camera camera;
    public GameObject leafPanel, cameraPanel;
    public UnityEngine.UI.Text text;
    public UnityEngine.UI.Image halfLeafImage;
    public LeavesInfo[] leavesInfo;

    private int leafSelected;
    private int leafDetected = -1;

    [HideInInspector]
    public Leaf[] leaves;

    private void Start()
    {
        //leaves = GameControl.control.Leaves;
        SetAllTicksFalse();
        OpenLeafMenu();
    }

    private void Update()
    {
        // TODO Detect if BTlocation is correct (is in leaf exposition)
        bool a = leafDetected == leafSelected;  // && IsLeafOnScreenCenter(leafDetected);
        if (a)
        {
            text.text = "Here";
            UpdateLeafFoundTicks(leafDetected);
            OpenLeafMenu();
            GameControl.control.Leaves[leafDetected].FoundLeaf();
        }
    }

    public void DetectedLeaf(int leafNumber)
    {
        leafDetected = leafNumber;
    }
    public void LostDetectedLeaf()
    {
        leafDetected = -1;
    }

    public void SelectedLeaf(int leafSelected)
    {
        CloseLeafMenu();
        this.leafSelected = leafSelected;
        halfLeafImage.sprite = leavesInfo[this.leafSelected].halfLeaf;
    }

    public void OpenLeafMenu()
    {
        camera.gameObject.SetActive(false);
        leafPanel.SetActive(true);
        cameraPanel.SetActive(false);
        halfLeafImage.enabled = false;
    }

    public void CloseLeafMenu()
    {
        camera.gameObject.SetActive(true);
        leafPanel.SetActive(false);
        cameraPanel.SetActive(true);
        halfLeafImage.enabled = true;
    }

    private void UpdateLeafFoundTicks(int leafNumber)
    {
        leavesInfo[leafNumber].tick.enabled = true;
    }

    private void SetAllTicksFalse()
    {
        foreach(LeavesInfo leaf in leavesInfo)
        {
            leaf.tick.enabled = false;
        }
    }

    private bool IsLeafOnScreenCenter(int leafNumber)
    {
        Vector3 objPosition = leavesInfo[leafNumber].centerPiece.transform.position;
        Vector3 screenPos = camera.WorldToScreenPoint(objPosition);
        int fifthX = camera.pixelWidth / 5;
        int fifthY = camera.pixelHeight / 5;
        Debug.Log("X: " + screenPos.x);
        Debug.Log("Y: " + screenPos.y);
        Debug.LogWarning(fifthX * 2);
        Debug.LogWarning(fifthY * 2);
        bool a = fifthX * 2 < screenPos.x && screenPos.x < fifthX * 3
            && fifthY * 2 < screenPos.y && screenPos.y < fifthY * 3;
        return a;
    }



    [System.Serializable]
    public struct LeavesInfo
    {
        public Sprite halfLeaf;
        public UnityEngine.UI.Image tick;
        public GameObject centerPiece;
    }
}