using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderMovement : BoatMovement
{
    [SerializeField]
    private GameObject currentDestination;
    [SerializeField]
    private int moveInterval, turnInterval;

    private int currentMoveFrameTimer, currentTurnFrameTimer;

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        if(moveInterval <= 0)
            moveInterval = 2;
        if(turnInterval <= 0)
            turnInterval = 4;
    }

    // Update is called once per frame
    private new void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
    }

    private new bool CanMove()
    {
        return base.CanMove();
    }

    private void Move()
    {
        if(CanMove())
        {
            IncrementMovementTimers();
            MoveTowardsDestination();

            if(Vector3.Distance(
                gameObject.transform.position,
                currentDestination.transform.position) <= 10.0f)
                FindNewDestination();
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void FindNewDestination()
    {
        currentDestination = FindRandomIsland();
    }

    private GameObject FindRandomIsland()
    {
        int randomIslandNum = Random.Range(0, IslandManager.instance.IslandParent.transform.childCount);
        return IslandManager.instance.IslandParent.transform.GetChild(randomIslandNum).gameObject;
    }

    private void MoveTowardsDestination()
    {
        if(currentMoveFrameTimer >= moveInterval)
        {
            // If the destination is in front of the boat, move forward
            if(Vector3.Dot(
                transform.forward,
                currentDestination.transform.position - gameObject.transform.position)
                > 0)
            {
                Move(moveSpeed);
                currentMoveFrameTimer = 0;
            }
        }

        if(currentTurnFrameTimer >= turnInterval)
        {
            // If the destination is to the right of the boat, turn right, otherwise turn left
            if(Vector3.Dot(
                transform.right,
                currentDestination.transform.position - gameObject.transform.position)
                >= 0)
                Turn(turnSpeed);
            else
                Turn(-turnSpeed);

            currentTurnFrameTimer = 0;
        }
    }

    private void IncrementMovementTimers()
    {
        currentMoveFrameTimer++;
        currentTurnFrameTimer++;
    }
}
