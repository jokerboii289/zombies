using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    //[SerializeField] Transform gunPivot;
    [SerializeField] GameObject aimHolder;
    int count = 0;
    public static bool playerWon;

    public static PlayerRun instance; // instance
    //win panel
    [SerializeField] GameObject winPanel;

    //timer for enemy to remove if remain behind
    public static bool eliminateEnemy = false;
    float timer=0;
    [SerializeField]float timeLimit;

    //aim cursor
    [SerializeField] GameObject aimCursor;

    //appear gun;
    [SerializeField]GameObject gun;

    //run towards gunpoint
    [SerializeField] Transform gunPoint;
    [SerializeField] float radialCheck;
    [SerializeField] float platformSpeed;
    public static bool stop=false;       //state to show player in top platform;
    public static bool makeEnemySlow=false;

    public static bool stopObstacleSpawn = false;
    //rotate player in direction he is climbing
    [SerializeField] Transform playerPivot;
    Vector3 forward;


    //at top of platform rotate player back to original pos;
    [SerializeField] Transform refForRotation;
    [SerializeField] float rotationSpeed;


    public static bool stopRun;
    [SerializeField] float speed;
    Rigidbody rBody;

    // Start is called before the first frame update
    void Start()
    {
        playerWon = false;
        count = 0;
        AudioManager.instance.Play("zombies");

        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 0;

        winPanel.SetActive(false);
        eliminateEnemy = false;
        stopRun = false;
        stopObstacleSpawn = false;
        makeEnemySlow = false;
        stop = false;
        aimCursor.SetActive(false);
        gun.SetActive(false);
        rBody = GetComponent<Rigidbody>();

        instance = this;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(!stopRun)
        rBody.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    private void Update()
    {
        
        if (PlayerAnimation.reachedTop)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation,refForRotation.rotation ,rotationSpeed*Time.deltaTime);
            transform.forward = forward;  // makin player face towards climb direction at end of pole
            var directionVector = new Vector3(gunPoint.transform.position.x, transform.position.y, gunPoint.transform.position.z) - transform.position;
            var magnitude = directionVector.magnitude;
           
            if (magnitude > radialCheck && PlayerAnimation.runOnPlatForm)
            {
                RunTowardsGunPoint();
            }
            else if (magnitude <= radialCheck)
            {
                gun.SetActive(true);
                transform.forward = PlayerShooting.direction; //rotate player in aim direction
                aimCursor.SetActive(true);
              //  transform.SetParent(gunPivot); // setting child to gunPivot
                stop = true;

               // gun.transform.SetParent(gunPivot);
            }

            //count timer of enemy elimination;
            timer += 1 * Time.deltaTime;
            if(timer>=timeLimit)
            {
                eliminateEnemy = true;
            }
        }

        if(transform.parent==playerPivot)
        {
            forward = transform.forward;
        }

        StartCoroutine(WinCondition());
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("pointtoclimb"))
        {
            Invoke("ZombiesPlay", 1.5f);
            var forward = transform.forward;
            stopRun = true;
            transform.localRotation = Quaternion.EulerAngles(new Vector3(0, 0, 0));
            PlayerPivot.instance.MakeChild();
            PlayerPivot.instance.MakeVirtualCameraChild(); 
            transform.forward = forward;
        }

        //for obstacle Spawn to stop
        if(other.gameObject.CompareTag("stopobstacles"))
        {
            stopObstacleSpawn = true;
            makeEnemySlow = true;         //temporary slow of enemy near the top of platform
        }

        if(other.gameObject.CompareTag("obstacle"))
        {
            rBody.isKinematic = true;
            gameObject.GetComponent<Animator>().enabled = false;   //playerDead
          
            PauseMenu.instance.PlayerDead();
        }

        if(other.gameObject.CompareTag("enemy")  && !PlayerAnimation.reachedTop && PlayerControl.leftJump)
        {
            EnableComponents();   //death call
        }
        if (other.gameObject.CompareTag("enemy") && !PlayerAnimation.reachedTop && PlayerControl.rightJump)
        {
            EnableComponents();
        }

        if(other.gameObject.CompareTag("pathobstacles"))
        {
            EnableComponents();
        }
       
        if(other.gameObject.CompareTag("jumppoint"))
        {
            gameObject.GetComponent<Animator>().SetBool("Jump",true);
            StartCoroutine(Delay());
        }
        
    }

    void RunTowardsGunPoint()
    {
        var direction = new Vector3(gunPoint.transform.position.x, transform.position.y, gunPoint.transform.position.z) - transform.position;
        direction.Normalize();
        transform.position += direction * platformSpeed * Time.deltaTime;
    }

 

    IEnumerator WinCondition()
    {
        yield return new WaitForSeconds(1f);
        if (!GameObject.FindGameObjectWithTag("enemy"))   //enemy check
        {
            count++;
            if (count == 1)
            {
                playerWon = true;
                aimHolder.SetActive(false);                            
            }
        }
    }

    void ZombiesPlay()
    {
        AudioManager.instance.Pause("zombies");
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.7f);
        gameObject.GetComponent<Animator>().SetBool("Jump", false);
    }

    void EnableComponents()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;     //player death
        gameObject.GetComponent<Animator>().enabled = false;

        PauseMenu.instance.PlayerDead();
    }
}
