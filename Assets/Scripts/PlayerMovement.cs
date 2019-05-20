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
        rigidbody.maxAngularVelocity = 100;
        controls.Player.Jump.performed += ctx => Jump();
        controls.Player.Movement.performed += ctx => xSpeed = ctx.ReadValue<Vector2>().x * moveSpeed * Time.deltaTime;
        controls.Player.Movement.performed += ctx => ySpeed = ctx.ReadValue<Vector2>().y * moveSpeed * Time.deltaTime;
        controls.Player.Movement.cancelled += ctx => xSpeed = 0f;
        controls.Player.Movement.cancelled += ctx => ySpeed = 0f;
    }

    void FixedUpdate() {
        Movement();
    }

    private void Movement()
    {
        // Get input
        float xInput = Input.GetAxis("Horizontal P" + playerInfo.playerIndex);
        float yInput = Input.GetAxis("Vertical P" + playerInfo.playerIndex);
        
    }
    
    private void Jump() {
        Debug.Log("Jump");
    }

    private void OnEnable() {
        controls.Enable();
        
        // Calculate speed with movement speed multiplier and time delta
        float xSpeed = xInput * moveSpeed * Time.deltaTime;
        float ySpeed = yInput * moveSpeed * Time.deltaTime;

        // Add speed to rigidbody torque
        rigidbody.AddTorque(new Vector3(xSpeed, 0, ySpeed));
    }
}
