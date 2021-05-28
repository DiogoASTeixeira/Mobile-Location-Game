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
                    LeafName.text = "Árvore Perto: " + species;
                    Handheld.Vibrate();
                    MobileNotifications.CreateNotification("Uma Árvore está Perto", "Pareces estar perto da árvore " + species);
                    Debug.Log("Here");
                    //TODO maybe put image of the leaf
                }
            }
            else
            {
                LeafName.text = "Não há árvores para descobrir por perto.";
            }
            gps.previousNotification = gps.ActiveNotification;
        }
        else gps = GPSLocation.Instance;
    }
}
