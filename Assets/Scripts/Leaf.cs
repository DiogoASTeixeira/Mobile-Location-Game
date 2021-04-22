using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Leaf
{

    private static readonly double EARTH_RADIUS = 6371;

    public readonly string speciesName;
    public readonly string scientificName;
    private MyVector2 treeCoordinates;
    private bool foundInside = false;
    private bool foundOutside = false;
    private bool inRangeTree = false;

    public bool IsTreeInRange { get => inRangeTree; set => inRangeTree = value; }

    [System.Serializable]
    public struct LeafStruct
    {
        public string speciesName;
        public string scientificName;
        public MyVector2 treeCoordinates;

        public override string ToString() => "Name: " + speciesName + "\nX: " + treeCoordinates.x + "\ny: " + treeCoordinates.y;
    }

    public Leaf(LeafStruct stru)
     {
        speciesName = stru.speciesName;
        scientificName = stru.scientificName;
        treeCoordinates = new MyVector2(stru.treeCoordinates.x, stru.treeCoordinates.y);
    }

    public double DistanceToTree(double lat, double lon)
    {
        double lat1 = lat;
        double lat2 = treeCoordinates.x;
        double lon1 = lon;
        double lon2 = treeCoordinates.y;

        double dLat = (lat2 - lat1) * Mathf.PI / 180;
        double dLon = (lon2 - lon1) * Mathf.PI / 180;

        lat1 = lat1 * Mathf.PI / 180;
        lat2 = lat2 * Mathf.PI / 180;

        double a = Mathf.Sin((float)dLat / 2) * Mathf.Sin((float)dLat / 2) +
                Mathf.Sin((float)dLon / 2) * Mathf.Sin((float)dLon / 2) * Mathf.Cos((float)lat1) * Mathf.Cos((float)lat2);
        double c = 2 * Mathf.Atan2(Mathf.Sqrt((float)a), Mathf.Sqrt((float)(1 - a)));
        return EARTH_RADIUS * c * 1000;
    }

    public void FoundLeaf() => foundInside = true;
    public void FoundTree() => foundOutside = true;
    public bool IsLeafFound() => foundInside;
    public bool IsTreeFound() => foundOutside;
    public void SetTreeCoordinates(MyVector2 coords)
    {
        treeCoordinates.x = coords.x;
        treeCoordinates.y = coords.y;
    }
    public override string ToString() => "X: " + treeCoordinates.x + "\ny: " + treeCoordinates.y;
}


