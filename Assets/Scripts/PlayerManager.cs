using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerManager
{
    public Transform spawnPoint;

    [HideInInspector] public int playerIndex;
    [HideInInspector] public GameObject instance;
    [HideInInspector] public int wins;

    private PlayerInfo playerInfo;
    private PlayerMovement movement;
    private PlayerJump jumping;

    public void Setup()
    {
        movement = instance.GetComponent<PlayerMovement>();
        jumping = instance.GetComponent<PlayerJump>();
        playerInfo = instance.GetComponent<Player>().playerInfo;
        
        playerInfo.playerIndex = playerIndex;
    }

    public void DisableControl()
    {
        movement.enabled = false;
        jumping.enabled = false;
    }

    public void EnableControl()
    {
        movement.enabled = true;
        jumping.enabled = true;
    }
}
