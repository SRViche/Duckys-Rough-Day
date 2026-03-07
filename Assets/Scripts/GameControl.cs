
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  
public class GameControl : MonoBehaviour
{
    public int timeToWin=20;
    static public GameControl Instance; //Instancia pública del gamecontrol
    [Header("Components")]
    [SerializeField] private UIController uiController;
    [SerializeField] private Animator animatorDucky; //Componentes del juego usados dentro del código
    [SerializeField] private PlayerController duckyScript;
    [SerializeField] public SFXManager sFXManager;

    private void Awake()
    {
        StopAllCoroutines();
        PlayerPrefs.SetInt("Lives",3);
        PlayerPrefs.SetInt("TimeToWin",PlayerPrefs.GetInt("TimeToWin",timeToWin));
        Instance=this;   //Se setean los playerprefs de vidas y tiempo
        Instance.SetReferences(); //Se inician las referencias de componentes
        sFXManager.GameSong(); //Se reproduce musica del juego
        DontDestroyOnLoad(this.gameObject);


    }
    private void SetReferences()
    {
        if (uiController == null)
        {
            uiController=FindFirstObjectByType<UIController>();
        }
        if (sFXManager==null)
        {
            sFXManager=FindFirstObjectByType<SFXManager>();
            
        }
        timeToWin=PlayerPrefs.GetInt("TimeToWin"); //Se obtiene tiempo de juego (20s)
        init(); //Inicio del timer
    }
    private void init()
    {
        if (uiController != null)
        {
            uiController.startTimer(); //Se manda a iniciar el timer al componente UIController
        }
    }
    public int GetCurrentLives()
    {
        return PlayerPrefs.GetInt("Lives",3); //Obtención de número de vidas
    }
    public void SpendLives()
    {
        if (GetCurrentLives() > 0)
        {
            int newLives=GetCurrentLives()-1;
            PlayerPrefs.SetInt("Lives",newLives); //De tener vidas aún, se restan, se guardan en PlayerPrefs y se actualizan en UI
            uiController.UpdateLives();
        }
        else
        {
            ActiveEndScene(); //Sin vidas, se acaba el juego y entra la EndScene
        }
        
    }
    public void CheckGameOver()
    {
        if (GetCurrentLives() == 0) //Se busca si ya perdío el jugador
        {
            ActiveEndScene();
        }
    }
    public void ActiveEndScene()
    {
        
        if (GetCurrentLives() != 0)
        {
            SceneManager.LoadScene("EndScene"); //Si aún tiene vidas y gano, se manda directo a la EndScene
        }
        else
        {
            animatorDucky.SetBool("isDead",true); //Si perdió, se produce animación de muerte y se llama a corrutina para
            StartCoroutine(WaitDeadAnimation());  //dar tiempo de que se complete la animación
        }
        
    }

    IEnumerator WaitDeadAnimation()
    {
        duckyScript.setSpeed(0f); //Imposibilita el movimiento estando muerto
        
        yield return new WaitForSeconds(2); //Se esperan dos segundos y pasa a la EndScene
        SceneManager.LoadScene("EndScene");
        
    }
    public void Home()
    {
        SceneManager.LoadScene("MenuScene"); //Con el boton Home regresa al menú principal
    }
}
