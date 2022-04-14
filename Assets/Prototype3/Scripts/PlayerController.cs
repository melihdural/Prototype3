using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    private Animator playerAnim;

    private AudioSource playerAudio;
    
    private float jumpCounter;

    private bool isPlayerReady;
    
    [HideInInspector]
    public bool isGameOver;

    [HideInInspector]
    public bool isOnGround;
    
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float gravityModifier;

    [SerializeField]
    private ParticleSystem explosionParticle, dirtParticle;

    [SerializeField]
    private AudioClip jumpSound, crashSound;
    
    [SerializeField]
    private GameObject background;

    [SerializeField]
    private GameObject spawnManager;

    [SerializeField]
    private GameObject showUI;


    private void Awake()
    {
        showUI.SetActive(true);
        spawnManager.SetActive(false);
    }
    
    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        
        background.GetComponent<MoveLeft>().speed = 10f;
        
        Physics.gravity *= gravityModifier;
        jumpCounter = 2;
        isPlayerReady = false;
    }

    private void Update()
    {
        if (isPlayerReady == false)
        {
            transform.Translate(Vector3.right * Time.deltaTime, Space.World);
        }


        if (Input.GetKeyDown(KeyCode.Space)  && !isGameOver && jumpCounter>0) 
        { 
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCounter--;
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
            
        
    }

    //Check Player Box Collider Touch Ground Box Collider
    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.CompareTag("Start"))
        {
            isPlayerReady = true;
            spawnManager.SetActive(true);
            showUI.SetActive(false);
            background.GetComponent<MoveLeft>().speed = 30f;
            playerAnim.SetFloat("Speed_f", 1);
        }
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCounter = 2;
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            isGameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 2);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            Camera.main.GetComponent<AudioSource>().Stop();

        }
    }
}
