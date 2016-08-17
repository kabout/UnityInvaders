using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BotonSalir : MonoBehaviour
{
    public bool exit;

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (exit)
                Application.Quit();
            else
                SceneManager.LoadScene("Portada");
        }
	}
}
