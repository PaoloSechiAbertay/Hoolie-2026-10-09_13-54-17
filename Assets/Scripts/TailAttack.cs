using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;

public class TailAttack : MonoBehaviour
{
    Rigidbody Rb;
    Coroutine clock;
    Coroutine attack;

    public GameObject tailTop;
    public GameObject DragonRight;
    public GameObject DragonLeft;
    public GameObject DragonSitLeft;
    public GameObject DragonSitRight;

    public FlashWarningDragonTail flashWarning;

    bool Onleft = true;
    public bool attacking = false;

    bool rotationCheck = false;
    public Vector3 rotation;
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

        tailRotation();
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
            DragonSitLeft.SetActive(true);
            DragonLeft.SetActive(false);
        }
        else
        {
            DragonSitRight.SetActive(true);
            DragonRight.SetActive(false);
        }

        tailTop.SetActive(true);


        if (Onleft)
        { 
            rotationCheck = true;

            yield return new WaitForSeconds(1.8f);
            transform.rotation = Quaternion.Euler(transform.rotation.x, -90, transform.rotation.z);
            Onleft = false;
            rotationCheck = false;  
            attacking = false;

            DragonSitLeft.SetActive(false);
            DragonRight.SetActive(true);

        }
        else
        {
            rotationCheck = true;

            yield return new WaitForSeconds(1.8f);
            transform.rotation = Quaternion.Euler(transform.rotation.x,90, transform.rotation.z);
            Onleft = true;
            rotationCheck = false;
            attacking = false;

            DragonSitRight.SetActive(false);
            DragonLeft.SetActive(true);
        }

        attack = null;
        tailTop.SetActive(false);
    }

    void tailRotation()
    {

        if (rotationCheck)
        {
            if (Onleft)
            {

                transform.Rotate(rotation * Time.deltaTime * speed);
            }
            else if (!Onleft)
            {

                transform.Rotate((-1*rotation) * Time.deltaTime * speed);
            }
        }
    }

}
