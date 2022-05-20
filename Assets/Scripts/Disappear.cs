using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float dist=60;
    // Start is called before the first frame update
 
    // Update is called once per frame
    void Update()
    {
        var distance = player.position.y- transform.position.y;
        if( distance>dist)
        {
            gameObject.SetActive(false);
        }
    }
}
