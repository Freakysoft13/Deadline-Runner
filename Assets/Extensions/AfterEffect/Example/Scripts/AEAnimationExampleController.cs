////////////////////////////////////////////////////////////////////////////////
//  
// @module $(modue_name)
// @author Stanislav Osipov lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AEAnimationExampleController : MonoBehaviour {

	private float w;
	private float h;

	private float r_color = 1f;
	private float g_color = 1f;
	private float b_color = 1f;
	

	public AfterEffectAnimation anim;


	//--------------------------------------
	// INITIALIZE
	//--------------------------------------


	void Start() {
		w = Screen.width * 0.14f;
		h = w * 0.3f;
	}

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------


	void OnGUI() {
		float startX = Screen.width - w * 1.5f;
		float StartY = h * 2f;

		Rect r = new Rect (startX, StartY, w, h);


		if(anim.isPlaying) {
			if(GUI.Button(r, "Stop")) {
				anim.Stop ();
			}
		} else {
			if(GUI.Button(r, "Play")) {
				anim.Play ();
			}
		}

		r.y += h * 1.2f;

		if(GUI.Button(r, "GotoAndStop 15")) {
			anim.GoToAndStop (15);
		}

		r.y += h * 1.2f;

		if(GUI.Button(r, "GoToAndPlay 30")) {
			anim.GoToAndPlay (30);
		}



		r.y += h * 1.2f;
		GUI.Label (r, "Anim Time Scale");
		r.y += h * 0.6f;
		anim.timeScale = GUI.HorizontalSlider (r, anim.timeScale, 0.1f, 2.0f);

		r.y += h * 0.7f;
		GUI.Label (r, "Anim Opacity");
		r.y += h * 0.6f;
		anim.opacity = GUI.HorizontalSlider (r, anim.opacity, 0.0f, 1.0f);


		r.y += h * 0.7f;
		GUI.Label (r, "R chanel");

		r.y += h * 0.6f;
		r_color =  GUI.HorizontalSlider (r, r_color, 0.0f, 1.0f);


		r.y += h * 0.7f;
		GUI.Label (r, "G chanel");
		r.y += h * 0.6f;
		g_color =  GUI.HorizontalSlider (r, g_color, 0.0f, 1.0f);


		r.y += h * 0.7f;
		GUI.Label (r, "B chanel");
		r.y += h * 0.6f;
		b_color =  GUI.HorizontalSlider (r, b_color, 0.0f, 1.0f);

		Color c = anim.MaterialColor;

		c.r = r_color;
		c.g = g_color;
		c.b = b_color;

		anim.MaterialColor = c;
		anim.UpdateColor ();

	}
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------
	
	//--------------------------------------
	//  EVENTS
	//--------------------------------------
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------
	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------


}
