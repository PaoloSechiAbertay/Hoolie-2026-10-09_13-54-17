using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChargePlayerAttack : MonoBehaviour
{
    [SerializeField] MicrophoneDetection microphoneDetection;
    public UnityEngine.UI.Slider MaxNoiseBar;
    public UnityEngine.UI.Slider NoiseBar;
    public UnityEngine.UI.Slider DragonHealthBar;
    public UnityEngine.UI.Slider MartinChargingBar;
    public GameObject StrikeMartinText;
    //public UnityEngine.UI.Image MicColour;

    public float baseDamage = 5f;
    public float maxDamage = 50f;
    public float minDamage = 5f;

    bool charging = true;
    public float AttackBar = 0;
    float maxCharge = 50;
    public int numberOfPlayers;
    public PlayerController[] players = new PlayerController[4];
    public GameObject MartinSprite;


    public DragonHealth dragon;

    public bool Striking = false;
    public TailAttack tail;

    public AudioSource StrikeWarningAudioSource; // source for strike martin warning sound

    private void Start()
    {
        MaxNoiseBar.gameObject.SetActive(false);
        NoiseBar.gameObject.SetActive(false);
        StrikeMartinText.gameObject.SetActive(false);
        MartinSprite.SetActive(false);
        MartinChargingBar.gameObject.SetActive(true);
    }

    private void Update()
    {
        MartinChargingBar.value = AttackBar;

        if (AttackBar >= maxCharge)
        {
            tail.attack = StartCoroutine(StrikeMartin());
        }
        
        if (charging)
        {
            numberOfPlayers = 0;
            foreach (PlayerController player in players)
            {
                if (player.playing)
                {
                    numberOfPlayers++;
                }
            }
            foreach (PlayerController player in players)
            {
                if (player.playing && !player.dead)
                {
                    ChargeBar();
                }
            }
            
        }
    }

    public void ChargeBar()
    {
        //scale with players but still push having most players
        //4 players do 0.45 each, adding to 1.8 per second
        //3 do 0.533 each, 1.6
        //2 do 0.7 each, 1.4
        //1 does 1.2, 1.2  
        AttackBar += ((1f / numberOfPlayers) + 0.2f) * Time.deltaTime; 
    }

    IEnumerator StrikeMartin()
    {
        charging = false;
        AttackBar = 0;
        Striking = true;

        MartinChargingBar.gameObject.SetActive(false);
        MartinSprite.SetActive(true);

        yield return new WaitForSeconds(1.2f);

        //UI for strike martin
        MaxNoiseBar.gameObject.SetActive(true);
        NoiseBar.gameObject.SetActive(true);
        StrikeMartinText.gameObject.SetActive(true);

        //play warning sound
        StrikeWarningAudioSource.Play();
        Debug.Log("Strike Martin Warning Sound Played");

        //audio input
        float maxNoise = 0;
        for (int i = 0; i < 55; i++)
        {
            //check mic noise
            float noise = (microphoneDetection.GetLoudnessFromMicrophone() * MicSettingsManager.instance.loudnessSensibility * MicSettingsManager.instance.sensitivitySetting) - MicSettingsManager.instance.noiseDampeningSetting;

            NoiseBar.value = noise;
            //if (noise > 1f) { MicColour.enabled = true; }
            //else { MicColour.enabled = false; }

            //find loudest point
            if (noise > maxNoise)
            {
                maxNoise = noise;
                MaxNoiseBar.value = maxNoise;
            }
            yield return new WaitForSeconds(0.1f) ;
        }

        //remove most UI
        MaxNoiseBar.value = 0;
        MaxNoiseBar.gameObject.SetActive(false);
        NoiseBar.gameObject.SetActive(false);
        StrikeMartinText.gameObject.SetActive(false);

        for (int i = 0; i < players.Length; i++)
        {
            if (!players[i].dead)
            {
                StartCoroutine(players[i].Attack());
            }
        }
        StartCoroutine(MartinSprite.GetComponent<MartinSpriteAttack>().Attack());
        yield return new WaitForSeconds(0.6f);

        //deal damage based on loudest noise in time
        float damage = baseDamage * maxNoise;
        if (damage > maxDamage) { damage = maxDamage; }
        if (damage < minDamage) { damage = minDamage; }

        if (!dragon.DamageDragon(damage)) // if dragon isn't dead
        {
            yield return new WaitForSeconds(1f);

            //remove martin & reset variables
            MartinSprite.SetActive(false);
            MartinChargingBar.gameObject.SetActive(true);
            charging = true;
            Striking = false;
            tail.attack = null;
        }

    }
}
