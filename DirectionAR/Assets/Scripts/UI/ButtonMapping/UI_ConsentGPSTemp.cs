using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ConsentGPSTemp : UI_Popup
{
    enum Buttons
    {
        AllowButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.AllowButton).gameObject, OnClickAllowButton);

        return true;
    }

    void OnClickAllowButton()
    {
        ClosePopupUI();
    }
}
