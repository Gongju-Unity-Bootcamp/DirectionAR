using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainButtonPopup : UI_Popup
{
    enum Buttons
    {
        NavButton,
        ARExButton,
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
        BindEvent(GetButton((int)Buttons.ARExButton).gameObject, OnClickARExButton);
        BindEvent(GetButton((int)Buttons.StreetButton).gameObject, OnClickStreetButton);
        BindEvent(GetButton((int)Buttons.QuitButton).gameObject, OnClickQuitButton);
        BindEvent(GetButton((int)Buttons.EmergencyButton).gameObject, OnClickEmergencyButton);
        BindEvent(GetButton((int)Buttons.MagnifierButton).gameObject, OnClickMagnifierButton);

        return true;
    }

    void OnClickNavButton()
    {
        BaseScene.SceneType = Define.Scene.Navigation;

        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_Popup>("Header");
    }

    void OnClickARExButton()
    {
        BaseScene.SceneType = Define.Scene.ARZone;

        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_Popup>("ARDocentMenu");
        Managers.UI.ShowPopupUI<UI_Popup>("Header");
    }

    void OnClickStreetButton()
    {
        Debug.Log("OnClickStreetButton");

        Managers.UI.CloseAllPopupUI();
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
