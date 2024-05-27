using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        SetupButtons();
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
                break;
            case MenuState.Pause:
                pauseUIParent.SetActive(true);
                break;
            case MenuState.GameEnd:
                gameEndUIParent.SetActive(true);
                break;
        }
    }
}