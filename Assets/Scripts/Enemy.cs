using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
   
    public float climbSpeed;
   
    public float tempClimbSpeed;
    
    public float secondRunSpeed; //platform

    public float obstacleHitEnemyDistance;

    public static Enemy instance;

    private void OnEnable()
    {
        instance = this;
    }
}

