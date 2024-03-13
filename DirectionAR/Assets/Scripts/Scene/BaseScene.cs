using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    public static Define.Scene SceneType = Define.Scene.Unknown;

    protected bool _init = false;

    private void Start()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if (_init)
            return false;

        _init = true;

        GameObject go = GameObject.Find("EventSystem");

        if (go == null)
        {
            go = Managers.Resource.Instantiate("EventSystem");
            go.name = "@EventSystem";

            DontDestroyOnLoad(go);
        }

        return true;
    }

    public virtual void Clear() { }
}
