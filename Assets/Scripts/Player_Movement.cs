using System;
using EthanTheHero;
using UnityEngine;
using Color = UnityEngine.Color;

public class Player_Movement : MonoBehaviour
{
    #region Game Variables Region
    [Header("Run")]
    [SerializeField] float speed = 10f;
    [Header("Jump")]

    [SerializeField] float jumpSpeed = 5f;

    private bool isFacingRight = true;
    private Rigidbody2D rb;
    [SerializeField] private bool isGrounded; // Using for Oncollsion2D
    [SerializeField] Vector3 boxSize;
    [SerializeField] float castDistance;

    [SerializeField] private Animator animator;
    public LayerMask groundLayer;
    private bool isDoubleJump;
    
    #endregion
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update(){
        // Debug.Log("Time in Update:"+Time.deltaTime);
       PlayerMovment();
       Jump();

    }
    private void FixedUpdate() {
    }

    private void PlayerMovment(){
        float horizontal = Input.GetAxis("Horizontal");
        
        Vector3 direction = new Vector3(horizontal,0,0).normalized;
        // Vector2 direction_1 = new Vector3(horizontal,rb.linearVelocity.y).normalized;

        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Freezee the Z-axis no rotation
        
        // ======Using linerVelocity for testing character Movement: Rigibody Movement
        if(isGround()){
            animator.SetBool("isJumping",false);    
            if(horizontal !=0){
                animator.SetBool("isRunning",true);
            }else{
                animator.SetBool("isRunning",false);
            }
            rb.linearVelocity = new Vector3(direction.x * speed , rb.linearVelocity.y,0);
        }else{
            animator.SetBool("isRunning",false);
            animator.SetBool("isJumping",true);
            rb.linearVelocity = new Vector3(direction.x * speed * .5f , rb.linearVelocity.y,0);
        }

        //====Addforce====
        // rb.AddForce(direction *speed * Time.deltaTime);

        //=== Move Positon:Using Vector 2
        //rb.MovePosition(rb.position+(direction_1 * speed * Time.deltaTime));

        if(direction.x > 0f && !isFacingRight){
            Flip();
        }
        if(direction.x < 0f && isFacingRight){
            Flip();
        }    
    }

   private void Flip(){
    Vector3 currentScale  = gameObject.transform.localScale;
    currentScale.x *= -1;
    transform.localScale = currentScale;
    isFacingRight = !isFacingRight;
    // Debug.Log(transform.localScale);
   }
   private void Jump(){
    bool jump = Input.GetKeyDown(KeyCode.Mouse0);
    if(isGround()){
        isDoubleJump = false;
    }
    if(jump){
        if( isGround() || !isDoubleJump){
            rb.AddForce(new Vector3(rb.linearVelocity.x , jumpSpeed , 0),ForceMode2D.Impulse);
            isDoubleJump = !isDoubleJump;
        }
    }
   }

    // =================1st way to detect collison with the ground using Tag Compare ======================= //
    #region OnCollisionEnter2D Region
//    private void OnCollisionEnter2D(Collision2D other) {
    
//     if(other.gameObject.CompareTag("Ground")){
//         isGrounded = true;
//     }
//    }

//    private void OnCollisionExit2D(Collision2D other) {
//     if(other.gameObject.CompareTag("Ground")){
//         isGrounded = false;
//     }
//    }
    #endregion
    
    // =================2nd way to detect collison with the ground using Raycast ======================= //
    #region Raycasting Collison

    private bool isGround(){
        Vector3 positiveBoxSize = new Vector3(MathF.Abs(boxSize.x),MathF.Abs(boxSize.y),0);
        if(Physics2D.BoxCast(gameObject.transform.position,positiveBoxSize,0f,-gameObject.transform.up,castDistance,groundLayer)){
            return true;
        }else{
            return false;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = isGround() ? Color.green : Color.cyan;
        Gizmos.DrawWireCube(gameObject.transform.position - gameObject.transform.up * castDistance, boxSize);
    }
    
    #endregion


}