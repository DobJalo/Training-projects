using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Movement : MonoBehaviour
{
    public InputAction LeftAction;
    public InputAction MoveAction;

    private Rigidbody rb;

    private float speed = 3f;
    private float sensitivity = 5f;
    private float rotationY;

    // limit the rotation in the Y-axis
    public float headMinY = -40f; 
    public float headMaxY = 40f;

    private void Start()
    {
        // enable the Action so that's available to the player
        MoveAction.Enable();

        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        // player movement
        Vector3 move = MoveAction.ReadValue<Vector3>(); // get direction
        Vector3 direction = transform.TransformDirection(move); // transform a local direction into a global one
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime); // move player
    }

    void Update()
    {
        // player rotation
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, headMinY, headMaxY);
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0); // apply rotation
    }
}
