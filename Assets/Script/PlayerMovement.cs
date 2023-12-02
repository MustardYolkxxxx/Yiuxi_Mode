using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float moveX;
    private Transform trans;
    public float playerSpeed;
    public float jumpForce;

    public PlayerGroundCheck groundCheck;
    private Rigidbody2D rb;

    public Animator ani;
    public enum PlayerState
    {
        idle,
        walk,
        jumpstart,
        falling,
        jumpend,
    }

    public PlayerState currentState;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {

            Movement();
        
        
    }
    // Update is called once per frame
    void Update()
    {
        Jump();
        ChangeState();
        AniCheck();
    }

    public void AniCheck()
    {
        ani.SetBool("Falling", currentState == PlayerState.falling);
        ani.SetBool("Idle", currentState == PlayerState.idle);
        ani.SetBool("Walk", currentState == PlayerState.walk);
    }
    public void ChangeState()
    {
        if(groundCheck.isOnGround == false)
        {
            currentState = PlayerState.falling;
        }

        if(groundCheck.isOnGround == true)
        {
            if (moveX == 0 )
            {
                currentState = PlayerState.idle;
            }
            else if (moveX != 0 )
            {
                currentState = PlayerState.walk;
            }
        }
        

        switch(currentState)
        {
            case PlayerState.idle:
                
                break;
            case PlayerState.walk:
                
                break;
            case PlayerState.falling:
                
                break;
        }
    }
    void Movement()
    {
        //if (groundCheck.isOnGround == true)
        //{
            moveX = Input.GetAxis("Horizontal");
        //}
        //else
        //{
        //    moveX = Input.GetAxis("Horizontal")/2;
        //}
        Vector3 moveDir = new Vector3(moveX, 0, 0);

            trans.Translate(moveDir * playerSpeed * Time.deltaTime);
        
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            ani.SetTrigger("Jump");
            StartCoroutine(JumpDelay());
        }
    }

    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(0.1f);
        
        rb.AddForce(trans.up * jumpForce, ForceMode2D.Impulse);
    }
}
