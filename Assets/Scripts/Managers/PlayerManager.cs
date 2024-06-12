using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Sailing,
    Trading
}

public class PlayerManager : MonoBehaviour
{
    #region Singleton Code
    // A public reference to this script
    public static PlayerManager instance = null;

    // Awake is called even before start
    private void Awake()
    {
        // If the reference for this script is null, assign it this script
        if(instance == null)
            instance = this;
        // If the reference is to something else (it already exists)
        // than this is not needed, thus destroy it
        else if(instance != this)
            Destroy(gameObject);
    }
    #endregion Singleton Code

    [SerializeField]
    private PlayerState currentPlayerState;
    public PlayerState CurrentPlayerState { get { return currentPlayerState; } }

    [SerializeField]
    private Boat player;
    public Boat Player { get { return player; } }

    private float money;
    public float Money { get { return money; } }

    public bool isBuying;

    // Start is called before the first frame update
    void Start()
    {
        UpgradeBoat(ResourceManager.instance.Boats[0]);
        ChangePlayerState(PlayerState.Sailing);
        money = 100.0f;
        isBuying = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangePlayerState(PlayerState newPlayerState)
    {
        currentPlayerState = newPlayerState;
        // Update UI
        UIManager.instance.ChangeUIState(
            GameManager.instance.CurrentMenuState, 
            currentPlayerState);
    }

    public void AddMoney(float amount)
    {
        money += amount;
        UIManager.instance.UpdatePlayerText();
    }

    public void RemoveMoney(float amount)
    {
        money -= amount;
        UIManager.instance.UpdatePlayerText();
    }

    public void ResourceAction(ResourceType resource, int amount)
    {
        // Check availability
        if(isBuying)
        {
            // BUYING GOODS FROM ISLAND
            // Check if player has room in cargo
            if(player.CargoSpace() >= amount)
            {
                // Check player money vs island price
                float buyPrice = player.CurrentIsland.GetComponent<TradingPost>().GetIslandPrice(resource, isBuying);
                if(money >= buyPrice)
                {
                    // Remove money from player
                    RemoveMoney(buyPrice * amount);
                    // Add resource to the player
                    player.AddCargo(resource, amount);
                }
                else
                {
                    Debug.Log("No Sale! Not enough money to buy resource.");
                }
            }
            else
            {
                Debug.Log("No Sale! Not enough space aboard.");
            }
        }
        else
        {
            // SELLING GOODS TO ISLAND
            // Check player cargo availabilty
            if(player.Cargo[resource] > 0)
            {
                // Add money to player
                float sellPrice = player.CurrentIsland.GetComponent<TradingPost>().GetIslandPrice(resource, isBuying);
                AddMoney(sellPrice * amount);
                // Remove the resource from player
                player.RemoveResource(resource, amount);
                UIManager.instance.DisplayTradingUI();
            }
            else
            {
                Debug.Log("No Sale! Trying to sell a resource you do not have.");
            }
        }
    }

    public void UpgradeBoat(GameObject boat)
    {
        Vector3 position = player.transform.position;
        Quaternion rotation = player.transform.rotation;
        Dictionary<ResourceType, int> cargo = player.Cargo;

        GameObject newPlayerBoat = Instantiate(boat, position, rotation);
        newPlayerBoat.name = string.Format("Player {0}", newPlayerBoat.GetComponent<BoatStats>().BoatName);
        newPlayerBoat.tag = "Player";
        Destroy(newPlayerBoat.GetComponent<BoatMovement>());
        newPlayerBoat.AddComponent<PlayerMovement>();
        Boat newBoat = newPlayerBoat.GetComponent<Boat>();

        Destroy(player.gameObject);
        player = newBoat;

        player.AddCargo(cargo);
        player.EnterIsland();

        UIManager.instance.UpdatePlayerText();
    }
}
