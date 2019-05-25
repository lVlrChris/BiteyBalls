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
    private PlayerModel playerModel;

    public void Setup()
    {
        movement = instance.GetComponent<PlayerMovement>();
        jumping = instance.GetComponent<PlayerJump>();
        playerModel = instance.GetComponentInChildren<PlayerModel>();
        playerInfo = instance.GetComponent<Player>().playerInfo;
        
        playerInfo.playerIndex = playerIndex;
    }

    public void DisableControl()
    {
        movement.enabled = false;
        jumping.enabled = false;
        playerModel.enabled = false;
    }

    public void EnableControl()
    {
        movement.enabled = true;
        jumping.enabled = true;
        playerModel.enabled = true;
    }

    public void Reset(Transform spawnpoint)
    {
        Rigidbody instanceRb = instance.GetComponent<Rigidbody>();
        instanceRb.velocity = Vector3.zero;
        instanceRb.angularVelocity = Vector3.zero;

        instance.transform.position = spawnpoint.transform.position;
        instance.transform.rotation = spawnpoint.transform.rotation;
        instance.transform.GetChild(0).transform.rotation = Quaternion.identity;

        instance.SetActive(false);
        instance.SetActive(true);
    }
}
