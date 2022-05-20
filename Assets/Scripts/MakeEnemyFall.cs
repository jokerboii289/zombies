using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeEnemyFall : MonoBehaviour
{

    //climb platform
    [SerializeField] bool stallZombie;
    float climbSpeed;
    float temporarySpeed;
    float originalSpeed;

    public static MakeEnemyFall instance;

    bool enemyCrossPoint;

    [SerializeField] GameObject playerBody;
    //for distance between enemy and player
    [SerializeField] GameObject player;
    float distance;
    public static bool zombieAttack=false; // zombie attack
    float timer = 0;


    //walking on top of platform
    float secondRunSpeed;
    [SerializeField] Transform playerPos;
    [SerializeField] float radialCheck;
    [SerializeField] float time;
    bool secondRun = false;

    //[SerializeField] float attackAnimationTime;

  //  [SerializeField] GameObject virtualcamera3;
    
    public static bool reachedTop=false;
    bool top = true;

    Rigidbody rBody;
    Animator animator;
   
    // Start is called before the first frame update
    void OnEnable()
    {
        top = false;
        enemyCrossPoint = false;
        secondRun = false;
        reachedTop = false;
        zombieAttack = false;
        instance = this;
        rBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();       
    }

    private void Start()
    {
        temporarySpeed = Enemy.instance.tempClimbSpeed;
        climbSpeed = Enemy.instance.climbSpeed;
        secondRunSpeed = Enemy.instance.secondRunSpeed;
        originalSpeed=climbSpeed;
    }

    private void Update()
    {
        //change speed
        if(!enemyCrossPoint)
            ChangeSpeed();

        if(WayPointSecond.start && !top)
        {
            Climb();
        }

        var dot = Vector3.Dot(player.transform.right, transform.right);

        //checkin distance between enemy and player for enemy attack
        distance = (player.transform.position- transform.position).magnitude;
        if(distance<9 && dot>0.85f)
        {
            var checkLeftRight=(player.transform.position - transform.position); // x value is positive means player is left to enemy and vise versa
            if(checkLeftRight.x<0)
            {
               // animator.SetBool("ClimbAttackL", true);
                zombieAttack = true;
            }
            else if(checkLeftRight.x > 0)
            {
              //  animator.SetBool("ClimbAttackR", true);
                zombieAttack = true;
            }
             
            if(zombieAttack && !reachedTop)
            {
                timer += 1 * Time.deltaTime;
                if(timer>=.8f)
                {
                    PlayerDeath();  //poison effect death
                    PEffect();
                }               
            }
        }
        else if(distance>10)
        {
            animator.SetBool("ClimbAttackL",false);
            animator.SetBool("ClimbAttackR",false);
            zombieAttack = false;
            timer = 0;
        }

        if(secondRun)
        {
           Delay();  //second run of zombies
        }

        if(PlayerRun.eliminateEnemy && !enemyCrossPoint)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("obstacle"))
        {
            effect();
            CheckDistance(); // sound effect
            animator.SetBool("Fall", true);//falling animation

            rBody.isKinematic = false;
            animator.enabled = false;
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            transform.SetParent(null);                
        } 

        if(other.gameObject.CompareTag("reachedtop"))
        {
            top = true;
            reachedTop = true;
            animator.applyRootMotion = true;
            transform.SetParent(null);
            animator.SetBool("ClimbPlatform", true);
         
            //optional enabling navmesh on reaching top 
           // gameObject.GetComponent<NavMeshAgent>().enabled = true;
            StartCoroutine(DelayRun());          
        }

        if(other.gameObject.CompareTag("enemycrosspoint"))
        {
            enemyCrossPoint = true;
        }

        if(other.gameObject.CompareTag("pathobstacles"))
        {
            gameObject.SetActive(false);
            effect();
        }
    }

    IEnumerator DelayRun()
    {
        yield return new WaitForSeconds(time);
        animator.SetBool("SecondRun", true);
        secondRun = true;
    }

    void  Delay()   // top platform
    {
        var direction = playerPos.position - new Vector3(transform.position.x, playerPos.position.y, transform.position.z);
        var distance = direction.magnitude;
        direction.Normalize();
        if (distance >= radialCheck)
        {
            transform.position += direction * secondRunSpeed * Time.deltaTime; // platform top
        }

        if(distance<radialCheck)    //zombie attack on top platform
        {
            animator.SetBool("Attack",true);
            StartCoroutine(DelayDeath());
        }
    }
    void effect()   //blood effect
    {
        var obj = EffectPool.instance.GetFromPool();
        if(obj!=null)
        {
            obj.transform.position = transform.position;
            obj.GetComponent<ParticleSystem>().Play();
        }
        //disable enemy
        gameObject.SetActive(false);
    }

    IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(1f);
        PauseMenu.instance.PlayerDead();
    }

    void PlayerDeath()
    {
        playerBody.transform.SetParent(null);
        rBody.useGravity = false;
        transform.SetParent(null);
        
        playerBody.GetComponent<Rigidbody>().isKinematic = true;
        playerBody.GetComponent<Rigidbody>().useGravity = true;     //player death
        playerBody.GetComponent<Animator>().enabled = false;
        
        PauseMenu.instance.DelayDeathTwo();
    }

    void PEffect()
    {
        var obj= PoisonEffect.instance.GetFromPool();
        if(obj!=null)
        {
            obj.transform.position = transform.position;
        }
        //disable enemy
        gameObject.SetActive(false);
    }

    void Climb()
    {
        if (stallZombie)
        {
            if (!PlayerControl.leftJump && !PlayerControl.rightJump && !PlayerAnimation.reachedTop)
                transform.position += transform.up * Time.deltaTime * climbSpeed;
            else if (PlayerAnimation.reachedTop)
            {
                transform.position += transform.up * Time.deltaTime * climbSpeed;
            }

            if (PlayerRun.makeEnemySlow)
            {
                climbSpeed = temporarySpeed;
            }
        }
        else
        {
            if ( !PlayerAnimation.reachedTop)
                transform.position += transform.up * Time.deltaTime * climbSpeed;
            else if (PlayerAnimation.reachedTop)
            {
                transform.position += transform.up * Time.deltaTime * climbSpeed;
            }

            if (PlayerRun.makeEnemySlow)
            {
                climbSpeed = temporarySpeed;
            }
        }
    }
     
    void ChangeSpeed()
    {
        var distanceXZ = (new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(player.transform.position.x, 0, player.transform.position.z)).magnitude;
        var distance =( player.transform.position.y - transform.position.y);
        var dot = Vector3.Dot(transform.right, player.transform.right);
        if(distance>=10 && dot>0 && distanceXZ<15 && distance<=70)
        {
            climbSpeed = Random.Range(originalSpeed + 3, originalSpeed + 5);
        }
        else if (distance >= 70)
        {
            climbSpeed = originalSpeed+10;
        }
        else if(distance<10)
        {
            climbSpeed = originalSpeed;
        } 
    }

    void CheckDistance()
    {
        if (GameObject.FindGameObjectWithTag("Player").activeInHierarchy && !PauseMenu.playerdead)
        {
            var playerr = playerPos.transform.position;
            float dist = Mathf.Abs(transform.position.y -playerr.y);
            if (dist <= Enemy.instance.obstacleHitEnemyDistance)
            {
                AudioManager.instance.Play("blood");
            }
        }
    }
}
