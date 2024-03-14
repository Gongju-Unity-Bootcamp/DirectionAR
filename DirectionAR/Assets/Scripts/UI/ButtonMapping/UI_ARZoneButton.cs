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

        Managers.UI.ShowPopupUI<UI_Popup>("Header");

        // BindButton(typeof(Buttons));

        // BindEvent(GetButton((int)Buttons.NavButton).gameObject, OnClickNavButton);

        return true;
    }
}
