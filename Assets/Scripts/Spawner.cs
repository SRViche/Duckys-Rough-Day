using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject[] Obstaculos;

    [Header("Spawn settings")]
    [SerializeField] private float direction;
    [SerializeField] private float maxHeight;
    [SerializeField] private float minHeight;
    [SerializeField] private float timeToSpawnMin;
    [SerializeField] private float timeToSpawnMax;

    void Start()
    {
        StartCoroutine(SpawnerTime());
        
    }

    IEnumerator SpawnerTime()
    {
        yield return new WaitForSeconds(Random.Range(timeToSpawnMin, timeToSpawnMax));
        int obj=Random.Range(0, Obstaculos.Length);
        GameObject nuevoObstaculo=Instantiate(Obstaculos[obj], new Vector3(transform.position.x, transform.position.y+Random.Range(minHeight, maxHeight),0), Quaternion.identity);
        nuevoObstaculo.SendMessage("SetDirection",direction, SendMessageOptions.DontRequireReceiver);
        StartCoroutine(SpawnerTime());

    }
}
