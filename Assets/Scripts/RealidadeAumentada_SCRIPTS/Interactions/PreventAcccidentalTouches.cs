using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PreventAcccidentalTouches : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{

    [SerializeField]
    GameObject DisableStickerTouch;

   
    public string scr;

    public void OnDrag(PointerEventData data)
    {
        // Debug.Log("Currently dragging " + this.name);
        // DisableStickerTouch.SetActive(false);
        (DisableStickerTouch.GetComponent(scr) as MonoBehaviour).enabled = false;
    }


    public void OnEndDrag(PointerEventData data)
    {
        //Debug.Log("Stopped dragging " + this.name + "!");
        //DisableStickerTouch.SetActive(true);
        (DisableStickerTouch.GetComponent(scr) as MonoBehaviour).enabled = true;

    }

    public void OnBeginDrag(PointerEventData data)
    {

        //DisableStickerTouch.SetActive(false);
       // Debug.Log("Dragging started");
    }

}
