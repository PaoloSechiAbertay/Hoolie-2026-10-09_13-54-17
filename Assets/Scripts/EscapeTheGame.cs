using UnityEngine;
using UnityEngine.InputSystem;

public class EscapeTheGame : MonoBehaviour
{
    private InputAction EscapeAction;

    // Quit the game when the Escape key is pressed - make sure is attached to a GameObject in the game scene
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("Escape key pressed. Quitting the game.");
            Application.Quit();
        }
    }
}
