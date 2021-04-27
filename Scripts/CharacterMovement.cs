using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    public float MovementSpeed;

    [Space(1)]
    [Header("Birds")]

    public float MaxHeight;
    public float BirdHeight;

    public GameObject BirdsRight;
    public GameObject BirdsLeft;

    public float TimerAddMin, TimerMax;

    private float timerBird;

    [HideInInspector]
    public static bool Hiding;
    [Space(1)]
    [Header("Shooting")]
    public GameObject Wind;
    public GameObject WindEffect;
    public Transform Mouth;
    public Animator MouthAnim;
    public float ShotAdd;

    private float timer;
    private float ShootTimer;

    [Space(1)]
    [Header("Float")]
    public float Speed;
    public float Distance;
    public float Minimum;
    public float FloatAmount;

    [Space(1)]
    [Header("Extras")]

    private Animator Anim;
    private SpriteRenderer Sr;

    private float AddHeight;
    private bool GoUp;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Sr = GetComponent<SpriteRenderer>();
        MouthAnim.SetBool("Shoot", false);
        Hiding = false;

    }

    // Update is called once per frame
    void Update()
    {

        //fire Wind
        timer = Time.time;
        if(Input.GetAxisRaw("Fire1") == 1 )
        {
            if(timer > ShootTimer)
            {
                ShootTimer = timer + ShotAdd;
                MouthAnim.SetTrigger("Shoot");
                Instantiate(Wind, Mouth.position, Quaternion.identity);
                Instantiate(WindEffect, Mouth.position, Quaternion.identity);
            }

        }
        //Spawn Birds
        if(transform.position.y > MaxHeight - BirdHeight)
        {
            if(timer > timerBird)
            {
                timerBird = timer + Random.Range(TimerAddMin, TimerMax);
                int Chance = Random.Range(0, 2);
                if(Chance == 0)
                {
                    Instantiate(BirdsRight, new Vector3(transform.position.x, MaxHeight / 2,0) + new Vector3(-25f, 7.17f + Random.Range(-1,2), 0), Quaternion.identity);
                }
                if(Chance == 1)
                {
                    Instantiate(BirdsLeft, new Vector3(transform.position.x, MaxHeight / 2, 0) + new Vector3(25f, 7.17f + Random.Range(-1, 2), 0), Quaternion.identity);
                }
            }
        }

        //Go up and down
        if (Input.GetAxisRaw("Vertical") == 0)
        {
            rb.position += new Vector2(0, AddHeight * FloatAmount);
            if (GoUp)
            {
                AddHeight = Mathf.Lerp(AddHeight, Distance, Speed * Time.deltaTime);
                if (AddHeight - Distance > -Minimum)
                {
                    GoUp = false;
                }
            }
            if (!GoUp)
            {
                AddHeight = Mathf.Lerp(AddHeight, -Distance, Speed * Time.deltaTime);
                if (AddHeight + Distance < Minimum)
               {
                    GoUp = true;
                }
            }
        }
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * MovementSpeed, Input.GetAxisRaw("Vertical") * MovementSpeed) * Time.fixedDeltaTime, ForceMode2D.Impulse);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 2f);

        rb.position = new Vector3(rb.position.x, Mathf.Clamp(rb.position.y, -MaxHeight, MaxHeight), 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bush"))
        {
            Hiding = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bush"))
        {
            Hiding = false;
        }
    }
}
