using UnityEngine;


public class ScalefromAudioClip : MonoBehaviour
{
    public AudioSource source;
    public Vector3 minScale;
    public Vector3 maxScale;
    public MicrophoneDetection detector;

    public float loudnessSensibility = 0.1f;
    public float threshold = 1000f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float loudness = detector.GetLoudnessFromAudioClip(source.timeSamples, source.clip) * loudnessSensibility;


        if(loudness<threshold)
            loudness = 0;
             
        //lerp value from minscale to maxscale
        transform.localScale = Vector3.Lerp(minScale, maxScale, loudness);
    }
}
