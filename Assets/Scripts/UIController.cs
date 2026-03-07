using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections;


public class UIController : MonoBehaviour
{
    [Header("Elements UI")]
    [SerializeField] private TextMeshProUGUI TimeText;
    [SerializeField] private Sprite SpendLives; //Componentes de UI
    [SerializeField] private Image[] livesImage; //Array de vidas
    int lives=3; //Total de vidas
    int time;

    void Start()
    {
        time=GameControl.Instance.timeToWin; //Se obtiene el tiempo inicial
        lives=PlayerPrefs.GetInt("Lives",lives); //Se obtienen vidas iniciales
    }
    public void ActiveText()
    {
        TimeText.text=" Tiempo: "+time; //Actualiza el texto del timer
    }
    public void startTimer()
    {
        StartCoroutine(MatchTime()); //Se inicializa el timer
    }
    public void UpdateLives()
    {
        lives=GameControl.Instance.GetCurrentLives();
        if(lives>=0 && lives < livesImage.Length) //Se actualiza el número de vidas, se colocan sprites para mostrar la perdida de vidas
        {
            livesImage[lives].sprite=SpendLives;
        }
        GameControl.Instance.CheckGameOver(); //Se verifica si hay GameOver
    }

    IEnumerator MatchTime()
    {
        yield return new WaitForSeconds(1);
        time-=1;
        ActiveText();
        if (time == 0)
        {
            GameControl.Instance.ActiveEndScene(); //Timer que dura el tiempo establecido, cuando se acaba el tiempo se pasa a EndScene
        }
        else
        {
            StartCoroutine(MatchTime());
        }
    }
}
