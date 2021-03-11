using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIVibrate : MonoBehaviour
{
    private static readonly float POI_PROXIMITY_RADIUS = 20.0f;

    private POICoords homeGarden;
    private POICoords workdesk;
    public static bool hasVibrated = false;

    private UnityAndroidVibrator androidVibrator;

    public readonly struct POICoords
    {
        private static readonly float EarthRadius = 6371e3F;
        public float latitude { get; }
        public float longitude { get; }

        public POICoords(float lat, float lon)
        {
            latitude = lat;
            longitude = lon;
        }

        public float calculateDistance(float lat, float lon) {
            float l1 = lat * Mathf.PI / 180;
            float l2 = latitude * Mathf.PI / 180;

            float lat_diff = (latitude - lat) * Mathf.PI / 180;
            float lon_diff = (longitude - lon) * Mathf.PI / 180;

            // a is the square of half the chord length between the points.
            float a = Mathf.Sin(lat_diff) * Mathf.Sin(lat_diff) +
                Mathf.Cos(l1) * Mathf.Cos(l2) *
                Mathf.Sin(lon_diff / 2) * Mathf.Sin(lon_diff / 2);

            // c is the angular distance in radians
            float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

            float distance = EarthRadius * c;

            return distance;
        }
    }

    void Start()
    {
        homeGarden = new POICoords(41.1234329f, -8.6548297f);
        workdesk = new POICoords(41.12326f, -8.654498f);
        androidVibrator = gameObject.AddComponent<UnityAndroidVibrator>();
    }

    void Update()
    {
        if (!hasVibrated)
        {
            // is in range and hasn't vibrated
            if (homeGarden.calculateDistance(GPSLocation.Instance.selfLatitude, GPSLocation.Instance.selfLongitude) < POI_PROXIMITY_RADIUS)
            {
                hasVibrated = true;
                if (Application.platform == RuntimePlatform.Android)
                    UnityAndroidVibrator.VibrateForGivenDuration(200);
                else
                    Handheld.Vibrate();
            }
        }
        else
        {
            // has Vibrated but left range
            if (homeGarden.calculateDistance(GPSLocation.Instance.selfLatitude, GPSLocation.Instance.selfLongitude) > POI_PROXIMITY_RADIUS)
            {
                hasVibrated = false;
            }
        }
    }
}
