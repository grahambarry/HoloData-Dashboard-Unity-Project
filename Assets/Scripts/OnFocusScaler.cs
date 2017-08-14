using System.Collections;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class OnFocusScaler : MonoBehaviour, IFocusable
{
    public Vector3 scaleFactor = new Vector3(1.2f, 1.2f, 1.2f);
    [Space(10)]
    public float timeToTargetScale = 1;
    public float timeToDefaultScale = 1;
   // public TextMesh theMetricText = new TextMesh();
    private float progress;

    private Vector3 defaultScale;

    private Coroutine coroutine;

    void Start()
    {
        defaultScale = transform.localScale;
    }

    public void OnFocusEnter()
    {
        StopCurrentCoroutineIfActive();
        coroutine = StartCoroutine(CountProgress(timeToTargetScale));
      
    }

    public void OnFocusExit()
    {
        StopCurrentCoroutineIfActive();
        coroutine = StartCoroutine(CountProgress(timeToDefaultScale * -1));
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
    //  ImbueMetricText.ImbueMetricIntTextToPoint ImbueMetricConstrutor2 = new ImbueMetricText.ImbueMetricIntTextToPoint(12);
        Vector3 targetScale = Vector3.Scale(defaultScale, scaleFactor);
        while (progress >= 0 && progress <= 1)
        {
            float increment = Time.deltaTime / time;
            progress += increment;
            transform.localScale = Vector3.Lerp(defaultScale, targetScale, progress);
            yield return null;
        }
        progress = Mathf.Clamp(progress, 0, 1);
    }

  

}