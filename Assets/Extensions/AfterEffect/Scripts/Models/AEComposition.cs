////////////////////////////////////////////////////////////////////////////////
//  
// @module <module_name>
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AEComposition : AESprite {


	private bool _isEnabled = true;

	[SerializeField]
	public float opacity;


	[SerializeField]
	private AECompositionTemplate composition;

	[SerializeField]
	private List<AESprite> _sprites = new List<AESprite>();
	

	//--------------------------------------
	// INITIALIZE
	//--------------------------------------

	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------
	
	public override void WakeUp() {
		foreach(AESprite sprite in sprites) {
			sprite.WakeUp ();
		} 
	}
	

	public override void init(AELayerTemplate layer, AfterEffectAnimation animation,  AELayerBlendingType forcedBlending) {

		base.init (layer, animation, forcedBlending);

		gameObject.name = layer.name + " (Composition)";
	
		composition = animation.animationData.getCompositionById (layer.id);


		InitSprites ();
		ApplayCompositionFrame (0);
	}




	public override void disableRenderer () {

		if(_isEnabled) {
			foreach(AESprite sprite in sprites) {
				sprite.disableRenderer ();
			} 

			_isEnabled = false;
		}


	}

	public override void enableRenderer () {

		if(!_isEnabled) {
			foreach(AESprite sprite in sprites) {
				sprite.enableRenderer ();
			} 

			_isEnabled = true;
		}

	}


	public override void GoToFrame(int index) {

		int frameIndex = 0;

		if(index >= _layer.inFrame) {
			frameIndex = index - _layer.inFrame;
		} else {
			disableRenderer ();
			return;
		}
		
		ApplayCompositionFrame(frameIndex);
	}
	
	public void ApplayCompositionFrame(int frameIndex) {
		AEFrameTemplate frame = _layer.GetFrame (frameIndex);
		if(frame == null) {
			disableRenderer ();
			return;
		}

		enableRenderer ();

		if(frame.IsPositionChanged) {
			Vector3 pos = frame.positionUnity;
			transform.localPosition = pos;
		}
	

		if(frame.IsPivotChnaged) {
			plane.localPosition = new Vector3 (-frame.pivot.x, frame.pivot.y, 0f);

			childAnchor.transform.localPosition = plane.transform.localPosition;
			childAnchor.transform.localScale = Vector3.one;


			Vector3 pos = plane.position;
			//TODO remove z index caclulcation
			pos.z = _anim.GetLayerGlobalZ (zIndex);
			plane.position = pos;
		}

		if(frame.IsRotationChanged) {
			transform.localRotation = Quaternion.Euler(new Vector3 (0f, 0f, -frame.rotation));
		}


		if(frame.IsScaleChanged) {
			transform.localScale = frame.scale;
		}



		opacity = frame.opacity * 0.01f * parentOpacity;


		foreach(AESprite sprite in sprites) {
			sprite.GoToFrame (frameIndex);
		} 
	}
	
	
	public override void SetColor(Color c) {
		foreach(AESprite sprite in sprites) {
			sprite.SetColor(c);
		} 
	}
	
	
	
	//--------------------------------------
	//  GET/SET
	//--------------------------------------


	public List<AESprite> sprites {
		get {
			return _sprites;
		}
	}
	

	//--------------------------------------
	//  EVENTS
	//--------------------------------------
	
	//--------------------------------------
	//  PRIVATE METHODS
	//--------------------------------------

	private void InitSprites() {

		_sprites.Clear ();


		foreach(AELayerTemplate layer in composition.layers) {
			AESprite sprite = null;

			layer.forcedBlending = _layer.blending;

			switch(layer.type) {
				case AELayerType.FOOTAGE:
				sprite = CreateFootage ();
				break;
				case AELayerType.COMPOSITION:
				sprite = CreateComposition ();
				break;
				default:
				Debug.LogError ("Unsupported layer type: " + layer.type.ToString());
				break;

			}

			_sprites.Add(sprite);

			sprite.transform.parent = plane.transform;
			sprite.parentIndex = zIndex;
			sprite.indexModifayer = indexModifayer * 0.01f;

			if(layer.parent != 0) {
				sprite.layerId = layer.index;
			} else {
				sprite.init (layer, _anim, blending); 
			}
		} 



		foreach(AELayerTemplate layer in composition.layers) {
			if(layer.parent != 0) {
				AESprite p = GetSpriteByLayerId(layer.parent);
				AESprite c = GetSpriteByLayerId (layer.index);
				p.AddChild (c);
				c.init (layer, _anim, blending); 
			}
		} 

		foreach(AESprite sprite in sprites) {
			sprite.parentComposition = this;
		} 

	}


	public AESprite GetSpriteByLayerId(int layerId) {
		foreach(AESprite sprite in sprites) {
			if(sprite.layerId == layerId) {
				return sprite;
			}
		} 

		Debug.LogWarning ("GetSpriteByLayerId  -> sprite not found, layer: " + layerId);
		return null;

	}
	


	protected virtual AEFootage CreateFootage() {
		return AEResourceManager.CreateFootage ();
	}

	protected virtual AEComposition CreateComposition() {
		return AEResourceManager.CreateComposition ();
	}

	
	//--------------------------------------
	//  DESTROY
	//--------------------------------------

}
