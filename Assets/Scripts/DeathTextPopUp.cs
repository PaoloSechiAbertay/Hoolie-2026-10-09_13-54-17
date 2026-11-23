using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeathTextPopUp : MonoBehaviour
{
    public PlayerController player;
    public GameObject UI;
    public GameObject text;
    public Slider timer;
    bool showingUI = false;

    // Update is called once per frame
    void Update()
    {
        if (!showingUI && player.playerHealth <= 0)
        {
            ShowUI();
        }
        else if (showingUI && player.playerHealth > 0)
        {
            RemoveUI();
        }
    }

    public void ShowUI()
    {
        showingUI = true;
        UI.SetActive(true);
        text.SetActive(false);
        timer.gameObject.SetActive(true);
        timer.value = 0;
        StartCoroutine(RespawnTimer());
    }

    void RemoveUI()
    {
        showingUI = false;
        UI.SetActive(false);
        text.SetActive(false);
        timer.gameObject.SetActive(false);
        timer.value = 0;
    }

    IEnumerator RespawnTimer()
    {
        for (float t = 0; t < timer.maxValue; t += Time.deltaTime)
        {
            timer.value = t;
            yield return null;
        }
        timer.gameObject.SetActive(false);
        timer.value = 0;
        text.SetActive(true);
    }
}
