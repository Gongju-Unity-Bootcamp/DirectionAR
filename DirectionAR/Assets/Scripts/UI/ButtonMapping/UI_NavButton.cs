using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_NavButton : UI_Popup
{
    enum Buttons
    {
        // NavButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BaseScene.SceneType = Define.SceneType.Navigation;

        GameObject header = GameObject.Find("Header");

        if (header != null) Managers.Resource.Destroy(header);

        Managers.UI.ShowSceneUI<UI_Scene>("Header");

        // BindButton(typeof(Buttons));

        // BindEvent(GetButton((int)Buttons.NavButton).gameObject, OnClickNavButton);

        return true;
    }
}
