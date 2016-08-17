using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Puntos : MonoBehaviour {

    public GameObject nivelCompletado;
    public GameObject juegoCompletado;
    public SiguienteNivel siguienteNivel;
    public Pelota pelota;
    public Barra barra;
    public Transform bloques;

    public static int puntos = 0;
    public Text textScore;

    // Use this for initialization
    void Start ()
    {
        UpdateScoreText();
    }

    public void AddScore (int points)
    {
        Puntos.puntos += points;
        UpdateScoreText();

        if(bloques.childCount == 0)
        {
            pelota.StopMovement();
            barra.enabled = false;

            if (siguienteNivel.IsLastLevel())
                juegoCompletado.SetActive(true);
            else
                nivelCompletado.SetActive(true);

            siguienteNivel.ActiveLoad();
        }
    }

    private void UpdateScoreText ()
    {
        textScore.text = string.Format("Puntos: {0}", Puntos.puntos);
    }
}
