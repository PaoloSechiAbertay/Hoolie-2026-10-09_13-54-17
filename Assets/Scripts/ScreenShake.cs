using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    //based on https://gist.github.com/ftvs/5822103

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    public IEnumerator Shake(float shakeDuration, float shakeAmount, float decreaseFactor)
    {
        float Duration = shakeDuration;
        if (Duration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            Duration -= decreaseFactor * Time.deltaTime;
        }
        else
        {
            Duration = 0f;
            camTransform.localPosition = originalPos;
        }
        yield return null;
    }
}