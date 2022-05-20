using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int count;
 
    //for fail panel
    [SerializeField] GameObject failPanel;

    private void Start()
    {
        count = 0;
        failPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        transform.forward = PlayerShooting.direction;

        if(GroundEnemy.playerDead)
        {
            count++;
            if (count == 1)
            {
                gameObject.GetComponent<Animator>().SetBool("death", true);        
                StartCoroutine(DelayFail());
            }
        }
    }

    IEnumerator DelayFail()
    {
        yield return new WaitForSeconds(3f);
        failPanel.SetActive(true);
        AudioManager.instance.Play("fail");
        StartCoroutine(Stop());
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }


    public void PlayDeathSound()
    {
        AudioManager.instance.Play("playerDies");
    }
}
