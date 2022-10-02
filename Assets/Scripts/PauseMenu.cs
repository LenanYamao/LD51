using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameManager gm;

    public void Resume()
    {
        gm.PauseGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
