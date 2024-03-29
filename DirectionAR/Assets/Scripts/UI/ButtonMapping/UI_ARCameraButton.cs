using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ARCameraButton : UI_Popup
{
    enum Buttons
    {
        BackButton,
        ReplayButton,
    }

    public override bool Init()
    {
        if (!base.Init()) return false;

        BindButton(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.BackButton).gameObject, OnClickBackButton);
        BindEvent(GetButton((int)Buttons.ReplayButton).gameObject, OnClickReplayButton);

        return true;
    }

    void OnClickBackButton()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex((int)Define.SceneNum.ARCamera));
        Scene previousScene = SceneManager.GetSceneByBuildIndex((int)Define.SceneNum.Main);
        foreach (GameObject gameObject in previousScene.GetRootGameObjects())
        {
            gameObject.SetActive(true);
        }
        
        BaseScene.SceneType = Define.SceneType.ARZone;
    }

    void OnClickReplayButton()
    {
        Managers.Android.ShowAndroidToastMessage("준비 중 입니다. :)");
    }
}
