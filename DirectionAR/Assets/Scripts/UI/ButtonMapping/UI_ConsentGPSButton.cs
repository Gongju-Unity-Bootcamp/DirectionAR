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
        CloseButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.AllowButton).gameObject, OnClickAllowButton);
        BindEvent(GetButton((int)Buttons.CloseButton).gameObject, OnClickCloseButton);

        return true;
    }

    void OnClickAllowButton()
    {
        if(!Managers.Android.CheckPerm(Permission.FineLocation))
        {
            StartCoroutine(Managers.Android.SetPermission(Permission.FineLocation));
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
