using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TankController : MonoBehaviour
{
    float movementSpeed = 300.0f;
    float fireRate = 2.0f;
    float timeStamp;
    [SerializeField] int playerID;

    [SerializeField] InputAction moveAction, rotateAction, fireAction;

    [SerializeField] Vector2 moveValue, rotateValue;

    [SerializeField] GameObject turret, barrelEnd, projectile;

    int numberOfProjectiles = 1;
    [SerializeField] int projectileIndex = 0;
    [SerializeField] List<GameObject> projectilePool;

    [SerializeField] Image fireCooldown;

    [SerializeField] AudioClip cannonFire;
    [SerializeField] AudioSource tankSounds;
    [SerializeField] ParticleSystem cannonSmoke;

    bool gamePaused = false;

    Rigidbody rbody;

    private void Start()
    {
        MyEvents.TogglePause.AddListener(TogglePause);
        rbody = GetComponent<Rigidbody>();
        tankSounds = GetComponent<AudioSource>();

        projectilePool = new List<GameObject>();
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            GameObject instantiatedObject = Instantiate(projectile);
            projectilePool.Add(instantiatedObject);
            Physics.IgnoreCollision(GetComponentInChildren<Collider>(), instantiatedObject.GetComponentInChildren<Collider>());
            instantiatedObject.GetComponent<Projectile>().SetPlayerID(playerID);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeStamp + fireRate && fireCooldown.color == Color.red)
        {
            fireCooldown.color = Color.green;
        }
    }

    private void FixedUpdate()
    {
        if (!gamePaused)
        {
            // move the object based on the values of the gamepad

            rbody.linearVelocity = transform.forward * moveValue.magnitude * movementSpeed * Time.fixedDeltaTime;

            if (moveValue.x != 0 || moveValue.y != 0)
            {
                transform.LookAt(transform.position + new Vector3(moveValue.x, 0, moveValue.y));
            }

            if (rotateValue.x != 0 || rotateValue.y != 0)
            {
                turret.transform.LookAt(turret.transform.position + new Vector3(rotateValue.x, 0, rotateValue.y));
            }
        }
    }

    public void SetPlayerID(int ID)
    {
        playerID = ID;
    }

    public int GetPlayerID()
    {
        return playerID;
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
        if (Time.time > timeStamp + fireRate && !gamePaused)
        {
            fireCooldown.color = Color.red;
            timeStamp = Time.time;
            projectilePool[projectileIndex].transform.position = barrelEnd.transform.position;
            projectilePool[projectileIndex].transform.rotation = barrelEnd.transform.rotation;
            projectilePool[projectileIndex].SetActive(true);
            
            projectilePool[projectileIndex].GetComponent<Rigidbody>().linearVelocity = projectilePool[projectileIndex].transform.forward * 20;
            

            tankSounds.PlayOneShot(cannonFire);
            
            cannonSmoke.Play();

            projectileIndex++;
            if (projectileIndex >= projectilePool.Count)
            {
                projectileIndex = 0;
            }
        }
    }

    public void TogglePause()
    {
        gamePaused = !gamePaused;
    }
}