using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
public class DissolveSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Material newMat;
    RaycastHit hit;
    private bool dissolveToggle;
    

    public float speed = .5f;

    private void Start(){
        
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        

    }

    private float t = 1.0f;
    private void Update(){


        if(dissolveToggle == true) DissolveNow();




    }

    IEnumerator DestroyDisolveNext()
    {
        
		Debug.Log("Destroy Started");

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        yield return null;

    }


    public void LeafSelected()
    {
        if (TrailScript.deleteToggle == true)
        {
            Debug.Log("upaaaa");

            this.GetComponent<Renderer>().material = newMat;

            StartCoroutine("DestroyDisolveNext");
            dissolveToggle = true;

        }
    }

    public void DissolveNow()
    {
        Material[] mats = spriteRenderer.materials;

       // mats[0].SetFloat("_DissolvePower", 1f);
        mats[0].SetFloat("_DissolvePower", Mathf.Sin(t));
       t -= Time.deltaTime;


        spriteRenderer.materials = mats;
    }

    
}
