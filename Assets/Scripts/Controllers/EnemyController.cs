using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 30f;
    public float attackDistance = 5f;
    public float speed = 15f;
    public Transform target;
    private CharacterStats targetStats;
    NavMeshAgent nav;

    public bool showGizmos = true;

    
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        nav = GetComponent<NavMeshAgent>();
        targetStats = target.GetComponent<CharacterStats>();
        nav.speed = speed;
        
    }

    
    void Update()
    {
        if(GetComponent<CharacterStats>().isAlive)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            // Idle animation
            if(distance > lookRadius)
            {
                nav.enabled = false;
                gameObject.GetComponent<Animator>().SetTrigger("Idle");
            }

            // Run animation and go to player
            if(distance <= lookRadius & distance > attackDistance)
            {
                nav.enabled = true;
                gameObject.GetComponent<Animator>().SetTrigger("Run");
                nav.SetDestination(target.position);
                
            }

            // Attack animation and attack player
            if(distance <= attackDistance)
            {
                FaceTarget();
                gameObject.GetComponent<Animator>().SetTrigger("Attack");
                nav.enabled = false;
            }
        }
    }
    
    void OnDrawGizmosSelected() {
        // Drawing trigger zones
        if(showGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, lookRadius);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }
    }

    void FaceTarget ()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}
    
    public void Die()
    {
        if(!GetComponent<CharacterStats>().isAlive)
        {
            nav.Stop();
            gameObject.GetComponent<Animator>().SetTrigger("Dead");
            nav.enabled = false;
        }
    }
}
