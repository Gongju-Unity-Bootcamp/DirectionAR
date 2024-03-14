using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HeaderButton : UI_Popup
{
    enum Texts
    {
        Title,
    }

    enum Buttons
    {
        BackButton,
        EmergencyButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.BackButton).gameObject, OnClickBackButton);
        BindEvent(GetButton((int)Buttons.EmergencyButton).gameObject, OnClickEmergencyButton);

        const string nav = "길찾기";
        const string arZone = "AR 체험존";
        const string street = "AR 거리뷰";

        switch (BaseScene.SceneType)
        {
            case Define.SceneType.Navigation:
                GetText((int)Texts.Title).text = nav;
                break;
            case Define.SceneType.ARZone:
                GetText((int)Texts.Title).text = arZone;
                break;
            case Define.SceneType.Street:
                GetText((int)Texts.Title).text = street;
                break;
        }

        return true;
    }

    void OnClickBackButton()
    {
        Managers.UI.CloseAllPopupUI();
        Managers.UI.ShowPopupUI<UI_Popup>("Main");
    }

    void OnClickEmergencyButton()
    {
        Debug.Log("OnClickEmergencyButton");
    }
}
