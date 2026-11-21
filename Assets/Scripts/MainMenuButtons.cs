using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public string SceneName;

    // Update is called once per frame
    void Update()
    {
        Inputs();
    }

    void Inputs()
    {
        if (InputSystem.actions.FindAction("Button1").WasPressedThisFrame() || 
            InputSystem.actions.FindAction("Button2").WasPressedThisFrame() || 
            InputSystem.actions.FindAction("Button3").WasPressedThisFrame() || 
            InputSystem.actions.FindAction("Button4").WasPressedThisFrame())
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
