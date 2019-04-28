using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpOptions options;

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
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        //Add mass options to player
        playerRigidbody.mass *= (1 + (options.increasePlayerMassPercentage / 100f));

        //Add scale options to player
        player.transform.localScale = new Vector3(
            player.transform.localScale.x * (1 + (options.increasePlayerSizePercentage / 100f)),
            player.transform.localScale.y * (1 + (options.increasePlayerSizePercentage / 100f)),
            player.transform.localScale.z * (1 + (options.increasePlayerSizePercentage / 100f))
        );

        //Add movement options to player
        playerMovement.MoveSpeed *= (1 + (options.increasePlayerMoveSpeedPercentage / 100f));

        //Destroy this power up
        Destroy(gameObject);
    }
}
