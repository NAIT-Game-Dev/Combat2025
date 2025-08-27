using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : MonoBehaviour
{
    float movementSpeed = 3.0f;
    float rotationSpeed = 100.0f;

    [SerializeField] InputAction moveAction, rotateAction;

    [SerializeField] Vector2 moveValue, rotateValue;

    [SerializeField] GameObject turret;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // move the object based on the values of the gamepad
        transform.Translate(Vector3.forward * moveValue.y * movementSpeed * Time.fixedDeltaTime);
        transform.Rotate(Vector3.up, moveValue.x * rotationSpeed * Time.fixedDeltaTime);
        if (rotateValue.x !=0 || rotateValue.y !=0 )
        {
            turret.transform.LookAt(transform.position + new Vector3(rotateValue.x, turret.transform.localPosition.y, rotateValue.y));
        }        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        rotateValue = context.ReadValue<Vector2>();
    }
}