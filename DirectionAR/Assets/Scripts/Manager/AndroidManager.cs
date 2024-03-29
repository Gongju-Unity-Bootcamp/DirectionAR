using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class AndroidManager : MonoBehaviour
{
    public static AndroidManager _instance { get; private set; }

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
        Event _event = Event.current;

#if UNITY_ANDROID
        if(BaseScene.SceneType == Define.SceneType.Call)
        {
            if(_event.type == EventType.KeyUp && _event.keyCode == KeyCode.Escape)
            {
                Managers.UI.ClosePopupUI();
                BaseScene.SceneType = Define.SceneType.Main;
            }

            return;
        }

        if (BaseScene.SceneType == Define.SceneType.Main || BaseScene.SceneType == Define.SceneType.Consent)
        {
            if (_event.type == EventType.KeyUp && _event.keyCode == KeyCode.Escape)
            {
                if (Time.time - backButtonPressTime > backButtonPressInterval)
                {
                    backButtonPressTime = Time.time;
                    ShowAndroidToastMessage($"'뒤로' 버튼을 한번 더 누르면 종료됩니다.");
                }
                else
                {
                    // 두 번째로 뒤로가기 버튼이 눌렸을 때
                    Application.Quit();
                }
            }
        }

        if (BaseScene.SceneType != Define.SceneType.Main && BaseScene.SceneType != Define.SceneType.ARCamera)
        {
            GameObject go = GameObject.Find("Header");

            if (go == null) return;

            if (_event.type == EventType.KeyUp && _event.keyCode == KeyCode.Escape)
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

        if(BaseScene.SceneType == Define.SceneType.ARCamera)
        {
            if(_event.type == EventType.KeyUp && _event.keyCode == KeyCode.Escape)
            {
                if(Time.time - backButtonPressTime > backButtonPressInterval)
                {
                    backButtonPressTime = Time.time;
                    ShowAndroidToastMessage("'뒤로' 버튼을 한번 더 누르면 종료됩니다.");
                }
                else
                {
                    SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex((int)Define.SceneNum.ARCamera));
                    Scene previousScene = SceneManager.GetSceneByBuildIndex((int)Define.SceneNum.Main);
                    
                    foreach(GameObject gameObject in previousScene.GetRootGameObjects())
                    {
                        gameObject.SetActive(true);
                    }

                    BaseScene.SceneType = Define.SceneType.ARZone;
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
            ShowAndroidToastMessage("Gps 장치가 꺼져있음.");
        }

        while (Input.location.status == LocationServiceStatus.Initializing && delay < maxtime)
        {
            yield return new WaitForSeconds(1.0f);
            delay++;
        }

        if (Input.location.status == LocationServiceStatus.Failed || Input.location.status == LocationServiceStatus.Stopped)
        {
            ShowAndroidToastMessage("위치정보를 가져오는데 실패함.");
        }

        if (delay >= maxtime)
        {
            ShowAndroidToastMessage("지연시간 초과.");
        }

        yield return new WaitForSeconds(5.0f);
    }

    public void EmergencyDialer(string phoneNumber)
    {
        AndroidJavaClass _unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject _unityActivity = _unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        // 다이얼 화면을 열기 위해 DIAL 액션을 사용한 Intent 생성
        AndroidJavaObject _intentObject = new AndroidJavaObject("android.content.Intent", "android.intent.action.DIAL");

        // 전화 번호를 설정하여 setData 메서드 호출
        AndroidJavaClass _uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject _uri = _uriClass.CallStatic<AndroidJavaObject>("parse", "tel:" + phoneNumber);
        _intentObject.Call<AndroidJavaObject>("setData", _uri);

        // startActivity 호출하여 다이얼 화면 열기
        _unityActivity.Call("startActivity", _intentObject);

    }
}
