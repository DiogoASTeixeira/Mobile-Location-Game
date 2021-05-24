using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class U10PS_DissolveOverTime : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public Material newMat;
    RaycastHit hit;
    private bool dissolveToggle;

    public float speed = .5f;

    private void Start(){
        meshRenderer = this.GetComponent<MeshRenderer>();
    }

    private float t = 0.0f;
    private void Update(){

       // if (dissolveToggle == true) DissolveNow();




       
    }

    IEnumerator DestroyDissolveNext()
    {

        Debug.Log("Destroy Started");

        yield return new WaitForSeconds(1f);
        Destroy(transform.parent.gameObject);
        yield return null;

    }


    public void LeafSelected()
    {
        if (TrailScript.deleteToggle == true)
        {

           // this.GetComponent<Renderer>().material = newMat;

            StartCoroutine("DestroyDissolveNext");
            dissolveToggle = true;

        }
    }

    public void DissolveNow()
    {
        Material[] mats = meshRenderer.materials;

        mats[0].SetFloat("_Cutoff", Mathf.Sin(t * speed));
        t += Time.deltaTime;

        // Unity does not allow meshRenderer.materials[0]...
        meshRenderer.materials = mats;
    }
}
