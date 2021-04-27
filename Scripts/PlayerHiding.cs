using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    public Text UI;
    public string[] Sentences;

    private int index;
    public float typingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator Type()
    {
        foreach(char letter in Sentences[index].ToCharArray())
        {
            UI.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
