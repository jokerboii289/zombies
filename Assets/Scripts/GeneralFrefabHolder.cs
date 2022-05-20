using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralFrefabHolder : MonoBehaviour
{
    //swipe
    [SerializeField] GameObject tapShow;
    // Start is called before the first frame update
    void Start()
    {
        tapShow.SetActive(true);
        StartCoroutine(Stop());
    }

   
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(3f);
        tapShow.SetActive(false);
    }
}
