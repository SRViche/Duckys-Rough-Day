using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("PlayerComponents")] //Componentes del personaje para acceder
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private CircleCollider2D bc;

    [Header("Movement settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce; //Caracteristicas del movimiento

    private float normalRadius=0.282851f;
    private float crouchRadius=0.1674884f;  //Radio del collider en su estado normal y el deseado cuando este agachado
    private Vector2 normalOffset=new Vector2(0.02079797f,0.03781533f); //Offset son posiciones del collider con referencia al objeto que tiene el componente
        private Vector2 crouchOffset=new Vector2(0.02079797f,-0.18f); //Un offset para normal y uno para agachado.



    [SerializeField] LayerMask groundLayer; //Componentes para verificar que este tocando el suelo
    [SerializeField] Transform groundCheck;

    private float horizontal; //Leer input

    private void FixedUpdate()
    {
        anim.SetBool("isJumping",!IsGrounded()); //Se ajusta el salto de acuerdo a la verificación si toca el suelo
        if (horizontal != 0)
        {
            anim.SetBool("isWalking",true);
        }
        else    //Se dispara animación de caminar cuando detecte input (1/-1)
        {
            anim.SetBool("isWalking", false);
        }
        if (horizontal < 0)
        {
            sr.flipX=true;
        } //Se rota el sprite de acuerdo al input
        else
        {
            sr.flipX=false;
        }
        
        rb.linearVelocity=new Vector2(horizontal*speed, rb.linearVelocity.y); //Se aplica velocidad al objeto
    }
    //Se uso el Nuevo Input System de Unity, te permite mapear acciones y no estar leyendo teclas especificas
    //Mejora accesibilidad, lectura y portabilidad a distintos dispositivos
    public void Move(InputAction.CallbackContext context)
    {
        horizontal=context.ReadValue<Vector2>().x;  //Lectura del input move
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            anim.SetBool("isJumping",true); //Lectura de Jump y se produce la transición.
            rb.linearVelocity=new Vector2(rb.linearVelocity.x, jumpForce); //Se hace el salto
        }
    }
    public void Crouch(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            anim.SetBool("isCrouching",true); //Se produce transición a agachado
            speed=0f; //Se detiene el movimiento
            bc.radius=crouchRadius;  //Se ajustan radio y posición del collider para mostrar que Ducky se agacho
            bc.offset=crouchOffset;
        }
        if (context.canceled)
        {
            anim.SetBool("isCrouching",false); //Cuando se deja de presionar la tecla se vuelve a la normalidad en la animación y 
            bc.radius=normalRadius;             //caracteristicas del collider, además se ajusta velocidad
            bc.offset=normalOffset;
            speed=4f;
        }
    }
    public void Run(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            anim.SetBool("isRunning",true); //Se produce transición y se aumenta la velocidad para hacer el estado corriendo
            speed=6f;
        }
        if (context.canceled)
        {
            anim.SetBool("isRunning",false); //Cuando se deja de presionar la tecla se vuelve a la normalidad
            speed=4f;
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f,0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer); //Se usa un collider a los pies de Ducky
                                                                                                                                    //verificar que este tocando el suelo
    }
    public void setSpeed(float sp)
    {
        speed=sp; //Metodo para establecer la velocidad, se usa en el GameControl cuando se pierden todas las vidas
    }
}
