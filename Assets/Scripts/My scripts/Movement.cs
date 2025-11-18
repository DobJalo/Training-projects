using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Movement : MonoBehaviour
{
    public InputAction LeftAction;
    public InputAction MoveAction;

    private Rigidbody rb; // player rigidbody
    private Rigidbody grabbedObject; // interactable object rigidbody

    private float speed = 3f; // player speed
    private float sensitivity = 5f; // mouse sensivity
    private float rotationY; // player rotation
    public float headMinY = -40f; // limit the rotation in the Y-axis
    public float headMaxY = 40f; // limit the rotation in the Y-axis
    public float grabDistance = 3f;

    public LayerMask grabbableLayer;
    private FixedJoint grabJoint;
    public Transform holdPoint; // joint for holding an object

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

        // if object is grabbed - drag it to the joint
        if (grabbedObject != null)
        {
            Vector3 objDirection = holdPoint.position - grabbedObject.position;
            grabbedObject.linearVelocity = objDirection * 10f; 
        }
    }

    void Update()
    {
        // player rotation
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, headMinY, headMaxY);
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0); // apply rotation

        // object grab
        if (Input.GetMouseButtonDown(0))
        {
            if (grabJoint != null)
            {
                TryGrab();
            }
            else
            {
                Release();
            }

        }
    }
    private void TryGrab()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance, grabbableLayer))
        {
            Rigidbody rb = hit.collider.attachedRigidbody;
            if (rb != null && !rb.isKinematic)
            {
                grabbedObject = rb;

                // creating a joint
                grabJoint = gameObject.AddComponent<FixedJoint>();
                grabJoint.connectedBody = grabbedObject;
                grabJoint.anchor = holdPoint.localPosition;
                grabJoint.breakForce = 5000f;
                grabJoint.breakTorque = 5000f;

                // move an object
                grabbedObject.MovePosition(holdPoint.position);
            }
        }
    }

    void Release()
    {
        if (grabJoint != null)
        {
            grabJoint.connectedBody = null;
            Destroy(grabJoint);
        }
        grabbedObject = null;
    }
}