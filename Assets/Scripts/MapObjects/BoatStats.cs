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
    private int health, damage, capacity;
    public int Health { get { return health; } }
    public int Damage { get { return damage; } }
    public int Capacity { get { return capacity; } }
    [SerializeField]
    private float priceToUpgrade, moveSpeed, turnSpeed, maxVelocity, maxAngularVelocity;
    public float PriceToUpgrade { get { return priceToUpgrade; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public float TurnSpeed { get { return turnSpeed; } }
    public float MaxVelocity { get { return maxVelocity; } }
    public float MaxAngularVelocity { get { return maxAngularVelocity; } }
}
