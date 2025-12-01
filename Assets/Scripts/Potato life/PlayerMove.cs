using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody playerRb;
    public Slider rotationSlider;
    public float speed = 3f; // player speed
    private float sensitivity = 500f; // rotation sensivity
    private Vector3 direction;
    private bool isHolding;
    public float jumpForce = 5f;
    private bool isGrounded = true;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    public void StartHold()
    {
        isHolding = true;
    }

    public void StopHold()
    {
        isHolding = false;
    }

    public void Up()
    {
        direction = transform.forward;
    }

    public void Left()
    {
        direction = -transform.right;
    }
    public void Right()
    {
        direction = transform.right;
    }
    public void Down()
    {
        direction = -transform.forward;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void Update()
    {
        // move player
        if (isHolding)
        {
            playerRb.MovePosition(playerRb.position + direction * speed * Time.deltaTime);
        }

        // rotate camera
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationSlider.value*sensitivity, transform.localEulerAngles.z);
    }
}

