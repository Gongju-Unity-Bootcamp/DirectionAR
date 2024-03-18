using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainButton : UI_Popup
{
    enum Buttons
    {
        NavButton,
        ARZoneButton,
        StreetButton,
        QuitButton,
        EmergencyButton,
        MagnifierButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BaseScene.SceneType = Define.SceneType.Main;

        GameObject header = GameObject.Find("Header");

        if (header != null) Managers.Resource.Destroy(header);

        BindButton(typeof(Buttons));
        
        BindEvent(GetButton((int)Buttons.NavButton).gameObject, OnClickNavButton);
        BindEvent(GetButton((int)Buttons.ARZoneButton).gameObject, OnClickARZoneButton);
        BindEvent(GetButton((int)Buttons.StreetButton).gameObject, OnClickStreetButton);
        BindEvent(GetButton((int)Buttons.QuitButton).gameObject, OnClickQuitButton);
        BindEvent(GetButton((int)Buttons.EmergencyButton).gameObject, OnClickEmergencyButton);
        BindEvent(GetButton((int)Buttons.MagnifierButton).gameObject, OnClickMagnifierButton);

        return true;
    }

    void OnClickNavButton()
    {
        Managers.UI.ShowPopupUI<UI_Popup>("Navigation");
    }

    void OnClickARZoneButton()
    {
        Managers.UI.ShowPopupUI<UI_Popup>("ARZoneMenu");
    }

    void OnClickStreetButton()
    {
        Debug.Log("OnClickStreetButton");
        Managers.UI.ShowPopupUI<UI_Popup>("Street");
    }

    void OnClickQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void OnClickEmergencyButton()
    {
        Debug.Log("OnClickEmergencyButton");
    }

    void OnClickMagnifierButton()
    {
        Debug.Log("OnClickMagnifierButton");
    }
}
