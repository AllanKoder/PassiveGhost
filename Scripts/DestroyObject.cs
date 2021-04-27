using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float DeathTime;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        DeathTime -= Time.deltaTime;
        if(DeathTime <= 0)
        {
            Destroy(gameObject);

        }
    }
    void Die()
    {
    }
}
