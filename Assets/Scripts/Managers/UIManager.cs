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
    private Button mainMenuToControlsButton, controlsToGameButton, pauseToMainMainButton, pauseToControlsButton, pauseToGameButton;

    [SerializeField]    // Text
    private TMP_Text healthText, moneyText, inventoryText;

    [SerializeField]    // Trading UI
    private GameObject resourceButtonsParent, resourceLinePrefab;
    [SerializeField]    // Trading Buttons
    private Button buyButton, sellButton;
    [SerializeField]    // Trading Button Images
    private Texture selectedButtonTexture, unselectedButtonTexture;

    // Start is called before the first frame update
    void Start()
    {
        SetupButtons();
        UpdateGameText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            switch(GameManager.instance.CurrentMenuState)
            {
                case MenuState.Sailing:
                    GameManager.instance.ChangeMenuState(MenuState.Pause);
                    break;
                case MenuState.Trading:
                    GameManager.instance.ChangeMenuState(MenuState.Sailing);
                    GameManager.instance.Player.GetComponent<Boat>().ExitIsland();
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
    }

    public void ChangeUIState(MenuState menuState)
    {
        for(int i = 0; i < canvas.transform.childCount; i++)
            canvas.transform.GetChild(i).gameObject.SetActive(false);

        switch(menuState)
        {
            case MenuState.MainMenu:
                mainMenuUIParent.SetActive(true);
                break;
            case MenuState.Controls:
                controlsUIParent.SetActive(true);
                break;
            case MenuState.Sailing:
                sailingUIParent.SetActive(true);
                break;
            case MenuState.Trading:
                tradingUIParent.SetActive(true);
                ChangeTradingState(true);
                CreateLinesOfResources();
                break;
            case MenuState.Pause:
                pauseUIParent.SetActive(true);
                break;
            case MenuState.GameEnd:
                gameEndUIParent.SetActive(true);
                break;
        }
    }

    public void UpdateGameText()
    {
        Boat boat = GameManager.instance.Player.GetComponent<Boat>();

        healthText.text = "Health: " + boat.Health;

        moneyText.text = "Gold: " + boat.Money;

        inventoryText.text = "";
        foreach(ResourceType resource in boat.Cargo.Keys)
        {
            if(boat.Cargo[resource] > 0)
                inventoryText.text += resource + ": " + boat.Cargo[resource] + "\n";
        }
    }

    public void CreateLinesOfResources()
    {
        Vector2 startingPos = new Vector2(0.0f, 25.0f);
        float yChange = -50.0f;

        // Clear resource buttons parent children
        for(int i = resourceButtonsParent.transform.childCount - 1; i >= 0; i--)
            Destroy(resourceButtonsParent.transform.GetChild(i).gameObject);

        int resourceCount = 0;
        foreach(Resource resource in ResourceManager.instance.Resources.Values)
        {
            Vector2 position = startingPos;
            position.y += yChange * resourceCount;

            CreateResourceLine(resource, position, resourceCount);
            resourceCount++;
        }
    }

    private void CreateResourceLine(Resource resource, Vector2 position, int count)
    {
        GameObject resourceLineUI = Instantiate(resourceLinePrefab, Vector2.zero, Quaternion.identity, resourceButtonsParent.transform);
        resourceLineUI.transform.localPosition = position;
        resourceLineUI.name = resource.Name + "ResourceLine";

        resourceLineUI.transform.GetComponentInChildren<RawImage>().texture = resource.Image;
        resourceLineUI.transform.GetChild(1).GetComponent<TMP_Text>().text = resource.Name;
        resourceLineUI.transform.GetChild(2).GetComponent<TMP_Text>().text = GameManager.instance.Player.GetComponent<Boat>().Cargo[resource.Type].ToString();
        resourceLineUI.transform.GetChild(3).GetComponent<TMP_Text>().text =
            GameManager.instance.Player.GetComponent<Boat>().CurrentIsland.GetComponent<Island>().GetIslandPrice(
                resource.Type, GameManager.instance.isBuying) + "g";
        resourceLineUI.transform.GetComponentInChildren<Button>().onClick.AddListener(() => GameManager.instance.ResourceAction(resource.Type));
    }

    private void ChangeTradingState(bool isBuying)
    {
        if(isBuying != GameManager.instance.isBuying)
        {
            GameManager.instance.isBuying = isBuying;
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
            CreateLinesOfResources();
        }
    }
}
