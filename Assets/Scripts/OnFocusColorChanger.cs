using System.Collections;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class OnFocusColorChanger : MonoBehaviour, IFocusable
{
    public Color focusedColor;
    [Space(10)]
    public float fadeInTime = 1;
    public float fadeOutTime = 1;

    private float progress;

    private Material material;
    private Color unfocusedColor;

    private Coroutine coroutine;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        unfocusedColor = material.GetColor("_Color");
    }

    public void OnFocusEnter()
    {
        StopCurrentCoroutineIfActive();
        coroutine = StartCoroutine(CountProgress(fadeInTime));
    }

    public void OnFocusExit()
    {
        StopCurrentCoroutineIfActive();
        coroutine = StartCoroutine(CountProgress(fadeOutTime * -1));
    }

    private void StopCurrentCoroutineIfActive()
    {
        if (progress != 0 && progress != 1)
        {
            StopCoroutine(coroutine);
        }
    }

    IEnumerator CountProgress(float time)
    {
        while (progress >= 0 && progress <= 1)
        {
            float increment = Time.deltaTime / time;
            progress += increment;
            material.SetColor("_Color", Color.Lerp(unfocusedColor, focusedColor, progress));
            yield return null;
        }
        progress = Mathf.Clamp(progress, 0, 1);
    }
}