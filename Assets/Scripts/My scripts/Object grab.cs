using UnityEngine;
using UnityEngine.InputSystem;

public class Objectgrab : MonoBehaviour
{
    public InputAction GrabAction;

    private float grabDistance = 3f;
    private Rigidbody grabbedObjectRb; // interactable object rigidbody
    private FixedJoint grabJoint;
    public LayerMask grabbableLayer;
    public Transform holdPoint; // joint for holding an object

    void Start()
    {
        GrabAction.Enable();
    }

    void FixedUpdate()
    {
        // if object is grabbed - drag it to the joint
        if (grabbedObjectRb != null)
        {
            Vector3 objDirection = holdPoint.position - grabbedObjectRb.position;
            grabbedObjectRb.linearVelocity = objDirection * 10f;
        }
    }

    private void Update()
    {

        // check if left mouse button is pressed
        if (GrabAction.triggered == true)
        {
            TryGrab();
        }
        else if (GrabAction.WasReleasedThisFrame())
        {
            Release();
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
                grabbedObjectRb = rb;

                // creating a joint
                grabJoint = gameObject.AddComponent<FixedJoint>();
                grabJoint.connectedBody = grabbedObjectRb;
                grabJoint.anchor = holdPoint.localPosition;
                grabJoint.breakForce = 5000f;
                grabJoint.breakTorque = 5000f;

                // move an object
                grabbedObjectRb.MovePosition(holdPoint.position);
            }
        }
    }

    void Release()
    {
        // reset created joint
        if (grabJoint != null)
        {
            grabJoint.connectedBody = null;
            Destroy(grabJoint);
        }
        grabbedObjectRb = null;
    }
}
