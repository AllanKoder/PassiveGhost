using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BirdAI : MonoBehaviour
{
    [HeaderAttribute("Jump")]

    public bool IsRight;
    public bool Stuck;
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
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Scale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Dead = false;
        CallOnce = false;
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
        SeePlayer = Physics2D.BoxCast(PlayerDetect.position, PlayerDetection, 0, PlayerDetectionDirection, PlayerDetectDistance, 1 << LayerMask.NameToLayer("Player"));
        OnGround = Physics2D.Linecast(Detection.position, Detection2.position, 1 << LayerMask.NameToLayer("Ground"));
        Timer = Time.time;
        if (Timer > Timing && !Dead && !PlayerThere && !Stuck)
        {
            Timing = Timer + TimeAdd;

            Right = !Right;
        }
        if (SeePlayer && !CharacterMovement.Hiding && !Dead)
        {
            CuriousIcon.SetActive(true);
            if(Vector2.Distance(transform.position, Player.position) < SightDistance)
            {
                PlayerThere = true;
                SeePlayerUI.SetActive(true);
                CuriousIcon.SetActive(false);
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



        if (!Dead && !PlayerThere)
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
        else if(!PlayerThere)
        {
            sr.sprite = DeadSprite;
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
            if(Timer > JumpTiming && !Dead && !PlayerThere) 
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
            rb.AddForce(-(collision.gameObject.transform.position - transform.position + new Vector3(0,1.5f,0)) * Time.fixedDeltaTime * DeathPower,ForceMode2D.Impulse);
            deathFly();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0f && collision.transform.position.y - transform.position.y > 0f)
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
