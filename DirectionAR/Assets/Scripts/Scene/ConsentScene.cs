using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class ConsentScene : BaseScene
{
    protected override bool Init()
    {
        if (!base.Init()) return false;

        SceneType = Define.SceneType.Unknown;

        if (!PlayerPrefs.HasKey("isFirstRun"))
        {
            PlayerPrefs.SetInt("isFirstRun", 1);
            Managers.UI.ShowPopupUI<UI_Popup>("ConsentInitPopup");
        }
        else
            SceneManager.LoadScene((int)Define.SceneNum.Awake);

        return true;
    }
}
