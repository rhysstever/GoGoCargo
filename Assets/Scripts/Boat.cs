using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private int health;
    private int damage;
    private int totalCapacity;
    private Dictionary<Resource, int> cargo;

    // Start is called before the first frame update
    void Start()
    {
        health = 10;
        damage = 1;
        totalCapacity = 100;
        SetupCargo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupCargo()
    {
        cargo = new Dictionary<Resource, int>();
        foreach(Resource resource in Enum.GetValues(typeof(Resource)))
        {
            cargo.Add(resource, 0);
        }
    }

    public int TotalCargoCount()
    {
        return cargo.Values.Sum();
    }

    public int TotalCargoSpace()
    {
        return totalCapacity - TotalCargoCount();
    }

    public void AddCargo(Resource resource, int amount)
    {
        cargo[resource] += Mathf.Min(amount, TotalCargoSpace());
    }

    public void RemoveResource(Resource resource, int amount)
    {
        if (cargo.ContainsKey(resource))
            cargo[resource] = Mathf.Max(0, cargo[resource] - amount);
    }
}
