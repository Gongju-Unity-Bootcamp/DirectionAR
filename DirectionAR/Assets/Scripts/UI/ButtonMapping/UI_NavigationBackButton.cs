using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_NavigationBackButton : MonoBehaviour
{
    public void BackButton()
    {
        SceneManager.LoadScene((int)Define.SceneNum.Main);
    }
}
