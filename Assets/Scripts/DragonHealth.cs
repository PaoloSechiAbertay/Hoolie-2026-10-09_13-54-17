using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DragonHealth : MonoBehaviour
{
    [SerializeField] float Health;
    float MAXHEALTH = 100;

    public ScreenShake Shake;
    public UnityEngine.UI.Slider HealthBar;

    AudioSource dragonAudioSource;
    public AudioClip dragonHurtSound;
    public AudioClip dragonDeathSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dragonAudioSource = GetComponent<AudioSource>();
        
        Health = MAXHEALTH;

        HealthBar.maxValue = MAXHEALTH;
        HealthBar.value = MAXHEALTH;
    }

    public void DamageDragon(float damage)
    {
        Health -= damage;
        Shake.shakeDuration = 1f;
        Shake.shakeAmount = damage / 100;

        dragonAudioSource.PlayOneShot(dragonHurtSound);

        if (Health < 0)
        {
            Health = 0;
            StartCoroutine(KillDragon());
        }

        HealthBar.value = Health;

        StartCoroutine(HealthBarFade());
    }

    IEnumerator HealthBarFade()
    {
        yield return new WaitForSeconds(5);
        HealthBar.gameObject.SetActive(false);
    }

    IEnumerator KillDragon()
    {
        dragonAudioSource.PlayOneShot(dragonDeathSound);
        Debug.Log("You defeated the dragon!!");
        //win game sequance
        Shake.shakeDuration = 1.5f;
        Shake.shakeAmount = 1f;
        yield return new WaitForSeconds(7f);
        Application.Quit();

    }
}
