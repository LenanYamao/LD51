using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    [Header("UI")]
    //public bool gamePaused = false;
    public BoolVariable gamePaused = default(BoolVariable);
    public GameObject pauseUi;

    [Header("Outline")]
    public Material outline;
    public FloatVariable outlineSize = default(FloatVariable);
    //public float outlineSize = 0.015f;
    public BoolVariable showOutline = default(BoolVariable);
    //public bool showOutline = true;

    [Header("Timer")]
    //public float timeRemaining = 10;
    public FloatVariable timeRemaining = default(FloatVariable);
    //public bool timerIsRunning = false;
    public BoolVariable timerIsRunning = default(BoolVariable);

    float originalTimeRemainingOn = 10;
    float originalTimeRemainingOff = 10;

    private void Start()
    {
        outline.SetFloat("_multValue", outlineSize);
        originalTimeRemainingOn = timeRemaining;
        originalTimeRemainingOff = timeRemaining / 2;
        timerIsRunning.Value = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            outline.SetFloat("_multValue", 0f);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            outline.SetFloat("_multValue", outlineSize);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (timerIsRunning && !gamePaused)
        {
            if (timeRemaining > 0)
            {
                timeRemaining.Value -= Time.deltaTime;
            }
            else
            {
                timeRemaining.Value = 0;
                timerIsRunning.Value = false;

                if (showOutline)
                {
                    showOutline.Value = false;
                    outline.SetFloat("_multValue", 0f);
                    timeRemaining.Value = originalTimeRemainingOff;
                    timerIsRunning.Value = true;
                }
                else
                {
                    showOutline.Value = true;
                    outline.SetFloat("_multValue", outlineSize);
                    timeRemaining.Value = originalTimeRemainingOn;
                    timerIsRunning.Value = true;
                }
            }
        }
    }

    public void PauseGame()
    {
        gamePaused.Value = !gamePaused.Value;
        if (gamePaused)
        {
            pauseUi.SetActive(true);
        }
        else
        {
            pauseUi.SetActive(false);
        }
    }
}
