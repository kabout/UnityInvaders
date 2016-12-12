using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Canvas MainMenu;
    public Canvas TeamMenu;
    public Canvas DllMenu;

    public void Start()
    {
        ChangeToMainMenu();
    }

    public void ChangeToTeamMenu()
    {
        MainMenu.gameObject.SetActive(false);
        DllMenu.gameObject.SetActive(false);
        TeamMenu.gameObject.SetActive(true);
    }

    public void ChangeToMainMenu()
    {
        TeamMenu.gameObject.SetActive(false);
        DllMenu.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
    }

    public void ChangeToDllMenu(string type)
    {
        switch(type)
        {
            case "StrategyDefenseLocation": break;
            case "StrategyDefenseSelection": break;
            case "StrategyAlienAttack": break;
        }

        TeamMenu.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(false);
        DllMenu.gameObject.SetActive(true);
    }

    public void SelectDll()
    {
        string path = EditorUtility.OpenFilePanel("Select Dll", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "dll");
        DllMenu.GetComponentInChildren<InputField>().text = path;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
