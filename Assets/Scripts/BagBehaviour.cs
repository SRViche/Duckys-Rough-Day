using UnityEngine;

public class BagBehaviour : MonoBehaviour
{

    [SerializeField] private float velocity;
    [SerializeField] private SpriteRenderer sr; //Componentes y parametros del movimiento de la bolsa
    [SerializeField] private float  spiralSpeed; //Velocidad radial de la trayectoria
    [SerializeField] private float spiralRadius; //Radio de la trayectoria

    private float timer; //Timer para calcular posicion siguiente
    private Vector3 startPosition; //Se guarda posición inicial como un pivote sobre el cual hará espirales
    private float direction=-1; //Una dirección seteable, se modifica en el spawner dependiendo de si viene de izquierda o derecha
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Destroyer"))
        {
            GameObject.Destroy(this.gameObject); //Detección de colisión para destruir el objeto, en la escena hay destroyer a los costados lejanos
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            GameControl.Instance.sFXManager.BagSound(); //Se dispara el sonido y se pierden vidas en la colisión con el jugador.
            GameControl.Instance.SpendLives();
            GameObject.Destroy(this.gameObject); //Se destruye el objeto
        }
    }
    public void SetDirection(float newDirection)
    {
        direction=newDirection;
        if (direction < 0)
        {
            sr.flipX=false;  //Se hace set a la dirección desde el Spawner, esto depende de donde venga el objeto
        }
        else
        {
            sr.flipX=true;
        }

        
    }
    void Start()
    {
        startPosition=transform.position; //Se almacena la posición de inicio
    }
    private void FixedUpdate()
    {
        timer+=Time.deltaTime*spiralSpeed; //Calculo para después calcular posición siguiente
        Vector3 movement=Vector3.right*direction*velocity*Time.deltaTime;
        startPosition+=movement;

        float xOffset=Mathf.Cos(timer) * spiralRadius; //Funciones seno y coseno para hacer el movimiento en espiral.
        float yOffset=Mathf.Sin(timer) * spiralRadius;
        this.transform.position=startPosition+new Vector3(xOffset, yOffset, 0); //Movimiento del objeto
    }
}
