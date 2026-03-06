using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("PlayerComponents")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sr;

    [Header("Movement settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;


    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;

    private float horizontal;

    private void FixedUpdate()
    {
        anim.SetBool("isJumping",!IsGrounded());
        if (horizontal != 0)
        {
            anim.SetBool("isWalking",true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        if (horizontal < 0)
        {
            sr.flipX=true;
        }
        else
        {
            sr.flipX=false;
        }
        
        rb.linearVelocity=new Vector2(horizontal*speed, rb.linearVelocity.y);
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontal=context.ReadValue<Vector2>().x;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            anim.SetBool("isJumping",true);
            rb.linearVelocity=new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    public void Run(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            anim.SetBool("isRunning",true);
            speed=6f;
        }
        if (context.canceled)
        {
            anim.SetBool("isRunning",false);
            speed=4f;
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f,0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
   
}
