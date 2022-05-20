using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShowCase : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
   
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position,transform.up,rotateSpeed*Time.deltaTime);
        if(PlayerRun.stop)
        {
            gameObject.SetActive(false);
        }
    }
}
