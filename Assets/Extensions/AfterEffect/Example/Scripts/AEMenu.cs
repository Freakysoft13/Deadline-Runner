////////////////////////////////////////////////////////////////////////////////
//  
// @module Affter Effect Importer
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////
/// 
using UnityEngine;
using System.Collections;

public class AEMenu : MonoBehaviour {

	private float w;
	private float h;

	// Use this for initialization
	void Start () {
		w = Screen.width * 0.2f;
		h = w * 0.3f;
	}
	

	void OnGUI() {
		float startX = w * 0.17f;
		float StartY = Screen.height - h * 1.5f;

				Rect r = new Rect (startX, StartY, w, h);

		if(GUI.Button(r, "Example 1")) {
			Application.LoadLevel ("Boxes");
		}

				r.x += w * 1.2f;

		if(GUI.Button(r, "Example 2")) {
			Application.LoadLevel ("FireSphere");
		}

		r.x += w * 1.2f;

		if(GUI.Button(r, "Example 3")) {
			Application.LoadLevel ("Fog_Sphere");
		}

		r.x += w * 1.2f;

		if(GUI.Button(r, "Example 4")) {
			Application.LoadLevel ("Bouncing_Sphere");
		}
	}
}
