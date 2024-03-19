using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override bool Init()
    {
        if (!base.Init()) return false;

        SceneType = Define.SceneType.Main;

        Managers.UI.ShowSceneUI<UI_Scene>("Footer");
        Managers.UI.ShowSceneUI<UI_Scene>("Main");

        return true;
    }
}
