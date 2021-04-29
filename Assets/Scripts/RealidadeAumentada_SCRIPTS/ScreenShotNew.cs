using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using AndroidNativeCore;

public class ScreenShotNew : MonoBehaviour
{
    [SerializeField]
    string sceneName;
    [SerializeField]
    GameObject Blink;

    //Set your screenshot resolutions
    public int captureWidth = 1920;
    public int captureHeight = 1500;
    // configure with raw, jpg, png, or ppm (simple raw format)
    public enum Format { RAW, JPG, PNG, PPM };
    public Format format = Format.JPG;
    // folder to write output (defaults to data path)
    private string outputFolder;
    // private variables needed for screenshot
    private Rect rect;
    private RenderTexture renderTexture;
    private Texture2D screenShot;
    private bool isProcessing;
    int heightCut;


    //Initialize Directory
    private void Start()
    {
        heightCut = Mathf.RoundToInt(Screen.height * 0.781f);
        Debug.Log(heightCut);
        outputFolder = Application.persistentDataPath + "/Screenshots/";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
            Debug.Log("Save Path will be : " + outputFolder);
        }
    } 

    private string CreateFileName(int width, int height)
    {
        //timestamp to append to the screenshot filename
        string timestamp = System.DateTime.Now.ToString("yyyyMMddTHHmmss");
        // use width, height, and timestamp for unique file 
        var filename = string.Format("{0}/screen_{1}x{2}_{3}.{4}", outputFolder, width, height, timestamp, format.ToString().ToLower());
        // return filename
        return filename;
    }


    public void CaptureScreenshot()
    {
        
        //Capture ScreenSize
        StartCoroutine(CaptureScreenshotCoroutine(Screen.width, heightCut));

        //Capture with New Size
        //StartCoroutine(CaptureScreenshotCoroutine(captureWidth, captureHeight));
    }

    private IEnumerator CaptureScreenshotCoroutine(int width, int height)
    {
        isProcessing = true;
        yield return new WaitForEndOfFrame();
        Texture2D tex = new Texture2D(width, height);
        tex.ReadPixels(new Rect(0, (heightCut/7), width, height), 0, 0);
        tex.Apply();

        yield return tex;

        //string path = SaveImageToGallery(tex, "Name", "Description");

        //save image to apk data folder
        byte[] encodedTexture = tex.EncodeToPNG();
        Destroy(tex);
        System.IO.File.WriteAllBytes(Application.persistentDataPath + "/" + "SavedScreen.png", encodedTexture);
        Debug.Log("Picture has been saved at:\n" + Application.persistentDataPath);
        Handheld.Vibrate();
        //Blink animation
        Blink.SetActive(true);
        //waiting for new scene load
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
        isProcessing = false;
    }

    /*Cleanup
    Destroy(renderTexture);
    renderTexture = null;
    screenShot = null;*/


    public void TakeScreenShot()
    {
        if (!isProcessing)
        {
            CaptureScreenshot();
        }
        else
        {
            Debug.Log("Currently Processing");
        }
    }




}
