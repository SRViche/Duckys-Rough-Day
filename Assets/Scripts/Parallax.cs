using UnityEngine;
using UnityEngine.Rendering;

public class Parallax : MonoBehaviour
{
    private float length;          // La anchura de la imagen 
    private float starpos;         // Posición inicial del objeto
    
    [Header("Configuración de Movimiento")]
    [SerializeField] private float parallaxEffect; //Intensidad del efecto

    void Start()
    {
        //Se guarda posición inicial
        starpos = transform.position.x;

        //Se calcula el tamaño de la imagen
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    //LateUpdate para esperar primero el movimiento de cámara
    void LateUpdate()
    {
        //Calculo de desplazamiento del fondo
        float temp = (Camera.main.transform.position.x * (1 - parallaxEffect));
        
        //Se calcula cuando debe moverse el objeto
        float dist = (Camera.main.transform.position.x * parallaxEffect);

        transform.position = new Vector3(starpos + dist, transform.position.y, transform.position.z); //Se actualiza posicion

        if (temp > starpos + length) 
        {
            starpos += length;
        }
        //Si retrocedemos mas alla del inicio de la imagen, se mueve el punto hacia atras
        else if (temp < starpos - length)
        {
            starpos -= length;
        }
    }
}