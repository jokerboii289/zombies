using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDisappear : MonoBehaviour
{
   // [SerializeField] float distance;
    [SerializeField] float rotateSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] int countLimit;

    int count = 0;
    Rigidbody rBody;
    private void Start()
    {
        count = 0;
        rBody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rBody.velocity = Vector3.down * moveSpeed;
    }

    private void Update()
    {
        transform.RotateAround(transform.right, -rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("ground"))
        {
            rBody.velocity = Vector3.zero;
            ObjectPooling.instance.AddToPool(gameObject);
        }
        if(other.gameObject.CompareTag("enemy"))
        {
           // CheckDistance();
            PlayerPivot.instance.VirtualCamera4(gameObject);
            count++;
            if(count>=countLimit)
            {
                ObjectPooling.instance.AddToPool(gameObject);
                count = 0;
            }
        }
    }
}
