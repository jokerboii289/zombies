using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPivot : MonoBehaviour
{
    //condition for camera zoom out
    [SerializeField] float yOffset;

    //camera system
    [SerializeField] GameObject virtualCamera1;
    [SerializeField] Transform virtualCamera2;
    [SerializeField] Transform virtualCamera3;
    [SerializeField] GameObject virtualCamera4;

    public static PlayerPivot instance;
    [SerializeField] Transform player;
    // Start is called before the first frame update

    void Start()
    {
        instance = this;
        virtualCamera1.SetActive(true); //first camera
        Invoke("TurnoffCam1", 0.5f);
    }

    void TurnoffCam1()
    {
        virtualCamera1.SetActive(false);
    }
    
    public void MakeChild()
    {
        player.transform.SetParent(transform);   // player set to pivot point 
    }

    public void UnChild()
    {
        player.transform.SetParent(null);
    }

    public void MakeVirtualCameraChild()
    {
        virtualCamera2.transform.transform.SetParent(transform);
        virtualCamera3.transform.transform.SetParent(transform);
        virtualCamera4.transform.SetParent(transform);
    }

    public void VirtualCamera4(GameObject obj)
    {
        var yDistance = player.position.y - obj.transform.position.y;
        var angle = Vector3.Dot(player.right, obj.transform.right);
        if(yDistance<=yOffset && angle>=0)
        {
            virtualCamera4.SetActive(true);
            StartCoroutine(Delay());
        }     
    }
    
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        virtualCamera4.SetActive(false);
    }

  
}
