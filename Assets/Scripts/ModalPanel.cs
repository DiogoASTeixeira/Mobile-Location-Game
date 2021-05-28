using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModalPanel : MonoBehaviour
{
    public GameObject CloseIcon;
    private Animator anim;
    public UnityEvent CloseModal;
    public float valuerange;
    public Image backdrop;
    public CanvasGroup otherclosebtn;
    public ParticleSystem leavesdrop;

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

    // Start is called before the first frame update
    void Start()
    {
        anim = CloseIcon.GetComponent<Animator>();
        anim.speed = 0;
        

    }

    // Update is called once per frame
    void Update()
    {

         //Debug.Log(this.transform.localPosition.y); //Debug Altura


        //Debug.Log(Map(0f, 1f, -550f, -310f,  this.transform.localPosition.y) + " " + this.transform.localPosition.y);
        valuerange = Map(1f, 0f, -500f, -400f, this.transform.localPosition.y);
        backdrop.color = new Color32(200, 200, 200, (byte) Map(0f, 255f, -500f, -400f, this.transform.localPosition.y));
        otherclosebtn.alpha = Map(0f, 1f, -450f, -400f, this.transform.localPosition.y);
        leavesdrop.startColor = new Color32(200, 200, 200, (byte) Map(0f, 255f, -500f, -400f, this.transform.localPosition.y));



        anim.Play("CloseModal_Animation", -1, valuerange);
        if (this.transform.localPosition.y < -500)
        {
            //Debug.Log("Closed Modal");
            //this.transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);

            CloseModal.Invoke();

        } else if (this.transform.localPosition.y > -90)
        {
            otherclosebtn.alpha = Map(1f, 0f, -80f, 70f, this.transform.localPosition.y);


        }
    }

    public void RestartPosition()
    {
        this.transform.localPosition = new Vector3(transform.localPosition.x, -401f, transform.localPosition.z);
    }
    
}
