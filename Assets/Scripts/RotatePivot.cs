using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePivot : MonoBehaviour
{ 
    // Update is called once per frame
    void Update()
    {
        transform.forward = PlayerShooting.direction;
    }
}
