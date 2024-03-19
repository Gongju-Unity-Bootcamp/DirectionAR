using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ARZoneInfoButton : UI_Popup
{
    enum Texts
    {
        Title,
        Info,
    }
    
    enum Buttons
    {
        CloseButton,
        GetLocationButton,
        StartButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.CloseButton).gameObject, OnClickCloseButton);
        BindEvent(GetButton((int)Buttons.GetLocationButton).gameObject, OnClickGetLocationButton);
        BindEvent(GetButton((int)Buttons.StartButton).gameObject, OnClickStartButton);

        return true;
    }

    void OnClickCloseButton()
    {
        ClosePopupUI();
    }

    void OnClickGetLocationButton()
    {
        Debug.Log("GetLocation");
    }

    void OnClickStartButton()
    {
        Debug.Log("Start");
    }
}
