using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerManager
{
    public int playerIndex;
    public int wins;
    public GameObject playerPrefab;
    [HideInInspector] public GameObject instance;

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
