using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int NumRoundsToWin = 5;        
    public float StartDelay = 3f;         
    public float EndDelay = 3f;           
    public CameraControl CameraControl;   
    public Text MessageText;              
    public GameObject TankPrefab;         
    public TankManager[] Tanks;           


    private int RoundNumber;          
    // Tiempo de espera inicial y tiempo de espera final    
    private WaitForSeconds StartWait;     
    private WaitForSeconds EndWait;  
    /// Se referencia al tanque ganador de la ronda     
    private TankManager RoundWinner;
    /// Se referencia al tanque ganador del juego 
    private TankManager GameWinner;       


    private void Start()
    {
        StartWait = new WaitForSeconds(StartDelay);
        EndWait = new WaitForSeconds(EndDelay);

        SpawnAllTanks();
        SetCameraTargets();

        StartCoroutine(GameLoop());
    }


    private void SpawnAllTanks()
    {
        for (int i = 0; i < Tanks.Length; i++)
        {
            Tanks[i].Instance =
                Instantiate(TankPrefab, Tanks[i].SpawnPoint.position, Tanks[i].SpawnPoint.rotation) as GameObject;
            Tanks[i].PlayerNumber = i + 1;
            Tanks[i].Setup();
        }
    }


    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[Tanks.Length];

        for (int i = 0; i < targets.Length; i++)
            targets[i] = Tanks[i].Instance.transform;

        CameraControl.Targets = targets;
    }


    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (GameWinner != null)
            SceneManager.LoadScene(0);
        else
            StartCoroutine(GameLoop());
    }


    private IEnumerator RoundStarting()
    {
        ResetAllTanks();
        DisableTankControl();

        CameraControl.SetStartPositionAndSize();
        RoundNumber++;
        MessageText.text = string.Format("ROUND {0}", RoundNumber);

        yield return StartWait;
    }


    private IEnumerator RoundPlaying()
    {
        EnableTankControl();
        MessageText.text = string.Empty;

        while(!OneTankLeft())
        {
            yield return null;
        }
    }


    private IEnumerator RoundEnding()
    {
        DisableTankControl();

        RoundWinner = GetRoundWinner();

        if(RoundWinner != null)
        {
            RoundWinner.Wins++;

            GameWinner = GetGameWinner();

            MessageText.text = EndMessage();
        }

        yield return EndWait;
    }


    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < Tanks.Length; i++)
            if (Tanks[i].Instance.activeSelf)
                numTanksLeft++;

        return numTanksLeft <= 1;
    }


    private TankManager GetRoundWinner()
    {
        for (int i = 0; i < Tanks.Length; i++)
        {
            if (Tanks[i].Instance.activeSelf)
                return Tanks[i];
        }

        return null;
    }


    private TankManager GetGameWinner()
    {
        for (int i = 0; i < Tanks.Length; i++)
        {
            if (Tanks[i].Wins == NumRoundsToWin)
                return Tanks[i];
        }

        return null;
    }


    private string EndMessage()
    {
        string message = "DRAW!";

        if (RoundWinner != null)
            message = RoundWinner.ColoredPlayerText + " WINS THE ROUND!";

        message += "\n\n\n\n";

        for (int i = 0; i < Tanks.Length; i++)
            message += Tanks[i].ColoredPlayerText + ": " + Tanks[i].Wins + " WINS\n";

        if (GameWinner != null)
            message = GameWinner.ColoredPlayerText + " WINS THE GAME!";

        return message;
    }


    private void ResetAllTanks()
    {
        for (int i = 0; i < Tanks.Length; i++)
            Tanks[i].Reset();
    }


    private void EnableTankControl()
    {
        for (int i = 0; i < Tanks.Length; i++)
            Tanks[i].EnableControl();
    }


    private void DisableTankControl()
    {
        for (int i = 0; i < Tanks.Length; i++)
            Tanks[i].DisableControl();
    }
}