using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeScene : BaseScene
{
    protected override bool Init()
    {
        if (!base.Init()) return false;

        SceneType = Define.SceneType.Unknown;

        LoadingManager.Instance.LoadScene((int)Define.SceneNum.Main);

        return true;
    }
}
