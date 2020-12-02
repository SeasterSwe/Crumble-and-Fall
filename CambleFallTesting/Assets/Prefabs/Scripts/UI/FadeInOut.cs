using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This Script Fades an Image in or out
public class FadeInOut : MonoBehaviour
{
    
    // Start is called before the first frame update
    public void FadeIn(bool fadeInElseOut, float fadeTime)
    {
        if (fadeInElseOut)
        {
            StartCoroutine(FadeIn(fadeTime, GetComponent<Image>().color, GetComponent<Image>() ));
        }
        else
        {
            StartCoroutine(FadeOut(fadeTime, GetComponent<Image>().color, GetComponent<Image>()));
        }
    }
    private IEnumerator FadeIn(float fullTime, Color orgColor, Image img)
    {
        float fadeTimeLeft = fullTime;
        orgColor.a = 0;
        while(fadeTimeLeft > 0)
        {
            orgColor.a += Time.deltaTime / fullTime;
            img.color = orgColor;
            fadeTimeLeft -= Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeOut(float fullTime, Color orgColor, Image img)
    {
        float fadeTimeLeft = fullTime;
        orgColor.a = 1;
        while (fadeTimeLeft > 0)
        {
            orgColor.a -= Time.deltaTime / fullTime;
            img.color = orgColor;
            fadeTimeLeft -= Time.deltaTime;
            yield return null;
        }
    }
}
