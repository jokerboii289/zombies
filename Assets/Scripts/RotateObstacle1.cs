using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObstacle1 : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool rotateLeft;
    // Update is called once per frame
    void Update()
    {
        if(rotateLeft)
            transform.RotateAround(transform.position ,transform.up, speed * Time.deltaTime);
        else
            transform.RotateAround(transform.position, transform.up, -speed * Time.deltaTime);
    }
}
