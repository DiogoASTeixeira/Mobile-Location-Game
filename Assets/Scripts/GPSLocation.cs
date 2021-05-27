using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using System;
using TMPro;

public class GPSLocation : MonoBehaviour
{
    private static readonly float CLOSE_PROXIMITY_RADIUS = 7.0f;
    private static readonly float MEDIUM_PROXIMITY_RADIUS = 15.0f;
    private static readonly float DISTANT_PROXIMITY_RADIUS = 30.0f;
    public enum Proximity
    {
        CLOSE, MEDIUM, DISTANT, FARAWAY
    }

    public static GPSLocation Instance { set; get; }
    private Leaf[] leaves;
    private LocationService location;
    private bool locationServiceStarted = false;
    private bool location_updated = false;
    private int foundLeafIndex = -1;
    private Proximity foundProximity = Proximity.FARAWAY;

    public Slider DebugSlider;
    public double fakeDistance;
    public double selfLatitude;
    public double selfLongitude;
    public double selfAccuracy;
    public short ActiveNotification = -1;
    public short previousNotification = -1;

    public TextMeshProUGUI PointsText;

    public MapTreeFocusApproach TreeMap;
    public Proximity currentViewPortProximity = Proximity.FARAWAY;
    public int currentViewPortIndex = -1;
    public Proximity previousViewPortProximity = Proximity.FARAWAY;
    public int previousViewPortIndex = -1;

    //Tree Box
    public GameObject FoundTreeBox;
    public TextMeshProUGUI FoundTreeText;
    public TextMeshProUGUI CounterText;
    public TextMeshProUGUI RadarTreeText;

       

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
        PointsText.text = GameControl.control.PointCounter.GetCounter() + " Points";
    }

    // Update is called once per frame
    void Update()
    {
        if (locationServiceStarted && location_updated)
        {
            location_updated = false;
            CheckForTrees();
        }
    }

    private void CheckForTrees()
    {
        Proximity closest_proximity = Proximity.FARAWAY;
        short closest_index = -1;
        for (short i = 0; i < leaves.Length; i++)
        {
            if (leaves[i].IsLeafFound() && !leaves[i].IsTreeFound())
            {
                Proximity prox = CheckIfInRange(leaves[i]);
                if(prox < closest_proximity)
                {
                    closest_proximity = prox;
                    closest_index = i;
                }
            }
        }
        // Tree is in range and hasn't been found
        /*if (closest_proximity != Proximity.FARAWAY)
        {
            foundLeafIndex = closest_index;
            foundProximity = closest_proximity;
            OpenFoundTreeBox();
            // Notify nearby tree
            ActiveNotification = closest_index;
        }
        */
        foundLeafIndex = closest_index;
        foundProximity = closest_proximity;
        switch (closest_proximity)
        {
            case Proximity.CLOSE:
                {
                    RadarTreeText.text = "A árvore " + leaves[closest_index].speciesName + " está muito próxima!";
                    OpenFoundTreeBox();
                    // Notify nearby tree
                    ActiveNotification = closest_index;
                    break;
                }
            case Proximity.MEDIUM:
                {
                    RadarTreeText.text = "Estás a aproximar-te da árvore " + leaves[closest_index].speciesName + "!";
                    break;
                }
            case Proximity.DISTANT:
                {
                    RadarTreeText.text = "A árvore " + leaves[closest_index].speciesName + " está perto!";
                    break;
                }
            default: //FARAWAY
                {
                    RadarTreeText.text = "Não há árvores próximas ainda por descobrir.";
                    break;
                }
        }
        currentViewPortIndex = closest_index;
        currentViewPortProximity = closest_proximity;
        if (currentViewPortProximity < previousViewPortProximity || currentViewPortIndex != previousViewPortIndex)
        {
            TreeMap.FocusMapOnTree(currentViewPortIndex);
        }

        previousViewPortProximity = currentViewPortProximity;
        previousViewPortIndex = currentViewPortIndex;
        return;
    }

    private void OpenFoundTreeBox()
    {
        FoundTreeBox.SetActive(true);
        FoundTreeText.text = "A espécie " + leaves[foundLeafIndex].speciesName + " está perto.";
    }

    public void CloseFoundTreeBox()
    {
        GameControl.control.SaveGame();
        FoundTreeBox.SetActive(false);
        leaves[foundLeafIndex].FoundTree();
        int c = 0;
        for (int i = 0; i < leaves.Length; i++)
        {
            if (leaves[i].IsTreeFound()) c++;
        }
        CounterText.text = c.ToString() + " / " + leaves.Length;
    }

    private Proximity CheckIfInRange(Leaf leaf)
    {
        double distance = leaf.DistanceToTree(selfLatitude, selfLongitude);
        if (distance <= CLOSE_PROXIMITY_RADIUS) return Proximity.CLOSE;
        if (distance <= MEDIUM_PROXIMITY_RADIUS) return Proximity.MEDIUM;
        if (distance <= DISTANT_PROXIMITY_RADIUS) return Proximity.DISTANT;
        return Proximity.FARAWAY;
        

    }

    private IEnumerator StartLocationService()
    {
        if (!location.isEnabledByUser)
        {
            Debug.LogError("GPS is not enabled");
            yield break;
        }

        location.Start(4.0f, 1.0f);
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
            location_updated = true;
            yield return new WaitForSeconds(1.0f);
        }
    }

    public Proximity GetProximity() => foundProximity;


    public float Map(float from, float to, float from2, float to2, float value)
    {
        if (value <= from2)
        {
            return from;
        }
        else if (value >= to2)
        {
            return to;
        }
        else
        {
            return (to - from) * ((value - from2) / (to2 - from2)) + from;
        }
    }

    public void DebugSliderShow()
    {
        fakeDistance = Map(50f, 0f, 0f, 1f, DebugSlider.value);

        return;
    }
}
