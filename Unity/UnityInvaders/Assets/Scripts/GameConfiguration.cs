using UnityEngine;
using System.Collections;

public class GameConfiguration : MonoBehaviour
{
    public enum GameVelocity
    {
        Pause = 0,
        Slow = 1,
        Normal = 3,
        Fast = 4
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

        StrategyLocationDefensesDllPath = Application.dataPath + @"/../Lib/StrategyLocationDefenses.dll";
        StrategySelectionDefensesDllPath = Application.dataPath + @"/../Lib/StrategySelectionDefenses.dll";
        StrategyAttackAliensDllPath = Application.dataPath + @"/../Lib/StrategyAlienAttack.dll";
        StrategyDefenderAliensDllPath = Application.dataPath + @"/../Lib/StrategyAliensDefender.dll";

        SizeMap = 300;
        CellMap = 10;
        NumUcos = int.MaxValue;
        NumUcosPerSecond = 2f;
        // 5 minutos
        MaxDurationBattleInSeconds = 300;
        NumObstacles = int.MaxValue;
        NumDefenses = int.MaxValue;
        Velocity = GameVelocity.Normal;
        DensityDefenses = 0.1f;
        DensityObstacles = 0.1f;
        NumAses = 1000;
    }

    public string StrategyLocationDefensesDllPath { get; set; }
    public string StrategySelectionDefensesDllPath { get; set; }
    public string StrategyAttackAliensDllPath { get; set; }
    public string StrategyDefenderAliensDllPath { get; set; }

    public int SizeMap { get; set; }
    public int CellMap { get; set; }
    public int NumUcos { get; set; }
    public float NumUcosPerSecond { get; set; }
    public int MaxDurationBattleInSeconds { get; set; }
    public int NumObstacles { get; set; }
    public int NumDefenses { get; set; }
    public GameVelocity Velocity { get; set; }
    public float DensityObstacles { get; set; }
    public float DensityDefenses { get; set; }
    public int NumAses { get; set; }
}
