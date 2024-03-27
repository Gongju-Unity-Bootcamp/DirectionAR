using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class AndroidManager : MonoBehaviour
{
    public static AndroidManager _instance { get; private set; }

    [Header("Naver API")]
    public string clientID = "fnz8tjmtru";
    public string secretKey = "CWIZPpnOZsShJzKKtH9u7uTB8fqdhPXGUBaDPbk7";

    public void Init()
    {
        _instance = Utils.GetOrAddComponent<AndroidManager>(Root);
        _instance.transform.SetParent(GameObject.Find("@Managers").transform);
    }

    void Update()
    {
        if (BaseScene.SceneType != Define.SceneType.ARNavigation)
        {
            StopAllCoroutines();
            return;
        }


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
                    ShowAndroidToastMessage($"'�ڷ�'��ư�� �ѹ� �� �����ø� ����˴ϴ�.");
                }
                else
                {
                    // �� ��°�� �ڷΰ��� ��ư�� ������ ��
                    Application.Quit();
                }
            }
        }
#endif
    }

    public void ShowAndroidToastMessage(string message)
    {
        Debug.Log(message);
#if UNITY_ANDROID
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
#endif
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

    public float delay;
    public float maxtime = 5f;

    private int frame = 0;

    public IEnumerator LoadGPS()
    {
        if (!CheckPerm(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            while (!CheckPerm(Permission.FineLocation))
            {
                yield return null;
            }
        }

        Input.location.Start();

        if (!Input.location.isEnabledByUser)
        {
            ShowAndroidToastMessage("Gps ��ġ�� ��������.");
        }

        while (Input.location.status == LocationServiceStatus.Initializing && delay < maxtime)
        {
            yield return new WaitForSeconds(1.0f);
            delay++;
        }

        if (Input.location.status == LocationServiceStatus.Failed || Input.location.status == LocationServiceStatus.Stopped)
        {
            ShowAndroidToastMessage("��ġ������ �������µ� ������.");
        }

        if (delay >= maxtime)
        {
            ShowAndroidToastMessage("�����ð� �ʰ�.");
        }

        yield return new WaitForSeconds(5.0f);
    }
}
