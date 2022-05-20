using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    [SerializeField] bool groundFireOn;

    [SerializeField] float cursorDistance; // distance from player

    [SerializeField] Transform player;
    [SerializeField] Transform bulletPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] private Camera platformCamera; // camera during shooting
    public static Vector3 direction;


    [SerializeField] Transform gunPoint;

    //timer to spawnBullet
    float timer=0;
    [SerializeField] float timerInterval;

   void OnEnable()
    {
        timer = 0;
        transform.position = gunPoint.position;
        transform.forward = player.forward;
    }

    // Update is called once per frame
    void Update()
    {
        Touch touch;

        if(Input.touchCount>0 && !PlayerRun.playerWon && !groundFireOn && !PauseMenu.playerdead)
        {
            touch = Input.GetTouch(0);
            Ray ray = platformCamera.ScreenPointToRay(touch.position);
            if(Physics.Raycast(ray,out RaycastHit raycastHit))
            {
                // shooting direction
                var hitPoint= raycastHit.point;
                //hitPoint.y = bulletPoint.position.y;
                hitPoint.y = transform.position.y;
                var difference= hitPoint - transform.position;
                if(difference.magnitude>cursorDistance)
                {
                    direction = difference.normalized;
                    transform.forward = direction;
                }
            }

            if (timer <= Time.time)
            {
                timer = timerInterval + Time.time;
                SpawnBullet();
            }
        }
        // for ground fire
        if(groundFireOn)
        GroundFire();
    }
    
    void SpawnBullet()     
    {
        var obj = bullet;
        if(obj!=null)
        {
            AudioManager.instance.Play("gun");
            Instantiate(bullet, bulletPoint.position,transform.rotation);
            var obj1 = MuzzleFlash.instance.GetFromPool();
            if(obj1!=null)
            {
                obj1.transform.position = bulletPoint.position;
                obj1.transform.forward = -bulletPoint.right;
            }
        }
    }

    void GroundFire()
    {
        Touch touch;
        if (Input.touchCount > 0 && !PauseMenu.playerWon && BigBoss.shoot)
        {
            touch = Input.GetTouch(0);
            Ray ray = platformCamera.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                // shooting direction
                var hitPoint = raycastHit.point;
                hitPoint.y = transform.position.y;
                var difference = hitPoint - transform.position;
                if (difference.magnitude > cursorDistance+5f)
                {
                    direction = difference.normalized;
                    transform.forward = direction;
                }
            }

            if (timer <= Time.time)
            {
                timer = timerInterval + Time.time;
                SpawnBullet();
            }
        }
    }
}
