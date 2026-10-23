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
    public UnityEngine.UI.Slider NoiseBar;
    public UnityEngine.UI.Slider DragonHealthBar;

    public float baseDamage = 5f;
    public float maxDamage = 50f;
    public float minDamage = 5f;

    bool charging = true;
    public float AttackBar = 0;
    float maxCharge = 50;
    public int numberOfPlayers;
    public PlayerController[] players = new PlayerController[4];
    

    public DragonHealth dragon;

    private void Start()
    {
        NoiseBar.gameObject.SetActive(false);
        DragonHealthBar.gameObject.SetActive(false);
    }

    private void Update()
    {
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
        NoiseBar.gameObject.SetActive(true);
        DragonHealthBar.gameObject.SetActive(true);
        print("Strike Martin!!");

        //audio input
        float maxNoise = 0;
        for (int i = 0; i < 50; i++)
        {
            //check mic noise
            float noise = microphoneDetection.GetLoudnessFromMicrophone() * loudnessSensibility;
            Debug.Log("you were: " + noise + " loud");
            NoiseBar.value = noise;

            //find loudest point
            if (noise > maxNoise)
            {
                maxNoise = noise;
            }
            yield return new WaitForSeconds(0.1f) ;

        }
        Debug.Log("the loudest you were was: " + maxNoise + " loud");

        //remove UI
        Debug.Log("stop striking, Martin");
        NoiseBar.gameObject.SetActive(false);

        float damage = baseDamage * maxNoise;
        if (damage > maxDamage) { damage = maxDamage; }
        if (damage < minDamage) { damage = minDamage; }

        //deal damage based on loudest noise in time
        dragon.DamageDragon(damage);

        charging = true;
    }
}
