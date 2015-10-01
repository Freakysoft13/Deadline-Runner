public class EffectSpawner : ObjectSpawner {

    public ObjectTypesDataHolder.EffectType[] effectTypes;
    public int[] obstaclePaddings;

    protected override System.Enum[] GetObjectTypes() {
        System.Enum[] tmpEnum = new System.Enum[effectTypes.Length];
        for (int i = 0; i < effectTypes.Length; i++) {
            tmpEnum[i] = effectTypes[i];
        }
        return tmpEnum;
    }

    protected override int[] GetObjectPaddings() {
        return obstaclePaddings;
    }
}
