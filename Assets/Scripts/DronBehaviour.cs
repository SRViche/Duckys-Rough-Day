using UnityEngine;


public class DronBehaviour : MonoBehaviour
{
    [SerializeField] private float velocity; //Componentes y parametros del movimiento del dron
    [SerializeField] private SpriteRenderer sr;
    private float direction=-1; //Una dirección seteable, se modifica en el spawner dependiendo de si viene de izquierda o derecha
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Destroyer"))
        {
            GameObject.Destroy(this.gameObject); //Detección de colisión para destruir el objeto, en la escena hay destroyer a los costados lejanos
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            GameControl.Instance.sFXManager.DronSound(); //Se dispara el sonido y se pierden vidas en la colisión con el jugador.
            GameControl.Instance.SpendLives();
            GameObject.Destroy(this.gameObject); //Se destruye el objeto
        }
    }
    public void SetDirection(float newDirection)
    {
        direction=newDirection;
        if (direction < 0)
        {
            sr.flipX=false; //Se hace set a la dirección desde el Spawner, esto depende de donde venga el objeto
        }
        else
        {
            sr.flipX=true;
        }

        
    }
    private void FixedUpdate()
    {
        Vector3 movement=Vector3.right*direction*velocity*Time.deltaTime; //Se actualiza posición 
        this.transform.position+=movement; 
    }
}
