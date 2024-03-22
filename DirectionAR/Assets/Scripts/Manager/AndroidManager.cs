using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class AndroidManager : MonoBehaviour
{
    public static AndroidManager _instance { get; private set; }

    public void Init()
    {
        _instance = Utils.GetOrAddComponent<AndroidManager>(Root);
        _instance.transform.SetParent(GameObject.Find("@Managers").transform);
    }

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@AndroidManager");
            if (root == null)
                root = new GameObject { name = "@AndroidManager" };

            return root;
        }
    }

    private float backButtonPressTime = 0f;
    private const float backButtonPressInterval = 2f;

    void OnGUI()
    {
        Event e = Event.current;

#if UNITY_ANDROID
        if (BaseScene.SceneType != Define.SceneType.Main)
        {
            GameObject go = GameObject.Find("Header");

            if (go == null) return;

            if (e.type == EventType.KeyUp && e.keyCode == KeyCode.Escape)
            {
                if (Managers.UI._popupStack.Count < 2)
                {
                    Managers.UI.CloseAllPopupUI();
                    Managers.Resource.Destroy(go);
                    BaseScene.SceneType = Define.SceneType.Main;

                    return;
                }

                Managers.UI.ClosePopupUI();

                go.GetComponent<UI_HeaderButton>().UpdatePrevTitle();

                return;
            }
        }
        if (BaseScene.SceneType == Define.SceneType.Main)
        {
            if (e.type == EventType.KeyUp && e.keyCode == KeyCode.Escape)
            {
                if (Time.time - backButtonPressTime > backButtonPressInterval)
                {
                    backButtonPressTime = Time.time;
                    ShowAndroidToastMessage($"'뒤로'버튼을 한번 더 누르시면 종료됩니다.");
                }
                else
                {
                    // 두 번째로 뒤로가기 버튼이 눌렸을 때
                    Application.Quit();
                }
            }
        }
#endif
    }

    public void ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaObject toastClass = new AndroidJavaObject("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject context = unityActivity.Call<AndroidJavaObject>("getApplicationContext");
                AndroidJavaObject textToShow = new AndroidJavaObject("java.lang.String", message);
                toastClass.CallStatic<AndroidJavaObject>("makeText", context, textToShow, toastClass.GetStatic<int>("LENGTH_SHORT")).Call("show");
            }));
        }
    }

    public bool CheckPerm(string _permission)
    {
        return Permission.HasUserAuthorizedPermission(_permission);
    }

    public IEnumerator SetPermission(string _permission)
    {
        if (!CheckPerm(_permission))
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
