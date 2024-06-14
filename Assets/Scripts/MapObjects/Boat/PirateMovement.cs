using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateMovement : BoatMovement
{
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private new void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }

    public new bool CanMove()
    {
        return base.CanMove();
    }

    private void Move()
    {
        if(CanMove())
        {

        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
