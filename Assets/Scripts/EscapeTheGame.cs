using UnityEngine;

public class EscapeTheGame : MonoBehaviour
{
    // Quit the game when the Escape key is pressed - make sure is attached to a GameObject in the game scene
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
