using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerCS : MonoBehaviour
{

    public float moveSpeed = 5;
    public float sprintSpeed = 7;
    public float gravity = 10;
    public float jumpForce = 5;
    public bool isDied = false;

    float verticalVelocity = 0;
    float sprint = 0;

    WaitForSeconds hideColDelay = new WaitForSeconds(0.5f);
    CharacterController controller;
    Transform cam;
    Animator animator;
    CharacterStats stats;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
        animator = GetComponentInChildren<Animator>();
        stats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float animState = 0;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animState = 1;
        }
        else if (Mathf.Abs(horizontal) >= 0.01f || Mathf.Abs(vertical) >= 0.01f)
        {
            animState = 0.5f;
        }
        animator.SetFloat("Move", animState);
        animator.SetBool("Attack", Input.GetKey(KeyCode.Mouse0));
        animator.SetBool("Walk", Input.GetKey(KeyCode.LeftControl));

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);
        if (controller.isGrounded)
        {
            if (Input.GetAxis("Jump") > 0.1)
                verticalVelocity = jumpForce;
        }
        else
            verticalVelocity -= gravity * Time.deltaTime;

        if (moveDirection.magnitude > 0.1)
        {
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        moveDirection = cam.TransformDirection(moveDirection);
        sprint = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
        moveDirection = new Vector3(moveDirection.x * sprint, verticalVelocity, moveDirection.z * sprint);

        controller.Move(moveDirection * Time.deltaTime);
    }

    private void AnimationLogic(float horizontal, float vertical)
    {
        float animState = 0;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animState = 1;
        }
        else if (Mathf.Abs(horizontal) >= 0.01f || Mathf.Abs(vertical) >= 0.01f)
        {
            animState = 0.5f;
        }
        animator.SetFloat("Move", animState);
        animator.SetBool("Attack", Input.GetKey(KeyCode.Mouse0));
        animator.SetBool("Walk", Input.GetKey(KeyCode.LeftControl));

    }

    public void DoAttack()
    {
        transform.Find("AttackCollider").GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(HideCollider());
        //Debug.Log("Player DoAttack");
    }

    IEnumerator HideCollider()
    {
        yield return hideColDelay;
        transform.Find("AttackCollider").GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Healing"))
        {
            stats.ChangeHp(other.GetComponent<Collection>().healingValue);
            LevelManager.instance.PlaySoundAtLoc(LevelManager.instance.audioList[3], transform.position);
            // Instantiate(LevelManager.instance.particleList[0], other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Item"))
        {
            LevelManager.instance.item++;
            LevelManager.instance.PlaySoundAtLoc(LevelManager.instance.audioList[2], transform.position);
            // Instantiate(LevelManager.instance.particleList[0], other.transform.position, other.transform.rotation);
            //Debug.Log("Item: " + LevelManager.instance.item);
            Destroy(other.gameObject);
        }
    }
}
