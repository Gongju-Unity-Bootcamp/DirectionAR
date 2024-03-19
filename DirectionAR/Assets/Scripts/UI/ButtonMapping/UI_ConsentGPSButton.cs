using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ConsentGPSButton : UI_Popup
{
    enum Buttons
    {
        DenyButton,
        AllowButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.DenyButton).gameObject, OnClickDenyButton);
        BindEvent(GetButton((int)Buttons.AllowButton).gameObject, OnClickAllowButton);

        return true;
    }

    void OnClickDenyButton()
    {
        Debug.Log("Deny GPS");
    }

    void OnClickAllowButton()
    {
        Debug.Log("Allow GPS");
    }
}
