using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody Rb;

    public float speed = 5;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Rb.linearVelocity = Vector3.back * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            Destroy(gameObject);
        }
    }
}
