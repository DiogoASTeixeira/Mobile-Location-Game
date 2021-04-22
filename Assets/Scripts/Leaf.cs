using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Leaf
{

    private static readonly float EARTH_RADIUS = 6371;

    public readonly string speciesName;
    public readonly string scientificName;
    private Vector2 treeCoordinates;
    private bool foundInside = false;
    private bool foundOutside = false;
    private bool inRangeTree = false;

    public bool IsTreeInRange { get => inRangeTree; set => inRangeTree = value; }

    [System.Serializable]
    public struct LeafStruct
    {
        public string speciesName;
        public string scientificName;
        public Vector2 treeCoordinates;

        public override string ToString() => "Name: " + speciesName + "\nX: " + treeCoordinates.x + "\ny: " + treeCoordinates.y;
    }

    public Leaf(LeafStruct stru)
     {
        speciesName = stru.speciesName;
        scientificName = stru.scientificName;
        treeCoordinates = new Vector2(stru.treeCoordinates.x, stru.treeCoordinates.y);
    }

    public float DistanceToTree(float lat, float lon)
    {
        float lat1 = lat;
        float lat2 = treeCoordinates.x;
        float lon1 = lon;
        float lon2 = treeCoordinates.y;

        float dLat = (lat2 - lat1) * Mathf.PI / 180;
        float dLon = (lon2 - lon1) * Mathf.PI / 180;

        lat1 = lat1 * Mathf.PI / 180;
        lat2 = lat2 * Mathf.PI / 180;

        float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
                Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2) * Mathf.Cos(lat1) * Mathf.Cos(lat2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        return EARTH_RADIUS * c * 1000;
    }

    public void FoundLeaf() => foundInside = true;
    public void FoundTree() => foundOutside = true;
    public bool IsLeafFound() => foundInside;
    public bool IsTreeFound() => foundOutside;
    public void SetTreeCoordinates(Vector2 coords)
    {
        treeCoordinates.x = coords.x;
        treeCoordinates.y = coords.y;
    }
    public override string ToString() => "X: " + treeCoordinates.x + "\ny: " + treeCoordinates.y;
}


