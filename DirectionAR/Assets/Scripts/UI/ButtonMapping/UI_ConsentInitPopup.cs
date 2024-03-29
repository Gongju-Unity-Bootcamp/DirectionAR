using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class UI_ConsentInitPopup : UI_Popup
{
    enum Buttons
    {
        StartButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.StartButton).gameObject, OnClickStartButton);

        return true;
    }

    void OnClickStartButton()
    {
        string[] RequiredPermissions = { Permission.Camera, Permission.FineLocation, Define.Permission.Call, Define.Permission.Media};

        Permission.RequestUserPermissions(RequiredPermissions);

        PlayerPrefs.SetInt("isFirstRun", 1);
        SceneManager.LoadScene((int)Define.SceneNum.Awake);
    }
}
