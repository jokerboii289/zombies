using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject gun;
    int count;
    [SerializeField] GameObject obstacleIndicator;
   
    [SerializeField] GameObject aimHolder;
    public static bool playerdead;
    [SerializeField] GameObject failPanel;
    [SerializeField] GameObject winPanel;
    public static PauseMenu instance;
    public static bool playerWon;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        playerWon = false;
        count = 0;
     
        playerdead = false;
        failPanel.SetActive(false);
        winPanel.SetActive(false);
        instance = this;
    }

    private void Update()
    {
        if(!GameObject.FindGameObjectWithTag("BigBoss") && !GameObject.FindGameObjectWithTag("enemy"))
        {
            count++;
            if (count == 1)
            {
                StartCoroutine(DelayWin()); //win panel
            }
        }
    }

    public void PlayerDead()
    {
        count++;
        if (count == 1)
        {
            playerdead = true;
            obstacleIndicator.SetActive(false);
            gun.SetActive(false);
            AudioManager.instance.Play("playerDies");
            StartCoroutine(Delay());          
        }
    }
    
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //for restart
    }

    public void DelayDeathTwo()
    {
        count++;
        if (count == 1)
        {
            obstacleIndicator.SetActive(false);
            playerdead = true;
            AudioManager.instance.Play("playerDies");
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        AudioManager.instance.Play("fail");
        aimHolder.SetActive(false);
        failPanel.SetActive(true);
        Invoke("Timescale", 1.5f);
    }

    void Timescale()
    {
        Time.timeScale = 0;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // for next level

        PlayerPrefs.SetInt("Index", (SceneManager.GetActiveScene().buildIndex) + 1);//save level index no

    }

    IEnumerator DelayWin()
    {
        playerWon = true;
        aimHolder.SetActive(false);
        yield return new WaitForSeconds(1f);
        winPanel.SetActive(true); // win panel
        AudioManager.instance.Play("win");
        //Invoke("Timescale", 1.5f);
    }
}
