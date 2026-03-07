using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip bag;
    [SerializeField] private AudioClip dron;
    [SerializeField] private AudioClip menuSound;
    [SerializeField] private AudioClip winSong;
    [SerializeField] private AudioClip loseSong; //Audioclips usados dentro del juego
    [SerializeField] private AudioClip gameSong;
    public void BagSound()
    {
        AudioSource.PlayClipAtPoint(bag, Camera.main.transform.position, 0.5f); //Función para reproducir sonido de bolsa
    }
    public void DronSound()
    {
        AudioSource.PlayClipAtPoint(dron, Camera.main.transform.position, 0.5f); //Función para reproducir sonido de dron
    }
    public void MenuSound()
    {
        AudioSource.PlayClipAtPoint(menuSound, Camera.main.transform.position, 0.5f); //Función para reproducir canción de menu
    }
    public void WinSong()
    {
        AudioSource.PlayClipAtPoint(winSong, Camera.main.transform.position, 0.5f); //Función para reproducir canción de victoria
    }
    public void LoseSong()
    {
        AudioSource.PlayClipAtPoint(loseSong, Camera.main.transform.position, 0.5f); //Función para reproducir canción de derrota
    }
    public void GameSong()
    {
        AudioSource.PlayClipAtPoint(gameSong, Camera.main.transform.position, 0.5f); //Función para reproducir canción del juego
    }

}
