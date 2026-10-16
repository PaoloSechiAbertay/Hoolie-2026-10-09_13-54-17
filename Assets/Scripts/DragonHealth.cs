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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health = MAXHEALTH;
    }

    public void DamageDragon(float damage)
    {
        Health -= damage;
        StartCoroutine(Shake.Shake(1, damage / 100, 1));
        if (Health < 0)
        {
            KillDragon();
        }
    }

    void KillDragon()
    {
        Debug.Log("You defeated the dragon!!");
        //win game sequance
        StartCoroutine(Shake.Shake(2, 1, 1));
    }
}
