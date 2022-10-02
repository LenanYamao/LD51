using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneName;
    public AudioClip clickSfx;
    public AudioSource source;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Play()
    {
        source.PlayOneShot(clickSfx);
        SceneManager.LoadScene(sceneName);
    }

    public void Retry()
    {
        source.PlayOneShot(clickSfx);
        SceneManager.LoadScene("Level");
    }

    public void Quit()
    {
        source.PlayOneShot(clickSfx);
        Application.Quit();
    }
}
