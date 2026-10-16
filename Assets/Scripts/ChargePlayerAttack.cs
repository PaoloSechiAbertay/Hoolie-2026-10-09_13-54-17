using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChargePlayerAttack : MonoBehaviour
{
    bool charging = true;
    public float AttackBar = 0;
    float maxCharge = 50;
    public int numberOfPlayers;
    public PlayerController[] players = new PlayerController[4];
    

    public DragonHealth dragon;

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
        print("Strike Martin!!");

        //audio input
        float maxNoise = 0;
        for (int i = 0; i < 10; i++)
        {
            float noise = (Random.Range(1f,50f)); //stand in for input    
            if (noise > maxNoise)
            {
                maxNoise = noise;
            }
            yield return new WaitForSeconds(0.1f) ;

        }
        
        
        //remove UI
        Debug.Log("stop striking, Martin");

        //deal damage based on loudest noise in time
        dragon.DamageDragon(maxNoise);

        charging = true;
    }
}
