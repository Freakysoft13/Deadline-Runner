    using UnityEngine;
    using System.Collections;

    public class FadeScene : MonoBehaviour
    {

        public Texture2D fadeOutTexture;                                                    //overlay texture
        public float fadeSpeed = 0.0f;                                                      //fading speed

        private int drawDepth = -1000;                                                      //the texture's order in the draw hierarchy (the lower number mean top postion)
        private float alpha = 1.0f;                                                         //the texture's alpha value between 1 and 0
        private int fadeDir = -1;                                                           //fade direction -1 = in 1 = out


        void OnGUI()
        {
            //fade out/in the alpha value using a direction,a speed and Time.deltaTime to convert the operation to seconds
            alpha += fadeDir * fadeSpeed * Time.deltaTime;
            //force (clamp) the number between 0 and 1 because GUI.color uses alpha between 0 and 1 value
            alpha = Mathf.Clamp01(alpha);

            //set color of the GUI. All color values remain the same & the Alpha is set to the alpa variable
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);            //seting alpha
            GUI.depth = drawDepth;                                                          // make the black texture render on top (draw last)
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);   // draw the texture to fit the entire screen area

        }

        // sets fadeDir to the direction parameter making the scene fade in -1 and out if 1
        public float BeginFade(int direction)
        {
            fadeDir = direction;
            return (fadeSpeed);    //return the fadeSpeed variable so it's easy to time the Application.LoadLevel();
        }

        //OnLevelWasLoaded is called when a level is loaded. It takes loaded level index (int)
        void OnLevelWasLoaded()
        {
            //alpha =1;     //use this if alpha is not set 1 by default;
            BeginFade(-1);  //call the fade in function;
        }

    }
