using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemy : MonoBehaviour
{
    
    Animator animator;
    public static bool playerDead;
    NavMeshAgent agent;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        playerDead = false;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.enabled)
        agent.SetDestination(player.transform.position);
        CheckDistance();
    }

    void CheckDistance()  //distance check in ground levels
    {
        var dist =Mathf.Abs( (player.transform.position - transform.position).magnitude);
        if(dist< 17)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
            agent.enabled = false;
            animator.SetBool("GroundAttack", true);
            playerDead = true;          
        }
    }

    
}
