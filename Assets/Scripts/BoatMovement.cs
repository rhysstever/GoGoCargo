using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float moveSpeed, turnSpeed;

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
	}

    private bool CanMove()
	{
        return true;
	}

    private void Move()
	{
        if(CanMove())
		{
            if(Input.GetKey(KeyCode.W))
            {
                transform.parent.GetComponent<Rigidbody>().AddForce(moveSpeed * Time.deltaTime * rb.transform.up);
            }
            
            if (Input.GetKey(KeyCode.S))
			{
                transform.parent.GetComponent<Rigidbody>().AddForce(-moveSpeed * Time.deltaTime * rb.transform.up);
            }

            if(Input.GetKey(KeyCode.A))
            {
                rb.AddTorque(new Vector3(0, -turnSpeed * Time.deltaTime, 0));
            }

            if(Input.GetKey(KeyCode.D))
            {
                rb.AddTorque(new Vector3(0, turnSpeed * Time.deltaTime, 0));
            }
        }
        else
            rb.velocity = Vector2.zero;
    }
}
