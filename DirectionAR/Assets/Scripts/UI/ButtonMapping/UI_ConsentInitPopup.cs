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
        bool camPermission = Permission.HasUserAuthorizedPermission(Permission.Camera);
        bool gpsPermission = Permission.HasUserAuthorizedPermission(Permission.FineLocation);
        
        if (!camPermission)
            Permission.RequestUserPermission(Permission.Camera);
        if (!gpsPermission)
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        
        SceneManager.LoadScene((int)Define.SceneNum.Awake);
    }
}
