using UnityEngine;

public class EventManager : MonoBehaviour
{

    private static EventManager instance;

    public static EventManager Instance {
        get {
            return instance;
        }

        set {
            instance = value;
        }
    }

    void Awake() {
        instance = this;
    }

    public delegate void PlayerDied();

    public PlayerDied OnPlayerDied;

    public void FirePlayerDied() {
        if (OnPlayerDied != null) {
            OnPlayerDied();
        }
    }

    public delegate void PlayerResurrected();

    public PlayerDied OnPlayerResurrected;

    public void FirePlayerResurrected() {
        if (OnPlayerResurrected != null) {
            OnPlayerResurrected();
        }
    }

    public delegate void BeforePlayerDied();

    public BeforePlayerDied OnBeforePlayerDied;

    public void FireBeforePlayerDied() {
        if (OnBeforePlayerDied != null) {
            OnBeforePlayerDied();
        }
    }

    public delegate void BeforePlayerResurrected();

    public BeforePlayerResurrected OnBeforePlayerResurrected;

    public void FireBeforePlayerResurrected() {
        if (OnBeforePlayerResurrected != null) {
            OnBeforePlayerResurrected();
        }
    }

    public delegate void HeadstartEnd();

    public PlayerDied OnHeadstartEnd;

    public void FireHeadstartEnd() {
        if (OnHeadstartEnd != null) {
            OnHeadstartEnd();
        }
    }

    public delegate void LevelUp();

    public LevelUp OnLevelUp;

    public void FireLevelUp() {
        if (OnLevelUp != null) {
            OnLevelUp();
        }
    }
}
