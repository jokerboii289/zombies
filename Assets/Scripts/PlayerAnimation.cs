using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] GameObject virtualCamera5;
    public static bool runOnPlatForm=false;
    [SerializeField] float time;// time to stall the player at top


    public static bool reachedTop = false;

    [SerializeField] Animator animator;


    private void Start()
    {
        reachedTop = false;
        runOnPlatForm = false;
    }
    // Update is called once per frame
    void Update()
    {

        if(!PlayerRun.stopRun)
        {
            animator.SetBool("run",true);
        }

        if (PlayerRun.stopRun)
        {
            animator.SetBool("run", false);
            animator.SetBool("ClimbUp", true);
        }

        if (PlayerControl.rightJump)
        {
            animator.SetBool("ClimbRight", true);
        }
        else
            animator.SetBool("ClimbRight", false);

        if (PlayerControl.leftJump)
        {
            animator.SetBool("ClimbLeft", true);
        }
        else
            animator.SetBool("ClimbLeft",false);


        if(PlayerRun.stop)
        {
            runOnPlatForm = false;
        }
        if (runOnPlatForm)
        {
            animator.SetBool("RunSecond", true);
        }
        if(!runOnPlatForm)
        {
            animator.SetBool("RunSecond", false);
        }

        if(PlayerRun.stop)
        {
            transform.localRotation = Quaternion.Euler(0,180,0);
            animator.SetBool("GunAim", true);          
        }


        if(PauseMenu.playerdead)
        {
            animator.SetBool("Death", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("reachedtop"))
        {
            animator.applyRootMotion = false;
            animator.SetBool("ClimbTop", true);
            reachedTop = true;
            StartCoroutine(Delay());
            PlayerPivot.instance.UnChild();
            virtualCamera5.SetActive(true); // active virtual camera5
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(time);
        runOnPlatForm = true;
        animator.applyRootMotion = false;
    }
  
}
