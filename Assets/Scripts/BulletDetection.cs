using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDetection : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("bullet"))
        {
            transform.parent.parent.gameObject.SetActive(false);
        }
    }
}
