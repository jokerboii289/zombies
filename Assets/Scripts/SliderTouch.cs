using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTouch : MonoBehaviour
{
    
    [SerializeField] Slider slider;
    [SerializeField] float maxvalue;
    [SerializeField] float midvalue;
    [SerializeField] float minValue;
    [SerializeField] float time;
    bool moveRight;
    [SerializeField]float speed;
    // Start is called before the first frame update
    private void Start()
    {
        slider.maxValue = maxvalue;
        slider.value = midvalue;
        slider.minValue = minValue;
        moveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(slider.value<=maxvalue && moveRight)
        {
            slider.value += speed*Time.deltaTime;
        }
        if (slider.value >= minValue && !moveRight)
        {
            slider.value -= speed * Time.deltaTime;
        }

        if(slider.value==maxvalue)
        {
            moveRight = false;
        }
        if (slider.value == minValue)
        {
            moveRight = true;
        }

        StartCoroutine(DelayDestroy());
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
