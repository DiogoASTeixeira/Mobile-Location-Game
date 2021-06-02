using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using System;
using TMPro;
using UnityEngine.Events;

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

    public IntroManager PanelManager;

    public double fakeDistance;
    public double selfLatitude;
    public double selfLongitude;
    public double selfAccuracy;
    public int ActiveNotification = -1;
    public int previousNotification = -1;

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

    public GameObject[] Modals;
    public bool isModalOpen = false;
       

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

        debug_slider.maxValue = 60.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Application.isEditor)
            Debug_Update();
        else if (locationServiceStarted && location_updated)
        {
            location_updated = false;
            CheckForTrees();
        }
    }

    private void Debug_Update()
    {
        //debug_slider.value = (float)lowest_debug_slider_distance;
        foundLeafIndex = debug_tree_index;

        if (debug_slider.value <= CLOSE_PROXIMITY_RADIUS) debug_proximity = Proximity.CLOSE;
        else if (debug_slider.value <= MEDIUM_PROXIMITY_RADIUS) debug_proximity = Proximity.MEDIUM;
        else if (debug_slider.value <= DISTANT_PROXIMITY_RADIUS) debug_proximity = Proximity.DISTANT;
        else debug_proximity = Proximity.FARAWAY;
        Debug.Log(debug_slider.value);
        switch (debug_proximity)
        {
            case Proximity.CLOSE:
                {
                    RadarTreeText.text = "A árvore " + leaves[foundLeafIndex].speciesName + " está muito próxima!";
                    //OpenFoundTreeBox();
                    // Notify nearby tree
                    //ActiveNotification = foundLeafIndex;
                    break;
                }
            case Proximity.MEDIUM:
                {
                    RadarTreeText.text = "Estás a aproximar-te da árvore " + leaves[foundLeafIndex].speciesName + "!";
                    break;
                }
            case Proximity.DISTANT:
                {
                    RadarTreeText.text = "A árvore " + leaves[foundLeafIndex].speciesName + " está perto!";
                    break;
                }
            default: //FARAWAY
                {
                    RadarTreeText.text = "Não há árvores próximas ainda por descobrir.";
                    break;
                }
        }
    }

    double curr_debug_slider_distance;
    double lowest_debug_slider_distance;
    public Slider debug_slider;
    public int debug_tree_index;
    public Proximity debug_proximity;
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
                    lowest_debug_slider_distance = curr_debug_slider_distance;
                    closest_proximity = prox;
                    closest_index = i;
                    Debug.Log(lowest_debug_slider_distance + "and" + curr_debug_slider_distance + " and " + closest_proximity);
                }
            }
        }
       
        if (!isModalOpen)
        {

            foundLeafIndex = closest_index;
            foundProximity = closest_proximity;
            switch (closest_proximity)
            {
                case Proximity.CLOSE:
                    {
                        RadarTreeText.text = "A árvore " + leaves[closest_index].speciesName + " está muito próxima!";
                        OpenFoundModal();
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
                Handheld.Vibrate();
            }

            previousViewPortProximity = currentViewPortProximity;
            previousViewPortIndex = currentViewPortIndex;
        }
        return;
    }

    private void OpenFoundTreeBox()
    {
        FoundTreeBox.SetActive(true);
        FoundTreeText.text = "A espécie " + leaves[foundLeafIndex].speciesName + " está perto.";
    }

    private void OpenFoundModal()
    {
        if (!isModalOpen)
        {
            isModalOpen = true;
            Modals[foundLeafIndex].SetActive(true);
        }
    }


    public void CheckIfFoundAllTrees()
    {
        if(GameControl.control.HasFoundAllTrees())
        {
            PanelManager.ShowNextPanel();
            GameControl.control.NavBar.SetActive(true);
        }
    }
    public void CloseFoundModal(int ModalIndex)
    {
        isModalOpen = false;
        leaves[ModalIndex].FoundTree();
        GameControl.control.SaveGame();
        int c = 0;
        for (int i = 0; i < leaves.Length; i++)
        {
            if (leaves[i].IsTreeFound()) c++;
        }
        CounterText.text = c.ToString() + " / " + leaves.Length;
    }

    public void CloseFoundTreeBox()
    {
        FoundTreeBox.SetActive(false);
        leaves[foundLeafIndex].FoundTree();
        GameControl.control.SaveGame();
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
        curr_debug_slider_distance = distance;
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
}
