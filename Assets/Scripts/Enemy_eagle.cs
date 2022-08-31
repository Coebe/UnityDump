using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_eagle : Enemy
{
    private Rigidbody2D rigidBody;
    private new Collider2D collider;
    public Transform highPoint, lowPoint;
    private bool up = true;
    public float speed;
    private float highY, lowY;

    protected override void Start()
    //与父级 virtual 相对应
    {
        //获得父级 start
        base.Start();
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        //delete the children of eagle
        rigidBody.transform.DetachChildren();
        //get the y
        highY = highPoint.position.y;
        lowY = lowPoint.position.y;
        //delete the transform definite
        Destroy(highPoint.gameObject);
        Destroy(lowPoint.gameObject);
    }

    void Update() {
        Movement();
    }

    void Movement() {
        if (up) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, speed);
            if (transform.position.y > highY) {
                up = false;
            }
        } else {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -speed);
            if (transform.position.y < lowY) {
                up = true;
            }
        }
    }
}
