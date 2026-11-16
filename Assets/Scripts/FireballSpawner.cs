using System.Collections;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    public GameObject fireball;
    public GameObject fireballHigh;
    public PlayerController player;
    public TailAttack attack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Clock());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Clock()
    {
        while (true)
        {
            if (!player.ghost && !player.dead && !attack.attacking)
            {
                Spawn();
                yield return new WaitForSeconds(Random.Range(1f, 2f));
            }
            yield return null;
        }
    }

    void Spawn()
    {
        int spawnpoint = Random.Range(0, 4);

        if (spawnpoint <= 2)
        {
            Instantiate(fireball, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
        else
        {
            Instantiate(fireballHigh, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
        }
    }
}
