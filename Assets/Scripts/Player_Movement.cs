using System;
using EthanTheHero;
using NUnit.Framework;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    [SerializeField] float speed = 200f;
    private bool isFacingRight = true;
    public Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update(){
        // Debug.Log("Time in Update:"+Time.deltaTime);
    }
    private void FixedUpdate() {
        PlayerMovment();
        Debug.Log("Time in FixedUpdate:"+Time.deltaTime);


    }

    private void PlayerMovment(){
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontal,rb.linearVelocity.y,0).normalized;
        Vector2 direction_1 = new Vector3(horizontal,rb.linearVelocity.y).normalized;

        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Freezee the Z-axis no rotation
        
        // ======Using linerVelocity for testing character Movement: Rigibody Movement
            rb.linearVelocity = new Vector3(direction.x * speed * Time.deltaTime, rb.linearVelocity.y,0);

        //====Addforce====
        //rb.AddForce(direction *speed * Time.deltaTime);

        //=== Move Positon:Using Vector 2
        //rb.MovePosition(rb.position+(direction_1 * speed * Time.deltaTime));

         Debug.Log("LinearVelocity Speed: " + rb.linearVelocity.magnitude); // magnitude gives the speed

       
        Debug.Log(direction);

        if(direction.x > 0 && !isFacingRight){
            Flip();
        }
        if(direction.x < 0 && isFacingRight){
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
}
