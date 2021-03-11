using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class GPSLocation : MonoBehaviour
{
    public static GPSLocation Instance { set; get; }

    public float selfLatitude;
    public float selfLongitude;

    private bool locationServiceStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Entered");
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            if (Input.location.isEnabledByUser) StartCoroutine(StartLocationService());
        }
        else Permission.RequestUserPermission(Permission.FineLocation);
    }

    // Update is called once per frame
    void Update()
    {
        if (locationServiceStarted)
        {
            UpdateCoordinates();
        }
    }

    private IEnumerator StartLocationService()
    {
        if(!Input.location.isEnabledByUser)
        {
            Debug.LogError("GPS is not enabled");
            yield break;
        }

        Input.location.Start(5f, 5f);
        int waitCounter = 20;


        while(Input.location.status == LocationServiceStatus.Initializing && waitCounter > 0)
        {
            yield return new WaitForSeconds(1f);
            waitCounter--;
        }

        if(waitCounter <= 0)
        {
            Debug.LogError("Location Service Initialization Timed Out");
            yield break;
        }

        if(Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Location Service Initialization Failed");
            yield break;
        }

        locationServiceStarted = true;

        UpdateCoordinates();
        yield break;
    }

    private void UpdateCoordinates()
    {
        selfLatitude = Input.location.lastData.latitude;
        selfLongitude = Input.location.lastData.longitude;
    }
}
