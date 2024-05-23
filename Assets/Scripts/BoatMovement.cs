using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float moveSpeed, turnSpeed, maxVelocity, maxAngularVelocity;

    public float currentMoveSpeed, currentTurnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void FixedUpdate()
	{
        Move();
        currentMoveSpeed = rb.velocity.magnitude;
        currentTurnSpeed = rb.angularVelocity.magnitude;
	}

    private bool CanMove()
	{
        return true;
	}

    private void Move()
	{
        if(CanMove())
		{
            // Translational - Boat Accelerating/Decelerating
            if(Input.GetKey(KeyCode.W))
            {
                Vector3 movementForce = moveSpeed * Time.deltaTime * rb.transform.forward;
                rb.AddForce(movementForce);
                // Clamp speed
                float clampedVelocity = Mathf.Clamp(rb.velocity.magnitude, 0, maxVelocity);
                rb.velocity = rb.velocity.normalized * clampedVelocity;
                // Set the camera's velocity to the same as the players so the camera follows the player
                Camera.main.GetComponent<Rigidbody>().velocity = rb.velocity;
            }
            
            if (Input.GetKey(KeyCode.S))
            {
                Vector3 movementForce = -moveSpeed * Time.deltaTime * rb.transform.forward;
                rb.AddForce(movementForce);
                // Clamp speed
                float clampedVelocity = Mathf.Clamp(rb.velocity.magnitude, 0, maxVelocity);
                rb.velocity = rb.velocity.normalized * clampedVelocity;
                // Set the camera's velocity to the same as the players so the camera follows the player
                Camera.main.GetComponent<Rigidbody>().velocity = rb.velocity;
            }

            // Rotational - Boat Turning
            if(Input.GetKey(KeyCode.A))
            {
                rb.AddTorque(new Vector3(0, -turnSpeed * Time.deltaTime, 0));
                // Clamp turn speed
                float clampedAngularVelocity = Mathf.Clamp(rb.angularVelocity.magnitude, 0, maxAngularVelocity);
                rb.angularVelocity = rb.angularVelocity.normalized * clampedAngularVelocity;
            }

            if(Input.GetKey(KeyCode.D))
            {
                rb.AddTorque(new Vector3(0, turnSpeed * Time.deltaTime, 0));
                // Clamp turn speed
                float clampedAngularVelocity = Mathf.Clamp(rb.angularVelocity.magnitude, 0, maxAngularVelocity);
                rb.angularVelocity = rb.angularVelocity.normalized * clampedAngularVelocity;
            }
        }
    }
}
