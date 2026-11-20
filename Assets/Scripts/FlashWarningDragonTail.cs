using UnityEngine;
using System.Collections;

public class FlashWarningDragonTail : MonoBehaviour
{
    public GameObject warningText;

    private AudioSource warningAudioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(warningAudioSource == null)
        {
            warningAudioSource = GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogWarning("Warning Audio Source not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FlashWarning()
    {
        warningAudioSource.Play();
        for (int i = 0; i < 3; i++)
        {
            warningText.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            warningText.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }

        warningText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        warningText.SetActive(false);

    }
}
