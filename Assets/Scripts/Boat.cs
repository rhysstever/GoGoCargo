using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private int health;
    public int Health { get { return health; } }

    private int damage;
    public int Damage { get { return damage; } }

    private float money;
    public float Money { get { return money; } }

    private int totalCapacity;
    private Dictionary<ResourceType, int> cargo;
    public Dictionary<ResourceType, int> Cargo { get { return cargo; } }
    
    private GameObject currentIsland;
    public GameObject CurrentIsland { get { return currentIsland; } }

    // Start is called before the first frame update
    void Start()
    {
        health = 10;
        damage = 1;
        money = 100.0f;
        totalCapacity = 100;
        SetupCargo();
        currentIsland = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(currentIsland == null)
                EnterIsland();
            else
                ExitIsland();
        }
    }

    private void SetupCargo()
    {
        cargo = new Dictionary<ResourceType, int>();
        foreach(ResourceType resource in Enum.GetValues(typeof(ResourceType)))
        {
            cargo.Add(resource, 0);
        }
    }

    public void RepairDamage(int amount)
    {
        health += amount;
        UIManager.instance.UpdateGameText();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        UIManager.instance.UpdateGameText();
    }

    public int TotalCargoCount()
    {
        return cargo.Values.Sum();
    }

    public int TotalCargoSpace()
    {
        return totalCapacity - TotalCargoCount();
    }

    public void AddCargo(ResourceType resource, int amount)
    {
        cargo[resource] += Mathf.Min(amount, TotalCargoSpace());
        UIManager.instance.UpdateGameText();
    }

    public void RemoveResource(ResourceType resource, int amount)
    {
        if (cargo.ContainsKey(resource))
        {
            cargo[resource] = Mathf.Max(0, cargo[resource] - amount);
            UIManager.instance.UpdateGameText();
        }
    }

    public void AddMoney(float amount)
    {
        money += amount;
        UIManager.instance.UpdateGameText();
    }

    public void RemoveMoney(float amount)
    {
        money -= amount;
        UIManager.instance.UpdateGameText();
    }

    private void EnterIsland()
    {
        foreach(Transform island in GameManager.instance.IslandsParent.transform)
        {
            if(island.GetComponent<Island>().IsClose)
            {
                // An island is close enough
                currentIsland = island.gameObject;
                GameManager.instance.ChangeMenuState(MenuState.Trading);
                break;
            }
        }
    }

    public void ExitIsland()
    {
        currentIsland = null;
    }
}
