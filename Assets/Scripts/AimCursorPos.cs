using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCursorPos : MonoBehaviour
{
    public static AimCursorPos instance;
    private void Start()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        Touch touch;
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            transform.position = touch.position;
        }
    }
}
