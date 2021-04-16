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
                    string species = GameControl.control.Leaves[gps.ActiveNotification].speciesName;
                    LeafName.text = "Tree Nearby: " + species;
                    Handheld.Vibrate();
                    MobileNotifications.CreateNotification("A Tree is nearby", "You seem to be close to the tree of the species " + species);
                    Debug.Log("Here");
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
