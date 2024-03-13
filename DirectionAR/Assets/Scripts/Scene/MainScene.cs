using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override bool Init()
    {
        if (!base.Init()) return false;

        SceneType = Define.Scene.Main;
        Managers.UI.ShowSceneUI<UI_Scene>("Footer");
        Managers.UI.ShowPopupUI<UI_Popup>("Main");

        return true;
    }
}
