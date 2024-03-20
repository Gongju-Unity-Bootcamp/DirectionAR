using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class UI_StreetButton : UI_Popup
{
    enum Buttons
    {
        // NavButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BaseScene.SceneType = Define.SceneType.Street;

        GameObject header = GameObject.Find("Header");

        if (header != null) header.GetComponent<UI_HeaderButton>().UpdateTitle();
        else if (header == null) Managers.UI.ShowSceneUI<UI_Scene>("Header");

        // BindButton(typeof(Buttons));

        // BindEvent(GetButton((int)Buttons.NavButton).gameObject, OnClickNavButton);

        return true;
    }
}
