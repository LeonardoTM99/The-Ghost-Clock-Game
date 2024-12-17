using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontal, vertical;
    public float speed = 6f;
    public Rigidbody2D rb;
    Vector2 movement;
    private bool isRunning;

    //Animations
    public Animator animator;
    string currentState;
    const string PLAYER_IDLE = "Player Idle";
    const string PLAYER_WALK_BACK = "Back Walk";
    const string PLAYER_WALK_FRONT = "Front Walk";
    const string PLAYER_WALK_LEFT = "Left Walk";
    const string PLAYER_WALK_RIGHT = "Right Walk";

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Horizontal", movement.x);
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Vertical", movement.y);

        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = !isRunning;
            if (isRunning)
            {
                speed = 10f;
            }
            else
            {
                speed = 6f;
            }
        }

    }

    private void FixedUpdate()
    {
        if(movement.x != 0 || movement.y != 0)
        {
            
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

            if (movement.x > 0)
            {
                ChangeAnimationState(PLAYER_WALK_RIGHT);
            }
            else if (movement.x < 0)
            {
                ChangeAnimationState(PLAYER_WALK_LEFT);
            }
            else if(movement.y > 0)
            {
                ChangeAnimationState(PLAYER_WALK_BACK);
            }
            else if(movement.y < 0)
            {
                ChangeAnimationState(PLAYER_WALK_FRONT);
            }

            animator.SetFloat("lastMoveX", movement.x);
            animator.SetFloat("lastMoveY", movement.y);

        }
        else
        {
            ChangeAnimationState(PLAYER_IDLE);
        }

    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        //Update current state
        currentState = newState;
    }

}
