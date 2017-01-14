using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    #region Fields

    public Canvas MainMenu;
    public Canvas GameMenu;

    public GameConfiguration gameConfiguration;

    public Toggle CbStrategyLocation;
    public Button BtStrategyLocation;
    private string pathStrategyLocationDll;

    public Toggle CbStrategySelection;
    public Button BtStrategySelection;
    private string pathStrategySelectionDll;

    public Toggle CbStrategyAttack;
    public Button BtStrategyAttack;
    private string pathStrategyAttackDll;

    public Toggle CbStrategyDefender;
    public Button BtStrategyDefender;
    private string pathStrategyDefenderDll;

    #endregion

    public void Start()
    {
        ChangeToMainMenu();
    }

    #region Methods of MainMenu

    public void ChangeToGameMenu(string type)
    {
        MainMenu.gameObject.SetActive(false);
        GameMenu.gameObject.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    #endregion

    #region Methods of GameMenu

    public void StartGame()
    {
        if (!string.IsNullOrEmpty(pathStrategyLocationDll))
            gameConfiguration.StrategyLocationDefensesDllPath = pathStrategyLocationDll;

        if (!string.IsNullOrEmpty(pathStrategySelectionDll))
            gameConfiguration.StrategySelectionDefensesDllPath = pathStrategySelectionDll;

        if (!string.IsNullOrEmpty(pathStrategyAttackDll))
            gameConfiguration.StrategyAttackAliensDllPath = pathStrategyAttackDll;

        if (!string.IsNullOrEmpty(pathStrategyDefenderDll))
            gameConfiguration.StrategyDefenderAliensDllPath = pathStrategyDefenderDll;

        SceneManager.LoadScene("GameScene");
    }

    public void ChangeToMainMenu()
    {
        GameMenu.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
    }

    public void SelectStrategyLocationDll()
    {
        SelectStrategyDll(BtStrategyLocation, ref pathStrategyLocationDll);
    }

    public void SelectStrategySelectionDll()
    {
        SelectStrategyDll(BtStrategySelection, ref pathStrategySelectionDll);
    }

    public void SelectStrategyAttackDll()
    {
        SelectStrategyDll(BtStrategyAttack, ref pathStrategyAttackDll);
    }

    public void SelectStrategyDefenderDll()
    {
        SelectStrategyDll(BtStrategyDefender, ref pathStrategyDefenderDll);
    }

    private void SelectStrategyDll(Button button, ref string path)
    {
        string initialPath = string.IsNullOrEmpty(path) ?
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) :
            path;

        string newPath = EditorUtility.OpenFilePanel("Select Dll", initialPath, "dll");

        if (newPath == string.Empty)
            return;

        button.GetComponentInChildren<Text>().text = "Change Dll";
        path = newPath;
    }

    public void AutomaticStrategyLocation()
    {
        AutomaticStrategy(BtStrategyLocation, CbStrategyLocation, ref pathStrategyLocationDll);
    }

    public void AutomaticStrategySelection()
    {
        AutomaticStrategy(BtStrategySelection, CbStrategySelection, ref pathStrategySelectionDll);
    }

    public void AutomaticStrategyAttack()
    {
        AutomaticStrategy(BtStrategyAttack, CbStrategyAttack, ref pathStrategyAttackDll);
    }

    public void AutomaticStrategyDefender()
    {
        AutomaticStrategy(BtStrategyDefender, CbStrategyDefender, ref pathStrategyDefenderDll);
    }

    private void AutomaticStrategy(Button button, Toggle toggle, ref string path)
    {
        button.interactable = !toggle.isOn;

        if (toggle.isOn)
        {
            button.GetComponentInChildren<Text>().text = "Select Dll";
            path = string.Empty;
        }
    }

    #endregion
}
