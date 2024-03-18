using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ARZoneButton : UI_Popup
{
    enum Buttons
    {
        // NavButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BaseScene.SceneType = Define.SceneType.ARZone;

        GameObject header = GameObject.Find("Header");

        if (header != null) header.GetComponent<UI_HeaderButton>().UpdateTitle();
        else if(header == null) Managers.UI.ShowSceneUI<UI_Scene>("Header");

        // BindButton(typeof(Buttons));

        // BindEvent(GetButton((int)Buttons.NavButton).gameObject, OnClickNavButton);

        return true;
    }
}
