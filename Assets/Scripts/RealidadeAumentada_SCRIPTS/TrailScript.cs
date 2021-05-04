
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GoogleARCore;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
#if UNITY_EDITOR
    // Set up touch input propagation while using Instant Preview in the editor.
    using Input = GoogleARCore.InstantPreviewInput;
#endif
*/

public class TrailScript : MonoBehaviour
{

	public GameObject stickerObject0;
	public GameObject stickerObject1;
	public GameObject stickerObject2;
	public GameObject stickerObject3;
	public GameObject stickerObject4;
	public GameObject stickerObject5;

	public GameObject phonecamera;
	public Vector3 Distance;
	public float spawnDistance;
	public Text debug;
	public int count_touch;
	public bool placeSticker;
	public int stickerType;
	
	//public List<GameObject> Points = new List<GameObject>();



	void Start()
	{
		
		debug.text = "Debug will appear here";
		placeSticker = false;
		
	}

	public void PlaceSticker()
    {
		placeSticker = true;
    }

	public void PlaceSticker0()
	{
		stickerType = 0;
	}
	public void PlaceSticker1()
	{
		stickerType = 1;
	}
	public void PlaceSticker2()
	{
		stickerType = 2;
	}
	public void PlaceSticker3()
	{
		stickerType = 3;
	}
	public void PlaceSticker4()
	{
		stickerType = 4;
	}
	public void PlaceSticker5()
	{
		stickerType = 5;
	}


	// Update is called once per frame
	void Update()
	{
		
		if (placeSticker == true)
        {
			Vector3 camPos = phonecamera.transform.position;
			Vector3 camDirection = phonecamera.transform.forward;
			Quaternion camRotation = phonecamera.transform.rotation;

			Vector3 spawnPos = (camDirection * spawnDistance);
			if (stickerType == 0)
            {
				GameObject cur = Instantiate(stickerObject0, (camPos + (camDirection - spawnPos)), camRotation); //cria a sticker 0
				cur.transform.SetParent(this.transform);
				placeSticker = false;
			}
			else if (stickerType == 1)
            {
				GameObject cur = Instantiate(stickerObject1, (camPos + (camDirection - spawnPos)), camRotation); //cria a sticker 1
				cur.transform.SetParent(this.transform);
				placeSticker = false;
			}
			else if (stickerType == 2)
			{
				GameObject cur = Instantiate(stickerObject2, (camPos + (camDirection - spawnPos)), camRotation); //cria a sticker 2
				cur.transform.SetParent(this.transform);
				placeSticker = false;
			}
			else if (stickerType == 3)
			{
				GameObject cur = Instantiate(stickerObject3, (camPos + (camDirection - spawnPos)), camRotation); //cria a sticker 3
				cur.transform.SetParent(this.transform);
				placeSticker = false;
			}
			else if (stickerType == 4)
			{
				GameObject cur = Instantiate(stickerObject4, (camPos + (camDirection - spawnPos)), camRotation); //cria a sticker 4
				cur.transform.SetParent(this.transform);
				placeSticker = false;
			}
			else if (stickerType == 5)
			{
				GameObject cur = Instantiate(stickerObject5, (camPos + (camDirection - spawnPos)), camRotation); //cria a sticker 5
				cur.transform.SetParent(this.transform);
				placeSticker = false;
			}


			
		}

		/*
		if (Input.touchCount > 1 && status == true )
		{
			if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
			{
				Debug.Log("Touched the UI");
			}

			else if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				//count_touch++;
				//debug.text = ("You touched me " + count_touch);
				
				Vector3 camPos = phonecamera.transform.position;
				Vector3 camDirection = phonecamera.transform.forward;
				Quaternion camRotation = phonecamera.transform.rotation;
				
				//Debug.Log("Touched" + camPos.x + " " + camPos.y + " " + camPos.z);
				Vector3 spawnPos =  (camDirection * spawnDistance);
				GameObject cur = Instantiate(stickerObject, (camPos + (camDirection - spawnPos)), camRotation);
				cur.transform.SetParent(this.transform);
			}
			
		}

		*/

	}
}