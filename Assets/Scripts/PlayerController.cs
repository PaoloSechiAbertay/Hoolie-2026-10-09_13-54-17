using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    InputAction Button;
    Rigidbody Rb;

    public int player_number = 1;

    public bool jumpingcooldown = false;
    public float jumpforce = 8f;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();

        Button = InputSystem.actions.FindAction("Button" + player_number);
        /*/switch (player_number)
        {
            case 1:
                Button = InputSystem.actions.FindAction("Button1"); 
                break;
            case 2:
                Button = InputSystem.actions.FindAction("Button2");
                break;
            case 3:
                Button = InputSystem.actions.FindAction("Button3");
                break;
            case 4:
                Button = InputSystem.actions.FindAction("Button4");
                break;
        }
        /*/
    }

    void Update()
    {
        Inputs();
    }

    void Inputs()
    {
        if (Button.IsPressed())
        {
            if (!jumpingcooldown)
            {
                StartCoroutine(Jump());
            }
        }
    }

    IEnumerator Jump()
    {
        jumpingcooldown = true;

        Rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);

        yield return new WaitForSeconds(2f);

        jumpingcooldown = false;
    }

}
