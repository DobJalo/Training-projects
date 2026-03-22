using UnityEngine;

public class Dignis_ObjectGrabber : MonoBehaviour
{
    public float grabDistance = 3f;
    public float holdDistance = 3f;
    public float moveSpeed = 10f;

    private GameObject grabbedObject;
    private Rigidbody grabbedRb;

    void Update()
    {
        // Mouse button pressed ? try grab
        if (Input.GetMouseButtonDown(0))
        {
            TryGrab();
        }

        // Mouse button released ? drop
        if (Input.GetMouseButtonUp(0))
        {
            DropObject();
        }

        // While holding ? move object
        if (grabbedObject != null)
        {
            MoveObject();
        }
    }

    void TryGrab()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, grabDistance))
        {
            if (hit.rigidbody != null)
            {
                grabbedObject = hit.collider.gameObject;
                grabbedRb = hit.rigidbody;

                grabbedRb.useGravity = false;
                grabbedRb.linearDamping = 10f; // smooth movement
            }
        }
    }

    void MoveObject()
    {
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;

        Vector3 direction = targetPosition - grabbedObject.transform.position;
        grabbedRb.linearVelocity = direction * moveSpeed;
    }

    void DropObject()
    {
        if (grabbedObject != null)
        {
            grabbedRb.useGravity = true;
            grabbedRb.linearDamping = 0f;

            grabbedObject = null;
            grabbedRb = null;
        }
    }
}
