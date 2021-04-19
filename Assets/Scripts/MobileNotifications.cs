using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class MobileNotifications : MonoBehaviour
{
    private static readonly string channelId = "channel_id";
    private static readonly string channelName = "Notification Channel";
    private static readonly string channelDescription = "Tree localisation";


    private static int notifID;
    void Start()
    {
        // Create the Notification Channel
        AndroidNotificationChannel channel = new AndroidNotificationChannel(channelId, channelName, channelDescription, Importance.Default);
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        AndroidNotification notification = new AndroidNotification
        {
            Title = "Start",
            Text = "Welcome",
            FireTime = System.DateTime.Now.AddSeconds(7)
        };

        //Send Notification
        AndroidNotificationCenter.SendNotification(notification, channelId);

    }

    public static void CreateNotification(string title, string text)
    {
        // Clear any previous notifications
        //AndroidNotificationCenter.CancelAllDisplayedNotifications();

        // Create Notification
        AndroidNotification notification = new AndroidNotification
        {
            Title = title,
            Text = text,
            FireTime = System.DateTime.Now.AddSeconds(5)
        };

        notification.FireTime = System.DateTime.Now.AddSeconds(5);

        //Send Notification
        notifID = AndroidNotificationCenter.SendNotification(notification, channelId);
    }

    private void Update()
    {
       
    }

    /***
     * Debug Function
     * Unhide Variable to use
     * */
    [HideInInspector]
    public UnityEngine.UI.Text DebugText;
    private void CheckNotificationStatus()
    {
        NotificationStatus status = AndroidNotificationCenter.CheckScheduledNotificationStatus(notifID);
        switch (status)
        {
            case NotificationStatus.Scheduled:
                {
                    DebugText.text = "Scheduled";
                    break;
                }
            case NotificationStatus.Delivered:
                {
                    DebugText.text = "Delivered";
                    break;
                }
            case NotificationStatus.Unknown:
                {
                    DebugText.text = "Unknown";
                    break;
                }
            default:
                {
                    DebugText.text = "DEFAULT";
                    break;
                }
        }
    }
}
