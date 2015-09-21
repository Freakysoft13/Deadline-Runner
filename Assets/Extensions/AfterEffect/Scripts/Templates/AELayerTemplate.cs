using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AELayerTemplate  {

	public int id;

	public int index;
	public int parent;
	
	public int width  = 1;
	public int height = 1;

	public string name;

	public AELayerType type;
	public AELayerBlendingType blending;
	public AELayerBlendingType forcedBlending = AELayerBlendingType.NORMAL;



	public int inFrame;
	public int outFrame;

	public float inTime;
	public float outTime;


	public string source;
	
	public List<AEFrameTemplate> frames =  new List<AEFrameTemplate>();

	
	public void addFrame(AEFrameTemplate frame) {
		frames.Add (frame);
	}

	public void setInOutTime(float timeIn, float timeOut, AECompositionTemplate tpl) {
		inTime = timeIn;
		outTime = timeOut;

		inFrame = Mathf.FloorToInt (inTime / tpl.frameDuration);
		outFrame = Mathf.FloorToInt (outTime / tpl.frameDuration);
		if (outFrame + 1 == tpl.totalFrames) {
			outFrame = tpl.totalFrames;
		}
	}


	public AEFrameTemplate GetFrame(int index) {
		if(index >= frames.Count) {
			return null;
		} else {
			return frames [index];
		}
	}


	public int totalFrames {
		get {
			return frames.Count;
		}
	}

	public int lastFrameIndex  {
		get {
			return totalFrames - 1;
		}
	}


	public string sourceNoExt {
		get {
			return source.Substring (0, source.Length - 4);
		}
	}




	
}
