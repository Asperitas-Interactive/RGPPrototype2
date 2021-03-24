using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{

    public float mouseSensitivity;
    float xRotation = 0f;
    float yRotation = 0f;

    public bool glide;

    //Movement Based Variables
    public CharacterController controller;


    public float speed = 12.0f;

    public float gravity = -9.81f;
    public float jumpHeight;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;
    float defaultPos;

    //Interaction Based Variables


    // Start is called before the first frame update
    void Start()
    {
        //controller = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {


        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;

        transform.localRotation = (Quaternion.Euler(0f, yRotation, 0f));


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
        Vector3 move = transform.right * x + transform.forward * y;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        //Fall down
        if (Input.GetButtonDown("Jump") && isGrounded)
        {

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
           
                jumpHeight = 2f;
                gravity = -19.6f;
            

        }

        if (Input.GetButton("Glide") && isGrounded)
        {
            if (glide)
            {
                jumpHeight = 4f;
                gravity = -3f;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            }
        }

        controller.Move(velocity * Time.deltaTime);


    }

};