using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool UsingController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Input.GetAxisRaw("JoyVertical")) > 0 || Mathf.Abs(Input.GetAxisRaw("JoyHorizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("RightJoyHorizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("RightJoyHorizontal")) > 0)
        {
            UsingController = true;
        }
        else if (Mathf.Abs(Input.GetAxisRaw("KeyVertical")) > 0 || Mathf.Abs(Input.GetAxisRaw("KeyHorizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("Mouse X")) > 0 || Mathf.Abs(Input.GetAxisRaw("Mouse Y")) > 0)
        {
            UsingController = false;
        }
    }
}
