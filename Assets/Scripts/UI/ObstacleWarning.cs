using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObstacleWarning : MonoBehaviour {

	private Image warningImg;

	public GameManager.Side side;
	// Use this for initialization
	void Start () {
		warningImg = GetComponent<Image>();
		EventManager.OnObstacleWarning += (show, side) => {
			if(!this.side.Equals(side)) { return; }
			Color currentColor = warningImg.color;
			if(!show) {
				currentColor.a = 0;
			} else {
				currentColor.a = 255;
			}
			warningImg.color = currentColor;
		};
	}
}
