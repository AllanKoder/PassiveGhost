using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    public float PlaceHeight;
    public Transform Bird;
    public Vector3 Offset;

    private Vector3 scale;
    // Start is called before the first frame update
    void Awake()
    {
        scale = transform.localScale;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        if (Bird.localScale.x > 0)
        {
            transform.localScale = scale;
            transform.position = new Vector3(Bird.position.x, Bird.position.y + PlaceHeight, 0) + Offset;

        }
        if (Bird.localScale.x < 0)
        {
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            transform.position = new Vector3(Bird.position.x, Bird.position.y + PlaceHeight, 0) + new Vector3(-Offset.x, Offset.y, Offset.z);

        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        if(Bird.localScale.x > 0)
        {
            transform.localScale = scale;
            transform.position = new Vector3(Bird.position.x, Bird.position.y + PlaceHeight, 0) + Offset;
        }
        if (Bird.localScale.x < 0)
        {
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            transform.position = new Vector3(Bird.position.x, Bird.position.y + PlaceHeight, 0) + new Vector3(-Offset.x, Offset.y,Offset.z);
        }
    }
}
