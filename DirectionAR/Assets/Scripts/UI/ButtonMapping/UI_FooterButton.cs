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
        GameObject header = GameObject.Find("Header");

        if (header != null) Managers.Resource.Destroy(header);

        Managers.UI.CloseAllPopupUI();

        BaseScene.SceneType = Define.SceneType.Main;
    }

    void OnClickNavButton()
    {

        Managers.UI.ShowPopupUI<UI_Popup>("Navigation");
    }

    void OnClickARZoneButton()
    {
        GameObject arZoneButton = GameObject.Find("ARZoneButton");

        if (arZoneButton != null)
        {
            Managers.ARMenu._content = null;
        }

        Managers.UI.ShowPopupUI<UI_Popup>("ARZoneMenu");
    }

    void OnClickStreetButton()
    {
        Managers.Android.ShowAndroidToastMessage("OnClickStreetButton");
        Managers.UI.ShowPopupUI<UI_Popup>("Street");
    }
}
