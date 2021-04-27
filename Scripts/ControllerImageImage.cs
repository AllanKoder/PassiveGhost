using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ControllerImageImage : MonoBehaviour
{

    private Text sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.UsingController)
        {
            sr.text = "-Press Right Trigger to start-";
        }
        else
        {
            sr.text = "-Click to start-";
        }
    }
}
