using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBossFight : MonoBehaviour
{
    public void PunchSound()
    {
        AudioManager.instance.Play("punches");
    }
}
