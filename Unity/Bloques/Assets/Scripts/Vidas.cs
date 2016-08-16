using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Vidas : MonoBehaviour {

    public static int vidas = 3;
    public Text textlifes;
    public Pelota pelota;
    public Barra barra;

	// Use this for initialization
	void Start ()
    {
        UpdateLifeText();
    }

    public void LostLife()
    {
        Vidas.vidas--;
        UpdateLifeText();

        pelota.Reset();
        barra.Reset();
    }

    private void UpdateLifeText()
    {
        textlifes.text = string.Format("Vidas: {0}", Vidas.vidas);
    }
}
