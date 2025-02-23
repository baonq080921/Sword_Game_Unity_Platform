using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Playyer_Combat : MonoBehaviour
{

    Player_Movement player_Movement;
    [SerializeField] private bool isAttacking = false;
    private float orginalSpeed ;
    private float speed ;
    private float attackDuration = .5f;
    private float attackCoolDown = 2f;
    private float nextAttackTime = 0f;
    Rigidbody2D rb;
    private Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();  // Get Rigidbody2D
        animator = GetComponent<Animator>(); // Get Animator
        player_Movement = GetComponent<Player_Movement>();
    }

    void Update(){

        if(Input.GetKeyDown(KeyCode.Mouse1) && !isAttacking && Time.time >= nextAttackTime){
            Attack01();
            nextAttackTime = Time.time + 1f/attackCoolDown;
        }
    }

    public void Attack01(){
        if(Input.GetKeyDown(KeyCode.Mouse1) && !isAttacking){
            animator.SetTrigger("isAttack01");
            isAttacking = true;
            orginalSpeed = player_Movement.speed;
            player_Movement.speed = 0;
            rb.linearVelocity = Vector3.zero;
            StartCoroutine(ResetAttack());
        }
    }

    private IEnumerator ResetAttack( )
    {
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        player_Movement.speed = orginalSpeed;
    }
}
