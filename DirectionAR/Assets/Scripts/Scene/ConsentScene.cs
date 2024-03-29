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

        SceneType = Define.SceneType.Consent;

        if (!PlayerPrefs.HasKey("isFirstRun"))
        {
            Managers.UI.ShowPopupUI<UI_Popup>("ConsentInitPopUp");
        }
        else
            SceneManager.LoadScene((int)Define.SceneNum.Awake);

        return true;
    }
}
