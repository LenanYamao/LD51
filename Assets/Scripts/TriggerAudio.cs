using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerAudio : MonoBehaviour
{
    public AudioClip clip;
    public bool endGame = false;
    bool played = false;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        var playerController = other.gameObject.GetComponentInParent<PlayerController>();
        if (playerController != null && !played && clip != null)
        {
            played = true;
            playerController.PlaySound(clip);
            if(endGame) StartCoroutine(FinishGame(playerController));
        }
    }

    IEnumerator FinishGame(PlayerController playerController)
    {
        playerController.stopMovement = true;
        yield return new WaitForSeconds(26);

        SceneManager.LoadScene("WinScreen");
    }
}
