using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ARZoneButton : UI_Popup
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        GameObject header = GameObject.Find("Header");

        BaseScene.SceneType = Define.SceneType.ARZone;

        if (header != null) header.GetComponent<UI_HeaderButton>().UpdateTitle();
        else if (header == null) Managers.UI.ShowSceneUI<UI_Scene>("Header");

        if (Managers.ARMenu._content == null)
        {
            Managers.ARMenu._content = transform.Find("Scroll View/Viewport/Content").transform;
        }

        LoadObjects();

        return true;
    }

    public static void LoadObjects()
    {
        ResetObjects();

        Managers.Resource.LoadARData();

        Dictionary<string, Item> _datas = Managers.Resource._arDatas;

        foreach (var obj in _datas)
        {
            Managers.ARMenu.Add(obj.Value);
        }

        Managers.ARMenu.ListItems();
    }

    public static void ResetObjects()
    {
        if (Managers.ARMenu.items.Count == 0) return;

        for(int i = 0; i < Managers.ARMenu.items.Count; i++)
        {
            Managers.ARMenu.Remove(Managers.ARMenu.items[i]);
        }

        Managers.ARMenu.items.Clear();
    }
}
