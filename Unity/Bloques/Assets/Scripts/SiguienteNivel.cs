using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SiguienteNivel : MonoBehaviour {

    public string LevelToLoad;
    public float delay;

    [ContextMenu("Activar Carga")]
    public void ActiveLoad()
    {
        Invoke("LoadScene", delay);
    }

    void LoadScene ()
    {
        if (!IsLastLevel())
            Vidas.vidas++;

        SceneManager.LoadScene(LevelToLoad);
    }

    public bool IsLastLevel()
    {
        return LevelToLoad == "Portada";
    }
}
