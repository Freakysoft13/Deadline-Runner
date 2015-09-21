////////////////////////////////////////////////////////////////////////////////
//  
// @module Affter Effect Importer
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEditor;
using System.Collections;

public class AETopMenu : EditorWindow {

	// Use this for initialization

	[MenuItem("GameObject/Create Other/Affter Effect Animation")]
	public static void CreateAEAnimation() {
		AfterEffectAnimation AE  =  new GameObject ("Affter Effect Animation").AddComponent<AfterEffectAnimation> ();
		SetPositionAndScale(AE.gameObject);

		AE.pivotCenterX = AEEditorConfig.PIVOT_X;
		AE.pivotCenterY = AEEditorConfig.PIVOT_Y;

		Selection.activeGameObject = AE.gameObject;
	}


	private static void SetPositionAndScale (GameObject obj) {
		obj.transform.localScale = AEEditorConfig.SCALE;
		obj.transform.position = AEEditorConfig.POSITION;

	}


}
