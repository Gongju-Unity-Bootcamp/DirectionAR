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
        BaseScene.SceneType = Define.SceneType.Navigation;

        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_Popup>("Header");
    }

    void OnClickARZoneButton()
    {
        BaseScene.SceneType = Define.SceneType.ARZone;

        Managers.UI.ClosePopupUI(this);
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
