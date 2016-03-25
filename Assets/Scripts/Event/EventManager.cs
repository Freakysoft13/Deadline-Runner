using UnityEngine;

public static class EventManager
{

    public delegate void PlayerDied();

	public static PlayerDied OnPlayerDied;

	public static void FirePlayerDied() {
        if (OnPlayerDied != null) {
            OnPlayerDied();
        }
    }

    public delegate void PlayerResurrected();

	public static PlayerDied OnPlayerResurrected;

	public static void FirePlayerResurrected() {
        if (OnPlayerResurrected != null) {
            OnPlayerResurrected();
        }
    }

    public delegate void BeforePlayerDied();

	public static BeforePlayerDied OnBeforePlayerDied;

	public static void FireBeforePlayerDied() {
        if (OnBeforePlayerDied != null) {
            OnBeforePlayerDied();
        }
    }

    public delegate void BeforePlayerResurrected();

	public static BeforePlayerResurrected OnBeforePlayerResurrected;

	public static void FireBeforePlayerResurrected() {
        if (OnBeforePlayerResurrected != null) {
            OnBeforePlayerResurrected();
        }
    }


    public delegate void Headstart();

	public static Headstart OnHeadstart;

	public static void FireHeadstart() {
        if (OnHeadstart != null) {
            OnHeadstart();
        }
    }

    public delegate void HeadstartEnd();

	public static HeadstartEnd OnHeadstartEnd;

	public static void FireHeadstartEnd() {
        if (OnHeadstartEnd != null) {
            OnHeadstartEnd();
        }
    }

    public delegate void LevelUp();

	public static LevelUp OnLevelUp;

	public static void FireLevelUp() {
        if (OnLevelUp != null) {
            OnLevelUp();
        }
    }

    public delegate void AnimationComplete(string animationName);

	public static AnimationComplete OnAnimationComplete;

	public static void FireAnimationComplete(string animationName) {
        if (OnAnimationComplete != null) {
            OnAnimationComplete(animationName);
        }
    }

	public delegate void ObstacleWarning(bool show, GameManager.Side side);

	public static ObstacleWarning OnObstacleWarning;

	public static void FireObstacleWarning(bool show, GameManager.Side side) {
		if (OnObstacleWarning != null) {
			OnObstacleWarning(show, side);
		}
	}
}
