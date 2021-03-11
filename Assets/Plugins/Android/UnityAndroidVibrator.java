package com.DefaultCompany.MobileLocationGame;

import android.os.Vibrator;
import com.unity3d.player.UnityPlayer;

public class UnityAndroidVibrator 
{
    private static UnityAndroidVibrator _instance;

    public UnityAndroidVibrator() {
    }

    public void VibrateForGivenDuration(int duration) {
        Vibrator vibs = (Vibrator)UnityPlayer.currentActivity.getApplicationContext().getSystemService("vibrator");
        vibs.vibrate((long)duration);
    }

    public void StopVibrate() {
        Vibrator vibs = (Vibrator)UnityPlayer.currentActivity.getApplicationContext().getSystemService("vibrator");
        vibs.cancel();
    }

    public void CustomVibrate(long[] Pattern) {
        Vibrator vibs = (Vibrator)UnityPlayer.currentActivity.getApplicationContext().getSystemService("vibrator");
        vibs.vibrate(Pattern, -1);
    }

    public static UnityAndroidVibrator instance() 
    {
        if (_instance == null) 
        {
            _instance = new UnityAndroidVibrator();
        }

        return _instance;
    }
}