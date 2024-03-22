using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARNavigationScene : BaseScene
{
    protected override bool Init()
    {
        if (!base.Init()) return false;

        SceneType = Define.SceneType.ARNavigation;

        StartCoroutine(Managers.Android.LoadGPS());

        return true;
    }
}
