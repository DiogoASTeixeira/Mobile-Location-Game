using AndroidNativeCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Android : MonoBehaviour {

    public Texture2D NotifiBigIcon;
    public Text flashText;
    public Text DeviceInfo;
    public Image resultImage;
    public GameObject devicePanel;
    public GameObject inputFieldObject;
    public InputField url;
    private long[] vibratePattren = {0,100,1000 };
    private bool flashOn,isInputFieldActive = false;
    private NetworkManager networkManager;
    private NotificationManager notifyManager;
    private NotificationManager.Channel channel;

    Flash flash;
    AndroidFileManager fileManager;

    void Start()
    {
        flash = new Flash();
        networkManager = new NetworkManager();
        notifyManager = new NotificationManager();
        channel = new NotificationManager.Channel();
        channel.id = "notification_0";
        channel.name = "Game Notification";
        channel.importance = NotificationManager.IMPORTANCE_MAX;
        channel.lockScreenVisibility = NotificationManager.VISIBILITY_PUBLIC;
        channel.enableLights = true;
        channel.enableVibration = true;
        channel.enableBadge = true;
        channel.lightColor = "#ffff";
        channel.description = "Notifications from Android Native Core Unity3d Plugin";
        notifyManager.createChannel(channel);
        fileManager = new AndroidFileManager();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AlertDialog alert = new AlertDialog();
            alert.build(AlertDialog.THEME_HOLO_DARK)
           .setTitle("Exit..?")
           .setMessage("Are you sure to exit..?")
           .setIcon("icon_warning")
           .setNegativeButtion("Return", () => { alert.dismiss(); })
           .setPositiveButtion("Exit", () => { Application.Quit(); })
           .show();
        }
            
    }

    public void deviceInfo()
    {
        devicePanel.SetActive(true);
        string info ="---Device Info---@Manfacuturer: "+Device.manfacture()+"@"+ "Device Id: "+Device.androidID()+"@" +"Model: "+Device.model()+"@"+"Android Version Name: "+Device.androidVersionName()+"@"+"Android version code: "+Device.androidVersionCode()+"@"+"Security Patch: "+Device.securityPatch()+"@ @"+
            "@---Wifi Info--@Is wifi Enabled: " + networkManager.isWifiEnabled()+"@Is wifi Connected: "+networkManager.isWifiConnected()+"@IP Address: "+networkManager.IpAddress()+"@Mac Address: "+networkManager.getMacAddress()+"@@---Telephone Info---@Is Mobile data enabled: "+networkManager.isMobileDataEnabled()+"@Telephone Id: "+networkManager.getTelephoneId();
        string device_info = info.Replace("@", System.Environment.NewLine);
        DeviceInfo.text = device_info;
    }

    public void saveScreenShot(){

        StartCoroutine(RecordFrame());
    }
    IEnumerator RecordFrame()
    {
        yield return new WaitForEndOfFrame();
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
        var texture = ScreenCapture.CaptureScreenshotAsTexture();
        byte[] data = texture.EncodeToPNG();
        if (!fileManager.isFileExits("/AndroidNativeCore/ScreenShots/"))
        {
            fileManager.makeDirectory("/AndroidNativeCore/ScreenShots/");
        }     
        fileManager.writeFile("/AndroidNativeCore/ScreenShots/", "screenshot-"+timestamp +".png", data);
        Toast.make("Screen shot saved on internel storage AndroidNativeCore/ScreenShots/screenshot-" + timestamp,Toast.LENGTH_SHORT);
        UnityEngine.Object.Destroy(texture);
    }

    public void toast()
    {
        Toast.make("hi android native core", Toast.LENGTH_SHORT);
    }

    public void writeFile()
    {
        string textData = "abcdfghijklmnopqrstuvwxyz";
        byte[] bytes = Encoding.ASCII.GetBytes(textData);
        fileManager.makeDirectory("/AndroidNativeCore/text/");
        fileManager.writeFile("/AndroidNativeCore/text", "textFile.txt", bytes);
        Toast.make("text file saved on Internel storage AndroidNativeCore/text/textFile.txt", Toast.LENGTH_SHORT);
    }
    public void readFile()
    {
        if (fileManager.isFileExits("/AndroidNativeCore/text/textFile.txt"))
        {
            byte[] data = fileManager.readFile("/AndroidNativeCore/text/textFile.txt");
            string text = Encoding.ASCII.GetString(data);
            Toast.make(text, Toast.LENGTH_SHORT);
        }
        
    }

    public void AlertGeneral()
    {
        AlertDialog alert = new AlertDialog();
        alert.build(AlertDialog.THEME_TRADITIONAL)
       .setTitle("Hi")
       .setIcon("alert_icon")
       .setMessage("This is traditional Alert dialog")
       .setNegativeButtion("Cansel", () => { Toast.make("Negative btn clicked",Toast.LENGTH_SHORT); alert.dismiss(); })
       .setPositiveButtion("Ok", () => { Toast.make("Positive btn clicked", Toast.LENGTH_SHORT); alert.dismiss(); })
       .show();   
    }
    public void AlertHoloDark()
    {
        AlertDialog alert = new AlertDialog();
        alert.build(AlertDialog.THEME_HOLO_DARK)
       .setTitle("Hi")
       .setMessage("This is Holo Dark Alert dialog")
       .setIcon("alert_icon")
       .setNegativeButtion("Cansel", () => { Debug.Log("Negitive btn clicked"); Toast.make("Negative btn clicked", Toast.LENGTH_SHORT); alert.dismiss(); })
       .setPositiveButtion("Ok", () => { Toast.make("Positive btn clicked", Toast.LENGTH_SHORT); alert.dismiss(); })
       .show();
    }
    public void AlertHoloLight()
    {
        AlertDialog alert = new AlertDialog();
        alert.build(AlertDialog.THEME_HOLO_LIGHT)
       .setTitle("Hi")
       .setMessage("This is Holo Light Alert dialog")
       .setNegativeButtion("Cansel", () => { Toast.make("Negative btn clicked", Toast.LENGTH_SHORT); alert.dismiss(); })
       .setPositiveButtion("Ok", () => { Toast.make("Positive btn clicked", Toast.LENGTH_SHORT); alert.dismiss(); })
       .show();
    }
    public void AlertDeviceDark()
    {
        AlertDialog alert = new AlertDialog();
        alert.build(AlertDialog.THEME_DEVICE_DEFAULT_DARK)
       .setTitle("Hi")
       .setMessage("This is Device default Dark Alert dialog")
       .setNegativeButtion("Cansel", () => { Toast.make("Negative btn clicked", Toast.LENGTH_SHORT); alert.dismiss(); })
       .setPositiveButtion("Ok", () => { Toast.make("Positive btn clicked", Toast.LENGTH_SHORT); alert.dismiss(); })
       .show();
    }
    public void AlertDeviceLight()
    {
        AlertDialog alert = new AlertDialog();
        alert.build(AlertDialog.THEME_DEVICE_DEFAULT_LIGHT)
       .setTitle("Hi")
       .setMessage("This is Device default light Alert dialog")
       .setNegativeButtion("Cansel", () => { Toast.make("Negative btn clicked", Toast.LENGTH_SHORT); alert.dismiss(); })
       .setPositiveButtion("Ok", () => { Toast.make("Positive btn clicked", Toast.LENGTH_SHORT); alert.dismiss(); })
       .show();
    }

    public void NotificationBigImage()
    {
        NotificationManager.Builder notfi = new NotificationManager.Builder();
        notfi.Create(notifyManager,"notification_0")
            .setContentTitle("Android Native Core")
            .setContentText("this notification with bigImage")
            .setAutoCansel(true)
            .setIcon("android_native_core")
            .setSound("notification_sound")
            .setGroup("Samples",true)
            .setPriority(NotificationManager.PRIORITY_MAX)
            .setGroup("GreatDeals",true)
            .setBigImage(NotifiBigIcon)
            .notify(1);
    }

    public void toggleInputField()
    {
        if (!isInputFieldActive)
        {
            inputFieldObject.SetActive(true);
            isInputFieldActive = true;
        }
        else
        {
            inputFieldObject.SetActive(false);
            isInputFieldActive = false;
        } 

        
    }
    public void NotificationBigUrl()
    {
        Debug.Log(channel.id);
        inputFieldObject.SetActive(false);    
        NotificationManager.Builder notificationBuilder = new NotificationManager.Builder();
        notificationBuilder.Create(notifyManager, "notification_0")
            .setIcon("android_native_core")
            .setContentTitle("Android Native Core")
            .setBigText("this is notification image from url")
            .setDefautlSound()
            .setAutoCansel(true)
            .setPriority(NotificationManager.PRIORITY_MAX)
            .setGroup("GreatDeals", true)
            .setBigImage(url.text)
            .notify(2);
    }
    public void deleteChannel()
    {
        notifyManager.deleteChannel("notification_0");
    }

    public void deviceFlash()
    {
        if (!flashOn)
        {
            flashOn = true;
            flash.setFlashEnable(true);
            flashText.text = "Flash Off";
        }
        else
        {
            flashOn = false;
            flash.setFlashEnable(false);
            flashText.text = "Flash On";
        }
    }

    public void vibrate()
    {
        Vibrator.Vibrate(500);
    }
    public void vibratorPattren()
    {
        Vibrator.Vibrate(vibratePattren,0);
    }
    public void vibrateCansel()
    {
        Vibrator.Cansel();
    }

    public void datePicker()
    {
        Pickers Pickers = new Pickers();
        Pickers.pickDate(8, 11, 2018, (int d, int m, int y) => { Toast.make("Picked date: "+d.ToString()+"/"+m.ToString()+"/"+y.ToString(),Toast.LENGTH_SHORT); });
    }
    public void TimePicker()
    {
        Pickers Pickers = new Pickers();
        Pickers.pickTime(8, 46, false, (int h, int m) => { Toast.make("Picked Time: " + h.ToString() + ":" + m.ToString(), Toast.LENGTH_SHORT); });
    }
    public void dail()
    {
        AndroidCore.dial("123456789");
    }
    public void Call()
    {
        AndroidCore.makeCall("123456789");
    }
    public void message()
    {
        AndroidCore.composeMessage("123456789", "This message composed from Android Native Core Unity Plugin");
    }
    public void mail()
    {
        AndroidCore.composeMail("user@example.com", "Android Native Core Demo", "This mail composed from Android Native Core Unity Plugin");
    }
    public void share()
    {
        AndroidCore.share("Get Android Native Feature in unity game with Android Native Core", NotifiBigIcon); 
    }

    public void closeDeviceInfo()
    {
        devicePanel.SetActive(false);
    }
    public void openYoutube()
    {
        AndroidCore.openApplicationView("vnd.youtube:kyD0q57zw40");
    }
    public void openSettings()
    {
        Settings settings = new Settings();
        settings.open(Settings.ACTION_WIFI_SETTINGS);
    }
}
