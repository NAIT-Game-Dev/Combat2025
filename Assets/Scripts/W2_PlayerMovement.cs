using UnityEngine;
using UnityEngine.InputSystem;

public class W2_PlayerMovement : MonoBehaviour
{
    float movementSpeed = 3.0f;
    float rotationSpeed = 100.0f;

    [SerializeField] InputAction moveAction;

    [SerializeField] Vector2 moveValue;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // move the object based on the values of the gamepad
        transform.Translate(Vector3.forward * moveValue.y * movementSpeed * Time.fixedDeltaTime);
        transform.Rotate(Vector3.up, moveValue.x * rotationSpeed * Time.fixedDeltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();
    }
}