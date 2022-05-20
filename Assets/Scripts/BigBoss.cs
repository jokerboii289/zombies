using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoss : MonoBehaviour
{
    [SerializeField] GameObject cursor;
    [SerializeField] GameObject Vcamera;
    [SerializeField] int noOfBulletToDie;
    [SerializeField] GameObject failPanel;
    [SerializeField] GameObject winPanel;
    Animator animator;
    GameObject player;
    [SerializeField] float speed;
    bool bossDeath;
    public static bool shoot;
    

 
    [SerializeField]float limitDistance;

    //boss health count
    int count;
   
    // Start is called before the first frame update
    void Start()
    {
        cursor.SetActive(false);
        bossDeath = false;
        shoot = false;
        
        count = 0;

        failPanel.SetActive(false);
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        winPanel.SetActive(false);

        //for boss check
        var obj = GameObject.FindGameObjectsWithTag("BigBoss");
        count = obj.Length;

        Invoke("DeactivateCamera", .5f);
        StartCoroutine(SetActive());
    }

    // Update is called once per frame
    void Update()
    {
        var direction = player.transform.position - transform.position;
        var distance = direction.magnitude;
        direction.Normalize();

        if(distance>limitDistance && !bossDeath)
        {
            transform.forward = direction;
            transform.position += direction * speed * Time.deltaTime;
        }
        else if(distance<=limitDistance)
        {
            animator.SetBool("Stomp",true);
        }
    }

    //fail panel
    public void PlayerDeath()
    {
        StartCoroutine(DelayFail());
    }

    IEnumerator DelayFail()
    {
        yield return new WaitForSeconds(3f);
        failPanel.SetActive(true);
    }
   
    private void OnTriggerEnter(Collider other)
    { 
        //edit the win condition
        if (other.gameObject.CompareTag("bullet"))
        {
            count++;
            if(count>noOfBulletToDie)   //boss
            {
                animator.SetBool("Death", true);
            }
            var obj = BloodEffect.instance.GetFromPool();
            if(obj!=null)
            {
                obj.transform.position = other.transform.position;
            }
            other.gameObject.SetActive(false);  //disable bullet
        }
    }
    
    void PlayerDyingAudio()  // called using animation
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("death",true);
        AudioManager.instance.Play("playerDies");
    }

    public void Count()
    {
        bossDeath = true;
        StartCoroutine(Disable());       
    }
    
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    void DeactivateCamera()
    {
        Vcamera.SetActive(false);
    }

    IEnumerator SetActive()
    {
        yield return new WaitForSeconds(6f);
        cursor.SetActive(true);
        shoot = true;
    }
}
