using UnityEngine;

public class Dignis_Movement : MonoBehaviour
{
    private float speed = 5;
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;

    private bool reversecontrols = false;

    public Transform cameraTransform;
    private Rigidbody rb;
    private float xRotation = 0f;

    public GameObject MenuObject;
    private bool moveAround = false;

    public Camera playerCamera;

    //jumping
    public float jumpForce = 5f;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.1f;
    private bool isGrounded;

    void Start()
    {
        playerCamera.farClipPlane = 1000;
        speed = 5;

        rb = GetComponent<Rigidbody>(); // Get Rigidbody component from Player




    }

    void FixedUpdate()
    {
        // Player movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
    }

    void Update()
    {
        //Running
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 8; //change speed
            playerCamera.fieldOfView = 100; //change camera view while running (inform player about running through the visuals)
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5;
            playerCamera.fieldOfView = 95.4f;
        }

        //Jumping

        //if player is on the ground (object has Ground layer) - jumps are allowed
        isGrounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.extents.y + groundCheckDistance, groundLayer);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Mouse Sensitivity
        if (PlayerPrefs.HasKey("MouseSensitivity"))
        {
            float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
            lookSpeedX = savedSensitivity;
            lookSpeedY = savedSensitivity;
        }

        // Get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;


        if (moveAround == false)
        {
            // Rotate player left/right
            transform.Rotate(Vector3.up * mouseX);

            // Rotate player up/down
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
}
