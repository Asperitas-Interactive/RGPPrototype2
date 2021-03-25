using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{


    float smoothTime = 0.1f;
    float smoothVel;

    public bool glide;

    //Movement Based Variables
    public CharacterController controller;
    public float speed = 12.0f;
    public float gravity = -9.81f;
    public float jumpHeight;

    public Vector3 external;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Transform cam;

    Rigidbody rb;

    bool isGrounded;
    float defaultPos;

    //Interaction Based Variables

    Vector3 dir;
    Vector3 move;

    // Start is called before the first frame update
    void Start()
    {
        //controller = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

       

        //Get input axes
        int x = (int)Input.GetAxis("Horizontal");
        int y = (int)Input.GetAxis("Vertical");

        //Move with local dir

        move = new Vector3(x, 0f, y).normalized;


        float angle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        float smAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref smoothVel, smoothTime);




        transform.rotation = Quaternion.Euler(0f, smAngle, 0f);

        if (move.magnitude > 0.1f)
        {

            dir = Quaternion.Euler(0f, smAngle, 0f) * Vector3.forward;

            {
                transform.forward = dir;
                rb.MovePosition(rb.position + dir * speed * Time.deltaTime);

            }
        }

       
        //Fall down

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            Debug.Log("jump");
        }

        if(Input.GetButton("Glide") && !isGrounded)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * 15f * Time.deltaTime * Physics.gravity.y), ForceMode.Acceleration);

        }

    }

    void FixedUpdate()
    {
        if (move.magnitude > 0.1f)
        {
            transform.forward = dir;
            rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
        }
    }

};