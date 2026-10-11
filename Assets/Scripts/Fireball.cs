using UnityEngine;

public class Fireball : MonoBehaviour
{

    float speed = 5;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -Vector3.forward * speed * Time.deltaTime;

        if (transform.position.z < -5)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            print(collision.gameObject + "hit");
            Destroy(gameObject);
        }
    }
}
