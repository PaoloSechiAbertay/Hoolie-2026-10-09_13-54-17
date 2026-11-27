using System.Collections;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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

    public GameObject[] attacks = new GameObject[5];
    public GameObject dragonSprites;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dragonAudioSource = GetComponent<AudioSource>();
        
        Health = MAXHEALTH;

        HealthBar.maxValue = MAXHEALTH;
        HealthBar.value = MAXHEALTH;
    }

    public bool DamageDragon(float damage)
    {
        Health -= damage;
        Shake.SetScreenShake(damage / 100, 1f);

        dragonAudioSource.PlayOneShot(dragonHurtSound);

        if (Health <= 0)
        {
            Health = 0;
            HealthBar.value = 0;
            StartCoroutine(KillDragon());
            return true;
        }

        HealthBar.value = Health;

        //StartCoroutine(HealthBarFade());
        return false;
    }

    IEnumerator HealthBarFade()
    {
        yield return new WaitForSeconds(5);
        HealthBar.gameObject.SetActive(false);
    }

    IEnumerator KillDragon()
    {
        //destroy attacks
        for (int i = 0; i < attacks.Length; i++)
        {
            Destroy(attacks[i]);
        }

        //win game sequance start
        dragonAudioSource.PlayOneShot(dragonDeathSound);
        Shake.SetScreenShake(1f, 1.5f);
        yield return new WaitForSeconds(1.5f);

        //dragon collapse
        for (float i = 0; i < 0.5; i += Time.deltaTime)
        {
            dragonSprites.transform.Rotate(new Vector3(-1, 0, 0), 90 * Time.deltaTime * 2);
            yield return null;
        }
        Shake.SetScreenShake(0.75f, 1.5f);

        //wait for end of cheering SFX
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("WinScene");
    }

}
