using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindProjectile : MonoBehaviour
{
    public float Speed;
    public float DeathTime;
    public GameObject DeathPart;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!GameController.UsingController)
        {
            var dir = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y,0) - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (GameController.UsingController)
        {
            var dir = new Vector3(Input.GetAxisRaw("RightJoyHorizontal"), Input.GetAxisRaw("RightJoyVertical"),0);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        Invoke("Death", DeathTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.right * Speed * Time.fixedDeltaTime;
    }
    void Death()
    {
        Instantiate(DeathPart, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Bush"))
        {
            Death();
        }
    }
}
