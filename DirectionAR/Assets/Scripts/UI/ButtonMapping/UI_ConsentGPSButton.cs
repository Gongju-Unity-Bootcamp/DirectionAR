using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class UI_ConsentGPSButton : UI_Popup
{
    enum Buttons
    { 
        AllowButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.AllowButton).gameObject, OnClickAllowButton);

        return true;
    }

    void OnClickAllowButton()
    {
        bool GPSPermission = Permission.HasUserAuthorizedPermission(Permission.FineLocation);
        
        if(!GPSPermission)
        {
            StartCoroutine(SetPermission());
            ClosePopupUI();
        }
        else
        {
            ClosePopupUI();
        }
    }

    IEnumerator SetPermission()
    {
        bool camPermission = Permission.HasUserAuthorizedPermission(Permission.Camera);

        if (!camPermission)
        {
            try
            {
#if UNITY_ANDROID
                using (var unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                using (AndroidJavaObject currentActivityObject = unityClass.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    string packageName = currentActivityObject.Call<string>("getPackageName");

                    using (var uriClass = new AndroidJavaClass("android.net.Uri"))
                    using (AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromParts", "package", packageName, null))
                    using (var intentObject = new AndroidJavaObject("android.content.Intent", "android.settings.APPLICATION_DETAILS_SETTINGS", uriObject))
                    {
                        intentObject.Call<AndroidJavaObject>("addCategory", "android.intent.category.DEFAULT");
                        intentObject.Call<AndroidJavaObject>("setFlags", 0x10000000);
                        currentActivityObject.Call("startActivity", intentObject);
                    }
                }
#endif
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            yield return new WaitForSeconds(1f);

            yield return new WaitUntil(() => Application.isFocused == true);
        }
    }
}
