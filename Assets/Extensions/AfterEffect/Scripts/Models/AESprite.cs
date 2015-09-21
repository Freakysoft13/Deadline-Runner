////////////////////////////////////////////////////////////////////////////////
//  
// @module <module_name>
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public abstract class AESprite : MonoBehaviour {

	public int layerId;
	public float zIndex;
	public float parentIndex = 0;
	public float indexModifayer = 1f;


	[SerializeField]
	public Transform plane;


	[SerializeField]
	public AELayerTemplate _layer;

	[SerializeField]
	protected AfterEffectAnimation _anim;

	[SerializeField]
	private GameObject _childAnchor = null;

	[SerializeField]
	public AEComposition parentComposition = null;


	[SerializeField]
	public AELayerBlendingType blending = AELayerBlendingType.NORMAL;
	



	//--------------------------------------
	// INITIALIZE
	//--------------------------------------
	
	public abstract void WakeUp();
	


	public virtual void init(AELayerTemplate layer, AfterEffectAnimation animation) {
		init (layer, animation, AELayerBlendingType.NORMAL);
	}

	public virtual void init(AELayerTemplate layer, AfterEffectAnimation animation, AELayerBlendingType forcedBlending) {
		_layer = layer;
		_anim = animation;

		layerId = layer.index;

		zIndex = parentIndex + (layer.index) * indexModifayer;

		if(forcedBlending == AELayerBlendingType.NORMAL) {
			blending = _layer.blending;
		} else {
			blending = forcedBlending;
		}

	}



	public abstract void GoToFrame (int index);
	public abstract void disableRenderer ();
	public abstract void enableRenderer ();
	public abstract void SetColor(Color c);
	
	//--------------------------------------
	//  PUBLIC METHODS
	//--------------------------------------

	public void AddChild(AESprite sprite) {
		sprite.transform.parent = childAnchor.transform;
	}
	

	//--------------------------------------
	//  GET/SET
	//--------------------------------------


	public float parentOpacity {
		get {
			if(parentComposition != null) {
				return parentComposition.opacity;
			} else {
				return 1f;
			}
		}
	}


	public GameObject childAnchor {
		get {
			if(_childAnchor == null) {
				_childAnchor = new GameObject ("ChildAnchor");
				_childAnchor.transform.parent = gameObject.transform;
				_childAnchor.transform.localPosition = plane.localPosition;
			}

			return _childAnchor;

		}
	}

	
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
