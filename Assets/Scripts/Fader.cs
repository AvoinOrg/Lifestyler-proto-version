using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public static Fader Instance;

    public Image fade;
    public float fadeTime = 0.33f;
    public AnimationCurve fadeCurve;
    Color targetColor;

    void Awake()
    {
        Instance = this;
        fade.color = Color.clear;
    }

    public void FadeIn(Color color)
    {
        StartCoroutine("FadeInAnim", color);
    }

    public void FadeOut()
    {
        StartCoroutine("FadeOutAnim");
    }

    IEnumerator FadeInAnim(Color color)
    {
        float v = 0.0f;

        while (v < fadeTime)
        {
            v += Time.deltaTime;
            fade.color = Color.Lerp(Color.clear, color, fadeCurve.Evaluate(v / fadeTime));
            yield return null;
        }

        fade.color = color;
    } 

    IEnumerator FadeOutAnim()
    {
        Color color = fade.color;
        float v = fadeTime;
   
        while (v > 0.0f)
        {
            v -= Time.deltaTime;
            fade.color = Color.Lerp(Color.clear, color, fadeCurve.Evaluate(v / fadeTime));
            yield return null;
        }

        fade.color = Color.clear;
    }

}
