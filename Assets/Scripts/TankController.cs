using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : MonoBehaviour
{
    float movementSpeed = 3.0f;
    float fireRate = 2.0f;
    float timeStamp;

    [SerializeField] InputAction moveAction, rotateAction, fireAction;

    [SerializeField] Vector2 moveValue, rotateValue;

    [SerializeField] GameObject turret, barrelEnd, projectile;

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
        transform.Translate(Vector3.forward * moveValue.magnitude * movementSpeed * Time.fixedDeltaTime);
        if (moveValue.x != 0 || moveValue.y != 0)
        {
            transform.LookAt(transform.position + new Vector3(moveValue.x, 0, moveValue.y));
        }

        if (rotateValue.x !=0 || rotateValue.y !=0 )
        {
            turret.transform.LookAt(turret.transform.position + new Vector3(rotateValue.x, 0, rotateValue.y));
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

    public void OnFire(InputAction.CallbackContext context)
    {
        if (Time.time > timeStamp + fireRate)
        {
            timeStamp = Time.time;
            GameObject instantiatedObject = Instantiate(projectile, barrelEnd.transform.position, barrelEnd.transform.rotation);
            instantiatedObject.GetComponent<Rigidbody>().velocity = instantiatedObject.transform.forward * 20;
        }
    }
}