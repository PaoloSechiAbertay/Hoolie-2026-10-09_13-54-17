using System.Collections;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    public GameObject fireball; 
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
            if (!player.ghost && attack.attacking == false)
            {
                Spawn();
                yield return new WaitForSeconds(Random.Range(2f, 4f));
            }
            yield return null;
        }
    }

    void Spawn()
    {
        int spawnpoint = Random.Range(0, 3);

        if (spawnpoint == 1)
        {
            Instantiate(fireball, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
        }
        else
        {
            Instantiate(fireball, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }
}
