using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    public GameObject player;
    private PlayerController playerScript;
    private Animator playerAnim;
    private Rigidbody rb;

    public bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerController>();
        playerAnim = player.GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody>();

        isOn = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn)
        {
            playerScript.enabled = false;
            rb.useGravity = false;

            if(Input.GetKey(KeyCode.W))
            {
                playerAnim.SetBool("isClimbing", true);
                playerAnim.SetBool("isWalking", false);
                playerAnim.SetBool("isIdle", false);

                player.transform.Translate(0, 0.01f, 0f);
            }

            else if(Input.GetKey(KeyCode.S))
            {
                playerAnim.SetBool("isClimbing", true);
                playerAnim.SetBool("isWalking", false);
                playerAnim.SetBool("isIdle", false);

                player.transform.Translate(0, 0, -0.5f);
            }
        }
        else
        {
            playerAnim.SetBool("isClimbing", false);
            playerAnim.SetBool("isWalking", false);
            playerAnim.SetBool("isIdle", true);

            playerScript.enabled = true;
            rb.useGravity = true;
        }
        
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            isOn = true;
            
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            isOn = false;
            player.transform.Translate(0, 0, 0.5f);

        }
    }
}
