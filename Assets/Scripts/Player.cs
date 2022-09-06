using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //serializeField 可以让 private 元素显示出来 用来 debug 的
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
    public int gem;
    public Text cherryNumber;
    public Text gemNumber;
    //bool default value is false
    private bool isHurt;

    private LevelManager levelManager;
    void Start()
    {
        //让系统自动获取该变量
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        levelManager = LevelManager.GetInstance();
    }

    void FixedUpdate()
    {
        if (!isHurt)
        {
            Move();
        }
        PlayerAnimation();
    }
    private void Update()
    {
        Jump();
        Crouch();
        // 放到 update 可以有效解决计数延迟问题
        cherryNumber.text = levelManager.cherryNum.ToString();
        gemNumber.text = levelManager.gemNum.ToString();
    }

    void Move()
    {
        //获取键盘按键（-1 到 1 的过程值)
        float horizontalMove = Input.GetAxis("Horizontal");
        //获取 -1 0 1 三个确切值
        float direction = Input.GetAxisRaw("Horizontal");
        //player 移动
        if (horizontalMove != 0)
        {
            player.velocity = new Vector2(horizontalMove * speed * Time.fixedDeltaTime, player.velocity.y);
            animator.SetFloat("run", Mathf.Abs(horizontalMove));
        }

        //player 朝向
        if (direction != 0)
        {
            diriction.localScale = new Vector3(direction, 1, 1);
        }
        //蹲
        Crouch();
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && player.IsTouchingLayers(groundCheck))
        {
            // 在X轴方向给一个正向的速度
            player.velocity = new Vector2(player.velocity.x, jumpForce);
            // 播放跳跃音频
            jumpAudio.Play();
            // 播放跳跃动画
            animator.SetBool("jumpUp", true);
        }
    }
    void Crouch()
    {
        if (Input.GetButton("Crouch"))
        {
            animator.SetBool("crouch", true);
            //“该碰撞体是否被启用” 否
            disCollider.enabled = false;
        }
        else if (!Physics2D.OverlapCircle(cellingCheck.position, 0.2f, groundCheck))
        {
            animator.SetBool("crouch", false);
            //“该碰撞体是否被启用” 是
            disCollider.enabled = true;
        }
    }
    void PlayerAnimation()
    {
        //animator.SetBool("idle", false);
        if (player.velocity.y < 0.1f && !collider.IsTouchingLayers(groundCheck))
        {
            animator.SetBool("jumpUp", true);
        }
        //Change animation jumpDown to idle
        if (animator.GetBool("jumpUp"))
        {
            if (player.velocity.y < 0)
            {
                animator.SetBool("jumpDown", true);
                animator.SetBool("jumpUp", false);
            }
        }
        else if (isHurt)
        {
            animator.SetBool("hurt", true);
            animator.SetFloat("run", 0);
            if (Mathf.Abs(player.velocity.x) < 0.1f)
            {
                //animator.SetBool("idle", true);
                animator.SetBool("hurt", false);
                isHurt = false;
            }
        }
        else if (collider.IsTouchingLayers(groundCheck))
        {
            animator.SetBool("jumpDown", false);
            //animator.SetBool("idle", true);
        }
        //Change animation idle to crouch
        if (animator.GetBool("crouch"))
        {
            animator.SetBool("crouch", true);
            //animator.SetBool("idle", false);

        }
        else
        {
            //animator.SetBool("idle", true);
            animator.SetBool("crouch", false);
        }
        // Change the animation idle to hurt
    }

    // Collection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //touching the levelManager.cherryNum then collect
        if (collision.tag == "Cherry")
        {
            cherryAudio.Play();
            //Destroy(collision.gameObject);
            collision.GetComponent<Animator>().Play("collected");
            //cherryNumber.text = levelManager.cherryNum.ToString();

        }
        //touching the Gem then collect
        if (collision.tag == "Gem")
        {
            gemAudio.Play();
            collision.GetComponent<Animator>().Play("collected");
            //已经在 gem script 使用过了 就不需要了
            //Destroy(collision.gameObject);
            //gemNumber.text = Gem.ToString();
        }
        if (collision.tag == "deadLine")
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("reStart", 2f);
        }
    }

    public void cherryCount()
    {
        levelManager.cherryNum += 1;
    }
    public void gemCount()
    {
        levelManager.gemNum += 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // 有父类就不需要单独调用 Enemy_Frog frog = collision.gameObject.GetComponent<Enemy_Frog>();
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (animator.GetBool("jumpDown") && transform.position.y > (collision.gameObject.transform.position.y + 1))
            {
                enemy.jumpOn();
                player.velocity = new Vector2(player.velocity.x, jumpForce / 2);
                //if player is hurt then bounce off enemy 
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                player.velocity = new Vector2(-5, player.velocity.y);
                hurtAudio.Play();
                animator.SetBool("hurt", true);
                isHurt = true;
                // if player is hurt then bounce off enemy 
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                player.velocity = new Vector2(5, player.velocity.y);
                hurtAudio.Play();
                animator.SetBool("hurt", true);
                isHurt = true;
            }
        }
    }

    void reStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
