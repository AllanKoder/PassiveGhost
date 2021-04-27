using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TimeLoad : MonoBehaviour
{
    public float Time;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Game", Time);
    }

    // Update is called once per frame
    void Game()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
