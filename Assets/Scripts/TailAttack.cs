using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TailAttack : MonoBehaviour
{

    bool Onleft = true;
    public bool attacking = true;

    public float speed = 5;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (Onleft)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x > 15)
            {
                Onleft = false;
                attacking = false;
            }
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x < -15)
            {
                Onleft = true;
                attacking = false;
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            print(collision.gameObject + "hit");
        }
    }
}
