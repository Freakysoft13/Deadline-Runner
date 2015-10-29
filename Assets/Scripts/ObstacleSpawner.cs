using UnityEngine;

public class ObstacleSpawner : ObjectSpawner
{

    //spawn parameters
    public ObjectTypesDataHolder.ObstacleType[] obstacleTypes;
    public int[] leftPaddings;
    public int[] rightPaddings;

    //difficulty increase
    public float maxSpreadDecRate = 1.0f;
    public float minSpreadAmpl = 2.0f;
    public float decTime = 10.0f;

    protected override void Update() {
        base.Update();
        IncreaseDifficulty();
    }

    private void IncreaseDifficulty() {
        if (Time.time - lastUpdateTime > decTime && (spread - maxSpreadDecRate) > minSpread + minSpreadAmpl) {
            spread -= maxSpreadDecRate;

            lastUpdateTime = Time.time;
        }
    }

    protected override System.Enum[] GetObjectTypes() {
        System.Enum[] tmpEnum = new System.Enum[obstacleTypes.Length];
        for (int i = 0; i < obstacleTypes.Length; i++) {
            tmpEnum[i] = obstacleTypes[i];
        }
        return tmpEnum;
    }

    protected override int[] GetObjectLeftPaddings() {
        return leftPaddings;
    }

    protected override int[] GetObjectRightPaddings() {
        return rightPaddings;
    }
}
