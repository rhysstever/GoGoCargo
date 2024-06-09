using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    protected Rigidbody rb;
    protected float moveSpeed, turnSpeed, maxVelocity, maxAngularVelocity;

    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        BoatStats stats = GetComponent<BoatStats>();
        moveSpeed = stats.MoveSpeed;
        turnSpeed = stats.TurnSpeed;
        maxVelocity = stats.MaxVelocity;
        maxAngularVelocity = stats.MaxAngularVelocity;
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    protected void FixedUpdate()
    {
        
    }

    protected bool CanMove()
    {
        return GameManager.instance.CurrentMenuState == MenuState.Sailing
            && GetComponent<Boat>().CurrentIsland == null;
    }

    protected void Move(float moveSpeed)
    {
        Vector3 movementForce = moveSpeed * Time.deltaTime * rb.transform.forward;
        rb.AddForce(movementForce);
        // Clamp speed
        float clampedVelocity = Mathf.Clamp(rb.velocity.magnitude, 0, maxVelocity);
        rb.velocity = rb.velocity.normalized * clampedVelocity;
    }

    protected void Turn(float turnSpeed)
    {
        rb.AddTorque(new Vector3(0, turnSpeed * Time.deltaTime, 0));
        // Clamp turn speed
        float clampedAngularVelocity = Mathf.Clamp(rb.angularVelocity.magnitude, 0, maxAngularVelocity);
        rb.angularVelocity = rb.angularVelocity.normalized * clampedAngularVelocity;
    }
}
