using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ARBackButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackButton()
    {
        Scene previousScene = SceneManager.GetSceneByName("Main");
        foreach (GameObject obj in previousScene.GetRootGameObjects())
        {
            obj.SetActive(true);
        }

        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}
