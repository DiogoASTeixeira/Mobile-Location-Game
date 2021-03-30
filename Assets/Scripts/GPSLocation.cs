using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using System;

public class GPSLocation : MonoBehaviour
{
    public static GPSLocation Instance { set; get; }
    private static readonly float POI_PROXIMITY_RADIUS = 10.0f;

    public float selfLatitude;
    public float selfLongitude;
    public float selfAccuracy;
    public bool hasVibrated = false;
    public float distance = POI_PROXIMITY_RADIUS + 1.0f;

    private bool locationServiceStarted = false;

    public readonly struct POICoords
    {
        private static readonly float EarthRadius = 6371;
        public float Latitude { get; }
        public float Longitude { get; }

        public POICoords(float lat, float lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        public float CalculateDistance(float lat, float lon)
        {
            float lat1 = lat;
            float lat2 = Latitude;
            float lon1 = lon;
            float lon2 = Longitude;

            float dLat = (lat2 - lat1) * Mathf.PI / 180;
            float dLon = (lon2 - lon1) * Mathf.PI / 180;

            lat1 = lat1 * Mathf.PI / 180;
            lat2 = lat2 * Mathf.PI / 180;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return (float)(EarthRadius * c * 1000);
        }
    }

    private LocationService location;
    private POICoords homeGarden;
    public Text debug;
    public Text vibrateDebug;
    public Text LeavesFoundText;



    void Start()
    {
        Debug.Log("Entered");
        Instance = this;
        DontDestroyOnLoad(gameObject);

        location = Input.location;

        homeGarden = new POICoords(41.12348871036983f, -8.654655184410982f);

        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            if (location.isEnabledByUser) StartCoroutine(StartLocationService());
        }
        else Permission.RequestUserPermission(Permission.FineLocation);
        
        int nLeavesFound = 0;
        foreach( bool found in GameControl.control.Leaves.GetFoundLeaves())
        {
            if (found) nLeavesFound++;
        }

        LeavesFoundText.text = "Leaves Found: " + nLeavesFound;
    }

    // Update is called once per frame
    void Update()
    {
        //UnityAndroidVibrator.VibrateForGivenDuration(200);

        if (locationServiceStarted)
        {
            CheckVibrateFlag();
        }


    }

    private IEnumerator StartLocationService()
    {
        if (!location.isEnabledByUser)
        {
            Debug.LogError("GPS is not enabled");
            yield break;
        }

        location.Start(5f, 1f);
        int waitCounter = 20;


        while(location.status != LocationServiceStatus.Running && waitCounter > 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            waitCounter--;
        }

        if(waitCounter <= 0)
        {
            Debug.LogError("Location Service Initialization Timed Out");
            yield break;
        }

        if(location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Location Service Initialization Failed");
            yield break;
        }

        locationServiceStarted = true;
        
        StartCoroutine(UpdateCoordinates());

        yield break;
    }

    private IEnumerator UpdateCoordinates()
    {
        while (true)
        {
            debug.text = "In";
            selfLatitude = location.lastData.latitude;
            selfLongitude = location.lastData.longitude;
            selfAccuracy= location.lastData.horizontalAccuracy;
            yield return new WaitForSeconds(1.0f);
            debug.text = "Out";
            yield return new WaitForSeconds(1.0f);
        }
    }

    void CheckVibrateFlag()
    {
        distance = homeGarden.CalculateDistance(Instance.selfLatitude, Instance.selfLongitude);
        if (distance < POI_PROXIMITY_RADIUS)
        {
            if (!hasVibrated)
            {
                // is in range and hasn't vibrated
                hasVibrated = true;
                vibrateDebug.text = "VIBRATE ON";
                //if (Application.platform == RuntimePlatform.Android)
                 //   UnityAndroidVibrator.VibrateForGivenDuration(200);
                //else
                    Handheld.Vibrate();
            }
        }
        else
        {
            // has Vibrated but left range
            if (distance > POI_PROXIMITY_RADIUS)
            {
                vibrateDebug.text = "VIBRATE OFF";
                hasVibrated = false;
            }
        }
    }
}
