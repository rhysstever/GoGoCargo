using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuState
{
    MainMenu,
    Controls,
    Sailing,
    Trading,
    Pause,
    GameEnd
}

public class GameManager : MonoBehaviour
{
    #region Singleton Code
    // A public reference to this script
    public static GameManager instance = null;

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
    private Boat player;
    public Boat Player { get { return player; } }
    [SerializeField]
    private GameObject islandsParent;
    public GameObject IslandsParent { get { return islandsParent; } }

    private Stack<MenuState> menus;
    [SerializeField]
    private MenuState currentMenuState;
    public MenuState CurrentMenuState { get { return currentMenuState; } }

    public bool isBuying;

    // Start is called before the first frame update
    void Start()
    {
        menus = new Stack<MenuState>();
        ChangeMenuState(MenuState.MainMenu);
        isBuying = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMenuState(MenuState newMenuState)
    {
        switch(newMenuState)
        {
            case MenuState.MainMenu:
                break;
            case MenuState.Controls:
                break;
            case MenuState.Sailing:
                break;
            case MenuState.Trading:
                break;
            case MenuState.Pause:
                break;
            case MenuState.GameEnd:
                break;
        }

        menus.Push(newMenuState);
        currentMenuState = menus.Peek();
        // Update UI
        UIManager.instance.ChangeUIState(currentMenuState);
    }

    public void ResourceAction(ResourceType resource, int amount)
    {
        // Check availability
        if(isBuying)
        {
            // BUYING GOODS FROM ISLAND
            // Check player money vs island price
            float buyPrice = player.CurrentIsland.GetComponent<TradingPost>().GetIslandPrice(resource, isBuying);
            if(player.Money >= buyPrice)
            {
                // Remove money from player
                player.RemoveMoney(buyPrice * amount);
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
            // SELLING GOODS TO ISLAND
            // Check player cargo availabilty
            if(player.Cargo[resource] > 0)
            {
                // Add money to player
                float sellPrice = player.CurrentIsland.GetComponent<TradingPost>().GetIslandPrice(resource, isBuying);
                player.AddMoney(sellPrice * amount);
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

    }
}
