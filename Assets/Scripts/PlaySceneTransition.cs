using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneTransition : MonoBehaviour
{
    
    [SerializeField] GameObject transition;
    [SerializeField] bool scenetransition;
    private void OnEnable()
    {
        transition.SetActive(false);
    }

    private void Update()
    {
        if (scenetransition && PlayerAnimation.reachedTop)
        {
            transition.SetActive(true);
            StartCoroutine(Transition());
        }
    }

    IEnumerator Transition()
    {
        yield return new WaitForSeconds(1f);
        //SceneManager.LoadScene("BossLvl4"); // transition on same scene'
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
