using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPointSecond : MonoBehaviour
{
    //animation
    Animator animator;

    //pass rotation
    public static GameObject pointDirection;

    //make child
 //   [SerializeField] Transform enemyPivot;

    public static bool start=false;
    WayPointSecond script;
    [SerializeField] GameObject[] wayPoints;
    int rand;
   
    public float minDistance;
   
    public bool go = true;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        go = true;
        start = false;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        script = GetComponent<WayPointSecond>();
        rand = Random.RandomRange(0, wayPoints.Length);
        pointDirection = wayPoints[rand];
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, wayPoints[rand].transform.position);       
        if (go)
        {
            if(dist<minDistance)
            { 
                start = true;
               // transform.SetParent(enemyPivot);  //setting parent
                animator.SetBool("ClimbTop", true);   //animation of enemy
                transform.forward = wayPoints[rand].transform.forward; //rotation of enemy
                script.enabled = false;
                agent.enabled = false;
            }
            if (dist > minDistance)
            {
                Move();
            }         
        }
    }

    private void Move()
    {     
        // waypoints with navemesh;
        agent.SetDestination(wayPoints[rand].transform.position);
    }
}
