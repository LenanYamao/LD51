using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public bool gamePaused = false;
    public GameObject pauseUi;

    [Header("Outline")]
    public Material outline;
    public float outlineSize = 0.015f;
    public bool showOutline = true;

    [Header("Timer")]
    public float timeRemaining = 10;
    public bool timerIsRunning = false;

    float originalTimeRemainingOn = 10;
    float originalTimeRemainingOff = 10;

    private void Start()
    {
        outline.SetFloat("_multValue", outlineSize);
        originalTimeRemainingOn = timeRemaining;
        originalTimeRemainingOff = timeRemaining/2;
        timerIsRunning = true;
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
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;

                if (showOutline)
                {
                    showOutline = false;
                    outline.SetFloat("_multValue", 0f);
                    timeRemaining = originalTimeRemainingOff;
                    timerIsRunning = true;
                }
                else
                {
                    showOutline = true;
                    outline.SetFloat("_multValue", outlineSize);
                    timeRemaining = originalTimeRemainingOn;
                    timerIsRunning = true;
                }
            }
        }
    }

    public void PauseGame()
    {
        gamePaused = !gamePaused;
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
