using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    bool bossDead;
    [SerializeField] bool kickTwo;
    bool kick;
    bool playerDead;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject failPanel;

    int count;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        bossDead = false;
        playerDead = false;
        kick = true;
        count = 0;
        StartCoroutine(Attack());

        winPanel.SetActive(false);
        failPanel.SetActive(false);
    }

    private void Update()
    {
        if (!playerDead)
        {
            if (Input.touchCount > 0 && kick && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                count++;
            }
            /*
            if (count > 2)
            {
                if(!kickTwo)
                    playerAnimator.SetBool("kick", true);
                if(kickTwo)
                    playerAnimator.SetBool("kick2", true);
                bossDead = true;
            }*/
            if(count>1)
            {
                playerAnimator.SetBool("punch", true);
            }
            if(count>3)
            {
                if (!kickTwo)
                    playerAnimator.SetBool("kick", true);
                if (kickTwo)
                    playerAnimator.SetBool("kick2", true);
                bossDead = true;
            }
            if(bossDead)
            {
                StartCoroutine(Fall());
            }
        }
    }
  

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool("attack1",true);
        
        StartCoroutine(Attack2());
    }

    IEnumerator Attack2()
    {
        yield return new WaitForSeconds(4f);
        animator.SetBool("attack2",true);       
    }

    IEnumerator Fall()
    {
        animator.SetBool("fall", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("attack1", false);
        animator.SetBool("attack2", false);
    }

    public void Win()
    {
        StartCoroutine(DelayWin());
    }


    public void PlayerDead()
    {
        print("fail");
    }
    
    void Block()
    {
        playerAnimator.SetBool("block", true);
    }

    void FightingPose()
    {
        playerAnimator.SetBool("block", false);
    }

    void PlayerDeath()
    {
        playerDead = true;
        playerAnimator.SetBool("death",true);
        StartCoroutine(DelayFail());
    }


    IEnumerator DelayWin()
    {
        yield return new WaitForSeconds(1f);
        winPanel.SetActive(true);
        AudioManager.instance.Play("win");
    }

    IEnumerator DelayFail()
    {
        AudioManager.instance.Play("playerDies");
        yield return new WaitForSeconds(2f);
        failPanel.SetActive(true);
        AudioManager.instance.Play("fail");
    }

    void AudioFall()
    {
        if (kick)
        {
            AudioManager.instance.Play("kick");

            StartCoroutine(DelaySlowMo());
        }
    }

    void PlayerKick()
    {
        kick = true;
    }
    void PlayerCant()
    {
        kick = false;
    }

    IEnumerator DelaySlowMo()
    {
        yield return new WaitForSeconds(.5f);
        TimeManager.instance.DoSlowMo();
    }
}
