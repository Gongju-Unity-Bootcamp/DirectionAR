using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private Image _coverImage;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _coverImage = Utils.FindChild<Image>(gameObject, "Background");

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.CloseButton).gameObject, OnClickCloseButton);
        BindEvent(GetButton((int)Buttons.GetLocationButton).gameObject, OnClickGetLocationButton);
        BindEvent(GetButton((int)Buttons.StartButton).gameObject, OnClickStartButton);

        _coverImage.sprite = Managers.ARMenu._currentItem.coverImage;
        GetText((int)Texts.Title).text = Managers.ARMenu._currentItem.title;
        GetText((int)Texts.Info).text = Managers.ARMenu._currentItem.info;

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
