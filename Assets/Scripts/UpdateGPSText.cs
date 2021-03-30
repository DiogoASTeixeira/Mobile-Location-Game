using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGPSText : MonoBehaviour
{
    public Text coordinates;
    public Text inRange;
    private GPSLocation gps;
    public Text leaf;

    // Update is called once per frame
    private void Start()
    {
        gps = GPSLocation.Instance;
    }

    void Update()
    {
        coordinates.text = "Lat: " + gps.selfLatitude.ToString() + "\nLon: " + gps.selfLongitude.ToString() + "\nAcc: " + gps.selfAccuracy.ToString();
        // inRange.text = (GPSLocation.Instance.hasVibrated ? "In Range" : "Too Far");
        inRange.text = gps.distance.ToString();
        leaf.text = "Leaves Found: " + GameControl.control.Leaves.getNumberFoundLeaves();
    }
}
