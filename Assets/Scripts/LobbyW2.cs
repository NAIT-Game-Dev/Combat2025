using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LobbyW2 : MonoBehaviour
{
    [SerializeField] GamepadManager gamepadManager;
    [SerializeField] GameManager gameManager;
    [SerializeField] List<TMP_Text> playerText;
    [SerializeField] Button startButton;

    [SerializeField] GameObject lobbyPanel;

    [SerializeField] PlayerInputManager playerInputManager;

    [SerializeField] GameObject[] spawnLocations;

    // Start is called before the first frame update
    void Start()
    {
        gamepadManager = GameObject.Find("GamepadManager").GetComponent<GamepadManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.startButton.wasPressedThisFrame)
            {
                gamepadManager.PlayerJoined(Gamepad.current.deviceId);
            }

            if (Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                gamepadManager.PlayerLeft(Gamepad.current.deviceId);

                for (int i = 0; i < playerText.Count; i++)
                {
                    playerText[i].text = "Press Start to Join";
                }
            }
        }

        for (int i = 0; i < gamepadManager.PlayerCount(); i++)
        {
            if (gamepadManager.PlayerStatus(i) > -1)
            {
                playerText[i].text = "Connected";
            }
            if (gamepadManager.PlayerStatus(i) == -1)
            {
                playerText[i].text = "Disconnected";
            }
        }

        if (gamepadManager.PlayerCount() == 0)
        {
            startButton.interactable = false;
        }
        else
        {
            startButton.interactable = true;
        }
    }

    public void StartGame()
    {
        for (int i = 0; i < gamepadManager.PlayerCount(); i++)
        {
            bool found = false;
            int index = 0;
            for (int j = 0; j < Gamepad.all.Count && !found; j++)
            {
                if (gamepadManager.gamepadID[i] == Gamepad.all[j].deviceId)
                {
                    found = true;
                    index = j;
                }
            }
            PlayerInput player = playerInputManager.JoinPlayer(i, -1, null,  Gamepad.all[index]);
            player.transform.position = spawnLocations[i].transform.position;
            player.GetComponent<TankController>().SetPlayerID(i);
            gameManager.AddTank(player.gameObject);
        }
        gameObject.SetActive(false);
    }
}
