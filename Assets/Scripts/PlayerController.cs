using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public InputAction Button1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Button1 = InputSystem.actions.FindAction("Button1");
    }

    // Update is called once per frame
    void Update()
    {
        inputs();
    }

    void inputs()
    {
        if (Button1.IsPressed())
        {
            Debug.Log("You Did 10 Damage");
        }
    }

}
