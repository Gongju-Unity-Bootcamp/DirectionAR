using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FooterButton : UI_Scene
{
    enum Buttons
    {
        HomeButton,
        NavButton,
        ExButton,
        StreetButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.HomeButton).gameObject, OnClickHomeButton);
        BindEvent(GetButton((int)Buttons.NavButton).gameObject, OnClickNavButton);
        BindEvent(GetButton((int)Buttons.ExButton).gameObject, OnClickARExButton);
        BindEvent(GetButton((int)Buttons.StreetButton).gameObject, OnClickStreetButton);

        return true;
    }

    void OnClickHomeButton()
    {
        BaseScene.SceneType = Define.Scene.Main;

        Managers.UI.CloseAllPopupUI();
        Managers.UI.ShowPopupUI<UI_Popup>("Main");
    }

    void OnClickNavButton()
    {
        BaseScene.SceneType = Define.Scene.Navigation;

        Managers.UI.CloseAllPopupUI();
        Managers.UI.ShowPopupUI<UI_Popup>("Header");
    }

    void OnClickARExButton()
    {
        BaseScene.SceneType = Define.Scene.ARZone;

        Managers.UI.CloseAllPopupUI();
        Managers.UI.ShowPopupUI<UI_Popup>("ARDocentMenu");
        Managers.UI.ShowPopupUI<UI_Popup>("Header");
    }

    void OnClickStreetButton()
    {
        Debug.Log("OnClickStreetButton");

        Managers.UI.CloseAllPopupUI();
    }
}
