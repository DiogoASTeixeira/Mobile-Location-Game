using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnUp : MonoBehaviour
{

    public GameObject Renderer;
    private TrailScript rendererScript;
    public int StickerNumber;
    public bool isSelected;

    private void Start()
    {
        rendererScript = Renderer.GetComponent<TrailScript>();
        isSelected = false;
    }
    
    public void StickerIsSelected()
    {
        isSelected = true;
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {

            transform.localPosition = new Vector3(0, 0, 0);
            if (isSelected == true)
            {
                rendererScript.stickerType = StickerNumber;
                rendererScript.placeSticker = true;
                //Debug.Log(rendererScript.stickerType);
                isSelected = false;
            }
            
            
        }
    }
}
