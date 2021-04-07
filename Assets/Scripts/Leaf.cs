using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Leaf : MonoBehaviour
{
    private Vector2 treeCoordinates;
    private bool foundInside = false;
    private bool foundOutside = false;

    [System.Serializable]
    public struct LeafStruct
    {
        public Vector2 treeCoordinates;
        //public bool foundInside;
        //public bool foundOutside;

        override public string ToString()
        {
            return "X: " + treeCoordinates.x + "\ny: " + treeCoordinates.y;
        }
    }

    public Leaf(float x, float y)
    {
        treeCoordinates = new Vector2(x, y);

    }
    public Leaf(Vector2 coords)
    {
        treeCoordinates = new Vector2(coords.x, coords.y);
    }

    public void FoundLeaf()
    {
        foundInside = true;
    }

    public void FoundTree()
    {
        foundOutside = true;
    }

    public bool IsLeafFound()
    {
        return foundInside;
    }

    public bool IsTreeFound()
    {
        return foundOutside;
    }

    public void SetTreeCoordinates(Vector2 coords)
    {
        treeCoordinates.x = coords.x;
        treeCoordinates.y = coords.y;
    }

    override public string ToString()
    {
        return "X: " + treeCoordinates.x + "\ny: " + treeCoordinates.y;
    }
}


