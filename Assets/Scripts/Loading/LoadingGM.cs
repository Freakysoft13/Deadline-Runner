using UnityEngine;
using System.Collections;

public class LoadingGM : MonoBehaviour
{

    [SerializeField]
    private FadeScene blackScreenLock;

    public float waitTime = 1.5f;

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

        yield return new WaitForSeconds(waitTime);

        yield return Application.LoadLevelAdditiveAsync(sceneName);

        yield return StartCoroutine(blackScreenLock.FadeOut());
        Destroy(this.gameObject, 1);
    }
}
