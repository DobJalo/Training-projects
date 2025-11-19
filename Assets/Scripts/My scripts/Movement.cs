using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Movement : MonoBehaviour
{
    public InputAction MoveAction;

    private Rigidbody rb; // player rigidbody

    private float speed = 3f; // player speed
    private float sensitivity = 5f; // mouse sensivity
    private float rotationY; // player rotation
    private float headMinY = -40f; // limit the rotation in the Y-axis
    private float headMaxY = 40f; // limit the rotation in the Y-axis

    private void Start()
    {
        // enable the Action so that's available to the player
        MoveAction.Enable();

        rb = GetComponent<Rigidbody>(); // get player rigidbody
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