using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton Code
    // A public reference to this script
    public static UIManager instance = null;

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
    private Canvas canvas;

    [SerializeField]    // UI Parents
    private GameObject mainMenuUIParent, controlsUIParent, sailingUIParent, tradingUIParent, pauseUIParent, gameEndUIParent;

    [SerializeField]    // Buttons
    private Button mainMenuToControlsButton, controlsToGameButton, pauseToMainMainButton, pauseToControlsButton, pauseToGameButton, gameEndToMainMenuButton;

    [SerializeField]    // Text 
    private TMP_Text healthText, moneyText, inventoryText;

    [SerializeField]    // Trading UI
    private GameObject resourceButtonsParent, resourceLinePrefab, shipUpgradeLinePrefab;
    [SerializeField]    // Trading Text
    private TMP_Text tradingHeader;
    [SerializeField]    // Trading Buttons
    private Button buyButton, sellButton;
    [SerializeField]    // Trading Button Images
    private Texture selectedButtonTexture, unselectedButtonTexture;

    // Start is called before the first frame update
    void Start()
    {
        SetupButtons();
        UpdatePlayerText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            switch(GameManager.instance.CurrentMenuState)
            {
                case MenuState.Sailing:
                    switch(PlayerManager.instance.CurrentPlayerState)
                    {
                        case PlayerState.Sailing:
                            GameManager.instance.ChangeMenuState(MenuState.Pause);
                            break;
                        case PlayerState.Trading:
                            PlayerManager.instance.ChangePlayerState(PlayerState.Sailing);
                            PlayerManager.instance.Player.ExitIsland();
                            break;
                    }
                    break;
                case MenuState.Pause:
                    GameManager.instance.ChangeMenuState(MenuState.Sailing);
                    break;
            }
        }
    }

    private void SetupButtons()
    {
        mainMenuToControlsButton.onClick.AddListener(() => GameManager.instance.ChangeMenuState(MenuState.Controls));
        controlsToGameButton.onClick.AddListener(() => GameManager.instance.ChangeMenuState(MenuState.Sailing));
        pauseToMainMainButton.onClick.AddListener(() => GameManager.instance.ChangeMenuState(MenuState.MainMenu));
        pauseToControlsButton.onClick.AddListener(() => GameManager.instance.ChangeMenuState(MenuState.Controls));
        pauseToGameButton.onClick.AddListener(() => GameManager.instance.ChangeMenuState(MenuState.Sailing));
        buyButton.onClick.AddListener(() => ChangeTradingState(true));
        sellButton.onClick.AddListener(() => ChangeTradingState(false));
        gameEndToMainMenuButton.onClick.AddListener(() => GameManager.instance.ChangeMenuState(MenuState.MainMenu));
    }

    public void ChangeUIState(MenuState menuState, PlayerState playerState)
    {
        switch(menuState)
        {
            case MenuState.MainMenu:
                ClearAllUI();
                mainMenuUIParent.SetActive(true);
                break;
            case MenuState.Controls:
                ClearAllUI();
                controlsUIParent.SetActive(true);
                break;
            case MenuState.Sailing:
                switch(playerState)
                {
                    case PlayerState.Sailing:
                        ClearAllUI();
                        sailingUIParent.SetActive(true);
                        break;
                    case PlayerState.Trading:
                        tradingUIParent.SetActive(true);
                        ChangeTradingState(true);
                        DisplayTradingUI();
                        break;
                }
                break;
            case MenuState.Pause:
                pauseUIParent.SetActive(true);
                break;
            case MenuState.GameEnd:
                ClearAllUI();
                gameEndUIParent.SetActive(true);
                break;
        }
    }

    private void ClearAllUI()
    {
        for(int i = 0; i < canvas.transform.childCount; i++)
            canvas.transform.GetChild(i).gameObject.SetActive(false);
    }

    public void UpdatePlayerText()
    {
        healthText.text = string.Format("Health: {0}", PlayerManager.instance.Player.Health);
        moneyText.text = string.Format("Gold: {0}", PlayerManager.instance.Money);
        inventoryText.text = string.Format(
            "Cargo: {0}/{1}\n", 
            PlayerManager.instance.Player.CargoCount(), 
            PlayerManager.instance.Player.Capacity);

        foreach(ResourceType resource in PlayerManager.instance.Player.Cargo.Keys)
        {
            if(PlayerManager.instance.Player.Cargo[resource] > 0)
                inventoryText.text += string.Format("\n{0}: {1}", resource, PlayerManager.instance.Player.Cargo[resource]);
        }
    }

    private void ChangeTradingState(bool isBuying)
    {
        if(isBuying != PlayerManager.instance.isBuying)
        {
            PlayerManager.instance.isBuying = isBuying;
            if(isBuying)
            {
                buyButton.GetComponent<RawImage>().texture = selectedButtonTexture;
                sellButton.GetComponent<RawImage>().texture = unselectedButtonTexture;
            }
            else
            {
                buyButton.GetComponent<RawImage>().texture = unselectedButtonTexture;
                sellButton.GetComponent<RawImage>().texture = selectedButtonTexture;
            }
            DisplayTradingUI();
        }
    }

    public void DisplayTradingUI()
    {
        Vector2 startingPos = new Vector2(0.0f, 25.0f);
        float yChange = -50.0f;

        // Clear resource buttons parent children
        for(int i = resourceButtonsParent.transform.childCount - 1; i >= 0; i--)
            Destroy(resourceButtonsParent.transform.GetChild(i).gameObject);

        int resourceCount = 0;


        if(PlayerManager.instance.Player.CurrentIsland.GetComponent<TradingPost>() != null)
        {
            tradingHeader.text = "Trading Offers";
            buyButton.gameObject.SetActive(true);
            sellButton.gameObject.SetActive(true);
            foreach(Resource resource in ResourceManager.instance.Resources.Values)
            {
                Vector2 position = startingPos;
                position.y += yChange * resourceCount;

                CreateResourceLine(resource, position);
                resourceCount++;
            }
        }
        else if(PlayerManager.instance.Player.CurrentIsland.GetComponent<Shipyard>() != null)
        {
            tradingHeader.text = "Upgrades";
            buyButton.gameObject.SetActive(false);
            sellButton.gameObject.SetActive(false);
            foreach(GameObject boat in ResourceManager.instance.Boats)
            {
                Vector2 position = startingPos;
                position.y += yChange * resourceCount;

                CreateShipUpgradeLine(boat, position);
                resourceCount++;
            }
        }
    }

    private void CreateResourceLine(Resource resource, Vector2 position)
    {
        GameObject resourceLineUI = Instantiate(resourceLinePrefab, Vector2.zero, Quaternion.identity, resourceButtonsParent.transform);
        resourceLineUI.transform.localPosition = position;
        resourceLineUI.name = resource.Name + "ResourceLine";

        resourceLineUI.transform.GetComponentInChildren<RawImage>().texture = resource.Image;
        resourceLineUI.transform.GetChild(1).GetComponent<TMP_Text>().text = resource.Name;

        // Only display quantity when selling
        string quantity = "";
        if(!PlayerManager.instance.isBuying)
            quantity = PlayerManager.instance.Player.Cargo[resource.Type].ToString();
        resourceLineUI.transform.GetChild(2).GetComponent<TMP_Text>().text = quantity;

        resourceLineUI.transform.GetChild(3).GetComponent<TMP_Text>().text =
            PlayerManager.instance.Player.CurrentIsland.GetComponent<TradingPost>().GetIslandPrice(
                resource.Type, PlayerManager.instance.isBuying) + "g";
        resourceLineUI.transform.GetComponentInChildren<Button>().onClick.AddListener(() => PlayerManager.instance.ResourceAction(resource.Type, 1));
    }

    public void CreateShipUpgradeLine(GameObject boat, Vector2 position)
    {
        GameObject shipUpgradeLineUI = Instantiate(shipUpgradeLinePrefab, Vector2.zero, Quaternion.identity, resourceButtonsParent.transform);
        shipUpgradeLineUI.transform.localPosition = position;

        BoatStats stats = boat.GetComponent<BoatStats>();

        shipUpgradeLineUI.transform.GetComponentInChildren<RawImage>().texture = stats.Image;
        shipUpgradeLineUI.transform.GetChild(1).GetComponent<TMP_Text>().text = stats.BoatName;
        shipUpgradeLineUI.transform.GetChild(2).GetComponent<TMP_Text>().text = string.Format("{0}g", stats.PriceToUpgrade);

        shipUpgradeLineUI.transform.GetComponentInChildren<Button>().onClick.AddListener(() => PlayerManager.instance.UpgradeBoat(boat));
    }
}
