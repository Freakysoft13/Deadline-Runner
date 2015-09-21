////////////////////////////////////////////////////////////////////////////////
//  
// @module Affter Effect Importer
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class AEFootage : AESprite {


	protected float w;
	protected float h;

	public float opacity = 1f;
	
	[SerializeField]
	private Color materialColor = Color.white;

	private bool _isEnabled = true;
	


	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	
	public override void WakeUp() {
		if(plane.GetComponent<Renderer>().sharedMaterial == null && _layer != null) {
			SetMaterial ();
		}
	}


	public override void init(AELayerTemplate layer, AfterEffectAnimation animation,  AELayerBlendingType forcedBlending) {

		base.init (layer, animation, forcedBlending);

		gameObject.name = layer.name + " (Footage)";
		SetMaterial ();
		
		
		color = _anim.MaterialColor;
		
		GoToFrame (0);

		AESpriteRenderer r = plane.gameObject.AddComponent<AESpriteRenderer> ();
		r.anim = _anim;
		r.enabled = false;
		
		
	}
	

	//--------------------------------------
	// PUBLIC METHODS
	//--------------------------------------
	

	public override void disableRenderer () {
		if(_isEnabled) {
			plane.GetComponent<Renderer>().enabled = false;
			_isEnabled = false;
		}

	}

	public override void enableRenderer () {
		if(!_isEnabled) {
			plane.GetComponent<Renderer>().enabled = true;
			_isEnabled = true;
		}

	}


	public override void GoToFrame(int index) {


		if(index < _layer.inFrame || index >= _layer.outFrame) {
			disableRenderer ();
			//return;
		} else {
			enableRenderer ();
		}
		

		AEFrameTemplate frame = _layer.GetFrame (index);
		if(frame == null) {
			return;
		}


		if(frame.IsPositionChanged) {
			Vector3 pos = frame.positionUnity;
			transform.localPosition = pos;
		}


		if(frame.IsPivotChnaged) {
			plane.localPosition = new Vector3 (-frame.pivot.x, frame.pivot.y, 0f);

			childAnchor.transform.localPosition = plane.transform.localPosition;
			childAnchor.transform.localScale = Vector3.one;


			Vector3 pos = plane.position;
			pos.z = _anim.GetLayerGlobalZ (zIndex);
			plane.position = pos;
		}


		if(frame.IsRotationChanged) {
			transform.localRotation = Quaternion.Euler(new Vector3 (0f, 0f, -frame.rotation));
		}



		if(frame.IsScaleChanged) {
			transform.localScale = frame.scale;
		}


		SetOpcity(parentOpacity * frame.opacity * 0.01f * _anim.opacity);
			 

	}


	public void SetOpcity(float op) {
		if(opacity != op) {
			opacity = op;

			materialColor.a = opacity;
			color = materialColor;
		} 
	}

	public override void SetColor(Color c) {
		materialColor = c;
		float a = color.a;
		c.a = a;
		color = c;
	}
	

	public virtual void SetMaterial() {
		string textureName = _anim.imagesFolder + _layer.sourceNoExt;
		Texture2D tex = Resources.Load (textureName) as Texture2D;

		if(tex != null) {
			plane.GetComponent<Renderer>().sharedMaterial = new Material (shader);
			plane.GetComponent<Renderer>().sharedMaterial.SetTexture("_MainTex", tex);
			
			plane.GetComponent<Renderer>().sharedMaterial.name = tex.name;
	
			w = _layer.width;
			h = _layer.height;
	
			plane.localScale = new Vector3 (w, h, 1); 
		} else {
			Debug.LogWarning("Affter Effect: Texture " + textureName + " not found");
		}

		
	}



	//--------------------------------------
	// GET / SET
	//--------------------------------------



	public Color color {
		get {
			Material m = plane.GetComponent<Renderer>().sharedMaterial;
			if(m.HasProperty("_Color")) {
				return m.color;
			} else {
				if(m.HasProperty("_TintColor")) {
					return m.GetColor ("_TintColor");
				} else {
					return Color.white;
				}

			}
		}

		set {
			if(plane.GetComponent<Renderer>().sharedMaterial.HasProperty("_Color")) {
				plane.GetComponent<Renderer>().sharedMaterial.color = value;
			}  else {
				if(plane.GetComponent<Renderer>().sharedMaterial.HasProperty ("_TintColor")) {
					plane.GetComponent<Renderer>().sharedMaterial.SetColor ("_TintColor", value);
				}

			}
		}
	}


	public Shader shader {
		get {
			Shader sh;

			switch(blending) {
				case AELayerBlendingType.ADD:
				sh = _anim.GetAddShader ();
				break;
				default:
				sh = _anim.GetNormalShader ();
				break;
			}

			return sh;
		}
	}


		




}
