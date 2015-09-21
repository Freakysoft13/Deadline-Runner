using UnityEngine;
using System.Collections;

public class WheelRotator : MonoBehaviour {
    public Animator s;


	void Start ()
    {
        FortunePick();
    }

    void FortunePick ()
    {
        int r = Random.Range(1, 100);
        print(r);
        if (r >= 1 && r <= 25)
        {
            print("First Option");
        }
        else if (r >= 26 && r <= 45)
        {
            print("Second Option");
        }
        else if (r >= 46 && r <= 65)
        {
            print("Third Option");
        }
        else if (r >= 66 && r <= 88)
        {
            print("Fourth Option");
        }
        else if (r >= 89 && r <= 100)
        {
            print("Fifth Option");
        }
    }
	
	void Update ()
    {
        //transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,0,-120),Time.deltaTime*rotationspeed);
	}
}
