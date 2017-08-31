using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml.Linq;
using SFB;

public class MenuController : MonoBehaviour
{

    #region Fields

    private string configurationPath;
    private string defaultConfigurationPath;
    private bool saveConfiguration = false;

    public Canvas MainMenu;
    public Canvas GameMenu;
    public Canvas OptionsMenu;

    public Toggle DefaultConfiguration;

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
        configurationPath = Path.Combine(Application.streamingAssetsPath, @"configuration.config");
        defaultConfigurationPath = Path.Combine(Application.streamingAssetsPath, @"configuration_default.config");

        ChangeToMainMenu();
        LoadDefaultConfiguration();
    }

    private void LoadDefaultConfiguration()
    {
        GameConfiguration.gameConfiguration.StrategyLocationDefensesDllPath = Path.Combine(Application.streamingAssetsPath, @"StrategyLocationDefenses.dll");
        GameConfiguration.gameConfiguration.StrategySelectionDefensesDllPath = Path.Combine(Application.streamingAssetsPath, @"StrategySelectionDefenses.dll");
        GameConfiguration.gameConfiguration.StrategyAttackAliensDllPath = Path.Combine(Application.streamingAssetsPath, @"StrategyAlienAttack.dll");
        GameConfiguration.gameConfiguration.StrategyDefenderAliensDllPath = Path.Combine(Application.streamingAssetsPath, @"StrategyAliensDefender.dll");

        bool configurationExists = File.Exists(configurationPath);
        string fileName = configurationExists ?
            configurationPath : defaultConfigurationPath;

        LoadConfiguration(fileName, configurationExists);        
    }

    private void LoadConfiguration(string fileName, bool configurationExists)
    {
        XDocument xmlDoc = XDocument.Load(fileName);

        GameConfiguration.gameConfiguration.MapSize = Convert.ToInt32(xmlDoc.Root.Element("MapSize").Value);
        GameConfiguration.gameConfiguration.CellMapSize = Convert.ToInt32(xmlDoc.Root.Element("CellMapSize").Value);
        GameConfiguration.gameConfiguration.NumAliensPerSecond = Convert.ToInt32(xmlDoc.Root.Element("NumAliensPerSecond").Value);
        GameConfiguration.gameConfiguration.MaxDurationBattleInSeconds = Convert.ToInt32(xmlDoc.Root.Element("MaxDurationBattleInSeconds").Value);
        GameConfiguration.gameConfiguration.Velocity = (GameConfiguration.GameVelocity)Enum.Parse(typeof(GameConfiguration.GameVelocity),
            xmlDoc.Root.Element("Velocity").Value);
        GameConfiguration.gameConfiguration.DensityObstacles = float.Parse(xmlDoc.Root.Element("DensityObstacles").Value);
        GameConfiguration.gameConfiguration.DensityDefenses = float.Parse(xmlDoc.Root.Element("DensityDefenses").Value);

        UpdateOptionsPanel();
        
        DefaultConfiguration.isOn = configurationExists;
    }

    private void UpdateOptionsPanel()
    {
        UpdateMapSize();
        UpdateCellMapSize();
        UpdateNumAliensPerSecond();
        UpdateMaxDurationBattle();
        UpdateVelocity();
        UpdateDensityObstacles();
        UpdateDensityDefenses();
    }

    private void UpdateDensityDefenses()
    {
        DefensesDensitySlider.value = GameConfiguration.gameConfiguration.DensityDefenses * 100;
    }

    private void UpdateDensityObstacles()
    {
        ObstaclesDensitySlider.value = GameConfiguration.gameConfiguration.DensityObstacles * 100;
    }

    private void UpdateVelocity()
    {
        switch(GameConfiguration.gameConfiguration.Velocity)
        {
            case GameConfiguration.GameVelocity.Slow: GameVelocityDropdown.value = 0; break;
            case GameConfiguration.GameVelocity.Normal: GameVelocityDropdown.value = 1; break;
            case GameConfiguration.GameVelocity.Fast: GameVelocityDropdown.value = 2; break;
            case GameConfiguration.GameVelocity.VeryFast: GameVelocityDropdown.value = 3; break;
        }
    }

    private void UpdateMaxDurationBattle()
    {
        MaxDurationBattleSlider.value = GameConfiguration.gameConfiguration.MaxDurationBattleInSeconds / 60;
    }

    private void UpdateNumAliensPerSecond()
    {
        NumAliensPerSecondSlider.value = GameConfiguration.gameConfiguration.NumAliensPerSecond;
    }

    private void UpdateCellMapSize()
    {
        switch (GameConfiguration.gameConfiguration.CellMapSize)
        {
            case 10: CellMapSizeDropdown.value = 0; break;
            case 20: CellMapSizeDropdown.value = 1; break;
            case 30: CellMapSizeDropdown.value = 2; break;
            case 40: CellMapSizeDropdown.value = 3; break;
            case 50: CellMapSizeDropdown.value = 4; break;
            case 60: CellMapSizeDropdown.value = 5; break;
            case 70: CellMapSizeDropdown.value = 6; break;
            case 80: CellMapSizeDropdown.value = 7; break;
            case 90: CellMapSizeDropdown.value = 8; break;
            case 100: CellMapSizeDropdown.value = 9; break;
        }
    }

    private void UpdateMapSize()
    {
        switch(GameConfiguration.gameConfiguration.MapSize)
        {
            case 100: MapSizeDropdown.value = 0; break;
            case 200: MapSizeDropdown.value = 1; break;
            case 300: MapSizeDropdown.value = 2; break;
            case 400: MapSizeDropdown.value = 3; break;
            case 500: MapSizeDropdown.value = 4; break;
            case 600: MapSizeDropdown.value = 5; break;
            case 700: MapSizeDropdown.value = 6; break;
            case 800: MapSizeDropdown.value = 7; break;
            case 900: MapSizeDropdown.value = 8; break;
            case 1000: MapSizeDropdown.value = 9; break;
        }
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
        UpdateGameConfiguration();

        if (saveConfiguration)
            SaveConfiguration(configurationPath);

        if (!string.IsNullOrEmpty(pathStrategyLocationDll))
            GameConfiguration.gameConfiguration.StrategyLocationDefensesDllPath = pathStrategyLocationDll;

        if (!string.IsNullOrEmpty(pathStrategySelectionDll))
            GameConfiguration.gameConfiguration.StrategySelectionDefensesDllPath = pathStrategySelectionDll;

        if (!string.IsNullOrEmpty(pathStrategyAttackDll))
            GameConfiguration.gameConfiguration.StrategyAttackAliensDllPath = pathStrategyAttackDll;

        if (!string.IsNullOrEmpty(pathStrategyDefenderDll))
            GameConfiguration.gameConfiguration.StrategyDefenderAliensDllPath = pathStrategyDefenderDll;

        SceneManager.LoadScene("GameScene");
    }

    private void UpdateGameConfiguration()
    {
        ChangeMapSize();
        ChangeCellMapSize();
        ChangeNumAliensPerSecond();
        ChangeMaxDurationBattle();
        ChangeGameVelocity();
        ChangeObstaclesDensity();
        ChangeDefensesDensity();
    }

    private void SaveConfiguration(string path)
    {
        XDocument xmlDoc = new XDocument();
        xmlDoc.Add(new XElement("Configuration", 
                       new XElement("MapSize", GameConfiguration.gameConfiguration.MapSize),
                       new XElement("CellMapSize", GameConfiguration.gameConfiguration.CellMapSize),
                       new XElement("NumAliensPerSecond", GameConfiguration.gameConfiguration.NumAliensPerSecond),
                       new XElement("MaxDurationBattleInSeconds", GameConfiguration.gameConfiguration.MaxDurationBattleInSeconds),
                       new XElement("Velocity", GameConfiguration.gameConfiguration.Velocity),
                       new XElement("DensityObstacles", GameConfiguration.gameConfiguration.DensityObstacles),
                       new XElement("DensityDefenses", GameConfiguration.gameConfiguration.DensityDefenses)));

        xmlDoc.Save(path);
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

        //string newPath = EditorUtility.OpenFilePanel("Select Dll", initialPath, "dll");
        var extensions = new[] { new ExtensionFilter("Dll Files", "dll")};
   

        IStandaloneFileBrowser standaloneFileBrowser = new StandaloneFileBrowserWindows();
        string[] newPath = standaloneFileBrowser.OpenFilePanel("Select Dll", initialPath, extensions, false);

        if (newPath.Length == 0)
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
        path = newPath[0];
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
        GameConfiguration.gameConfiguration.DensityDefenses = DefensesDensitySlider.value / 100;
    }

    public void ChangeObstaclesDensity()
    {
        ObstaclesDensityText.text = string.Format("{0} %", (int)ObstaclesDensitySlider.value);
        GameConfiguration.gameConfiguration.DensityObstacles = ObstaclesDensitySlider.value / 100;
    }

    public void ChangeMaxDurationBattle()
    {
        MaxDurationBattleText.text = string.Format("{0} minutes", (int)MaxDurationBattleSlider.value);
        GameConfiguration.gameConfiguration.MaxDurationBattleInSeconds = (int)MaxDurationBattleSlider.value * 60;
    }

    public void ChangeNumAliensPerSecond()
    {
        NumAliensPerSecondText.text = string.Format("{0} aliens", (int)NumAliensPerSecondSlider.value);
        GameConfiguration.gameConfiguration.NumAliensPerSecond = NumAliensPerSecondSlider.value;
    }

    public void ChangeGameVelocity()
    {
        switch(GameVelocityDropdown.value)
        {
            case 0: GameConfiguration.gameConfiguration.Velocity = GameConfiguration.GameVelocity.Slow; break;
            case 1: GameConfiguration.gameConfiguration.Velocity = GameConfiguration.GameVelocity.Normal; break;
            case 2: GameConfiguration.gameConfiguration.Velocity = GameConfiguration.GameVelocity.Fast; break;
            case 3: GameConfiguration.gameConfiguration.Velocity = GameConfiguration.GameVelocity.VeryFast; break;
        }
    }

    public void ChangeMapSize()
    {
        switch(MapSizeDropdown.value)
        {
            case 0: GameConfiguration.gameConfiguration.MapSize = 100; break;
            case 1: GameConfiguration.gameConfiguration.MapSize = 200; break;
            case 2: GameConfiguration.gameConfiguration.MapSize = 300; break;
            case 3: GameConfiguration.gameConfiguration.MapSize = 400; break;
            case 4: GameConfiguration.gameConfiguration.MapSize = 500; break;
            case 5: GameConfiguration.gameConfiguration.MapSize = 600; break;
            case 6: GameConfiguration.gameConfiguration.MapSize = 700; break;
            case 7: GameConfiguration.gameConfiguration.MapSize = 800; break;
            case 8: GameConfiguration.gameConfiguration.MapSize = 900; break;
            case 9: GameConfiguration.gameConfiguration.MapSize = 1000; break;
        }
    }

    public void ChangeCellMapSize()
    {
        switch (CellMapSizeDropdown.value)
        {
            case 0: GameConfiguration.gameConfiguration.CellMapSize = 10; break;
            case 1: GameConfiguration.gameConfiguration.CellMapSize = 20; break;
            case 2: GameConfiguration.gameConfiguration.CellMapSize = 30; break;
            case 3: GameConfiguration.gameConfiguration.CellMapSize = 40; break;
            case 4: GameConfiguration.gameConfiguration.CellMapSize = 50; break;
            case 5: GameConfiguration.gameConfiguration.CellMapSize = 60; break;
            case 6: GameConfiguration.gameConfiguration.CellMapSize = 70; break;
            case 7: GameConfiguration.gameConfiguration.CellMapSize = 80; break;
            case 8: GameConfiguration.gameConfiguration.CellMapSize = 90; break;
            case 9: GameConfiguration.gameConfiguration.CellMapSize = 100; break;
        }
    }

    public void ChangeDefaultConfiguration()
    {
        if (DefaultConfiguration.isOn)
            saveConfiguration = true;
    }

    public void ImportConfiguration()
    {
        try
        {
            string initialPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var extensions = new[] { new ExtensionFilter("Config Files", "config") };


            IStandaloneFileBrowser standaloneFileBrowser = new StandaloneFileBrowserWindows();
            string[] newPath = standaloneFileBrowser.OpenFilePanel("Select Dll", initialPath, extensions, false);

            if (newPath.Length == 0)
                return;

            LoadConfiguration(newPath[0], false);
        }
        catch(Exception ex)
        {
            Debug.LogError("The file isn't a valid configuration file.");
            Debug.LogError(ex);
        }
    }

    public void ExportConfiguration()
    {
        try
        {
            string initialPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var extensions = new[] { new ExtensionFilter("Config Files", "config") };


            IStandaloneFileBrowser standaloneFileBrowser = new StandaloneFileBrowserWindows();
            string newPath = standaloneFileBrowser.SaveFilePanel("Select Dll", initialPath, "configuration.config", extensions);

            if (newPath == string.Empty)
                return;

            SaveConfiguration(newPath);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error saving the configuration");
            Debug.LogError(ex);
        }

    }

    #endregion
}
