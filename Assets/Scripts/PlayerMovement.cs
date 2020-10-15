using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    enum movementDirection { idle = 0,up,down,left,right};
    movementDirection currentDirection;
    Vector2 moveAmount;
    Animator animator;
    public float speedMultiplier;
    public bool isIdle()
    {
        return currentDirection == movementDirection.idle;
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        moveAmount = new Vector2();
    }


    void Update()
    {
        moveAmount.x = 0;
        moveAmount.y = 0;
        if (SaveManager.inputEnabled)
        {
            InputMovementDirection();
            SetAnimation(currentDirection);
            Move();
        }
        else
        {
            SetAnimation(movementDirection.idle);
        }
    }
    void Move()
    {
        transform.Translate(moveAmount*Time.deltaTime*speedMultiplier);
    }

    void InputMovementDirection()
    {
        currentDirection = movementDirection.idle;


        if (Input.GetKey(KeyCode.D))
        {
            currentDirection = movementDirection.right;
            moveAmount.x += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            currentDirection = movementDirection.left;
            moveAmount.x -= 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            currentDirection = movementDirection.up;
            moveAmount.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            currentDirection = movementDirection.down;
            moveAmount.y -= 1;
        }


    }

    void SetAnimation(movementDirection dir)
    {
        if(dir == movementDirection.idle)
        {
            if (animator.GetInteger("AnimState") == 0)
                return;
            animator.SetInteger("IdleDir", animator.GetInteger("AnimState") );
            animator.SetInteger("AnimState", 0); 
        }

        if (dir == movementDirection.up)
        {
            animator.SetInteger("AnimState", 1);
        }

        if (dir == movementDirection.down)
        {
            animator.SetInteger("AnimState", 2);
        }
        

        if (dir == movementDirection.right)
        {
            animator.SetInteger("AnimState", 3);
        }

        if (dir == movementDirection.left)
        {
            animator.SetInteger("AnimState", 4);
        }

    }
}
