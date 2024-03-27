using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.Android;

public class UI_HeaderButton : UI_Scene
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

        UpdateTitle();

        return true;
    }

    const string nav = "길찾기";
    const string arZone = "AR 체험존";
    const string street = "AR 거리뷰";

    public void UpdateTitle()
    {
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

        type.Push(BaseScene.SceneType);
    }

    Stack<Define.SceneType> type = new Stack<Define.SceneType> { };

    public void UpdatePrevTitle()
    {
        if (type.Count() <= 1)
            return;
            
        type.Pop();

        switch (type.Peek())
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
    }

    void OnClickBackButton()
    {
        if (Managers.UI._popupStack.Count < 2)
        {
            Managers.UI.CloseAllPopupUI();
            Managers.Resource.Destroy(GameObject.Find("Header")); 
            BaseScene.SceneType = Define.SceneType.Main;
        }

        Managers.UI.ClosePopupUI();

        UpdatePrevTitle();
    }

    void OnClickEmergencyButton()
    {        
        bool callPermission = Permission.HasUserAuthorizedPermission(Define.Call.CallPermission);

        if(!callPermission)
        {
            Managers.UI.ShowPopupUI<UI_Popup>("ConsentCallPopUp");
        }
        else
        {
            Managers.Android.EmergencyDialer(Define.Call.EmergencyNumber);
        }
    }
}

