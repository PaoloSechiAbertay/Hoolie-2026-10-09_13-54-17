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
    public float loudnessSensibility = 100f;
    public UnityEngine.UI.Slider MaxNoiseBar;
    public UnityEngine.UI.Slider NoiseBar;
    public UnityEngine.UI.Slider DragonHealthBar;
    public GameObject StrikeMartinText;
    public UnityEngine.UI.Image MicColour;

    public float sensitivitySetting = 1;
    public float noiseDampeningSetting = 0;

    public float baseDamage = 5f;
    public float maxDamage = 50f;
    public float minDamage = 5f;

    bool charging = true;
    public float AttackBar = 0;
    float maxCharge = 50;
    public int numberOfPlayers;
    public PlayerController[] players = new PlayerController[4];
    

    public DragonHealth dragon;

    InputActionMap numbers;

    private void Start()
    {
        MaxNoiseBar.gameObject.SetActive(false);
        NoiseBar.gameObject.SetActive(false);
        //DragonHealthBar.gameObject.SetActive(false);
        StrikeMartinText.gameObject.SetActive(false);


        numbers = InputSystem.actions.FindActionMap("Numbers");
    }

    private void Update()
    {

        updateSettings();

        if (AttackBar >= maxCharge)
        {
            StartCoroutine(StrikeMartin());
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

        //UI for strike martin
        MaxNoiseBar.gameObject.SetActive(true);
        NoiseBar.gameObject.SetActive(true);

        //DragonHealthBar.gameObject.SetActive(true);
        StrikeMartinText.gameObject.SetActive(true);
        print("Strike Martin!!");

        //audio input
        float maxNoise = 0;
        for (int i = 0; i < 50; i++)
        {
            //check mic noise
            float noise = (microphoneDetection.GetLoudnessFromMicrophone() * loudnessSensibility * (1 / sensitivitySetting)) - noiseDampeningSetting;

            NoiseBar.value = noise;
            if (noise > 1f) { MicColour.enabled = true; }
            else { MicColour.enabled = false; }

            //find loudest point
            if (noise > maxNoise)
            {
                maxNoise = noise;
                MaxNoiseBar.value = maxNoise;
            }
            yield return new WaitForSeconds(0.1f) ;

        }
       
        //remove UI
        Debug.Log("stop striking, Martin");
        MaxNoiseBar.gameObject.SetActive(false);
        NoiseBar.gameObject.SetActive(false);
        StrikeMartinText.gameObject.SetActive(false);

        float damage = baseDamage * maxNoise;
        if (damage > maxDamage) { damage = maxDamage; }
        if (damage < minDamage) { damage = minDamage; }

        //deal damage based on loudest noise in time
        dragon.DamageDragon(damage);

        charging = true;
    }


    void updateSettings()
    {

        if (numbers.FindAction("One").WasPressedThisFrame()) { sensitivitySetting = 1; }
        else if (numbers.FindAction("Two").WasPressedThisFrame()) { sensitivitySetting = 2; }
        else if (numbers.FindAction("Three").WasPressedThisFrame()) { sensitivitySetting = 3; }
        else if (numbers.FindAction("Four").WasPressedThisFrame()) { sensitivitySetting = 4; }
        else if (numbers.FindAction("Five").WasPressedThisFrame()) { sensitivitySetting = 5; }
        else if (numbers.FindAction("Six").WasPressedThisFrame()) { sensitivitySetting = 6; }
        else if (numbers.FindAction("Seven").WasPressedThisFrame()) { sensitivitySetting = 7; }
        else if (numbers.FindAction("Eight").WasPressedThisFrame()) { sensitivitySetting = 8; }
        else if (numbers.FindAction("Nine").WasPressedThisFrame()) { sensitivitySetting = 9; }
        else if (numbers.FindAction("Zero").WasPressedThisFrame()) { sensitivitySetting = 10; }

        if (numbers.FindAction("0").WasPressedThisFrame()) { noiseDampeningSetting = 0; }
        else if(numbers.FindAction("1").WasPressedThisFrame()) { noiseDampeningSetting = 1; }
        else if(numbers.FindAction("2").WasPressedThisFrame()) { noiseDampeningSetting = 2; }
        else if(numbers.FindAction("3").WasPressedThisFrame()) { noiseDampeningSetting = 3; }
        else if(numbers.FindAction("4").WasPressedThisFrame()) { noiseDampeningSetting = 4; }
        else if(numbers.FindAction("5").WasPressedThisFrame()) { noiseDampeningSetting = 5; }
        else if(numbers.FindAction("6").WasPressedThisFrame()) { noiseDampeningSetting = 6; }
        else if(numbers.FindAction("7").WasPressedThisFrame()) { noiseDampeningSetting = 7; }
        else if(numbers.FindAction("8").WasPressedThisFrame()) { noiseDampeningSetting = 8; }
        else if(numbers.FindAction("9").WasPressedThisFrame()) { noiseDampeningSetting = 9; }

        print(sensitivitySetting);
        print(noiseDampeningSetting);
    }
}
