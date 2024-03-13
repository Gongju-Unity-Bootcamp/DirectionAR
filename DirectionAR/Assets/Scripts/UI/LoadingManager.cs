using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

public class LoadingManager : MonoBehaviour
{
    [Header("Singleton")]
    private static LoadingManager _instance;

    [Header("UI")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Slider _loadBar;

    [Header("SceneNum")]
    private int loadSceneIndex;

    public static LoadingManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<LoadingManager>();
                if (obj != null)
                {
                    _instance = obj;
                }
                else
                {
                    _instance = Create();
                }
            }
            return _instance;
        }
    }

    private static LoadingManager Create()
    {
        return Instantiate(Resources.Load<LoadingManager>(string.Concat(Path.PREFAB, "LoadingUI")));
    }

    public void LoadScene(int sceneIndex)
    {
        gameObject.SetActive(true);
        loadSceneIndex = sceneIndex;
        StopAllCoroutines();
        StartCoroutine(LoadSceneProcess());
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator LoadSceneProcess()
    {
        _loadBar.value = 0f;
        yield return StartCoroutine(Fade(true));

        AsyncOperation op = SceneManager.LoadSceneAsync(loadSceneIndex);

        while (!op.isDone)
        {
            _loadBar.value = Mathf.Clamp01(op.progress / 0.9f);
            yield return null;
        }

        yield return StartCoroutine(Fade(false));
    }

    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;
        float startAlpha = isFadeIn ? 0f : 1f;
        float targetAlpha = isFadeIn ? 1f : 0f;

        _canvasGroup.alpha = startAlpha;

        while (timer <= 1f)
        {
            yield return null;
            timer += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer);
        }

        if (!isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }
}
