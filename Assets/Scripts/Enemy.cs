using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    protected Animator animator;
    protected AudioSource deathAudio;

    // virtual 意思是可以被子级重新编写
    protected virtual void Start() {
        animator = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }

    public void Destroy() {
        Destroy(gameObject);
    }

    public void jumpOn() {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        animator.SetTrigger("death");
        deathAudio.Play();
    }
}
