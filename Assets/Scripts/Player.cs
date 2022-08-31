using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    //serializeField ������ private Ԫ����ʾ���� ���� debug ��
    /*[SerializeField]*/
    private Rigidbody2D player;
    private Animator animator;
    public Transform diriction;
    public new Collider2D collider;
    public Collider2D disCollider;
    public Transform cellingCheck;
    public LayerMask groundCheck;
    public AudioSource jumpAudio, hurtAudio, cherryAudio, gemAudio;
    public float speed;
    public float jumpForce;
    public int Cherry;
    public int Gem;
    public Text cherryNumber;
    public Text gemNumber;
    //bool default value is false
    private bool isHurt;

    void Start() {
        //��ϵͳ�Զ���ȡ�ñ���
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        if (!isHurt) {
            Move();
        }
        PlayerAnimation();
    }
    private void Update() {
        Jump();
        Crouch();
        // �ŵ� update ������Ч��������ӳ�����
        cherryNumber.text = Cherry.ToString();
        gemNumber.text = Gem.ToString();
    }

    void Move() {
        //��ȡ���̰�����-1 �� 1 �Ĺ���ֵ)
        float horizontalMove = Input.GetAxis("Horizontal");
        //��ȡ -1 0 1 ����ȷ��ֵ
        float Direction = Input.GetAxisRaw("Horizontal");
        //player �ƶ�
        if (horizontalMove != 0) {
            player.velocity = new Vector2(horizontalMove * speed * Time.fixedDeltaTime, player.velocity.y);
            animator.SetFloat("run", Mathf.Abs(horizontalMove));
        }

        //player ����
        if (Direction != 0) {
            diriction.localScale = new Vector3(Direction, 1, 1);
        }
        //��
        Crouch();
    }
    void Jump() {
        if (Input.GetButtonDown("Jump") && player.IsTouchingLayers(groundCheck)) {
            // ��X�᷽���һ��������ٶ�
            player.velocity = new Vector2(player.velocity.x, jumpForce);
            // ������Ծ��Ƶ
            jumpAudio.Play();
            // ������Ծ����
            animator.SetBool("jumpUp", true);
        }
    }
    void Crouch() {
        if (Input.GetButton("Crouch")) {
            animator.SetBool("crouch", true);
            //������ײ���Ƿ����á� ��
            disCollider.enabled = false;
        } else if (!Physics2D.OverlapCircle(cellingCheck.position, 0.2f, groundCheck)) {
            animator.SetBool("crouch", false);
            //������ײ���Ƿ����á� ��
            disCollider.enabled = true;
        }
    }
    void PlayerAnimation() {
        //animator.SetBool("idle", false);
        if (player.velocity.y < 0.1f && !collider.IsTouchingLayers(groundCheck)) {
            animator.SetBool("jumpUp", true);
        }
        //Change animation jumpDown to idle
        if (animator.GetBool("jumpUp")) {
            if (player.velocity.y < 0) {
                animator.SetBool("jumpDown", true);
                animator.SetBool("jumpUp", false);
            }
        } else if (isHurt) {
            animator.SetBool("hurt", true);
            animator.SetFloat("run", 0);
            if (Mathf.Abs(player.velocity.x) < 0.1f) {
                //animator.SetBool("idle", true);
                animator.SetBool("hurt", false);
                isHurt = false;
            }
        } else if (collider.IsTouchingLayers(groundCheck)) {
            animator.SetBool("jumpDown", false);
            //animator.SetBool("idle", true);
        }
        //Change animation idle to crouch
        if (animator.GetBool("crouch")) {
            animator.SetBool("crouch", true);
            //animator.SetBool("idle", false);

        } else {
            //animator.SetBool("idle", true);
            animator.SetBool("crouch", false);
        }
        // Change the animation idle to hurt
    }

    // Collection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //touching the Cherry then collect
        if (collision.tag == "Cherry") {
            cherryAudio.Play();
            //Destroy(collision.gameObject);
            collision.GetComponent<Animator>().Play("collected");
            //cherryNumber.text = Cherry.ToString();

        }
        //touching the Gem then collect
        if (collision.tag == "Gem") {
            gemAudio.Play();
            collision.GetComponent<Animator>().Play("collected");
            //�Ѿ��� gem script ʹ�ù��� �Ͳ���Ҫ��
            //Destroy(collision.gameObject);
            //gemNumber.text = Gem.ToString();
        }
        if (collision.tag == "deadLine") {
            GetComponent<AudioSource>().enabled = false;
            Invoke("reStart", 2f);
        }
    }

    public void cherryCount() {
        Cherry += 1;
    }
    public void gemCount() {
        Gem += 1;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            // �и���Ͳ���Ҫ�������� Enemy_Frog frog = collision.gameObject.GetComponent<Enemy_Frog>();
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (animator.GetBool("jumpDown") && transform.position.y > (collision.gameObject.transform.position.y + 1)) {
                enemy.jumpOn();
                player.velocity = new Vector2(player.velocity.x, jumpForce / 2);
                //if player is hurt then bounce off enemy 
            } else if (transform.position.x < collision.gameObject.transform.position.x) {
                player.velocity = new Vector2(-5, player.velocity.y);
                hurtAudio.Play();
                animator.SetBool("hurt", true);
                isHurt = true;
                // if player is hurt then bounce off enemy 
            } else if (transform.position.x > collision.gameObject.transform.position.x) {
                player.velocity = new Vector2(5, player.velocity.y);
                hurtAudio.Play();
                animator.SetBool("hurt", true);
                isHurt = true;
            }
        }
    }

    void reStart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
