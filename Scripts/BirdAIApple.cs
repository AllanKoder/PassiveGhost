using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BirdAIApple : MonoBehaviour
{
    [HeaderAttribute("Jump")]

    public bool Stuck;
    public bool IsRight;
    public float TimeAdd;
    public float JumpTime;
    public float ForceX, ForceY;

    private float Timer;
    private float Timing;
    private float JumpTiming;
    private bool Right;

    public Sprite JumpingSprite;
    public Sprite GroundedSprite;
    public Sprite DeadSprite;
    public Sprite SpeakSprite;
    [Space]
    [HeaderAttribute("Apple")]

    public GameObject AppleUI;
    public float AppleDistance;
    public Sprite EatingSprite;
    private bool Eating;

    [Space]
    [HeaderAttribute("Death")]
    public float DeathPower;
    public float FlyDie;
    public GameObject deathIcon;
    public GameObject RestartPanel;
    [Space]
    [HeaderAttribute("Ground")]
    public Transform Detection;
    public Transform Detection2;
    [Space]
    [HeaderAttribute("Detect")]
    public Transform PlayerDetect;
    public float PlayerDetectDistance;
    public float SightDistance;
    public Vector2 PlayerDetection;
    public Vector2 PlayerDetectionDirection;


    public GameObject CuriousIcon;
    public GameObject SeePlayerUI;


    Transform Player;

    private bool SeePlayer;
    private bool PlayerThere;
    private bool OnGround;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Vector3 Scale;
    private bool Dead;
    private bool CallOnce;
    private Transform ApplePosition;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Scale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Dead = false;
        CallOnce = false;
        Eating = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        PlayerThere = false;
        SeePlayerUI.SetActive(false);

    }
    private void Awake()
    {
        Right = IsRight;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(PlayerDetect.position, PlayerDetection);

    }
    // Update is called once per frame


    void Update()
    {
        GameObject[] Apples;
        Apples = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject apple in Apples)
        {
            if (Vector2.Distance(apple.transform.position, transform.position) < AppleDistance && Vector2.Distance(apple.GetComponent<Rigidbody2D>().velocity, Vector2.zero) < 0.2f)
            {
                Eating = true;
                ApplePosition = apple.transform;

            }
        }
        if (!PlayerThere && !Dead & !Eating)
        {
            AppleUI.SetActive(true);
        }

        if (Eating && !Dead)
        {
            sr.sprite = EatingSprite;
            AppleUI.SetActive(false);
            transform.localScale = Scale;
            if (ApplePosition.transform.position.x - transform.position.x > 0)
            {
                sr.flipX = false;
            }
            if (ApplePosition.transform.position.x - transform.position.x < 0)
            {
                sr.flipX = true;
            }
        }

        SeePlayer = Physics2D.BoxCast(PlayerDetect.position, PlayerDetection, 0, PlayerDetectionDirection, PlayerDetectDistance, 1 << LayerMask.NameToLayer("Player"));
        OnGround = Physics2D.Linecast(Detection.position, Detection2.position, 1 << LayerMask.NameToLayer("Ground"));
        Timer = Time.time;
        if (Timer > Timing && !Dead && !PlayerThere && !Eating && !Stuck)
        {
            Timing = Timer + TimeAdd;

            Right = !Right;
        }
        if (SeePlayer && !CharacterMovement.Hiding && !Dead && !Eating)
        {
            CuriousIcon.SetActive(true);
            if (Vector2.Distance(transform.position, Player.position) < SightDistance)
            {
                PlayerThere = true;
                SeePlayerUI.SetActive(true);
                CuriousIcon.SetActive(false);
                AppleUI.SetActive(false);
                sr.sprite = SpeakSprite;
                Invoke("SeeDeathFly", 2f);
            }
        }
        else
        {
            CuriousIcon.SetActive(false);

        }


        if (PlayerThere)
        {
            CuriousIcon.SetActive(false);

        }



        if (!Dead && !PlayerThere && !Eating)
        {
            if (OnGround)
            {
                if (Right)
                {
                    transform.localScale = Scale;
                }
                if (!Right)
                {
                    transform.localScale = new Vector3(-Scale.x, Scale.y, Scale.z);
                }
                sr.sprite = GroundedSprite;

            }
            else
            {
                sr.sprite = JumpingSprite;
            }
            deathIcon.SetActive(false);
        }
        else if (!PlayerThere && !Eating)
        {
            sr.sprite = DeadSprite;
            if (Vector2.Distance(rb.velocity, Vector2.zero) < 3f)
            {
                deathIcon.SetActive(true);
                Invoke("restartLevel", 2f);
            }
        }
        if (Dead)
        {
            sr.sprite = DeadSprite;
            AppleUI.SetActive(false);

            if (Vector2.Distance(rb.velocity, Vector2.zero) < 3f)
            {
                deathIcon.SetActive(true);
                Invoke("restartLevel", 2f);
            }
        }

    }

    private void FixedUpdate()
    {
        if (OnGround)
        {
            if (Timer > JumpTiming && !Dead && !PlayerThere && !Eating)
            {
                JumpTiming = Timer + JumpTime;
                if (Right)
                {
                    rb.AddForce(new Vector2(ForceX, ForceY), ForceMode2D.Impulse);
                }
                if (!Right)
                {
                    rb.AddForce(new Vector2(-ForceX, ForceY), ForceMode2D.Impulse);
                }
            }


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile") && !PlayerThere)
        {
            rb.AddForce(-(collision.gameObject.transform.position - transform.position + new Vector3(0, 1.5f, 0)) * Time.fixedDeltaTime * DeathPower, ForceMode2D.Impulse);
            deathFly();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0f && collision.transform.position.y - transform.position.y > 0.1f)
            {
                Dead = true;
            }
        }
    }

    void deathFly()
    {
        Dead = true;
        rb.constraints = RigidbodyConstraints2D.None;
    }

    void SeeDeathFly()
    {
        SeePlayerUI.SetActive(false);
        CuriousIcon.SetActive(false);
        sr.sprite = DeadSprite;
        if (Vector2.Distance(rb.velocity, Vector2.zero) < 3f)
        {
            deathIcon.SetActive(true);

        }
        if (!Dead)
        {
            if (Player.position.x - transform.position.x < 0)
            {
                rb.AddForce(new Vector3(3, 0.5f, 0) * Time.deltaTime * FlyDie, ForceMode2D.Impulse);
            }
            if (Player.position.x - transform.position.x > 0)
            {
                rb.AddForce(new Vector3(-3, 0.5f, 0) * Time.deltaTime * FlyDie, ForceMode2D.Impulse);
            }
            Dead = true;
            rb.constraints = RigidbodyConstraints2D.None;
            Invoke("restartLevel", 2f);

        }
    }

    void restartLevel()
    {
        if (!CallOnce)
        {
            Instantiate(RestartPanel, RestartPanel.transform.position, Quaternion.identity);
            Invoke("Loading", 1f);
            CallOnce = true;
        }
    }
    void Loading()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
