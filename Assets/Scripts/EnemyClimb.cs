using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClimb : MonoBehaviour     //attached to pivot
{
    [SerializeField] float speed;
    [SerializeField] float tempSpeed;

    // Update is called once per frame
    void Update()
    {
        if(!PlayerControl.leftJump && !PlayerControl.rightJump && !PlayerAnimation.reachedTop)
          transform.position += transform.up * Time.deltaTime* speed; 
        else if(PlayerAnimation.reachedTop)
        {
            transform.position += transform.up * Time.deltaTime * speed;
        }


        if(PlayerRun.makeEnemySlow)
        {
            speed = tempSpeed;
        }
    }
   

}
