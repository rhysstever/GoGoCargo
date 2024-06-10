using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : BoatMovement
{
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
        CheckPlayerInput();
    }

    private new bool CanMove()
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
            {
                Move(moveSpeed);
                // Set the camera's velocity to the same as the players so the camera follows the player
                Camera.main.GetComponent<Rigidbody>().velocity = rb.velocity;
            }

            if(Input.GetKey(KeyCode.S))
            {
                Move(-moveSpeed);
                // Set the camera's velocity to the same as the players so the camera follows the player
                Camera.main.GetComponent<Rigidbody>().velocity = rb.velocity;
            }

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
            Camera.main.GetComponent<Rigidbody>().velocity = rb.velocity;
        }
    }
}
