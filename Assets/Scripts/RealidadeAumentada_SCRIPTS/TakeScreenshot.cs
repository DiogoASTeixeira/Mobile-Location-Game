using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakeScreenshot : MonoBehaviour {

	[SerializeField]
	string sceneName;
	[SerializeField]
	GameObject Blink;
	[SerializeField]
	GameObject HideUI;

	public void TakeAShot()
	{
		StartCoroutine ("CaptureIt");
	}

	IEnumerator CaptureIt()
	{
		string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
		string fileName = "Screenshot" + timeStamp + ".png";
		string pathToSave = fileName;
		HideUI.SetActive(false);
		ScreenCapture.CaptureScreenshot(pathToSave);
		yield return new WaitForEndOfFrame();
		Blink.SetActive(true);
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(sceneName);

	}

}
