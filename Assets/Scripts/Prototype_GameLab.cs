using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Prototype_GameLab : MonoBehaviour
{
    public enum ControlType
    {
        WASD,
        Arrows
    }

    public ControlType controlType;

    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (controlType == ControlType.WASD)
        {
            if (Input.GetKey(KeyCode.A)) horizontal = 1f;
            if (Input.GetKey(KeyCode.D)) horizontal = -1f;
            if (Input.GetKey(KeyCode.W)) vertical = -1f;
            if (Input.GetKey(KeyCode.S)) vertical = 1f;
        }
        else if (controlType == ControlType.Arrows)
        {
            horizontal = 0f;
            vertical = 0f;

            if (Input.GetKey(KeyCode.LeftArrow)) horizontal = 1f;
            if (Input.GetKey(KeyCode.RightArrow)) horizontal = -1f;
            if (Input.GetKey(KeyCode.UpArrow)) vertical = -1f;
            if (Input.GetKey(KeyCode.DownArrow)) vertical = 1f;
        }

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            // Move
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);

            // Rotate toward movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}