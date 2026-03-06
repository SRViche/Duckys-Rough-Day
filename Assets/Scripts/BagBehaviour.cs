using UnityEngine;

public class BagBehaviour : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float  spiralSpeed;
    [SerializeField] private float spiralRadius;

    private float timer;
    private Vector3 startPosition;
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
    void Start()
    {
        startPosition=transform.position;
    }
    private void FixedUpdate()
    {
        timer+=Time.deltaTime*spiralSpeed;
        Vector3 movement=Vector3.right*direction*velocity*Time.deltaTime;
        startPosition+=movement;

        float xOffset=Mathf.Cos(timer) * spiralRadius;
        float yOffset=Mathf.Sin(timer) * spiralRadius;
        this.transform.position=startPosition+new Vector3(xOffset, yOffset, 0);
    }
}
