using UnityEngine;
using UnityEngine.InputSystem;

public class W1_PlayerMovement : MonoBehaviour
{
    float movementSpeed = 3.0f;

    [SerializeField] int playerID, gamepadID;

    float forward, backward, right, left, xValue, zValue;

    // Update is called once per frame
    void Update()
    {
        // if you gamepads are connected then any player objects will be controlled by the keyboard. This is mostly for testing purposes as you will need to put a prefab in the scene.
        #region Keyboard Input
        //if (Gamepad.all.Count == 0)
        //{
        //    if (Keyboard.current.wKey.isPressed)
        //    {
        //        forward = 1;
        //    }
        //    else
        //    {
        //        forward = 0;
        //    }

        //    if (Keyboard.current.sKey.isPressed)
        //    {
        //        backward = 1;
        //    }
        //    else
        //    {
        //        backward = 0;
        //    }

        //    if (Keyboard.current.aKey.isPressed)
        //    {
        //        left = 1;
        //    }
        //    else
        //    {
        //        left = 0;
        //    }

        //    if (Keyboard.current.dKey.isPressed)
        //    {
        //        right = 1;
        //    }
        //    else
        //    {
        //        right = 0;
        //    }
        //    xValue = right - left;
        //    zValue = forward - backward;
        //}
        #endregion

        if (playerID != -1)
        {
            #region Controller Input for Gamepad
            // playerID is the index of the gamepad in the Gamepad list. Make sure the playerID does not exceed the number of gamepads plugged in.
            if (Gamepad.all.Count > playerID)
            {
                #region IsPressed
                // use the controls of the gamepad in the list based on the playerID index. In this example we are setting values based on whether the control is pressed.
                //if (Gamepad.all[playerID].leftStick.up.isPressed)
                //{
                //    forward = 1;
                //}
                //else
                //{
                //    forward = 0;
                //}

                //if (Gamepad.all[playerID].leftStick.down.isPressed)
                //{
                //    backward = 1;
                //}
                //else
                //{
                //    backward = 0;
                //}

                //if (Gamepad.all[playerID].leftStick.right.isPressed)
                //{
                //    right = 1;
                //}
                //else
                //{
                //    right = 0;
                //}

                //if (Gamepad.all[playerID].leftStick.left.isPressed)
                //{
                //    left = 1;
                //}
                //else
                //{
                //    left = 0;
                //}

                //xValue = right - left;
                //zValue = forward - backward;
                #endregion

                #region Value
                // use the controls of the gamepad in the list based on the playerID index. In this example we are getting the float values of the left stick controls.
                forward = Gamepad.all[playerID].leftStick.up.value;
                backward = Gamepad.all[playerID].leftStick.down.value;
                right = Gamepad.all[playerID].leftStick.right.value;
                left = Gamepad.all[playerID].leftStick.left.value;

                xValue = right - left;
                zValue = forward - backward;
                #endregion
            }
            #endregion
        }
    }

    private void FixedUpdate()
    {
        // move the object based on the values of the gamepad
        transform.Translate(new Vector3(xValue * movementSpeed * Time.fixedDeltaTime, 0, zValue * movementSpeed * Time.fixedDeltaTime));
    }

    public void SetPlayerID(int id)
    {
        // set the player id to the index of the gamepad in use.
        playerID = id;
    }

    public int GetPlayerID()
    {
        // return the player id.
        return playerID;
    }

    public int GetGamepadID()
    {
        // return the gamepad ID that the object is currently linked to.
        return gamepadID;
    }

    public void SetGamepadID(int id)
    {
        // set the gamepad ID that the object should be linked to.
        gamepadID = id;
    }
}
