using UnityEngine;
using System.Collections;
using System.IO;

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

        StrategyLocationDefensesDllPath = Path.Combine(Application.streamingAssetsPath, @"StrategyLocationDefenses.dll");
        StrategySelectionDefensesDllPath = Path.Combine(Application.streamingAssetsPath, @"StrategySelectionDefenses.dll");
        StrategyAttackAliensDllPath = Path.Combine(Application.streamingAssetsPath, @"StrategyAlienAttack.dll");
        StrategyDefenderAliensDllPath = Path.Combine(Application.streamingAssetsPath, @"StrategyAliensDefender.dll");

        MapSize = 300;
        CellMapSize = 10;
        NumAliensPerSecond = 2f;
        // 5 minutos
        MaxDurationBattleInSeconds = 300;
        Velocity = GameVelocity.Normal;
        DensityDefenses = 0.2f;
        DensityObstacles = 0.2f;
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
