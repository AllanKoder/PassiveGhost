using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerImage : MonoBehaviour
{
    public Sprite KeyBoard;
    public Sprite Controller;

    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.UsingController)
        {
            sr.sprite = Controller;
        }
        else
        {
            sr.sprite = KeyBoard;
        }
    }
}
