﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestText : MonoBehaviour
{
    public Text leaf;

    // Update is called once per frame
    private void Start()
    {
    }

    void Update()
    {
        leaf.text = GameControl.control.NumberOfFoundLeaves() + ": " + GameControl.control.Leaves.Length.ToString();
    }
}