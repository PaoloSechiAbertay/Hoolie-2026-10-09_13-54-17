using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;

public class TailAttack : MonoBehaviour
{
    Rigidbody Rb;
    Coroutine clock;
    Coroutine attack;


    public FlashWarningDragonTail flashWarning;

    bool Onleft = true;
    public bool attacking = false;

    float speed = 10;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        clock = StartCoroutine(Clock());
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            if (attack == null)
            {
                attack = StartCoroutine(Attack());
            }
        }
        else if (clock == null)
        {
            clock = StartCoroutine(Clock());
        }
    }

    IEnumerator Clock()
    {
        yield return new WaitForSeconds(Random.Range(10f, 20f));
        attacking = true;
        clock = null;
    }

    IEnumerator Attack()
    {
        flashWarning.StartCoroutine(flashWarning.FlashWarning());

        yield return new WaitForSeconds(2f);

        if (Onleft)
        {
            Rb.linearVelocity = Vector3.right * speed;
            while (transform.position.x < 15)
            {
                yield return null;
            }
            transform.position = new Vector3(15, transform.position.y, transform.position.z);
            Rb.linearVelocity = Vector3.zero;
            Onleft = false;
            attacking = false;
        }
        else
        {
            Rb.linearVelocity = Vector3.left * speed;
            while (transform.position.x > -15)
            {
                yield return null;
            }
            transform.position = new Vector3(-15, transform.position.y, transform.position.z);
            Rb.linearVelocity = Vector3.zero;
            Onleft = true;
            attacking = false;
        }

        attack = null;
    }

}
