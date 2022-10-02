using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMonster : MonoBehaviour
{
    public AudioClip clip;
    bool played = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var playerController = other.GetComponent<PlayerController>();
            if (playerController != null && !played && clip != null)
            {
                played = true;
                playerController.PlaySound(clip);
            }
        }
    }
}
