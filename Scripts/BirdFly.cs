using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BirdFly : MonoBehaviour
{

    public float Speed;
    public GameObject Skull;
    public Sprite Death;
    public Sprite Death2;
    public Sprite Death3;
    public GameObject RestartPanel;

    private Rigidbody2D rb;
    private Animator Anim;
    private bool CallOnce;

    private bool Dead;
    private int RandomNumb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Dead = false;
        CallOnce = false;
        rb.gravityScale = 0;
        Skull.SetActive(false);
        RandomNumb = Random.Range(0, 3);
        Anim.SetInteger("Number", RandomNumb);
    }

    private void Update()
    {
         if(Dead)
        {
            rb.gravityScale = 1;
            Skull.SetActive(true);
            GetComponent<Animator>().enabled = false;
            GetComponent<DestroyObject>().enabled = false;

            switch (RandomNumb)
            {
                case 0:
                    GetComponent<SpriteRenderer>().sprite = Death;
                    break;
                case 1:
                    GetComponent<SpriteRenderer>().sprite = Death2;
                    break;
                case 2:
                    GetComponent<SpriteRenderer>().sprite = Death3;
                    break;

            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Dead)
        {
            rb.velocity = new Vector2(Speed * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Dead = true;
            Invoke("restartLevel", 3f);
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
