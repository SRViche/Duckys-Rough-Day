using UnityEngine;
using UnityEngine.SceneManagement;
public class ControlsController : MonoBehaviour
{
    [SerializeField] private SFXManager sFXManager; //Manejador de sonidos
    void Awake()
    {
        sFXManager.MenuSound(); //Se reproduce música de fondo al entrar al juego
    }
    public void StartToPlay()
    {
        SceneManager.LoadScene("GameScene"); //Al presionar un botón se carga la escena principal
    }
}