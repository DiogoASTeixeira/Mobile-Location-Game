using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRandomColor : MonoBehaviour


{
    public int LeafId;

    // Start is called before the first frame update
    void Start()
    {
        if (LeafId == 1) { GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 0.11f, 0.72f, 0.72f, 0.65f, 0.8f); }

        if (LeafId == 2) { GetComponent<Renderer>().material.color = Random.ColorHSV(0.12f, 0.27f, 0.71f, 0.71f, 0.65f, 0.8f); }

        if (LeafId == 3) { GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 0.11f, 0.59f, 0.62f, 0.8f, 0.75f); }

        if (LeafId == 4) { GetComponent<Renderer>().material.color = Random.ColorHSV(0.11f, 0.20f, 0.71f, 0.9f, 0.65f, 0.95f); }

        if (LeafId == 5) { GetComponent<Renderer>().material.color = Random.ColorHSV(0.17f, 0.33f, 0.55f, 0.8f, 0.5f, 0.7f); }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
