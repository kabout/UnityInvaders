using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class GameConfiguration : MonoBehaviour
{
    public enum GameVelocity
    {
        Pause = 0,
        Slow = 1,
        Normal = 2,
        Fast = 3,
        VeryFast = 4
    }

    public static GameConfiguration gameConfiguration;

    void Awake()
    {
        if (gameConfiguration == null)
        {
            gameConfiguration = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (gameConfiguration != this)
            Destroy(gameObject);
    }

    public string StrategyLocationDefensesDllPath { get; set; }
    public string StrategySelectionDefensesDllPath { get; set; }
    public string StrategyAttackAliensDllPath { get; set; }
    public string StrategyDefenderAliensDllPath { get; set; }

    public int MapSize { get; set; }
    public int CellMapSize { get; set; }
    public float NumAliensPerSecond { get; set; }
    public int MaxDurationBattleInSeconds { get; set; }
    public GameVelocity Velocity { get; set; }
    public float DensityObstacles { get; set; }
    public float DensityDefenses { get; set; }
}
