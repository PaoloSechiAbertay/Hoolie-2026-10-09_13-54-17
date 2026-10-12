using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    InputAction Button;
    Rigidbody Rb;
    MeshRenderer Renderer;

    Coroutine jump;

    public int playerHealth;

    public int player_number = 1;

    public bool jumpingcooldown = false;
    public float jumpforce = 8f;

    public bool ghost = false;
    Material Mat;
    public Material ghostMat;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Renderer = GetComponent<MeshRenderer>();
        Mat = Renderer.material;


        Button = InputSystem.actions.FindAction("Button" + player_number);
    }

    void Update()
    {
        Inputs();
    }

    void Inputs()
    {
        if (Button.WasPressedThisFrame())
        {
            if (!jumpingcooldown && jump == null && !ghost)
            {
                jump = StartCoroutine(Jump());
            }
        }
    }

    IEnumerator Jump()
    {
        jumpingcooldown = true;

        Rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);

        yield return new WaitForSeconds(2f);

        jumpingcooldown = false;
        jump = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attack" && !ghost)
        {
            StartCoroutine(Damage());
        }
    }

    IEnumerator Damage()
    {
        playerHealth -= 10;
        Debug.Log(playerHealth);

        ghost = true;
        Renderer.material = ghostMat;

        yield return new WaitForSeconds(5f);

        Renderer.material = Mat;
        ghost = false;

    }

}
