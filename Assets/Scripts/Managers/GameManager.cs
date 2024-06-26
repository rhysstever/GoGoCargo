using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuState
{
    MainMenu,
    Controls,
    Sailing,
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
    private GameObject islandsParent;
    public GameObject IslandsParent { get { return islandsParent; } }

    private Stack<MenuState> menus;
    [SerializeField]
    private MenuState currentMenuState;
    public MenuState CurrentMenuState { get { return currentMenuState; } }

    // Start is called before the first frame update
    void Start()
    {
        menus = new Stack<MenuState>();
        ChangeMenuState(MenuState.MainMenu);
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
                PlayerManager.instance.SetupPlayerManager();
                NPCManager.instance.SetupNPCs();
                break;
            case MenuState.Controls:
                break;
            case MenuState.Sailing:
                break;
            case MenuState.Pause:
                break;
            case MenuState.GameEnd:
                break;
        }

        menus.Push(newMenuState);
        currentMenuState = menus.Peek();
        // Update UI
        UIManager.instance.ChangeUIState(
            currentMenuState,
            PlayerManager.instance.CurrentPlayerState);
    }
}
