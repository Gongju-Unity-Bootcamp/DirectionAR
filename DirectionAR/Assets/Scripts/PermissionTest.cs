using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class PermissionTest : MonoBehaviour
{
    private void TakeScreenShot()
    {
#if UNITY_ANDROID
        CheckAndroidPermissionAndDo(Permission.ExternalStorageWrite, () => Test());
#else
        Test();
#endif
    }

#if UNITY_ANDROID
    private void CheckAndroidPermissionAndDo(string permission, Action actionIfPermissionGranted)
    {

        if (!Permission.HasUserAuthorizedPermission(permission))
        {
            PermissionCallbacks _permissionCallBacks = new PermissionCallbacks();
            _permissionCallBacks.PermissionGranted += context => Debug.Log($"{context} ����");
            _permissionCallBacks.PermissionGranted += context => actionIfPermissionGranted();

            _permissionCallBacks.PermissionDenied += context => Debug.Log($"{context} ����");

            _permissionCallBacks.PermissionDeniedAndDontAskAgain += context => Debug.Log($"{context} �ٽ� ���� ����");

            Permission.RequestUserPermission(permission, _permissionCallBacks);
        }
        else
        {
            actionIfPermissionGranted();
        }
    }
#endif

#if UNITY_ANDROID
    private void Test()
    {
        Debug.Log("ScreenShot");
    }
#endif

    void Start()
    {
        TakeScreenShot();
    }

    
    void Update()
    {
        
    }
}
