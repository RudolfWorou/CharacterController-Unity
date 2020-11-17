using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float walkSpeed = 2;
    public float runSpeed = 6;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;

    public float jumpforce = 2f;
    private int extrajumps;
    public int extraJumpsValues;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;

  

    public Rigidbody rb;
    Animator animator;
    Transform cameraT;
    CharacterController controller;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        extrajumps = extraJumpsValues;
    }

    void FixedUpdate()
    {
        Collider[] hits = Physics.OverlapSphere(groundCheck.position, checkRadius);
        int Numberofground = 0;
        foreach (Collider hit in hits)
        {
            if (hit.tag == "ground")
            {
                Numberofground++;
            }
        }
        if (Numberofground > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }


        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);


        if (isGrounded == true)
        {
            extrajumps = extraJumpsValues;
            
        }

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);


        float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;
        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && extrajumps > 0)
        {
            animator.SetTrigger("jump");
            rb.velocity = Vector3.up * jumpforce;
            extrajumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extrajumps == 0 && isGrounded == true)
        {
            animator.SetTrigger("jump");
            rb.velocity = Vector3.up * jumpforce;
        }


        if (Input.GetKeyDown(KeyCode.X) && isGrounded == true)
        {
            animator.SetTrigger("flip");
            
            
        }
        // animator.SetBool("jumponspot", false);

    }

  


}
