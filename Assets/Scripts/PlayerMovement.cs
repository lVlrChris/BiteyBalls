using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float maxSpinSpeed;
    [SerializeField]
    private float playerBounce;

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
    
    void Update()
    {
        //Max spin speed set in update to tweak while playing.
        rigidbody.maxAngularVelocity = maxSpinSpeed;
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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.DrawRay(collision.contacts[0].point, collision.contacts[0].normal * -12, Color.red, 5f);

        //Check if collision is a player
        if (collision.collider.CompareTag("Player"))
        {
            float bounceForce = collision.relativeVelocity.magnitude * playerBounce;
            Vector3 direction = collision.collider.transform.position - transform.position;

            Debug.Log("BounceForce: " + bounceForce + " direction: " + direction);

            collision.collider.GetComponent<Rigidbody>().AddForce(direction * bounceForce, ForceMode.Impulse);
            // rigidbody.AddForce(direction * -bounceForce, ForceMode.Impulse);
        }
    }
}