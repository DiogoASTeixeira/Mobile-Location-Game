using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using AndroidNativeCore;


public class ShowScreenShot : MonoBehaviour
{

	[SerializeField]
	GameObject canvas;
	string[] files = null;
	int whichScreenShotIsShown = 0;
	private Texture2D CapturedImage;

	// Use this for initialization
	void Start()
	{
		files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
		if (files.Length > 0)
		{
			GetPictureAndShowIt();
		}
	}

	void GetPictureAndShowIt()
	{
		string pathToFile = files[whichScreenShotIsShown];
		Texture2D texture = GetScreenshotImage(pathToFile);
		Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
			new Vector2(0.5f, 0.5f));
		canvas.GetComponent<Image>().sprite = sp;
		CapturedImage = texture;
	}

	Texture2D GetScreenshotImage(string filePath)
	{
		Texture2D texture = null;
		byte[] fileBytes;
		if (File.Exists(filePath))
		{
			fileBytes = File.ReadAllBytes(filePath);
			texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
			texture.LoadImage(fileBytes);
		}
		return texture;
	}

	public void NextPicture()
	{
		if (files.Length > 0)
		{
			whichScreenShotIsShown += 1;
			if (whichScreenShotIsShown > files.Length - 1)
				whichScreenShotIsShown = 0;
			GetPictureAndShowIt();
		}
	}

	public void PreviousPicture()
	{
		if (files.Length > 0)
		{
			whichScreenShotIsShown -= 1;
			if (whichScreenShotIsShown < 0)
				whichScreenShotIsShown = files.Length - 1;
			GetPictureAndShowIt();
		}
	}

	//Android Save to Gallery Code
	protected const string MEDIA_STORE_IMAGE_MEDIA = "android.provider.MediaStore$Images$Media";
	protected static AndroidJavaObject m_Activity;

	protected static string SaveImageToGallery(Texture2D a_Texture, string a_Title, string a_Description)
	{
		using (AndroidJavaClass mediaClass = new AndroidJavaClass(MEDIA_STORE_IMAGE_MEDIA))
		{
			using (AndroidJavaObject contentResolver = Activity.Call<AndroidJavaObject>("getContentResolver"))
			{
				AndroidJavaObject image = Texture2DToAndroidBitmap(a_Texture);
				return mediaClass.CallStatic<string>("insertImage", contentResolver, image, a_Title, a_Description);
			}
		}
	}

	protected static AndroidJavaObject Texture2DToAndroidBitmap(Texture2D a_Texture)
	{
		byte[] encodedTexture = a_Texture.EncodeToPNG();
		using (AndroidJavaClass bitmapFactory = new AndroidJavaClass("android.graphics.BitmapFactory"))
		{
			return bitmapFactory.CallStatic<AndroidJavaObject>("decodeByteArray", encodedTexture, 0, encodedTexture.Length);
		}
	}

	protected static AndroidJavaObject Activity
	{
		get
		{
			if (m_Activity == null)
			{
				AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				m_Activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			}
			return m_Activity;
		}
	}
	//End of Android Save to Gallery Code


	public void SaveScreenShotToGallery()
	{
		string pathToFile = files[whichScreenShotIsShown];

		//Texture2D tex = GetScreenshotImage(pathToFile);
		string path = SaveImageToGallery(CapturedImage, "Name", "Description");
		Debug.Log("Saved to Gallery");
		Toast.make("Imagem guardada na Galeria", Toast.LENGTH_SHORT);
		//Destroy(tex);
	}

	public void ShareThisImage()
	{
		string pathToFile = files[whichScreenShotIsShown];
		
		AndroidCore.share("Experimenta a App FloRA, cria e descobre em realidade aumentada", CapturedImage);
		Toast.make("Imagem Partilhada", Toast.LENGTH_SHORT);

		//Destroy(tex);


	}



}
