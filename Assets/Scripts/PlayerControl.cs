using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    float countAngle; //for swipe
  
    //virtual camera zoomout during climb
    [SerializeField] GameObject virtualCamera3;
    

    //rotation speed
    [SerializeField]
    float rotationSpeed;
 
   // float count=0;
    [SerializeField] float rotationTime;

    //swipeControl
    private Vector2 startpos;
    private Vector2 currentPos;
  
    private bool stopTouch = false;

    public float swipeRange;
    public static bool leftJump=false;
    public static bool rightJump=false;

    //hold mechanics
    public static bool hold;
    [SerializeField] float moveSpeed;
   

    [SerializeField] float playerSpeed;
    Rigidbody rBody;
    // Start is called before the first frame update
    private void Start()
    {
        
        countAngle = 0;
        rightJump = false;
        leftJump = false;
        stopTouch = false;
        virtualCamera3.SetActive(false);
        rBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!rightJump && !leftJump && !PlayerAnimation.reachedTop)
            rBody.transform.position += transform.up * Time.fixedDeltaTime * playerSpeed;
    }

    private void Update()
    {
        if (rightJump)
        {
            var temp = Quaternion.Euler(new Vector3(0, countAngle, 0));
            transform.rotation = Quaternion.Slerp(transform.rotation, temp, rotationSpeed * Time.deltaTime);
        }
        if (leftJump)
        {
            var temp = Quaternion.Euler(new Vector3(0, countAngle, 0));
            transform.rotation = Quaternion.Slerp(transform.rotation, temp, rotationSpeed * Time.deltaTime);
        }
        Swipe();      
    }
   
    private void Swipe()
    {
        if (PlayerRun.stopRun)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !stopTouch)
            {               
                countAngle = transform.eulerAngles.y;

                startpos = Input.GetTouch(0).position;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && !stopTouch)
            {
                currentPos = Input.GetTouch(0).position;
                Vector2 dist = currentPos - startpos;

                if (!stopTouch)
                {
                    if (dist.x < -swipeRange)
                    {
                        countAngle += 60;
                        //function for leftmovements
                        virtualCamera3.SetActive(true);      //virtual camera3 enabling
                        leftJump = true;
                        stopTouch = true;
                        AudioManager.instance.Play("swipe");
                    }
                    if (dist.x > swipeRange)
                    {
                        countAngle -= 60;
                        //function for rightmovement
                        virtualCamera3.SetActive(true);      //virtual camera3 enabling
                        rightJump = true;
                        stopTouch = true;
                        AudioManager.instance.Play("swipe");
                    } 
                }
            }
          

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {   
                hold = false;
                StartCoroutine(Delay());
            }

            IEnumerator Delay()
            {
                yield return new WaitForSeconds(rotationTime);
                rightJump = false;
                leftJump = false;
                stopTouch = false;
                virtualCamera3.SetActive(false); //virtual camera disable
            }
        }
    }
       
    
}
