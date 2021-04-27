using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GhostSpeak : MonoBehaviour
{
    public Text UI;
    public string[] Sentences;
    public GameObject Screen;

    private int index;
    private bool hit;
    private bool One;
    public float typingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        foreach (char letter in Sentences[index].ToCharArray())
        {
            UI.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Fire1") == 1 && hit)
        {
            hit = false;
            if(index < Sentences.Length - 1 && UI.text == Sentences[index])
            {
                index++;
                UI.text = "";
                StartCoroutine(Type());
            }
        }
        else if (Input.GetAxisRaw("Fire1") < 1)
        {
            hit = true;
        }

        if (index + 1 == Sentences.Length)
        {
            if (One)
            {
                Invoke("LoadNext", 1f);
                Instantiate(Screen, Screen.transform.position, Quaternion.identity);
                One = false;
            }
        }
        else
        {
            One = true;
        }
    }

    void LoadNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
