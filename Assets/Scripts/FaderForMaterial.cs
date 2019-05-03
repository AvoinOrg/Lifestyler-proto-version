using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaderForMaterial : MonoBehaviour
{
    public static FaderForMaterial Instance;

    public MeshRenderer fade;
    public float fadeTime = 0.33f;
    public AnimationCurve fadeCurve;
    Color targetColor;

    void Awake()
    {
        Instance = this;
        fade.material.SetColor("_Color", Color.clear);
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
            fade.material.SetColor("_Color", Color.Lerp(Color.clear, color, fadeCurve.Evaluate(v / fadeTime)));
            yield return null;
        }

        fade.material.SetColor("_Color", color);
    }

    IEnumerator FadeOutAnim()
    {
        Color color = fade.material.GetColor("_Color");
        float v = fadeTime;

        while (v > 0.0f)
        {
            v -= Time.deltaTime;
            fade.material.SetColor("_Color", Color.Lerp(Color.clear, color, fadeCurve.Evaluate(v / fadeTime)));
            yield return null;
        }

        fade.material.SetColor("_Color", Color.clear);
    }
}
