using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FooterButton : UI_Scene
{
    enum Buttons
    {
        HomeButton,
        NavButton,
        ARZoneButton,
        StreetButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.HomeButton).gameObject, OnClickHomeButton);
        BindEvent(GetButton((int)Buttons.NavButton).gameObject, OnClickNavButton);
        BindEvent(GetButton((int)Buttons.ARZoneButton).gameObject, OnClickARZoneButton);
        BindEvent(GetButton((int)Buttons.StreetButton).gameObject, OnClickStreetButton);

        return true;
    }

    void OnClickHomeButton()
    {
        BaseScene.SceneType = Define.SceneType.Main;

        Managers.UI.CloseAllPopupUI();
        Managers.UI.ShowPopupUI<UI_Popup>("Main");
    }

    void OnClickNavButton()
    {
        BaseScene.SceneType = Define.SceneType.Navigation;

        Managers.UI.CloseAllPopupUI();
        Managers.UI.ShowPopupUI<UI_Popup>("Header");
    }

    void OnClickARZoneButton()
    {
        BaseScene.SceneType = Define.SceneType.ARZone;

        Managers.UI.CloseAllPopupUI();
        Managers.UI.ShowPopupUI<UI_Popup>("ARZoneMenu");
        Managers.UI.ShowPopupUI<UI_Popup>("Header");
    }

    void OnClickStreetButton()
    {
        Debug.Log("OnClickStreetButton");
        BaseScene.SceneType = Define.SceneType.Street;

        Managers.UI.CloseAllPopupUI();
        Managers.UI.ShowPopupUI<UI_Popup>("Header");
    }
}
