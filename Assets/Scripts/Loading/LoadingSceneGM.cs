using UnityEngine;
using System.Collections;

public class LoadingSceneGM : Singleton<LoadingSceneGM> {

	public static void UnloadLoadingScene()
    {
        GameObject.Destroy(instance.gameObject);
    }
}
