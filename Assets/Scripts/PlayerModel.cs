using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    private PlayerInfo playerInfo;
    float xHeading;
    float yHeading;

    void Start()
    {
        playerInfo = this.GetComponentInParent<Player>().playerInfo;
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal P" + playerInfo.playerIndex);
        xHeading = xInput != 0 ? xInput : xHeading;

        float yInput = Input.GetAxis("Vertical P" + playerInfo.playerIndex);
        yHeading = yInput != 0 ? yInput : yHeading;

        if (xHeading != 0 || yHeading != 0) {
            transform.rotation = Quaternion.LookRotation(new Vector3(xHeading, 0, yHeading));
        }
    }
}
