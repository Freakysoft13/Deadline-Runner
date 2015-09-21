using UnityEngine;
using System.Collections;

[System.Serializable]
public class AEFrameTemplate  {

	public int index;


	public Vector3 scale;
	public Vector3 pivot;

	public float rotation;
	public float opacity;


	public Vector3 position;
	public Vector3 positionUnity;



	public bool IsNothingChnaged = false;

	public bool IsScaleChanged = true;
	public bool IsPivotChnaged = true;

	public bool IsRotationChanged = true;
	public bool IsOpcityChanged = true;

	public bool IsPositionChanged = true;



	public void SetPosition(Vector3 pos) {
		position = pos;
		positionUnity = position;
		positionUnity.y = positionUnity.y * (-1);
	}



	public void CompareToFrame(AEFrameTemplate lastFrame) {
		if(lastFrame == null) {
			return;
		}

	

		if(V3Equal(scale, lastFrame.scale)) {
			IsScaleChanged = false;
		}

		if(V3Equal(pivot, lastFrame.pivot)) {
			IsPivotChnaged = false;
		}

		if(V3Equal(position, lastFrame.position)) {
			IsPositionChanged = false;
		}

		if(FCompare(rotation, lastFrame.rotation)) {
			IsRotationChanged = false;
		}

		if(FCompare(opacity, lastFrame.opacity)) {
			IsOpcityChanged = false;
		}


	}



	private bool V3Equal(Vector3 a, Vector3 b){
		return Vector3.SqrMagnitude(a - b) < 0.0001;
	}

	private bool FCompare(float a, float b){
		return Mathf.Abs(a - b) < 0.01;
	}

}
