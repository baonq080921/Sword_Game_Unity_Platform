using System;
using EthanTheHero;
using NUnit.Framework;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    #region Game Variables Region
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    private bool isFacingRight = true;
    public Rigidbody2D rb;
    [SerializeField] bool isGrounded;
    private bool isDoubleJump;
    
    #endregion
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start ting isDoubleJump: "+ isDoubleJump);
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
        if(isGrounded){
            rb.linearVelocity = new Vector3(direction.x * speed , rb.linearVelocity.y,0);
        }else{
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
    if(isGrounded){
        isDoubleJump = false;
    }
    if(jump){
        if(isGrounded || !isDoubleJump){
            rb.linearVelocity = new Vector3(rb.linearVelocity.x , jumpSpeed, 0);
            isDoubleJump = !isDoubleJump;
        }
    }
   }

    // =================1st way to detect collison with the ground using Tag Compare ======================= //
    #region OnCollisionEnter2D Region
   private void OnCollisionEnter2D(Collision2D other) {
    
    if(other.gameObject.CompareTag("Ground")){
        isGrounded = true;
    }
   }

   private void OnCollisionExit2D(Collision2D other) {
    if(other.gameObject.CompareTag("Ground")){
        isGrounded = false;
    }
   }
    #endregion
    


}
