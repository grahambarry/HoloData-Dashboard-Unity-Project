using System.Collections;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class OnFocusRotator : MonoBehaviour, IFocusable
{
    public Vector3 rotationAmount;
    [Space(10)]
    public float timeToCompleteRotation = 1;
    public float timeToDefaultRotation = 1;

    private float progress;

    private Vector3 defaultRotation;

    void Start()
    {
        defaultRotation = transform.eulerAngles;
    }

    public void OnFocusEnter()
    {
        if (progress == 0 || progress == 1)
        {
            StartCoroutine(CountProgress(timeToCompleteRotation));
        }
    }

    public void OnFocusExit()
    {
        if (progress == 0 || progress == 1)
        {
            StartCoroutine(CountProgress(timeToDefaultRotation * -1));
        }
    }

    IEnumerator CountProgress(float time)
    {
        Vector3 targetRotation = defaultRotation + rotationAmount;
        while (progress >= 0 && progress <= 1)
        {
            float increment = Time.deltaTime / time;
            progress += increment;
            transform.eulerAngles = Vector3.Lerp(defaultRotation, targetRotation, progress);

            yield return null;
        }
        progress = Mathf.Clamp(progress, 0, 1);
    }
}