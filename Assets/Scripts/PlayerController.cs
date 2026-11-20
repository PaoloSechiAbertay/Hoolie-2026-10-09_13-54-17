using System.Collections;
using Unity.VisualScripting;
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
    public SpriteRenderer backRenderer;
    public ScreenShake ScreenShake;

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
    public AudioSource maidenTwirlAudioSource;

    public Sprite MadienSprite;
    Material Mat;
    public Material GhostMat;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Renderer = GetComponentInChildren<SpriteRenderer>();
        Renderer.sprite = MadienSprite;
        backRenderer.sprite = MadienSprite;

        Mat = Renderer.material;

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

        int rand = Random.Range(0, 40);
        if (rand == 0)
        {
            maidenTwirlAudioSource.Play();
            StartCoroutine(Spin(new Vector3 (0, 0, 720)));
        }
        else if (rand == 1)
        {
            maidenTwirlAudioSource.Play();
            StartCoroutine(Spin(new Vector3(0, 720, 0)));
        }
        else if (rand == 2)
        {
            maidenTwirlAudioSource.Play();
            StartCoroutine(Spin(new Vector3(720, 0, 0)));
        }

        Rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        for (float i = 0; i < 0.5f; i += Time.deltaTime)
        {
            Rb.AddForce(Vector3.down * 4f, ForceMode.Acceleration);
            yield return null;
        }


        jumpingcooldown = false;
        jump = null;
    }

    IEnumerator Spin(Vector3 spinAxis)
    {
        if (Random.Range(-1, 1) == -1) ;
        {
            spinAxis = (spinAxis * -1);
        }

        yield return new WaitForSeconds(0.2f);
        for (float f = 0; f < 0.5f; f += Time.deltaTime)
        {
            transform.Rotate(spinAxis * Time.deltaTime);
            yield return null;
        }
        transform.rotation = Quaternion.identity;
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
        //Debug.Log(playerHealth);

        StartCoroutine(GhostState(2f));
        ScreenShake.SetScreenShake(0.05f, 0.05f);

        if (playerHealth <= 0)
        {
            ScreenShake.SetScreenShake(0.1f, 0.1f);
            dead = true;
            Rb.linearVelocity = Vector3.zero;
            transform.localPosition = new Vector3(transform.localPosition.x, 2, transform.localPosition.z);
            StartCoroutine(TimeOut());
        }

        healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x - 0.2f, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

    void UnDeathState()
    {
        if (transform.localPosition.y < 2.5f)
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
        Renderer.material = GhostMat;
        healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x - 0.2f, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        yield return new WaitForSeconds(time);
        Renderer.material = Mat;
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
