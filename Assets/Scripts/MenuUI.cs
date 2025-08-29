using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ExitGame();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReplayGame()
    {
        MyEvents.Replay.Invoke();
    }

    public void OpenLobby()
    {
        MyEvents.OpenLobby.Invoke();
    }
}
