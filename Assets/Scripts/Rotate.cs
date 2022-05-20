using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float speed;
   

    // Update is called once per frame
    void Update()
    {
        // transform.LeanRotateAround(transform.up, speed, 4);
        // transform.LeanRotate(transform.up, 1); // change
        transform.RotateAround(transform.position, transform.up, speed * Time.deltaTime);
    }
}
