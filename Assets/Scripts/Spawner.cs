using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject[] Obstaculos; //Array de objetos que pueden instanciarse

    [Header("Spawn settings")]
    [SerializeField] private float direction; //Dirección de trayectoria
    [SerializeField] private float maxHeight;
    [SerializeField] private float minHeight; //Parametros para spawnear objetos (Altura y tiempo)
    [SerializeField] private float timeToSpawnMin;
    [SerializeField] private float timeToSpawnMax;

    void Start()
    {
        StartCoroutine(SpawnerTime());
        
    }

    IEnumerator SpawnerTime()
    {
        yield return new WaitForSeconds(Random.Range(timeToSpawnMin, timeToSpawnMax)); //Tiempo random de espera para spawnear objeto
        int obj=Random.Range(0, Obstaculos.Length); //Se busca un indice random para generar objeto random.
        GameObject nuevoObstaculo=Instantiate(Obstaculos[obj], new Vector3(transform.position.x, transform.position.y+Random.Range(minHeight, maxHeight),0), Quaternion.identity);
        nuevoObstaculo.SendMessage("SetDirection",direction, SendMessageOptions.DontRequireReceiver); //Se manda la dirección a la que debe ir cada objeto
        StartCoroutine(SpawnerTime()); //Corrutina para spawnear cada cierta cantidad de tiempo

    }
}
