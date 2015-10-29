using UnityEngine;
using System.Collections;
using System;

public class CrystalSpawner : ObjectSpawner
{

    public ObjectTypesDataHolder.CrystalType[] crystalTypes;
    public int[] leftPaddings;
    public int[] rightPaddings;

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
}
