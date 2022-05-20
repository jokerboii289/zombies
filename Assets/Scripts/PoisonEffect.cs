using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : MonoBehaviour
{
    public static PoisonEffect instance;

    private List<GameObject> poolOfobject = new List<GameObject>();

    [SerializeField]
    private int limitOfPool;
    [SerializeField] GameObject Prefab;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Grow();
    }

    void Grow()
    {
        for (int i = 0; i < limitOfPool; i++)
        {
            var obj = Instantiate(Prefab);
            AddToPool(obj);
        }
    }

    public GameObject GetFromPool()
    {
        for (int i = 0; i < limitOfPool; i++)
        {
            if (!poolOfobject[i].activeInHierarchy)
            {
                poolOfobject[i].SetActive(true);
                return poolOfobject[i];
            }
        }

        return null;
    }

    public void AddToPool(GameObject obj)
    {
        obj.SetActive(false);
        poolOfobject.Add(obj);
    }
}
