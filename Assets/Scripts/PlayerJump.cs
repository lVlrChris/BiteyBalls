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

    private PlayerInfo playerInfo;
    private new Rigidbody rigidbody;
    private float distToGround;

    void Start()
    {
        playerInfo = GetComponent<Player>().playerInfo;
        rigidbody = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    void Update()
    {
        Jump();
        Fall();
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
            rigidbody.velocity += Vector3.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } 
        else if (rigidbody.velocity.y > 0 && !Input.GetButton("Jump P" + playerInfo.playerIndex))
        {
            rigidbody.velocity += Vector3.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
