using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class PlayerMovement : MonoBehaviour
{    
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

    private new Rigidbody rigidbody;

    private InputMaster controls;
    
    private float xSpeed;
    private float ySpeed;

    private PlayerInfo playerInfo;

    void Awake()
    {
        controls = new InputMaster();
    }

    void Start()
    {
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

    void Update()
    {
        
    }

    private void Movement()
    {
        //Debug.Log("xSpeed: " + xSpeed + "ySpeed: " + ySpeed);
        rigidbody.AddTorque(new Vector3(xSpeed, 0, ySpeed));
    }

    private void Jump() {
        Debug.Log("Jump");
    }

    private void OnEnable() {
        controls.Enable();
    }
}
