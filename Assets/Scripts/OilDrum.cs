using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OilDrum : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    [SerializeField] float explosionRadius;
    [SerializeField] float exlposionForce;
    [SerializeField] int noOfHitsToExlpode;
    // Start is called before the first frame update
  
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("bullet"))
        {
            noOfHitsToExlpode--;//barrel hp

            Collider[] colliders= Physics.OverlapSphere(transform.position, explosionRadius);
            foreach(Collider nearbyObjects in colliders)
            {
                if (noOfHitsToExlpode <= 0)
                {
                    Rigidbody rBody = nearbyObjects.GetComponent<Rigidbody>();
                    if (rBody != null)
                    {
                         var navMesh=nearbyObjects.gameObject.GetComponent<NavMeshAgent>();
                        if(navMesh!=null)
                        {
                            navMesh.enabled = false;
                        }
                        rBody.isKinematic = false;
                        rBody.AddExplosionForce(exlposionForce, transform.position, explosionRadius);
                    }
                    
                    if (nearbyObjects.CompareTag("enemy") || nearbyObjects.CompareTag("BigBoss"))
                    {                    
                        nearbyObjects.gameObject.SetActive(false);   //disable objects near explosion radius;
                        var obj = BloodEffect.instance.GetFromPool();
                        if (obj != null)
                        {                          
                            obj.transform.position = nearbyObjects.gameObject.transform.position;  // particle effect
                        }
                    }

                    Instantiate(explosionEffect, transform.position, Quaternion.identity);
                    AudioManager.instance.Play("BarrelBlast");     //barrel explo sound
                    gameObject.SetActive(false);
                }
            }
            other.gameObject.SetActive(false);  //disable bullet
        }
    }
}
