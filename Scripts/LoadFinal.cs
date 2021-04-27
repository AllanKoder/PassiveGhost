using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadFinal : MonoBehaviour
{
    public GameObject Panel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Fire1") == 1)
        {
            Invoke("Loading", 2f);
        }
    }
    void Loading()
    {
        Instantiate(Panel, Panel.transform.position, Quaternion.identity);
        Invoke("Loading2", 1f);
    }
    void Loading2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
