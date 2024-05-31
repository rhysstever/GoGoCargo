using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatStats : MonoBehaviour
{
    [SerializeField]
    private string boatName;
    public string BoatName { get { return name; } }
    [SerializeField]
    private Texture image;
    public Texture Image { get { return image; } }
    [SerializeField]
    private float priceToUpgrade;
    public float PriceToUpgrade { get { return priceToUpgrade; } }
    [SerializeField]
    private float moveSpeed, turnSpeed, maxVelocity, maxAngularVelocity;
    public float MoveSpeed { get { return moveSpeed; } }
    public float TurnSpeed { get { return turnSpeed; } }
    public float MaxVelocity { get { return maxVelocity; } }
    public float MaxAngularVelocity { get { return maxAngularVelocity; } }
}
