using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARZoneScene : BaseScene
{
    protected override bool Init()
    {
        if (!base.Init()) return false;

        SceneType = Define.SceneType.ARCamera;

        return true;
    }
}
