using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private SFXManager sFXManager;
    void Awake()
    {
        sFXManager.MenuSound(); //Reproducción de canción de menu
    }
    public void ContinueControls()
    {
        SceneManager.LoadScene("ControlsScene"); //Función para pasar a la escena donde se muestran los controles
    }
    public void ExitGame()
    {
        //Función para salir del juego
        Application.Quit();
    }
}
