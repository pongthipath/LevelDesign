using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isOnGround;
    public bool isCrouching;

    public float speed = 0;
    public float runSpeedNorm;
    public float walkSpeed = 0.05f;
    public float runSpeed = 0.1f;
    public float crouchSpeed = 0.02f;

    public float rotSpeed;
    public float jumpHeight;
    Rigidbody rb;
    CapsuleCollider col_size;
    Animator anim;

    //camera 
    public Transform playerCam, centerPoint, player1stView;
    float TurnX, TurnY, JoyTurnX, JoyTurnY;

    public GameObject thirdCam;
    public GameObject firstCam;
    public static int camMode;
    public GameObject ResetPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col_size = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();

        runSpeedNorm = runSpeed;
        isOnGround = true;

        Debug.Log("on ground");

        thirdCam.SetActive(true);
        firstCam.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        //toggle crouch
        if (Input.GetButton("Crouching"))
        {
            if (isCrouching)
            {             
                isCrouching = false;
                anim.SetBool("isCrouching", false);
                col_size.height = 2;
                col_size.center = new Vector3(0, 1, 0);
            }
            else
            {
                Debug.Log("Crouching");

                isCrouching = true;
                anim.SetBool("isCrouching", true);
                speed = crouchSpeed;
                col_size.height = 1;
                col_size.center = new Vector3(0, 0.5f, 0);
            }
        }
        var z = Input.GetAxis("Vertical") * speed;
        var x = Input.GetAxis("Horizontal") * speed;

        transform.Translate(0, 0, z);
        transform.Translate(x, 0, 0);

        if (Input.GetKey(KeyCode.Space) && isOnGround == true)
        {
            rb.AddForce(0, jumpHeight, 0);
            anim.SetTrigger("isJumping");
            isCrouching = false;
            isOnGround = false;
        }
        if(isCrouching)
        {
            //Crouching controls animation
            if (Input.GetButton("Horizontal"))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else if (Input.GetButton("Vertical"))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }
        else if(Input.GetButton("Running"))
        {
            
            speed = runSpeed;
            

            //running controls
            if(Input.GetButton("Horizontal"))
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", true);
                anim.SetBool("isIdle", false);
            }
            else if(Input.GetButton("Vertical"))
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", true);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }
        else if(!isCrouching)
        {
            speed = walkSpeed;
            //Standing controls
            if(Input.GetButton("Horizontal"))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else if(Input.GetButton("Vertical"))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);
            }
        }
        //camera mouse
        //3rd cam
        TurnX += Input.GetAxis("Mouse X") * 5;
        //TurnX = Mathf.Clamp(TurnX, -90, 90);
        TurnY += Input.GetAxis("Mouse Y") * 5;
        TurnY = Mathf.Clamp(TurnY, -30, 40);
        transform.localEulerAngles = new Vector3(-TurnY, TurnX, 0);

        //camera joystick
        //3rd cam
        JoyTurnX += Input.GetAxis("JoyCameraXAxis") * 5;
        //TurnX = Mathf.Clamp(TurnX, -90, 90);
        JoyTurnY += Input.GetAxis("JoyCameraYAxis") * 5;
        JoyTurnY = Mathf.Clamp(JoyTurnY, -20, 30);
        transform.localEulerAngles = new Vector3(-JoyTurnY, JoyTurnX, 0);

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            RotateView();
        }
        
        if (Input.GetAxis("Vertical") > 0 | Input.GetAxis("Vertical") < 0)
        {
            Quaternion turnAngle = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);
            Quaternion turnAngle2 = Quaternion.Euler(0, player1stView.eulerAngles.y, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, turnAngle, Time.deltaTime * rotSpeed/2);


            //transform.rotation = Quaternion.Slerp(transform.rotation, turnAngle, Time.deltaTime * rotSpeed / 2);
        }

        //switch cam

        if (Input.GetButtonDown("Camera"))
        {
            if(camMode==1)
            {
                camMode = 0;
            }
            else
            {
                camMode += 1;
            }
            StartCoroutine(CamChange());
        }
        if (Input.GetButtonDown("ResetPos"))
        {
            transform.position = ResetPosition.transform.position;
        }
    }

    void OnCollisionEnter()
    {
    isOnGround = true;
        Debug.Log("on ground2");
    }

    IEnumerator CamChange()
    {
        yield return new WaitForSeconds(0.01f);
        if(camMode == 0)
        {
            thirdCam.SetActive(true);
            firstCam.SetActive(false);
        }
        if(camMode == 1)
        {
            firstCam.SetActive(true);
            thirdCam.SetActive(false);
        }
    }
    private void RotateView()
    {
        //mouseX += Input.GetAxis("Mouse X");
        //mouseY -= Input.GetAxis("Mouse Y");
        //mouseY2 -= Input.GetAxis("Mouse Y");
        //playerCam.rotation = Quaternion.Euler(0, mouseX, 0);
        //player1stView.rotation = Quaternion.Euler(0, mouseX, 0);
        //mouseY = Mathf.Clamp(mouseY, -20, 40);
        //playerCam.LookAt(centerPoint);
        //centerPoint.localRotation = Quaternion.Euler(mouseY, mouseX, 0);
        //centerPoint.position = new Vector3(transform.position.x, transform.position.y + mouseYPosition, transform.position.z);

        //mouseY2 = Mathf.Clamp(mouseY2, -50, 25);
        //player1stView.localRotation = Quaternion.Euler(mouseY2, mouseX, 1);
        //player1stView.position = new Vector3(transform.position.x, transform.position.y
        //+ mouseYPosition2, transform.position.z);
    }
}
