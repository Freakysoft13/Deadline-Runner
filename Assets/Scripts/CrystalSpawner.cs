using UnityEngine;
using System.Collections;
using System;

public class CrystalSpawner : ObjectSpawner
{

    public ObjectTypesDataHolder.CrystalType[] crystalTypes;
    public int[] leftPaddings;
    public int[] rightPaddings;
    public int minPauseDistance = 100;
    public int maxPauseDistance = 300;
    private int pauseDistance;

    protected override System.Enum[] GetObjectTypes() {
        System.Enum[] tmpEnum = new System.Enum[crystalTypes.Length];
        for (int i = 0; i < crystalTypes.Length; i++) {
            tmpEnum[i] = crystalTypes[i];
        }
        return tmpEnum;
    }

    protected override int[] GetObjectLeftPaddings() {
        return leftPaddings;
    }

    protected override int[] GetObjectRightPaddings() {
        return rightPaddings;
    }

    protected override void Start() {
        pauseDistance = UnityEngine.Random.Range(minPauseDistance, maxPauseDistance);
        base.Start();
    }

    protected override void SpawnObjects() {
        /*if(pauseDistance -
                    GameManager.Instance.Player.GetDistance() < 0) {
            isUpperPaused = !isUpperPaused;
            pauseDistance += UnityEngine.Random.Range(minPauseDistance, maxPauseDistance);
        }*/
        base.SpawnObjects();
    }
}
