using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class GPSLocation : MonoBehaviour
{
    public float latitude;
    public float longitude;

    public Text gpsText;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Entered");
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            if (Input.location.isEnabledByUser) StartCoroutine(GetLocation());
        }
        else Permission.RequestUserPermission(Permission.FineLocation);
    }

    // Update is called once per frame
    void Update()
    {
        updateCoordinates();
        gpsText.text = "Lat: " + latitude + "\nLon: " + longitude;
        Debug.Log("Lat: " + latitude + "\nLon: " + longitude);
    }

    private IEnumerator GetLocation()
    {
        Input.location.Start(1f, 0.5f);
        while(Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(0.5f);
        }
        updateCoordinates();
        yield break;
    }

    private void updateCoordinates()
    {
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
    }
}
