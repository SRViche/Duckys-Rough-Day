using UnityEngine;


public class DronBehaviour : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private SpriteRenderer sr;
    private float direction=-1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Destroyer"))
        {
            GameObject.Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
    public void SetDirection(float newDirection)
    {
        direction=newDirection;
        if (direction < 0)
        {
            sr.flipX=false;
        }
        else
        {
            sr.flipX=true;
        }

        
    }
    private void FixedUpdate()
    {
        Vector3 movement=Vector3.right*direction*velocity*Time.deltaTime;
        this.transform.position+=movement;
    }
}
