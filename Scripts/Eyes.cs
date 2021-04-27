using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    public float Clamp;

    Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.UsingController)
        {
            transform.localPosition = Vector3.ClampMagnitude(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0) - transform.position, Clamp);
        }
        if (GameController.UsingController)
        {
            transform.localPosition = Vector3.ClampMagnitude(new Vector3(Input.GetAxisRaw("RightJoyHorizontal"), Input.GetAxisRaw("RightJoyVertical"),0), Clamp);
        }
    }
}
