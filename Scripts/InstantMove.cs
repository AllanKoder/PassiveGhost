using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantMove : MonoBehaviour
{
    public Transform Pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Pos.position;
    }
}
