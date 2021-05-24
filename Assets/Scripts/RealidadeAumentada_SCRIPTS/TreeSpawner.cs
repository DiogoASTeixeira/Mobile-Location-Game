using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{

    public GameObject spawnThatTree;
    private bool placedTree;
    private GameObject Parent;

    void Start()
    {
        Parent = this.transform.parent.gameObject;

    }

    // Update is called once per frame
    void Update()
    {

        // Debug.Log(this.transform.position);


        if (this.transform.position.y < -1.5f && placedTree == false)
        {
            Debug.Log("Seed has fell");
            GameObject treeSpawn = Instantiate(spawnThatTree,this.transform.position, this.transform.rotation, Parent.transform);
            placedTree = true;
            Destroy(gameObject);
            return;
        }




    }
}
