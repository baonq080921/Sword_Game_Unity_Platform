using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Playyer_Combat : MonoBehaviour
{

#region Game Variable 
    Player_Movement player_Movement;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private float attackRange = .5f;
    [SerializeField] private int attackDamage = 10;
    private float orginalSpeed ;
    private float speed ;
    private float attackDuration = .5f;
    private float attackCoolDown = 2f;
    private float nextAttackTime = 0f;
    private Animator animator;
    private Rigidbody2D rb;

    
    public  Transform attackPoint;
    public LayerMask enemyLayers;
    
    #endregion
    


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
            isHitEnemies();
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

    private void isHitEnemies(){
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);
        foreach(Collider2D enemy in hitEnemies){
            Debug.Log("We hit the enemy\t"+ enemy.name);
            enemy.GetComponent<Enemies>().takeDamage(attackDamage);
        }

    }


    // Draw the  circle debug the attack point;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
       Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
}
