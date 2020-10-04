using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public Image fader;
    public bool fadingIn;
    public bool fadingOut;
    public float fadeSpeed = 2f;
    private readonly float initFadeSpeed = 2f;
    private readonly Color alphaOne = new Color(0f, 0f, 0f, 1f);

    private void Update()
    {
        if (fadingIn)
        {
            fader.color -= alphaOne * fadeSpeed * Time.deltaTime;
        }
        else if (fadingOut)
        {
            fader.color += alphaOne * fadeSpeed * Time.deltaTime;
        }
    }

    public IEnumerator SimpleFade(bool realtime) 
    {
        fadingOut = true;

        if(realtime)
            yield return new WaitForSecondsRealtime(2f);
        else
            yield return new WaitForSeconds(2f);

	fadingOut = false;

        if(realtime)
            yield return new WaitForSecondsRealtime(0.6f);
        else
            yield return new WaitForSeconds(0.6f);

        fadingIn = true;
        
        if(realtime)
            yield return new WaitForSecondsRealtime(2f);
        else
            yield return new WaitForSeconds(2f);
        
        fadingIn = false;
    }

    public IEnumerator BlinkSequence(bool realtime, float fadeOutTime, float fullFadeTime, float fadeInTime, float fadeSpeedOut, float fadeSpeedIn)
    {
        fadeSpeed = fadeSpeedOut;
        fadingOut = true;

        if(realtime)
            yield return new WaitForSecondsRealtime(fadeOutTime);
        else
            yield return new WaitForSeconds(fadeOutTime);

        fadingOut = false;

        if(realtime)
            yield return new WaitForSecondsRealtime(fullFadeTime);
        else
            yield return new WaitForSeconds(fullFadeTime);

        fadeSpeed = fadeSpeedIn;
        fadingIn = true;

        if(realtime)
            yield return new WaitForSecondsRealtime(fadeInTime);
        else
            yield return new WaitForSeconds(fadeInTime);

        fadingIn = false;
        fadeSpeed = initFadeSpeed;
    }

    public IEnumerator FadeIn()
    {
        fadingIn = true;
        yield return new WaitForSeconds(3f);
        fadingIn = false;
    }

    public IEnumerator FadeOut()
    {
        fadingOut = true;
        yield return new WaitForSeconds(3f);
        fadingOut = false;
    }
}
