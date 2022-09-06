using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //public float attackRadius = 5;
    public float navRadius = 30;
    [SerializeField] float attackCooldown = 3f;

    NavMeshAgent agent;
    Animator animator;
    bool canAttack = true;
    CharacterStats stats;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        stats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Run", agent.velocity.magnitude);
        float distance = Vector3.Distance(transform.position, LevelManager.instance.playerTransform.position);
        //Debug.Log("Enemy attack:" + LevelManager.instance);
        if (distance < navRadius && !LevelManager.instance.player.GetComponent<PlayerControllerCS>().isDied)
        {
            agent.SetDestination(LevelManager.instance.playerTransform.position);
            if (distance <= agent.stoppingDistance)
            {
                if (canAttack)
                {
                    StartCoroutine(cooldown());
                    animator.SetTrigger("Attack");
                }
            }
        }
    }

    IEnumerator cooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("I touched player!!");
            stats.ChangeHp(-other.GetComponentInParent<CharacterStats>().atk);
        }
    }

    public void DamagePlayer()
    {
        LevelManager.instance.player.GetComponent<CharacterStats>().ChangeHp(-stats.atk);
    }


}
