using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGPSText : MonoBehaviour
{
    public Text coordinates;
    public Text LeafName;
    private GPSLocation gps;

    void Update()
    {
        if (gps != null)
        {
            coordinates.text =
                "Lat: " + gps.selfLatitude.ToString() +
                "\nLon: " + gps.selfLongitude.ToString() +
                "\nAcc: " + gps.selfAccuracy.ToString();


            if(gps.ActiveNotification != -1)
            {
                if(gps.ActiveNotification != gps.previousNotification) // new tree in Range
                {
                    LeafName.text = "Tree Nearby: " + GameControl.control.Leaves[gps.ActiveNotification].speciesName;
                    Handheld.Vibrate();
                    //TODO maybe put image of the leaf
                }
            }
            else
            {
                LeafName.text = "No undiscovered trees nearby";
            }
            gps.previousNotification = gps.ActiveNotification;
        }
        else gps = GPSLocation.Instance;
    }
}
