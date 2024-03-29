using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class UI_ConsentCallButton : UI_Popup
{
    enum Buttons
    {
        AllowButton,
        CloseButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));

        BaseScene.SceneType = Define.SceneType.Call;

        BindEvent(GetButton((int)Buttons.AllowButton).gameObject, OnClickAllowButton);
        BindEvent(GetButton((int)Buttons.CloseButton).gameObject, OnClickCloseButton);

        return true;
    }

    void OnClickAllowButton()
    {
        if(!Managers.Android.CheckPerm(Define.Permission.Call))
        {
            StartCoroutine(Managers.Android.SetPermission(Define.Permission.Call));
            ClosePopupUI();
        }
        else
        {
            ClosePopupUI();
        }
    }

    void OnClickCloseButton()
    {
        ClosePopupUI();
    }
}
