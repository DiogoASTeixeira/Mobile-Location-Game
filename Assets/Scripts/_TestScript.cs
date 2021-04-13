using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TestScript : MonoBehaviour
{
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public Camera camera;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    public GameObject[] centerPiece;

    public LeavesInfo[] leavesInfo;

    private int leafDetected = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(leafDetected >= 0)
        {
            if (leafDetected == 0)
            {
                if (IsLeafOnScreenCenter())
                {
                    //do thing
                    Debug.LogWarning("SUCCESS");
                }

            }
        }
    }

    public void DetectedLeaf(int leafNumber)
    {
        leafDetected = leafNumber;
        if (leafNumber == 0)
        { 
            if(IsLeafOnScreenCenter())
            {
                //do thing
                Debug.Log("SUCCESS");
            }

        }
    }

    private bool IsLeafOnScreenCenter()
    {
        Vector3 objPosition = centerPiece[0].transform.position;
        Vector3 screenPos = camera.WorldToScreenPoint(objPosition);
        int sixthX = camera.pixelWidth / 6;
        int sixthY = camera.pixelHeight / 6;
        Debug.Log("X: " + screenPos.x);
        Debug.Log("Y: " + screenPos.y);
        return sixthX < screenPos.x && screenPos.x < sixthX * 2
            && sixthY < screenPos.y && screenPos.y < sixthY * 2;
    }

    public void LostDetectedLeaf()
    {
        leafDetected = -1;
    }

    [System.Serializable]
    public struct LeavesInfo
    {
        public long id;
        public string type;
        public string name;
        public string equipmentSlot;

    }
}
