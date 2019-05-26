using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    private float jumpVelocity = 5f;
    [SerializeField]
    private float fallMultiplier = 2.5f;
    [SerializeField]
    private float lowJumpMultiplier = 2f;
    [SerializeField]
    private float airSpeed;

    private PlayerInfo playerInfo;
    private new Rigidbody rigidbody;
    private float distToGround;

    void Start()
    {
        playerInfo = GetComponent<Player>().playerInfo;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Jump();
        Fall();
        MidAirMove();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump P" + playerInfo.playerIndex) && IsGrounded())
        {
            rigidbody.velocity += Vector3.up * jumpVelocity;
        }
    }

    private void Fall()
    {
        if (rigidbody.velocity.y < 0)
        {
            rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } 
        else if (rigidbody.velocity.y > 0 && !Input.GetButton("Jump P" + playerInfo.playerIndex))
        {
            rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    
    private void MidAirMove()
    {
        if (!IsGrounded())
        {
            // Get input
            float xInput = Input.GetAxis("Horizontal P" + playerInfo.playerIndex);
            float yInput = Input.GetAxis("Vertical P" + playerInfo.playerIndex);

            Vector3 direction = new Vector3(-yInput, 0, xInput) * airSpeed * Time.deltaTime;

            rigidbody.AddForce(direction, ForceMode.Force);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
    }
}
