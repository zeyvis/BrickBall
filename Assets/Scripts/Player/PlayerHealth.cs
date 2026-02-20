using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,IKillable
{
    public event Action OnPlayerDied;

    private SpeedBoostController _speedBoostController;    
    private bool _isDead;

    private void Start()
    {
        _speedBoostController = GetComponent<SpeedBoostController>();
        
    }
    

    public void Kill()
    {
        if (!_speedBoostController.playerBeastMode)
            Die();
        else
            Debug.Log("player not died because player in the beast mode");
    }
    public void Die()
    {
        if (_isDead) return;
        _isDead = true;

        OnPlayerDied?.Invoke();

    }
 
}
