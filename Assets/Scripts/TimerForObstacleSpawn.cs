using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerForObstacleSpawn : MonoBehaviour
{
    //for direction check
    GameObject temp;

    //indicator
    [SerializeField] float timeOfIndicationAfterObstacleSpawn;
    [SerializeField]float indicationTime;
    [SerializeField] GameObject indicator;

   // [SerializeField] Vector3 offset;
    [SerializeField] Vector3 offset;//yoffset
    [SerializeField] GameObject player;
    [SerializeField] float timeInterval;
    float time;

    float delay;

    private void Start()
    {
        delay = 0;
        indicator.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerRun.stopRun)
        {
            delay += 1 * Time.deltaTime;
            if (delay > 3)
            {
                if (PlayerRun.stopRun && !PlayerControl.leftJump && !PlayerControl.rightJump && !PlayerRun.stopObstacleSpawn)
                {
                    time += 1 * Time.deltaTime;
                    if (time >= timeInterval)
                    {
                        SpawnObstacle();
                        time = 0;
                    }
                }
            }
        }
    }

    void SpawnObstacle()
    {
        var obj = ObjectPooling.instance.GetFromPool();
        if(obj!=null)
        {
            temp = obj;
            obj.transform.forward = player.transform.forward;
            obj.transform.position = player.transform.position +offset;
            obj.transform.forward = player.transform.forward;
            StartCoroutine(Indicator());
        }
    }

    IEnumerator Indicator()
    {
        yield return new WaitForSeconds(timeOfIndicationAfterObstacleSpawn);
        //dot product to check direction direction of obstacle falling
        var dot = Vector3.Dot(temp.transform.right, player.transform.right);
        if (dot >=0.8)
        {
            indicator.SetActive(true);
            StartCoroutine(IndicatorDeactivateTime());
        }
    }

    IEnumerator IndicatorDeactivateTime()
    {
        yield return new WaitForSeconds(indicationTime);
        indicator.SetActive(false);
    }
}
