using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOver : MonoBehaviour
{
    [SerializeField] private SFXManager sFXManager;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private GameObject duckySprite;  //Componentes del UI 
    [SerializeField] Image imageBackground; 
    [SerializeField] Sprite[] imagesForBackground; //Array de imágenes, una para estado ganar y otra para estado perder
    void Start()
    {
        if (PlayerPrefs.GetInt("Lives") > 0)
        {
            SetWinAnimation();
            resultText.text="Ducky logró seguir su camino"; //Si gana, se dispará animación de victoria, se muestra un texto y se pone fondo de imagen de victoria
            imageBackground.sprite= imagesForBackground[0];
            sFXManager.WinSong(); //Reproducción de música de victoria
        }
        else
        {
            SetLoseAnimation();
            resultText.text="Ducky se perdió en la ciudad"; //Si pierde, se dispará animación de derrota, se muestra un texto y se pone fondo de imagen de derrota
            imageBackground.sprite= imagesForBackground[1];
            sFXManager.LoseSong(); //Reproducción de musica de derrota
        }
    }
    private void SetWinAnimation()
    {
        duckySprite.GetComponent<Animator>().SetTrigger("isWinning"); //Se produce transición a animación de victoria
    }
    private void SetLoseAnimation()
    {
        duckySprite.GetComponent<Animator>().SetTrigger("isDead"); //Se produce transición a animación de derrota
    }
    public void StartToPlay()
    {
        SceneManager.LoadScene("GameScene"); //Función de boton para volver a jugar
    }
    public void ExitGame()
    {
        //Función de boton para salir del juego
        Application.Quit();
    }
}
