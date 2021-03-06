
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


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
	public GameObject stickerObject6;
	public GameObject stickerObject7;
	public GameObject stickerObject8;
	public GameObject stickerObject9;
	public GameObject stickerObject10;
	public GameObject stickerObject11;
	public GameObject stickerObject12;
	public GameObject[] disableStickerIfNotFound;
	public GameObject[] disableRealIfNotFound;

	public GameObject phonecamera;
	public Vector3 Distance;
	public float spawnDistance;
	public Text debug;
	public int count_touch;
	public bool placeSticker;
	public int stickerType;
	public static bool deleteToggle;
	public GameObject TutorialToggle;
	private GameControl Control;
	Anchor leafAnchor;
	private bool hasPlacedLeafAnchor;

	//public List<GameObject> Points = new List<GameObject>();



	void Start()
	{
		Control = GameControl.control;
		debug.text = "Debug will appear here";
		placeSticker = false;
		deleteToggle = false;
		GameControl.control.NavBar.SetActive(false);
		Leaf[] leaves = Control.Leaves;

		for (short i = 0; i < leaves.Length; i++)
		{
			if (leaves[i].IsTreeFound())
			{
				disableStickerIfNotFound[i].SetActive(true);
				disableRealIfNotFound[i].SetActive(true);
			}

		}
	}
	public void deleteOn()
	{
		deleteToggle = true;
	}
	public void deleteOff()
	{
		deleteToggle = false;
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
	public void PlaceSticker6()
	{
		stickerType = 6;
	}
	public void PlaceSticker7()
	{
		stickerType = 7;
	}
	public void PlaceSticker8()
	{
		stickerType = 8;
	}
	public void PlaceSticker9()
	{
		stickerType = 9;
	}
	public void PlaceSticker10()
	{
		stickerType = 10;
	}
	public void PlaceSticker11()
	{
		stickerType = 11;
	}
	public void PlaceSticker12()
	{
		stickerType = 12;
	}
	public void SetTutorialSeen()
	{
		Control.seen_tutorial = true;
		Control.SaveGame();
	}
	public void ShowTutorial()
	{
		Control.seen_tutorial = false;
		
	}

	
	
	public GameObject touchedObject = null;
	Vector3 InicialCamPos;

	public void deselectObject()
    {
		touchedObject = null;

	}

	// Update is called once per frame
	void Update()
	{

		if (Control.seen_tutorial == true)
		{
			TutorialToggle.SetActive(false);


		}

		/*
		if (Input.GetMouseButtonDown(0) || Input.touchCount > 1)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			

			if (Physics.Raycast(ray, out hit, 100))
			{

				touchedObject = hit.transform.gameObject;
				InicialCamPos = phonecamera.transform.position;
				

			}


		}

		if (touchedObject != null)
        {
			Vector3 camPos = phonecamera.transform.position;
			Vector3 camDirection = phonecamera.transform.forward;
			Debug.Log(camDirection);
			Quaternion camRotation = phonecamera.transform.rotation;
			Vector3 spawnPos = (camDirection * 0.5f);
			touchedObject.transform.position = camPos + (camDirection - spawnPos);
			touchedObject.transform.rotation = camRotation;


		}

		*/


		if (placeSticker == true)
		{
			Vector3 camPos = phonecamera.transform.position;
			Vector3 camDirection = phonecamera.transform.forward;
			Quaternion camRotation = phonecamera.transform.rotation;
			Vector3 spawnPos = (camDirection * spawnDistance);
			Vector3 spawnPos2 = (camDirection * 0.01f);

			if (hasPlacedLeafAnchor == false)
			{
				leafAnchor = Session.CreateAnchor(new Pose(camPos + (camDirection - spawnPos), this.transform.rotation));
				hasPlacedLeafAnchor = true;
				Debug.Log("Coloquei uma ancora de folhas em:" + leafAnchor.transform.position + " " + leafAnchor.transform.rotation);

			}


			
			if (stickerType == 0)
			{
				GameObject cur = Instantiate(stickerObject0, (camPos + (camDirection - (spawnPos * 0.001f))), this.transform.rotation); //cria a sticker 0
				cur.transform.SetParent(leafAnchor.transform);
				placeSticker = false;
			}
			else if (stickerType == 1)
			{
				GameObject cur = Instantiate(stickerObject1, (camPos + (camDirection - spawnPos)), camRotation); //cria a sticker 1
				cur.transform.SetParent(leafAnchor.transform);
				placeSticker = false;
			}
			else if (stickerType == 2)
			{
				GameObject cur = Instantiate(stickerObject2, (camPos + (camDirection - spawnPos)), camRotation); //cria a sticker 2
				cur.transform.SetParent(leafAnchor.transform);
				placeSticker = false;
			}
			else if (stickerType == 3)
			{
				GameObject cur = Instantiate(stickerObject3, (camPos + (camDirection - spawnPos)), camRotation); //cria a sticker 3
				cur.transform.SetParent(leafAnchor.transform);
				placeSticker = false;
			}
			else if (stickerType == 4)
			{
				GameObject cur = Instantiate(stickerObject4, (camPos + (camDirection - spawnPos)), camRotation); //cria a sticker 4
				cur.transform.SetParent(leafAnchor.transform);
				placeSticker = false;
			}
			else if (stickerType == 5)
			{
				GameObject cur = Instantiate(stickerObject5, (camPos + (camDirection - spawnPos)), camRotation); //cria a sticker 5
				cur.transform.SetParent(leafAnchor.transform);
				placeSticker = false;
			}
			// real leaves
			else if (stickerType == 6)
			{
				GameObject cur = Instantiate(stickerObject6, (camPos + (camDirection - spawnPos)), camRotation); //cria a sticker 6
				cur.transform.SetParent(leafAnchor.transform);
				placeSticker = false;
			}
			else if (stickerType == 7)
			{
				GameObject cur = Instantiate(stickerObject7, (camPos + (camDirection - spawnPos)), camRotation); //cria a sticker 7
				cur.transform.SetParent(leafAnchor.transform);
				placeSticker = false;
			}
			else if (stickerType == 8)
			{
				GameObject cur = Instantiate(stickerObject8, (camPos + (camDirection - spawnPos)), camRotation); //cria a sticker 8
				cur.transform.SetParent(leafAnchor.transform);
				placeSticker = false;
			}
			else if (stickerType == 9)
			{
				GameObject cur = Instantiate(stickerObject9, (camPos + (camDirection - spawnPos)), camRotation); //cria a sticker 9
				cur.transform.SetParent(leafAnchor.transform);
				placeSticker = false;
			}
			else if (stickerType == 10)
			{
				GameObject cur = Instantiate(stickerObject10, (camPos + (camDirection - spawnPos)), camRotation); //cria a sticker 10
				cur.transform.SetParent(leafAnchor.transform);
				placeSticker = false;
			}
			else if (stickerType == 11)
			{
				GameObject cur = Instantiate(stickerObject11, (camPos + (camDirection - spawnPos)), camRotation); //cria a sticker 11
				cur.transform.SetParent(leafAnchor.transform);
				placeSticker = false;
			}
			else if (stickerType == 12)
			{
				GameObject cur = Instantiate(stickerObject12, (camPos + (camDirection - spawnPos2)), camRotation); //cria a sticker 12
				cur.transform.SetParent(leafAnchor.transform);
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