using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class ARCameraController : MonoBehaviour
{
    public Button CameraButton;
    public Button GalleryButton;

    public string folderName = "KCCI-C";
    public string fileName = "AR 체험존";
    public string extName = "png";

    private bool _willTakeScreenShot = false;

    private Texture2D _imageTexture;

    private string RootPath
    {
        get
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            return Application.dataPath;
#elif UNITY_ANDROID
            return $"/storage/emulated/0/DCIM/{Application.productName}/";
            //return Application.persistentDataPath;
#endif
        }
    }
    private string FolderPath => $"{RootPath}/{folderName}";
    private string TotalPath => $"{FolderPath}/{fileName}_{DateTime.Now.ToString("MMdd_HHmmss")}.{extName}";

    private string lastSavedPath;

    private void Awake()
    {
        CameraButton.onClick.AddListener(TakeScreenShot);
        GalleryButton.onClick.AddListener(ShowGallery);
    }

    private void TakeScreenShot()
    {
        bool galleryPermission = Managers.Android.CheckPerm(Define.Permission.Media);

#if UNITY_ANDROID
        if (!galleryPermission)
        {
            Managers.UI.ShowPopupUI<UI_Popup>("ConsentMediaPopUp");
        }
        else
        {
            _willTakeScreenShot = true;
            OnPostRender();
        }
#endif
    }

    private void ShowGallery()
    {
        Managers.Android.ShowAndroidToastMessage("준비 중 입니다. :)");
    }

    private void OnPostRender()
    {
        if(_willTakeScreenShot)
        {
            CaptureScreenAndSave();
            ReadScreenShotAndShow(GalleryButton.image);
            _willTakeScreenShot = false;
        }
    }

    private void CaptureScreenAndSave()
    {
        string totalPath = TotalPath;

        Texture2D screenTex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        Rect area = new Rect(0f, 0f, Screen.width, Screen.height);

        screenTex.ReadPixels(area, 0, 0);

        bool succeeded = true;
        try
        {
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            File.WriteAllBytes(totalPath, screenTex.EncodeToPNG());
        }
        catch (Exception e)
        {
            succeeded = false;
            Debug.LogWarning($"Screen Shot Save Failed : {totalPath}");
            Debug.LogWarning(e);
        }

        Destroy(screenTex);

        if (succeeded)
        {
            Debug.Log($"Screen Shot Saved : {totalPath}");
            lastSavedPath = totalPath;
        }

        RefreshAndroidGallery(totalPath);
    }

    [System.Diagnostics.Conditional("UNITY_ANDROID")]
    private void RefreshAndroidGallery(string imageFilePath)
    {
#if !UNITY_EDITOR
        AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass classUri = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject objIntent = new AndroidJavaObject("android.content.Intent", new object[2]
        { "android.intent.action.MEDIA_SCANNER_SCAN_FILE", classUri.CallStatic<AndroidJavaObject>("parse", "file://" + imageFilePath) });
        objActivity.Call("sendBroadcast", objIntent);
#endif
    }

    private void ReadScreenShotAndShow(Image destination)
    {
        string folderPath = FolderPath;
        string totalPath = lastSavedPath;

        if (Directory.Exists(folderPath) == false)
        {
            Debug.LogWarning($"{folderPath} 폴더가 존재하지 않습니다.");
            return;
        }
        if (File.Exists(totalPath) == false)
        {
            Debug.LogWarning($"{totalPath} 파일이 존재하지 않습니다.");
            return;
        }

        if (_imageTexture != null)
            Destroy(_imageTexture);
        if (destination.sprite != null)
        {
            Destroy(destination.sprite);
            destination.sprite = null;
        }

        try
        {
            byte[] texBuffer = File.ReadAllBytes(totalPath);

            _imageTexture = new Texture2D(1, 1, TextureFormat.RGB24, false);
            _imageTexture.LoadImage(texBuffer);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"스크린샷 파일을 읽는 데 실패하였습니다.");
            Debug.LogWarning(e);
            return;
        }

        Rect rect = new Rect(0, 0, _imageTexture.width, _imageTexture.height);
        Sprite sprite = Sprite.Create(_imageTexture, rect, Vector2.one * 0.5f);
        destination.sprite = sprite;
    }
}
