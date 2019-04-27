﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treat : MonoBehaviour
{
    public TreatOptions treatOptions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp(GameObject player)
    {
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();

        playerRigidbody.mass *= (1 + (treatOptions.increasePlayerMassPercentage / 100f));
        player.transform.localScale = new Vector3(
            player.transform.localScale.x * (1 + (treatOptions.increasePlayerSizePercentage / 100f)),
            player.transform.localScale.y * (1 + (treatOptions.increasePlayerSizePercentage / 100f)),
            player.transform.localScale.z * (1 + (treatOptions.increasePlayerSizePercentage / 100f))
        );

        Destroy(gameObject);
    }
}
