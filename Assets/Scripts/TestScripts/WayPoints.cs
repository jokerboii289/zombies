using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WayPoints : MonoBehaviour
{
    [SerializeField] GameObject[] arrayOfPoints;
    [SerializeField] int divideNo;
    public static int firstPoint;
    int secondPoint;
   // public static int startPointToMove;


    /// <summary>
    /// /////
    /// </summary>
   // GameObject[] wayPoints;
    List<GameObject> wayPoints = new List<GameObject>();
    int num = 0;

    public float minDistance;
    public float speed;

    public bool moveRandomly = true;
    public bool go = true;

  

    // Start is called before the first frame update
    void Start()
    {
        var rand = RandomSet();
        print(rand);
        
        firstPoint = rand - 2;
        secondPoint = rand-1;
       
        wayPoints.Add(arrayOfPoints[firstPoint]);
        wayPoints.Add(arrayOfPoints[secondPoint]);
       
       
    }

    // Update is called once per frame
    void Update()
    {
       
       
        float dist = Vector3.Distance(gameObject.transform.position, wayPoints[num].transform.position);
       
        if (go)
        {
            if (dist > minDistance)
            {
                Move();
            }
            //move around randomly
            else if (dist < minDistance && moveRandomly)
            {
                num = Random.Range(0, wayPoints.Count);
            }
            else if (dist < minDistance && !moveRandomly) //move sequentially randomly
            {
                if (num + 1 == wayPoints.Count)
                {
                    num = 0;
                }
                else
                    num++;
            }

        }
    }

    private void Move()
    {   
        //gameObject.transform.LookAt(wayPoints[num].transform.position);
       // gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;    
    }

    int RandomSet()
    {
        var setLenght = arrayOfPoints.Length / divideNo;
        var rand = Random.Range(1, setLenght+1 );
        rand = rand * divideNo;
        return rand;
    }

}
