using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public AudioSource backSound;
    public GameObject panelPause;
    // Start is called before the first frame update
    void Start()
    {
        panelPause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Paused()
    {
        backSound.Pause();
        Time.timeScale = 0f;
        panelPause.SetActive(true);
    }

    public void Resume()
    {
        backSound.Play();
        Time.timeScale = 1f;
        panelPause.SetActive(false);
    }

    public void Restart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 1f;
    }

}
