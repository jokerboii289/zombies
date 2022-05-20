using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //sound for zombie explosion
    [SerializeField]bool activatezombiExplosionSound;

    [SerializeField] Rigidbody rBody;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rBody.AddForce(transform.forward * speed,ForceMode.Impulse);
    }
    private void Update()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("bullethitbox"))
        {
            if(activatezombiExplosionSound)
            {
                AudioManager.instance.Play("blood");
            }
            ZombieEffect();  
            gameObject.SetActive(false);
        }
    }

    void ZombieEffect()
    {
        var zombieEffect= EffectPool.instance.GetFromPool();
        if(zombieEffect!=null)
        {
            zombieEffect.transform.position = transform.position;
            zombieEffect.GetComponent<ParticleSystem>().Play();
        }
    }
}
