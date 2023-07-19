using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovePlayer : MonoBehaviour
{

    public float moveSpeed;
    public float climbSpeed;
    public float jumpForce;

    private bool isJumping;
    public bool isGrounding;
    //to hide a public value on the inspector : 
    [HideInInspector]
    public bool isClimbing;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;



    public Rigidbody2D rb;
    public CapsuleCollider2D capsuleCollider;
    public Animator animator;

    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;

    public float speedVelo;

    private float horizontalMovement;
    private float verticalMovement;
    private bool speaking;

    public static MovePlayer instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Two instance of MovePlayer are in the scene !");
            return;
        }

        instance = this;
        speaking = false;
    }

    private void Update()
    {

        //define the "force" to move horizontaly : 
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        //same for vertical : 
        verticalMovement = Input.GetAxis("Vertical") * climbSpeed * Time.fixedDeltaTime;

        /*NB : Input.GetAxis("Horizontal") récup quelle touche est préssée entre <- et ->
                et si on met "Vertical" regarde les touche UP et DOWN*/

        //if tap the space bar :
        /*NB : utilisé Input.GetButton("Jump") pour la barre espace plutôt que Input.GetButtonDown("Jump")*/
        if (Input.GetButton("Jump") && isGrounding && !isClimbing)
        {
            isJumping = true;
        }


        if (!speaking)
        {
            Flip(rb.velocity.x);
        }
        

        float characterVelocity = Mathf.Abs(rb.velocity.x);

        //condition to activate the animations : 
        animator.SetFloat("Speed", characterVelocity);
        animator.SetBool("isClimbing", isClimbing);
    }

    /*only for the physic function not for input ... input in update ...*/
    void FixedUpdate()
    {
        

        //check if the player is on ground : 
        //check if it count the 2nd floor (posY of player have to be upper than 4)
        if(this.transform.position.y < 3.8)
        {
            GameObject.FindGameObjectsWithTag("2ndFloor")[0].GetComponent<TilemapCollider2D>().enabled = false;
        }
        else
        {
            GameObject.FindGameObjectsWithTag("2ndFloor")[0].GetComponent<TilemapCollider2D>().enabled = true;
            //to avoid falling more : 
            isGrounding = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
            if (isGrounding)
            {
               if(!speaking)
                    rb.velocity = new Vector3(horizontalMovement, 0, 0.5f);
            }
                


        }

        isGrounding = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

        //moving the player :

        if (!speaking)
        {
            MovingPlayer(horizontalMovement, verticalMovement);
        }
           
        
        

        
    }

    void MovingPlayer(float _horizontalMovement, float _verticalMovment)
    {
        //horizontal movement on ground : 
        if (!isClimbing)
        {
            //take as target velocity both "force" to move horizontaly and verticaly ( horizontaly calcuate before and verticaly as the gravity force of the RigidBody2D)
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);

            // apply to the rigidbody the diretion of the force calculated :
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

            if (isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        }
        //when climbing ladder : 
        else
        {
            //vertical movement : 
            //velocity.x put to 0 to avoid the "inertial effect" when grabing the ladder...
            Vector3 targetVelocity = new Vector2(0, _verticalMovment);

            //
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        }

       
    }

    void Flip(float _velocity)
    {
        //switch the direction of the movement we flip the sprite
        //we have to put 0.1f and not 0 because the velocity is between -1 and 1 
        //but if we release the touche LEFT the touch will go from 1 to -0.1f

        speedVelo = _velocity;
        if(_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if(_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    //to stop the player from moving : 
    public void speechStart()
    {
        speaking = true;
    }


    //to check the state of the speak animation : 
    public void speechAnimationFinished()
    {
        speaking = false;
        animator.SetTrigger("speechFinished");
        
    }
}
