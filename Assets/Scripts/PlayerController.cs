using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public GameManager gm;
    public LayerMask layerMaskItem;
    public Transform cameraPos;

    [Header("Movement")]
    public bool stopMovement = false;
    public float moveSpeed;
    public Transform orientation;

    [Header("Audio")]
    public AudioSource sfxSource;
    public AudioSource sfxRight;
    public AudioSource sfxLeft;

    [Header("Monster")]
    public List<AudioClip> monsterSounds;
    public List<AudioClip> reactionSounds;
    bool monsterSoundPlayed = false;
    bool reaction1 = false;
    bool reaction2 = false;
    bool reaction3 = false;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (gm.gamePaused) return;
        if (stopMovement) return;
        PlayerInput();
        CollectItem();
        SpeedControl();
        if (!gm.showOutline && !monsterSoundPlayed) RandomMonsterAction();
        if (gm.showOutline) monsterSoundPlayed = false;
    }
    private void FixedUpdate()
    {
        if (gm.gamePaused) return;
        if (stopMovement) return;
        MovePlayer();
    }

    void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
    }
    void CollectItem()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out hit, 1f, layerMaskItem))
            {
                if (hit.collider != null)
                {
                    var hitGo = hit.transform.gameObject;
                    var item = hitGo.GetComponent<Item>();
                    if (item.collectAudio != null) PlaySound(item.collectAudio);
                    if (item.interactAudio != null) PlaySound(item.interactAudio);
                    Destroy(item.unlock);
                    Destroy(hitGo);
                }
            }
        }
    }
    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    void RandomMonsterAction()
    {
        if (monsterSounds == null || monsterSounds.Count == 0) return;

        monsterSoundPlayed = true;

        var monsterAttack = Random.Range(0, 100);
        
        if(monsterAttack < 25 || (!reaction1 || !reaction2))
        {
            var monsterAct = Random.Range(0, monsterSounds.Count);
            PlaySound(monsterSounds[monsterAct], true);

            if (!reaction1 && !reaction2 && !reaction3)
            {
                reaction1 = true;
                PlaySound(reactionSounds[0]);
            }
            else if (reaction1 && !reaction2 && !reaction3)
            {
                reaction2 = true;
                PlaySound(reactionSounds[1]);
            }
        }
    }

    public void PlaySound(AudioClip clip, bool playRandom = false)
    {
        if (playRandom)
        {
            var source = Random.Range(0, 3);
            if(source == 0)
            {
                sfxSource.pitch = Random.Range(0.5f, 1.5f);
                sfxSource.PlayOneShot(clip);
                sfxSource.pitch = 1;
            }
            else if (source == 1)
            {
                sfxSource.pitch = Random.Range(0.5f, 1.5f);
                sfxRight.PlayOneShot(clip);
                sfxSource.pitch = 1;
            }
            else if (source == 2)
            {
                sfxSource.pitch = Random.Range(0.5f, 1.5f);
                sfxLeft.PlayOneShot(clip);
                sfxSource.pitch = 1;
            }
            return;
        }
        sfxSource.PlayOneShot(clip);
    }
}
