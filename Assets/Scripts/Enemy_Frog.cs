using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{
    private Rigidbody2D rigidBody;
    public Transform leftPoint, rightPoint;
    public LayerMask ground;
    private bool faceLeft = true;
    public float speed, jumpForce;
    private float leftx, rightx;
    private new Collider2D collider;


    // override 与父级 virtual 相对应
    protected override void Start()
    {
        //frog
        base.Start();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        //delete the children of frog
        rigidBody.transform.DetachChildren();
        //get the x
        leftx = leftPoint.position.x;
        rightx = rightPoint.position.x;
        //delete the transform definite
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }


    void Update()
    {
        Animation();
        // Movement();
    }

    void Movement()
    {
        // 调取堆栈信息
        // string stackInfo = new System.Diagnostics.StackTrace().ToString();
        // Debug.Log(stackInfo);

        if (faceLeft)
        {
            if (collider.IsTouchingLayers(ground))
            {
                animator.SetBool("jumpUp", true);
                rigidBody.velocity = new Vector2(-speed, jumpForce);
            }
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                //animator.SetBool("idle", false);
                faceLeft = false;
            }
        }
        else
        {
            if (collider.IsTouchingLayers(ground))
            {
                animator.SetBool("jumpUp", true);
                rigidBody.velocity = new Vector2(speed, jumpForce);
            }
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
        }
    }


    void Animation()
    {
        if (animator.GetBool("jumpUp"))
        {
            if (rigidBody.velocity.y < 0.1)
            {
                animator.SetBool("jumpUp", false);
                animator.SetBool("jumpDown", true);
            }
        }
        if (collider.IsTouchingLayers(ground) && animator.GetBool("jumpDown"))
        {
            animator.SetBool("jumpDown", false);
        }
    }


}
