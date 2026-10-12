using UnityEngine;


public class ScaleFromMicrophone : MonoBehaviour
{
    public AudioSource source;
    public Vector3 minScale;
    public Vector3 maxScale;
    public MicrophoneDetection detector;

    public float loudnessSensibility;
    public float threshold;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;

        //remove tiny changes in background noise
        if (loudness < threshold)
        { loudness = threshold; }

        Debug.Log(loudness);

        //lerp value from minscale to maxscale
        transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
    }
}
