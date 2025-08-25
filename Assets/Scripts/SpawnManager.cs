using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] List<GameObject> players;
    [SerializeField] List<int> gamepadID;

    [SerializeField] bool destroyOnDisconnect;

    // Update is called once per frame
    void Update()
    {
        // If a new gamepad gets plugged in.
        if (Gamepad.all.Count > gamepadID.Count)
        {
            // Add the gamepadID to a list
            gamepadID.Add(Gamepad.all[Gamepad.all.Count - 1].deviceId);

            if (players.Count < Gamepad.all.Count)
            {
                // Spawn a player object for the new gamepad
                GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

                // Add the new player object to a list
                players.Add(newPlayer);

                // Set the gamepadID for the player to be the one that just got plugged in
                newPlayer.GetComponent<W1_PlayerMovement>().SetGamepadID(Gamepad.all[Gamepad.all.Count - 1].deviceId);
            }
            else
            {
                bool found = false;
                for (int i = 0; i < players.Count && !found; i++)
                {
                    if (players[i].GetComponent<W1_PlayerMovement>().GetPlayerID() == -1)
                    {
                        players[i].GetComponent<W1_PlayerMovement>().SetGamepadID(Gamepad.all[Gamepad.all.Count - 1].deviceId);
                        found = true;
                    }
                }
            }
            
            UpdatePlayerID();
        }

        // If all gamepads are disconnected then clear the lists and destroy the last player object
        if (Gamepad.all.Count == 0 && gamepadID.Count > 0)
        {
            gamepadID.Clear();
            if (destroyOnDisconnect)
            {
                Destroy(players[0]);
                players.Clear();
            }
            UpdatePlayerID();
        }
        
        // If a gamepad got disconnected then find the player object that was connected to it
        if (Gamepad.all.Count < gamepadID.Count)
        {
            // Go through the list of gamepadIDs
            for (int i = 0; i < gamepadID.Count; i++)
            {
                bool found = false;
                // Search throught the current gamepads that are connected
                for (int j = 0; j < Gamepad.all.Count && !found; j++)
                {
                    // If the gamepadID is in the list of gamepads connected then you found it and do nothing
                    if (gamepadID[i] == Gamepad.all[j].deviceId)
                    {
                        found = true; 
                    }
                }
                // If the gamepadID was not found then remove at that index from both lists and destroy the player object
                if (!found)
                {
                    gamepadID.RemoveAt(i);
                    if (destroyOnDisconnect)
                    {
                        Destroy(players[i]);
                        players.RemoveAt(i);
                    }                                        
                }
            }
            // Update the playerIDs so that all gamepads will control the player objects that they did before
            UpdatePlayerID();
        }
    }

    void UpdatePlayerID()
    {
        bool playerFound;
        for (int i = 0; i < players.Count; i++)
        {
            playerFound = false;
            for (int j = 0; j < gamepadID.Count; j++)
            {
                if (players[i].GetComponent<W1_PlayerMovement>().GetGamepadID() == gamepadID[j])
                {
                    players[i].GetComponent<W1_PlayerMovement>().SetPlayerID(j);
                    playerFound = true;
                }
            }
            if (!playerFound)
            {
                players[i].GetComponent<W1_PlayerMovement>().SetPlayerID(-1);
            }
        }
    }
}
