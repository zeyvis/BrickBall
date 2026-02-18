using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,IKillable
{
    private SpeedBoostController _speedBoostController;
    private void Start()
    {
        _speedBoostController = GetComponent<SpeedBoostController>();
    }
    public void Kill()
    {
        if (!_speedBoostController.playerBeastMode)
            Debug.Log("Player died");
        else
            Debug.Log("player not died because player in the beast mode");
    }

  
}
