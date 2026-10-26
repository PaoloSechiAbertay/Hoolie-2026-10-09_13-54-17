using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public bool playing = false;

    InputAction Button;
    Rigidbody Rb;
    SpriteRenderer Renderer;

    Coroutine jump;

    public int playerHealth;
    int MAXHEATH = 30;
    public GameObject healthBar;
    public bool ghost = false;
    public bool dead = false;

    public int player_number = 1;
  

    public bool jumpingcooldown = false;
    public float jumpforce = 8f;

    public AudioSource maidenJumpAudioSource;
    public AudioSource maidenDeathAudioSource;

    public Sprite MadienSprite;
    //Material Mat;
    //public Material GhostMat;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Renderer = GetComponentInChildren<SpriteRenderer>();
        Renderer.sprite = MadienSprite;

        //Mat = Renderer.material;

        Button = InputSystem.actions.FindAction("Button" + player_number);

        playerHealth = 30;
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
                if (!playing)
                {
                    playing = true;
                }
            }
        }
    }

    IEnumerator Jump()
    {
        maidenJumpAudioSource.Play();
        UnDeathState();

        jumpingcooldown = true;

        Rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        yield return new WaitForSeconds(1.5f);

        jumpingcooldown = false;
        jump = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ghost)
        {
            return;
        }
        if (other.gameObject.tag == "Tail")
        {
            Damage();
        }
        else if (other.gameObject.tag == "Attack")
        {
            Damage();
            Destroy(other.gameObject);
        }

    }

    void Damage()
    {
        playerHealth -= 10;
        Debug.Log(playerHealth);

        StartCoroutine(GhostState(2f));

        if (playerHealth <= 0)
        {
            dead = true;
            Rb.linearVelocity = Vector3.zero;
            transform.localPosition = new Vector3(transform.localPosition.x, 1, transform.localPosition.z);
            StartCoroutine(TimeOut());
        }

        healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x - 0.2f, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

    void UnDeathState()
    {
        if (transform.localPosition.y < 2)
        {
            playing = true;
            dead = false;
            playerHealth = MAXHEATH;
            StartCoroutine(GhostState(1f));
        }

    }

    IEnumerator GhostState(float time)
    {
        maidenDeathAudioSource.Play();
        ghost = true;
        //Renderer.material = GhostMat;
        healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x - 0.2f, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        yield return new WaitForSeconds(time);
        // Renderer.material = Mat;
        ghost = false;
    }

    IEnumerator TimeOut()
    {
        for (int i = 0; i < 20; i++)
        {
            if (dead)
            {
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield break;
            }
        }
        playing = false;
    }

}
