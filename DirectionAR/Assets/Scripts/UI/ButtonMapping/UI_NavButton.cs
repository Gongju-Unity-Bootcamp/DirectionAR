using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_NavButton : UI_Popup
{
    enum Buttons
    {
        ViewAllButton,
        PublicPlaceButton,
        ARZoneButton,
        HospitalButton,
        ParkButton,
        ToiletButton,
        ParkingLotButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BaseScene.SceneType = Define.SceneType.Navigation;

        GameObject header = GameObject.Find("Header");

        if (header != null) header.GetComponent<UI_HeaderButton>().UpdateTitle();
        else if (header == null) Managers.UI.ShowSceneUI<UI_Scene>("Header");

        BindButton(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.ViewAllButton).gameObject, OnClickViewAllButton);
        BindEvent(GetButton((int)Buttons.PublicPlaceButton).gameObject, OnClickPublicPlaceButton);
        BindEvent(GetButton((int)Buttons.ARZoneButton).gameObject, OnClickARZoneButton);
        BindEvent(GetButton((int)Buttons.HospitalButton).gameObject, OnClickHospitalButton);
        BindEvent(GetButton((int)Buttons.ParkButton).gameObject, OnClickParkButton);
        BindEvent(GetButton((int)Buttons.ToiletButton).gameObject, OnClickToiletButton);
        BindEvent(GetButton((int)Buttons.ParkingLotButton).gameObject, OnClickParkingLotButton);

        return true;
    }

    void OnClickViewAllButton()
    {
        Managers.Android.ShowAndroidToastMessage("ViewAll");
    }

    void OnClickPublicPlaceButton()
    {
        Managers.Android.ShowAndroidToastMessage("PublicPlace");
    }

    void OnClickARZoneButton()
    {
        Managers.Android.ShowAndroidToastMessage("ARZone");
    }

    void OnClickHospitalButton()
    {
        Managers.Android.ShowAndroidToastMessage("Hospital");
    }

    void OnClickParkButton()
    {
        Managers.Android.ShowAndroidToastMessage("Park");
    }

    void OnClickToiletButton()
    {
        Managers.Android.ShowAndroidToastMessage("Toilet");
    }

    void OnClickParkingLotButton()
    {
        Managers.Android.ShowAndroidToastMessage("ParkingLot");
    }
}
