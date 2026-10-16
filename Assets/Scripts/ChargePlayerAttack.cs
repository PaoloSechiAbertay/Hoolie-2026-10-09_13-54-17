using UnityEditor;
using UnityEngine;

public class ChargePlayerAttack : MonoBehaviour
{
    public float AttackBar = 0;
    float maxCharge = 100;
    public int numberOfPlayers;
    public PlayerController[] players = new PlayerController[4];

    private void Update()
    {
        if (AttackBar >= maxCharge)
        {
            //run martin strike
            print("Strike Martin!!");
            AttackBar = 0;
        }
        foreach(PlayerController player in players)
        {
            if (player.on && !player.dead)
            {
                ChargeBar();
            }
        }
    }

    public void ChargeBar()
    {
        //scale with players but still push having most players
        //4 players do 0.45 each, adding to 1.8,
        //3 do 0.533 each, 1.6
        //2 do 0.7 each, 1.4
        //1 does 1, 1.2  
        AttackBar += (1f / numberOfPlayers) + 0.2f; 
    }
}
