using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FadeScene : MonoBehaviour
{
    private float fadingSpeed = 1.0f;
    [SerializeField]
    private float alphaMin = 0;
    [SerializeField]
    private float alphaMax = 1.0f;
    [SerializeField]
    private bool startingVisibility = false;
    [SerializeField]
    private bool fadeOnAwake = false;
    [SerializeField]
    private bool continuous = false;
    [SerializeField]

    private SpriteRenderer blackSprite = null;

    public bool isVisible
    {
        get
        {
            if (blackSprite.color.a == alphaMax)
                return true;
            else return false;
        }

    }

    public bool isHidden
    {
        get
        {
            if (blackSprite.color.a == alphaMin)
                return true;
            else return false;
        }

    }
    public float fadeSpeed
    {
        get
        {
            return fadingSpeed;
        }
        set
        {
            fadingSpeed = value;
        }
    }
    public float minAlpha
    {
        get
        {
            return alphaMin;
        }
        set
        {
            alphaMin = value;
        }
    }
    public float maxAlpha
    {
        get
        {
            return alphaMax;
        }
        set
        {
            alphaMax = value;
        }
    }
    public bool continues
    {
        get
        {
            return continuous;
        }
        set
        {
            continuous = value;
        }
    }

    void Start()
    {
        blackSprite = GetComponent<SpriteRenderer>();
        if (blackSprite == null)
        {
            Debug.LogError("Pichal' de Sprite renderer?");
            return;
        }

        if (startingVisibility)
        {
            Color spriteColor = blackSprite.color;
            spriteColor.a = maxAlpha;
            blackSprite.color = spriteColor;
            if (fadeOnAwake)
            {
                StartCoroutine(FadeOut());
            }
        }
        else
        {
            Color spriteColor = blackSprite.color;
            spriteColor.a = alphaMin;
            blackSprite.color = spriteColor;
            if (fadeOnAwake)
            {
                StartCoroutine(FadeIn());
            }
        }

    }

    public IEnumerator FadeIn()
    {
        Color spriteColor = blackSprite.color;

        while (spriteColor.a < alphaMax)
        {
            yield return null;
            spriteColor.a += fadingSpeed * Time.deltaTime;
            blackSprite.color = spriteColor;
        }
        spriteColor.a = alphaMax;
        blackSprite.color = spriteColor;

        if (continuous)
        {
            StartCoroutine(FadeOut());
        }
    }
    public IEnumerator FadeOut()
    {
        Color spriteColor = blackSprite.color;

        while (spriteColor.a > alphaMin)
        {
            yield return null;
            spriteColor.a -= fadingSpeed * Time.deltaTime;
            blackSprite.color = spriteColor;
        }
        spriteColor.a = alphaMin;
        blackSprite.color = spriteColor;

        if (continuous)
        {
            StartCoroutine(FadeIn());
        }
    }

    /*
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

    public IEnumerator LoadScene(string sceneName, string music)
    {

        // Fade to black
        yield return StartCoroutine(m_blackness.FadeInAsync());

        // Load loading screen
        yield return Application.LoadLevelAsync("LoadingScreen");

        // !!! unload old screen (automatic)

        // Fade to loading screen
        yield return StartCoroutine(m_blackness.FadeOutAsync());

        float endTime = Time.time + m_minDuration;

        // Load level async
        yield return Application.LoadLevelAdditiveAsync(sceneName);

        if (Time.time < endTime)

            yield return new WaitForSeconds(endTime - Time.time);

        // Load appropriate zone's music based on zone data
        MusicManager.PlayMusic(music);

        // Fade to black
        yield return StartCoroutine(m_blackness.FadeInAsync());

        // !!! unload loading screen
        LoadingSceneManager.UnloadLoadingScene();

        // Fade to new screen
        yield return StartCoroutine(m_blackness.FadeOutAsync());

    }
    */

}
