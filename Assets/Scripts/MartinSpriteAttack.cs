using System.Collections;
using UnityEngine;

public class MartinSpriteAttack : MonoBehaviour
{
    Vector3 Home;
    public Vector3 attackTarget;
    float attackSpeed = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Home = transform.position;
    }


    public IEnumerator Attack()
    {
        //rotate towards dragon
        for (float i = 0; i < 0.1; i += Time.deltaTime)
        {
            transform.Rotate(new Vector3(1, 0, 0), 45 * Time.deltaTime * 10); //45 degrees of rotation, time since last frame * 10 because we want it done in a tenth of a second
            yield return null;
        }

        //attack
        for (float i = 0; i < 0.5; i += Time.deltaTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, attackTarget, Time.deltaTime * attackSpeed);
            yield return null;
        }

        //reset
        for (float i = 0; i < 0.5; i += Time.deltaTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, Home, Time.deltaTime * attackSpeed);
            yield return null;
        }
        transform.position = Home;
        for (float i = 0; i < 0.1; i += Time.deltaTime)
        {
            transform.Rotate(new Vector3(-1, 0, 0), 45 * Time.deltaTime * 10);
            yield return null;
        }
        transform.rotation = Quaternion.identity;
    }
}
