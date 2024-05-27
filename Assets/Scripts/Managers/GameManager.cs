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
    private GameObject player;
    public GameObject Player { get { return player; } }
    [SerializeField]
    private GameObject islandsParent;
    public GameObject IslandsParent { get { return islandsParent; } }

    [SerializeField]
    private MenuState currentMenuState;
    public MenuState CurrentMenuState { get { return currentMenuState; } }

    // Start is called before the first frame update
    void Start()
    {
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

        currentMenuState = newMenuState;
        // Update UI
        UIManager.instance.ChangeUIState(newMenuState);
    }
}
