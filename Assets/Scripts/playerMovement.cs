using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    bool isGrounded;
    float defaultPos;
    //Interaction Based Variables


    // Start is called before the first frame update
    void Start()
    {
        //controller = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {


      


      


        //Falling Down
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0.5f)
        {
            velocity.y = -2f;
            gravity = -19.6f;
        }

        //Get input axes
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");


        //Move with local dir
        Vector3 move = new Vector3(x, 0f, y).normalized;


        float angle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        float smAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref smoothVel, smoothTime);

        transform.rotation = Quaternion.Euler(0f, smAngle, 0f);

        if (move.magnitude > 0.1f || external.magnitude >0.1f)
        {

            Vector3 dir = Quaternion.Euler(0f, smAngle, 0f) * Vector3.forward;
            controller.Move(dir * speed * Time.deltaTime + external * Time.deltaTime);
        }
        velocity.y += gravity * Time.deltaTime;

        //Fall down

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += 20f;
        }
        if(Input.GetButton("Glide"))
        {
            if (gravity < -1f)
                gravity += 5f * Time.deltaTime;
            else
                gravity = -1f;
        } 

        else
        {
            gravity = -19.6f;
        }


        

        controller.Move(velocity * Time.deltaTime);


    }

};