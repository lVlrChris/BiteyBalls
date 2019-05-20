using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

    private PlayerInfo playerInfo;
    private new Rigidbody rigidbody;

    void Start()
    {
        playerInfo = this.GetComponent<Player>().playerInfo;
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        // Get input
        float xInput = Input.GetAxis("Horizontal P" + playerInfo.playerIndex);
        float yInput = Input.GetAxis("Vertical P" + playerInfo.playerIndex);

        // Calculate speed with movement speed multiplier and time delta
        float xSpeed = xInput * moveSpeed * Time.deltaTime;
        float ySpeed = yInput * moveSpeed * Time.deltaTime;

        // Add speed to rigidbody torque
        rigidbody.AddTorque(new Vector3(xSpeed, 0, ySpeed));
    }
}