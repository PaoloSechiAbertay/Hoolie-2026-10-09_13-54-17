using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DragonHealth : MonoBehaviour
{
    [SerializeField] float Health;
    float MAXHEALTH = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health = MAXHEALTH;
    }

    public void DamageDragon(float damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            KillDragon();
        }
    }

    void KillDragon()
    {
        Debug.Log("You defeated the dragon!!");
        //win game sequance
    }
}
