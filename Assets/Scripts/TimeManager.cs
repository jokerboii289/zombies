
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowDown = 0.05f;
    public float slowDownLength = 2f;
    public static TimeManager instance;
    // Start is called before the first frame update

    private void Start()
    {
        instance = this;
    }   
    // Update is called once per frame
    void Update()
    {
        Time.timeScale += (1 / slowDownLength) * Time.unscaledDeltaTime;     //returing back to the normal time
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }
    public void DoSlowMo()
    {
        Time.timeScale = slowDown;                     // slow down time 
        Time.fixedDeltaTime = Time.timeScale * 0.02f; 
    }
}
