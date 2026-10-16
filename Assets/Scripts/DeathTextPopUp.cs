using UnityEngine;

public class DeathTextPopUp : MonoBehaviour
{
    public PlayerController player;
    public GameObject text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowUp();
    }

    public void ShowUp()
    {
        if (player.playerHealth <= 0)
        {
            text.SetActive(true);
        }
        else if(player.playerHealth > 0)
        {
            text.SetActive(false);
        }
    }
}
