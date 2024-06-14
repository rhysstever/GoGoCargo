using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : BoatMovement
{
    private Rigidbody cameraRb;

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        cameraRb = Camera.main.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private new void FixedUpdate()
    {
        base.FixedUpdate();
        CheckPlayerInput();
    }

    public new bool CanMove()
    {
        return base.CanMove() 
            && PlayerManager.instance.CurrentPlayerState == PlayerState.Sailing;
    }

    private void CheckPlayerInput()
    {
        if(CanMove())
        {
            // Translational - Boat Accelerating/Decelerating
            if(Input.GetKey(KeyCode.W))
                Move(moveSpeed);
            if(Input.GetKey(KeyCode.S))
                Move(-moveSpeed);

            // Rotational - Boat Turning
            if(Input.GetKey(KeyCode.A))
                Turn(-turnSpeed);
            if(Input.GetKey(KeyCode.D))
                Turn(turnSpeed);
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        cameraRb.velocity = rb.velocity;
    }
}
