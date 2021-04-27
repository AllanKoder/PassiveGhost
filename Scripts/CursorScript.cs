using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public float Clamp;
    public float Multiple;

    private Transform Player;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        if (!GameController.UsingController)
        {
            transform.localPosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y,0);
            sr.color = Color.white;

        }
        if (GameController.UsingController)
        {
            transform.localPosition = Vector3.ClampMagnitude(new Vector3(Input.GetAxisRaw("RightJoyHorizontal") * Multiple, Input.GetAxisRaw("RightJoyVertical") * Multiple, 0), Clamp) + Player.position;
            if(Input.GetAxisRaw("RightJoyHorizontal") == 0 && Input.GetAxisRaw("RightJoyVertical") == 0)
            {
                sr.color = Color.clear;
            }
            else
            {
                sr.color = Color.white;
            }
        }
    }
}
