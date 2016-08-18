using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Vidas : MonoBehaviour {

    public static int vidas = 3;
    public Text textlifes;
    public GameObject textGameOver;
    public Pelota pelota;
    public Barra barra;
    public SiguienteNivel siguienteNivel;

	// Use this for initialization
	void Start ()
    {
        UpdateLifeText();
    }

    public void LostLife()
    {
        if (Vidas.vidas <= 0)
            return;

        Vidas.vidas--;
        UpdateLifeText();

        if(Vidas.vidas == 0)
        { 
            textGameOver.SetActive(true);
            pelota.StopMovement();
            barra.enabled = false;

            siguienteNivel.LevelToLoad = "Portada";
            siguienteNivel.ActiveLoad();
        }
        else
        {
            barra.Reset();
            pelota.Reset();
        }
    }

    private void UpdateLifeText()
    {
        textlifes.text = string.Format("Vidas: {0}", Vidas.vidas);
    }
}
