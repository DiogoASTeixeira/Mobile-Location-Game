using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGPSText : MonoBehaviour
{
    public Text coordinates;
    public Text inRange;

    // Update is called once per frame
    void Update()
    {
        coordinates.text = "Lat: " + GPSLocation.Instance.selfLatitude.ToString() + "\nLon: " + GPSLocation.Instance.selfLongitude.ToString();
        inRange.text = (POIVibrate.hasVibrated ? "In Range" : "Too Far");
    }
}
