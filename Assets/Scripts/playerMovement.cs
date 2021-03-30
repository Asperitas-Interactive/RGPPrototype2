using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngineInternal;

public class playerMovement : MonoBehaviour
{

    float glideTimer = 0.0f;
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

    [Range(0f, -3.0f)]
    public float glideFactor = -1.8f;

    Rigidbody rb;

    bool isJumping;
    bool isGrounded;
    float defaultPos;
    bool canGlide;
    bool isGliding;
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

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");



        //if (!isGrounded)
        //{
        //    x = Input.GetAxis("Horizontal");
        //    y = Input.GetAxis("Vertical");
        //}

        //Get input axes
        

        //Move with local dir
       
        move = new Vector3(x, 0f, y).normalized;


        float angle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        float smAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref smoothVel, smoothTime);

        if (isGrounded)
        {
            isJumping = false;
            canGlide = false;
            isGliding = false;
        }


        if (move.magnitude > 0.1f)
        {
            //transform.rotation = Quaternion.Euler(0f, smAngle, 0f);


            dir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            if(transform.parent == null)
            {
                transform.forward = dir;
                rb.MovePosition(rb.position + dir * speed * Time.deltaTime);

            }
        }

       
        //Fall down

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            isJumping = true;
            defaultPos = transform.position.y;
        }

        if (Input.GetButtonUp("Jump"))
        {
            canGlide = true;
            glideTimer = 5.0f;
        }

        if (Input.GetButton("Jump") && canGlide)
        {
            RaycastHit hit = new RaycastHit();

            glideTimer -= Time.deltaTime;

            float distToGround = 5f;
            if(!isGliding)
            {
               if(Physics.Raycast(transform.position, -Vector3.up, out hit))
               {
                    //Debug.Log(hit.distance);
                    distToGround = hit.distance;
               }

                rb.velocity = new Vector3(rb.velocity.x, -0.5f, rb.velocity.z);

                isGliding = true;
            }
            float currDist;

            Physics.Raycast(transform.position, -Vector3.up, out hit);
            currDist = hit.distance;
            
            if(glideTimer > 0.0f)
            {

            }
            else
            {
                rb.AddForce(Vector3.up * distToGround * -3 * Time.deltaTime, ForceMode.VelocityChange);
            }

            if (rb.velocity.y < -1.0f)
                rb.AddForce(Vector3.up * distToGround * 10f * Time.deltaTime, ForceMode.VelocityChange);
            else
                rb.AddForce(Vector3.up * 1f * Time.deltaTime, ForceMode.VelocityChange);

        }

    }

    void FixedUpdate()
    {
        if (move.magnitude > 0.1f  &&transform.parent!=null)
        {

            transform.forward = dir;
            rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //4 is water
        if (other.gameObject.layer == 4)
        {
            rb.AddForce(0.0f, -1.0f * Physics.gravity.y, 0.0f, ForceMode.VelocityChange);
        }

    }
};