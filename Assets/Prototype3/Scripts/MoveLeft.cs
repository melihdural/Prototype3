using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [HideInInspector]
    public float speed = 30f;

    private PlayerController playerControllerScript;

    private Animator playerAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.isGameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);

            if (Input.GetKeyDown(KeyCode.S) && playerControllerScript.isOnGround == true)
            {
                speed = 60f;
                playerAnim.SetFloat("Speed_f", 2);
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                speed = 30f;
                playerAnim.SetFloat("Speed_f", 1);

            }
            
        }


    }
}
