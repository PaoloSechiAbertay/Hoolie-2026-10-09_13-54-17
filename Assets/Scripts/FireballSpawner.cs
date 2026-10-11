using System.Collections;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    public GameObject fireball; 
    public PlayerController player;

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
            if (!player.ghost)
            {
                Spawn();
                yield return new WaitForSeconds(Random.Range(2f, 4f));
            }
            yield return null;
        }
    }

    void Spawn()
    {
        Random.Range(2f, 4f);
        Instantiate(fireball, new Vector3(transform.position.x, transform.position.y + Random.Range(0, 3), transform.position.z), Quaternion.identity);
    }
}
