using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using System;
using TMPro;

public class GPSLocation : MonoBehaviour
{
    public static GPSLocation Instance { set; get; }
    private Leaf[] leaves;
    private static readonly float POI_PROXIMITY_RADIUS = 10.0f;
    private LocationService location;
    private bool locationServiceStarted = false;
    private int foundLeafIndex = -1;

    public double selfLatitude;
    public double selfLongitude;
    public double selfAccuracy;
    public int ActiveNotification = -1;
    public int previousNotification = -1;

    //Tree Box
    public GameObject FoundTreeBox;
    public TextMeshProUGUI FoundTreeText;
    public TextMeshProUGUI CounterText;

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        location = Input.location;


        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            if (location.isEnabledByUser) StartCoroutine(StartLocationService());
        }
        else Permission.RequestUserPermission(Permission.FineLocation);

        leaves = GameControl.control.Leaves;

        int c = 0;
        for (int i = 0; i < leaves.Length; i++)
        {
            if (leaves[i].IsTreeFound()) c++;
        }
        CounterText.text = c.ToString() + " / " + leaves.Length.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (locationServiceStarted)
        {
            CheckForTrees();
        }
    }

    private void CheckForTrees()
    {
        for (int i = 0; i < leaves.Length; i++)
        {
            if (leaves[i].IsLeafFound() && !leaves[i].IsTreeFound())
            {
                if (CheckIfInRange(leaves[i]))
                {
                    // Tree is in range and hasn't been found
                    foundLeafIndex = i;
                    OpenFoundTreeBox();
                    // Notify nearby tree
                    ActiveNotification = i;
                    return;
                }
            }
        }
    }

    private void OpenFoundTreeBox()
    {
        FoundTreeBox.SetActive(true);
        FoundTreeText.text = "A espécie " + leaves[foundLeafIndex].speciesName + " está perto.";
    }

    public void CloseFoundTreeBox()
    {
        FoundTreeBox.SetActive(false);
        leaves[foundLeafIndex].FoundTree();
        int c = 0;
        for (int i = 0; i < leaves.Length; i++)
        {
            if (leaves[i].IsTreeFound()) c++;
        }
        CounterText.text = c.ToString() + " / " + leaves.Length;
    }

    private bool CheckIfInRange(Leaf leaf)
    {
        return leaf.DistanceToTree(selfLatitude, selfLongitude) < POI_PROXIMITY_RADIUS;
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


        while (location.status != LocationServiceStatus.Running && waitCounter > 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            waitCounter--;
        }

        if (waitCounter <= 0)
        {
            Debug.LogError("Location Service Initialization Timed Out");
            yield break;
        }

        if (location.status == LocationServiceStatus.Failed)
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
            selfLatitude = location.lastData.latitude;
            selfLongitude = location.lastData.longitude;
            selfAccuracy = location.lastData.horizontalAccuracy;
            yield return new WaitForSeconds(1.0f);
        }
    }
}
