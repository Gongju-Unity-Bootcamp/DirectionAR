using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ARBackButton : MonoBehaviour
{
    public void BackButton()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex((int)Define.SceneNum.ARCamera));
        Scene previousScene = SceneManager.GetSceneByBuildIndex((int)Define.SceneNum.Main);
        foreach (GameObject obj in previousScene.GetRootGameObjects())
        {
            obj.SetActive(true);
        }
    }
}
