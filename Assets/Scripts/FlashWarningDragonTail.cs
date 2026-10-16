using UnityEngine;
using System.Collections;

public class FlashWarningDragonTail : MonoBehaviour
{
    public GameObject warningText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FlashWarning()
    {
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
