using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using UnityEditor;

public class MenuScript : MonoBehaviour
{
    //[DllImport("user32.dll")]
    //private static extern void SaveFileDialog(); 

    #region Fields

    public Canvas MainMenu;
    public Canvas GameMenu;
    public Canvas OptionsMenu;

    public GameConfiguration gameConfiguration;

    public Dropdown MapSizeDropdown;
    public Dropdown CellMapSizeDropdown;

    public Slider MaxDurationBattleSlider;
    public Text MaxDurationBattleText;

    public Slider DefensesDensitySlider;
    public Text DefensesDensityText;

    public Slider ObstaclesDensitySlider;
    public Text ObstaclesDensityText;

    public Dropdown GameVelocityDropdown;

    public Slider NumAliensPerSecondSlider;
    public Text NumAliensPerSecondText;


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

    public void ChangeToGameMenu()
    {
        MainMenu.gameObject.SetActive(false);
        OptionsMenu.gameObject.SetActive(false);
        GameMenu.gameObject.SetActive(true);
    }

    public void ChangeToOptionsMenu()
    {
        GameMenu.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(false);
        OptionsMenu.gameObject.SetActive(true);
    }

    public void ChangeToMainMenu()
    {
        GameMenu.gameObject.SetActive(false);
        OptionsMenu.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
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

        //System.Windows.Forms.SaveFileDialog ofd = new System.Windows.Forms.SaveFileDialog();
        //ofd.InitialDirectory = initialPath;
        //ofd.Title = "Select Dll";
        //ofd.DefaultExt = "dll";

        //string newPath =  string.Empty;

        //if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
        //    return;

        //newPath = ofd.FileName;

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

    public void ChangeDefensesDensity()
    {
        DefensesDensityText.text = string.Format("{0} %", (int)DefensesDensitySlider.value);
        gameConfiguration.DensityDefenses = (int)DefensesDensitySlider.value / 100;
    }

    public void ChangeObstaclesDensity()
    {
        ObstaclesDensityText.text = string.Format("{0} %", (int)ObstaclesDensitySlider.value);
        gameConfiguration.DensityObstacles = (int)ObstaclesDensitySlider.value / 100;
    }

    public void ChangeMaxDurationBattle()
    {
        MaxDurationBattleText.text = string.Format("{0} minutes", (int)MaxDurationBattleSlider.value);
        gameConfiguration.MaxDurationBattleInSeconds = (int)MaxDurationBattleSlider.value * 60;
    }

    public void ChangeNumAliensPerSecond()
    {
        NumAliensPerSecondText.text = string.Format("{0} aliens", (int)NumAliensPerSecondSlider.value);
        gameConfiguration.NumAliensPerSecond = NumAliensPerSecondSlider.value;
    }

    public void ChangeGameVelocity()
    {
        switch(GameVelocityDropdown.value)
        {
            case 0: gameConfiguration.Velocity = GameConfiguration.GameVelocity.Slow; break;
            case 1: gameConfiguration.Velocity = GameConfiguration.GameVelocity.Normal; break;
            case 2: gameConfiguration.Velocity = GameConfiguration.GameVelocity.Fast; break;
            case 3: gameConfiguration.Velocity = GameConfiguration.GameVelocity.VeryFast; break;
        }
    }

    public void ChangeMapSize()
    {
        switch(MapSizeDropdown.value)
        {
            case 0: gameConfiguration.MapSize = 100; break;
            case 1: gameConfiguration.MapSize = 200; break;
            case 2: gameConfiguration.MapSize = 300; break;
            case 3: gameConfiguration.MapSize = 400; break;
            case 4: gameConfiguration.MapSize = 500; break;
            case 5: gameConfiguration.MapSize = 600; break;
            case 6: gameConfiguration.MapSize = 700; break;
            case 7: gameConfiguration.MapSize = 800; break;
            case 8: gameConfiguration.MapSize = 900; break;
            case 9: gameConfiguration.MapSize = 1000; break;
        }
    }

    public void ChangeCellMapSize()
    {
        switch (CellMapSizeDropdown.value)
        {
            case 0: gameConfiguration.CellMapSize = 10; break;
            case 1: gameConfiguration.CellMapSize = 20; break;
            case 2: gameConfiguration.CellMapSize = 30; break;
            case 3: gameConfiguration.CellMapSize = 40; break;
            case 4: gameConfiguration.CellMapSize = 50; break;
            case 5: gameConfiguration.CellMapSize = 60; break;
            case 6: gameConfiguration.CellMapSize = 70; break;
            case 7: gameConfiguration.CellMapSize = 80; break;
            case 8: gameConfiguration.CellMapSize = 90; break;
            case 9: gameConfiguration.CellMapSize = 100; break;
        }
    }

    #endregion
}
