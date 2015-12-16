using UnityEngine;
using System.Collections;

public class LoadingGM : MonoBehaviour
{

    [SerializeField]
    private FadeScene blackScreenLock;

    public static LoadingGM instance;

    public void StartGameBtn()
    {
        DontDestroyOnLoad(this);
        StartCoroutine(LoadSceneAsync("Main"));
    }
    public IEnumerator LoadSceneAsync(string sceneName)
    {
        //Fade
        yield return StartCoroutine(blackScreenLock.FadeIn());
        yield return Application.LoadLevelAsync("LoadingScene");
        yield return Application.LoadLevelAdditiveAsync(sceneName);
        yield return StartCoroutine(blackScreenLock.FadeOut());
        LoadingSceneGM.UnloadLoadingScene();
    }
}
