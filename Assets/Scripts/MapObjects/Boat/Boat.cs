using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private int health, capacity;
    public int Health { get { return health; } }
    public int Capacity { get { return capacity; } }

    private Dictionary<ResourceType, int> cargo;
    public Dictionary<ResourceType, int> Cargo { get { return cargo; } }

    private GameObject currentIsland;
    public GameObject CurrentIsland { get { return currentIsland; } }

    private void Awake()
    {
        SetupCargo();
        health = GetComponent<BoatStats>().Health;
        capacity = GetComponent<BoatStats>().Capacity;
        currentIsland = null;
    }

    // Start is called before the first frame update
    void Start()
    {

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
        if(gameObject.tag == "Player")
            UIManager.instance.UpdatePlayerText();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if(gameObject.tag == "Player")
            UIManager.instance.UpdatePlayerText();

        if(health <= 0)
            Destroy(gameObject);
    }

    public int CargoCount()
    {
        return cargo.Values.Sum();
    }

    public int CargoSpace()
    {
        return capacity - CargoCount();
    }

    public void AddCargo(ResourceType resource, int amount)
    {
        cargo[resource] += Mathf.Min(amount, CargoSpace());
        if(gameObject.tag == "Player")
            UIManager.instance.UpdatePlayerText();
    }

    public void AddCargo(Dictionary<ResourceType, int> cargo)
    {
        foreach(ResourceType resource in cargo.Keys)
            AddCargo(resource, cargo[resource]);
    }

    public void RemoveResource(ResourceType resource, int amount)
    {
        if (cargo.ContainsKey(resource))
        {
            cargo[resource] = Mathf.Max(0, cargo[resource] - amount);
            if(gameObject.tag == "Player")
                UIManager.instance.UpdatePlayerText();
        }
    }

    public void EnterIsland()
    {
        foreach(Transform island in GameManager.instance.IslandsParent.transform)
        {
            if(island.GetComponent<Island>().IsClose)
            {
                // An island is close enough
                currentIsland = island.gameObject;
                PlayerManager.instance.ChangePlayerState(PlayerState.Trading);
                break;
            }
        }
    }

    public void ExitIsland()
    {
        currentIsland = null;
    }
}
